using CSCore;
using CSCore.Codecs;
using CSCore.SoundOut;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace DigimonWorld.Frontend.WPF.Services;

public static class Jukebox
{
    private static readonly string LeomonSongTitle = "19) Leomon";
    
    private static bool _hasPlayedMusic;
    private static readonly WasapiOut SoundOut;
    private static readonly CompositeDisposable CompositeDisposable;

    private static readonly BehaviorSubject<string> CurrentSongTitleSubject = new(string.Empty);
    private static readonly BehaviorSubject<double> CurrentPositionSubject = new(0);
    private static readonly BehaviorSubject<double> SongLengthSubject = new(0);
    private static readonly BehaviorSubject<float> VolumeSubject = new(50);

    private static readonly IObservable<long> IntervalObservable = Observable.Interval(TimeSpan.FromMilliseconds(100));

    private static IDisposable? _currentPositionSubscription;

    private static IWaveSource? _currentWaveSource;

    private static readonly List<string> ActivePlaylist = [];
    private static bool _isPaused;
    private static int _currentTrackIndex;
    private static bool _shuffleEnabled;
    private static bool _repeatCurrent;
    private static float _volumeBeforeMute;
    private static string _currentSongTitle;

    public static readonly IObservable<string> CurrentSongTitleObservable = CurrentSongTitleSubject.AsObservable();
    public static readonly IObservable<double> CurrentPositionObservable = CurrentPositionSubject.AsObservable();
    public static readonly IObservable<double> SongLengthObservable = SongLengthSubject.AsObservable();
    public static readonly IObservable<float> VolumeObservable = VolumeSubject.AsObservable();

    static Jukebox()
    {
        SoundOut = new WasapiOut();

        CompositeDisposable = new CompositeDisposable(SoundOut, CurrentSongTitleSubject, CurrentPositionSubject, SongLengthSubject);

        LoadMusicResources();

        LoadCurrentTrack();

        SoundOut.Stopped += OnPlaybackStopped;
        
        CurrentSongTitleObservable.Subscribe(OnLeomonsSongStarted);
    }

    public static float Volume
    {
        get => SoundOut.Volume;
        set
        {
            if (Math.Abs(SoundOut.Volume - value) < 0.001) return;

            SoundOut.Volume = value;
        }
    }

    public static void EnableShuffle()
    {
        _shuffleEnabled = true;

        EventHub.SignalShuffleEnabled();
    }

    public static void DisableShuffle()
    {
        _shuffleEnabled = false;

        EventHub.SignalShuffleDisabled();
    }

    public static void PreviousSong()
    {
        int trackIndexIncrease = _shuffleEnabled ? new Random().Next(0, ActivePlaylist.Count) : 1;

        _currentTrackIndex = (_currentTrackIndex - trackIndexIncrease + ActivePlaylist.Count) % ActivePlaylist.Count;
        
        LoadCurrentTrack();

        if (!_isPaused && _hasPlayedMusic)
        {
            SoundOut.Play();
            
            if (_currentSongTitle == LeomonSongTitle) return;
            
            EventHub.SignalPreviousSongStarted();
        }
    }

    public static void PlayPause()
    {
        switch (SoundOut.PlaybackState)
        {
            case PlaybackState.Stopped:
                PlayCurrentTrack();
                _hasPlayedMusic = true;
                _isPaused = false;
                EventHub.SignalCurrentSongStarted();
                break;
            case PlaybackState.Paused:
                SoundOut.Resume();
                _isPaused = false;
                EventHub.SignalCurrentSongStarted();
                break;
            case PlaybackState.Playing:
                SoundOut.Pause();
                _isPaused = true;
                EventHub.SignalPause();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public static void NextSong()
    {
        int trackIndexIncrease = _shuffleEnabled ? new Random().Next(0, ActivePlaylist.Count) : 1;

        _currentTrackIndex = (_currentTrackIndex + trackIndexIncrease) % ActivePlaylist.Count;
        
        LoadCurrentTrack();

        if (_isPaused || !_hasPlayedMusic) return;
        
        SoundOut.Play();

        if (_currentSongTitle == LeomonSongTitle) return;
            
        EventHub.SignalNextSongStarted();
    }

    public static void RepeatCurrentSong()
    {
        _repeatCurrent = true;
        
        EventHub.SignalRepeatCurrentSongMode();
    }

    public static void PlayAllSongs()
    {
        _repeatCurrent = false;
        
        EventHub.SignalPlayAllSongsMode();
    }

    public static void Mute()
    {
        if (SoundOut.Volume == 0) return;
        
        _volumeBeforeMute = SoundOut.Volume;

        VolumeSubject.OnNext(0);

        SoundOut.Volume = 0;
        
        EventHub.SignalMute();
    }

    public static void UnMute()
    {
        if(_volumeBeforeMute == 0) return;
        
        VolumeSubject.OnNext(_volumeBeforeMute * 100);

        SoundOut.Volume = _volumeBeforeMute;
        
        EventHub.SignalUnmute();
    }

    private static void LoadMusicResources()
    {
        string musicDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Music");

        if (!Directory.Exists(musicDirectory))
        {
            throw new DirectoryNotFoundException($"The music directory '{musicDirectory}' does not exist.");
        }

        List<string> fileNames = Directory.GetFiles(musicDirectory)
            .Where(file => file.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) || file.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
            .Order()
            .ToList();

        if (fileNames.Count == 0)
        {
            throw new Exception("No music files found in the directory.");
        }

        ActivePlaylist.AddRange(fileNames);
    }


    private static void LoadCurrentTrack()
    {
        if (_currentPositionSubscription != null)
        {
            _currentPositionSubscription.Dispose();

            CompositeDisposable.Remove(_currentPositionSubscription);
        }

        if (SoundOut.PlaybackState != PlaybackState.Stopped)
        {
            SoundOut.Stop();
        }

        if (_currentWaveSource != null)
        {
            CompositeDisposable.Remove(_currentWaveSource);

            _currentWaveSource.Dispose();
        }

        if (_currentTrackIndex < 0 || _currentTrackIndex >= ActivePlaylist.Count)
        {
            throw new IndexOutOfRangeException($"Current track index {_currentTrackIndex} is out of range.");
        }

        string resourceName = ActivePlaylist[_currentTrackIndex];

        string outputDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Frontend.WPF", "Music");

        string filePath = Path.Combine(outputDirectory, resourceName);

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Could not find track at path {filePath}");
        }

        _currentWaveSource = CodecFactory.Instance.GetCodec(filePath);

        SongLengthSubject.OnNext(_currentWaveSource.GetLength().TotalSeconds);

        float currentVolume = SoundOut.Volume;

        SoundOut.Initialize(_currentWaveSource);

        SoundOut.Volume = currentVolume;

        _currentPositionSubscription = IntervalObservable.Subscribe(_ => CurrentPositionSubject.OnNext(_currentWaveSource.GetPosition().TotalSeconds));

        CompositeDisposable.Add(_currentPositionSubscription);

        _currentSongTitle = Path.GetFileNameWithoutExtension(filePath);
        CurrentSongTitleSubject.OnNext(_currentSongTitle);
    }

    private static void PlayCurrentTrack()
    {
        LoadCurrentTrack();

        SoundOut.Play();

        _currentPositionSubscription = IntervalObservable.Subscribe(_ => CurrentPositionSubject.OnNext(_currentWaveSource.GetPosition().TotalSeconds));

        CompositeDisposable.Add(_currentPositionSubscription);
    }

    private static void OnPlaybackStopped(object? sender, PlaybackStoppedEventArgs e)
    {
        if (SoundOut.PlaybackState != PlaybackState.Stopped || _isPaused) return;

        if (!_repeatCurrent)
        {
            int trackIndexIncrease = _shuffleEnabled ? new Random().Next(0, ActivePlaylist.Count) : 1;

            _currentTrackIndex = (_currentTrackIndex + trackIndexIncrease) % ActivePlaylist.Count;
        }

        PlayCurrentTrack();
    }

    private static void OnLeomonsSongStarted(string songTitle)
    {
        if (songTitle == LeomonSongTitle)
        {
            EventHub.SignalLeomonsThemeStarted();
        }
    }

    public static void Dispose()
    {
        SoundOut.Stopped -= OnPlaybackStopped;

        CompositeDisposable.Dispose();
    }
}