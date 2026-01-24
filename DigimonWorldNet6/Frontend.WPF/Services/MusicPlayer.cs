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
using DigimonWorld.Frontend.WPF.Constants;
using Generics.Configuration;
using Generics.Enums;

namespace DigimonWorld.Frontend.WPF.Services;

public static class MusicPlayer
{
    private const string LEOMON_SONG_TITLE = "19) Leomon";

    private static readonly WasapiOut SoundOut;
    private static readonly CompositeDisposable CompositeDisposable;

    private static readonly BehaviorSubject<string> CurrentSongTitleSubject = new(string.Empty);
    private static readonly BehaviorSubject<double> CurrentPositionSubject = new(0);
    private static readonly BehaviorSubject<double> SongLengthSubject = new(0);

    private static readonly IObservable<long> IntervalObservable = Observable.Interval(TimeSpan.FromMilliseconds(100));

    private static readonly List<string> ActivePlaylist = [];

    private static IDisposable? _currentPositionSubscription;

    private static IWaveSource? _currentWaveSource;

    private static MusicPlayerConfig _musicPlayerConfig = null!;

    private static int _currentTrackIndex;
    private static float _volumeBeforeMute;
    private static string? _currentSongTitle;

    public static readonly IObservable<string> CurrentSongTitleObservable = CurrentSongTitleSubject.AsObservable();
    public static readonly IObservable<double> CurrentPositionObservable = CurrentPositionSubject.AsObservable();
    public static readonly IObservable<double> SongLengthObservable = SongLengthSubject.AsObservable();
    private static PlayMode _playMode = PlayMode.Stopped;

    static MusicPlayer()
    {
        SoundOut = new WasapiOut();

        CompositeDisposable = new CompositeDisposable(SoundOut,
            CurrentSongTitleSubject,
            CurrentPositionSubject,
            SongLengthSubject,
            EventHub.MusicPlayerClosedObservable.Subscribe(_ => OnMusicPlayerClosed())
        );

        LoadMusicResources();

        LoadCurrentTrack();

        LoadConfig(UserConfigurationManager.CurrentMusicPlayerConfig.FirstAsync().Wait());
        
        SoundOut.Stopped += OnPlaybackStopped;

        CurrentSongTitleObservable.Subscribe(OnLeomonsSongStarted);
    }

    public static PlayMode PlayMode
    {
        get => _playMode;
        private set => _playMode = value;
    }

    public static float Volume
    {
        get => SoundOut.Volume;
        set
        {
            if (Math.Abs(SoundOut.Volume - value) < 0.001) return;

            SoundOut.Volume = value;
            
            EventHub.SignalVolumeChanged(value);

            EventHub.SignalMuteMode(value == 0 ? MuteMode.Mute : MuteMode.Unmuted);
        }
    }

    public static ShuffleMode ShuffleMode { get; private set; }

    public static RepeatMode RepeatMode { get; private set; }

    public static void SetShuffleMode(ShuffleMode shuffleMode)
    {
        ShuffleMode = shuffleMode;

        EventHub.SignalShuffleMode(shuffleMode);
    }

    public static void PreviousSong()
    {
        int trackIndexIncrease = ShuffleMode == ShuffleMode.Shuffle ? new Random().Next(0, ActivePlaylist.Count) : 1;

        _currentTrackIndex = (_currentTrackIndex - trackIndexIncrease + ActivePlaylist.Count) % ActivePlaylist.Count;

        LoadCurrentTrack();

        if (PlayMode == PlayMode.Stopped) return;

        SoundOut.Play();

        if (_currentSongTitle == LEOMON_SONG_TITLE) return;

        EventHub.SignalPreviousSongStarted();
    }

    public static void PlayPause()
    {
        switch (SoundOut.PlaybackState)
        {
            case PlaybackState.Stopped:
                StartPlaying();
                break;
            case PlaybackState.Paused:
                Unpause();
                break;
            case PlaybackState.Playing:
                Pause();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        EventHub.SignalPlayModeChanged(PlayMode);
    }

    public static void NextSong()
    {
        int trackIndexIncrease = ShuffleMode == ShuffleMode.Shuffle ? new Random().Next(0, ActivePlaylist.Count) : 1;

        _currentTrackIndex = (_currentTrackIndex + trackIndexIncrease) % ActivePlaylist.Count;

        LoadCurrentTrack();

        if (PlayMode == PlayMode.Stopped) return;

        SoundOut.Play();

        if (_currentSongTitle == LEOMON_SONG_TITLE) return;

        EventHub.SignalNextSongStarted();
    }

    public static void SetRepeatMode(RepeatMode repeatMode)
    {
        RepeatMode = repeatMode;

        EventHub.SignalRepeatMode(RepeatMode);
    }

    public static void Mute()
    {
        if (Volume == 0) return;

        _volumeBeforeMute = SoundOut.Volume;

        Volume = 0;
    }

    public static void UnMute()
    {
        if (_volumeBeforeMute == 0) return;

        Volume = _volumeBeforeMute;
    }

    private static void OnMusicPlayerClosed()
    {
        switch (_musicPlayerConfig.OnCloseAction)
        {
            case MusicPlayerOnCloseAction.Pause:
                Pause();
                break;
            case MusicPlayerOnCloseAction.Nothing:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
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

        Volume = currentVolume;

        _currentPositionSubscription = IntervalObservable.Subscribe(_ => CurrentPositionSubject.OnNext(_currentWaveSource.GetPosition().TotalSeconds));

        CompositeDisposable.Add(_currentPositionSubscription);

        _currentSongTitle = Path.GetFileNameWithoutExtension(filePath);
        CurrentSongTitleSubject.OnNext(_currentSongTitle);
    }

    private static void LoadConfig(MusicPlayerConfig musicPlayerConfig)
    {
        _musicPlayerConfig = musicPlayerConfig;

        SetShuffleMode(musicPlayerConfig.ShuffleMode);

        SetRepeatMode(musicPlayerConfig.RepeatMode);

        Volume = musicPlayerConfig.Volume / 100f;
        
        _volumeBeforeMute = Volume;

        if (musicPlayerConfig.MuteMode == MuteMode.Mute)
        {
            Mute();
        }
        else
        {
            UnMute();
        }
    }

    private static void PlayCurrentTrack()
    {
        LoadCurrentTrack();

        SoundOut.Play();

        _currentPositionSubscription = IntervalObservable.Subscribe(_ => CurrentPositionSubject.OnNext(_currentWaveSource.GetPosition().TotalSeconds));

        CompositeDisposable.Add(_currentPositionSubscription);
    }

    private static void StartPlaying()
    {
        PlayCurrentTrack();

        PlayMode = PlayMode.Playing;
    }

    private static void Unpause()
    {
        SoundOut.Resume();

        PlayMode = PlayMode.Playing;
    }

    private static void Pause()
    {
        SoundOut.Pause();

        PlayMode = PlayMode.Paused;
    }

    private static void OnPlaybackStopped(object? sender, PlaybackStoppedEventArgs e)
    {
        if (SoundOut.PlaybackState != PlaybackState.Stopped || PlayMode == PlayMode.Paused) return;

        if (RepeatMode == RepeatMode.All)
        {
            int trackIndexIncrease = ShuffleMode == ShuffleMode.Shuffle ? new Random().Next(0, ActivePlaylist.Count) : 1;

            _currentTrackIndex = (_currentTrackIndex + trackIndexIncrease) % ActivePlaylist.Count;
        }

        PlayCurrentTrack();
    }

    private static void OnLeomonsSongStarted(string songTitle)
    {
        if (songTitle == LEOMON_SONG_TITLE)
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