using System;
using System.Reactive.Disposables;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Services;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.UserControls.MusicPlayer;

public class MusicPlayerViewModel : BaseViewModel, IDisposable
{
    private readonly CompositeDisposable _compositeDisposable = new();

    private string _currentSongTitle = string.Empty;
    private double _currentPosition;
    private TimeSpan _songLength;
    private bool _shuffleEnabled;
    private bool _repeatSingleSongEnabled;
    private bool _muteEnabled;

    public MusicPlayerViewModel()
    {
        ToggleShuffleCommand = new CommandHandler(ToggleShuffle);

        PreviousSongCommand = new CommandHandler(PreviousSong);

        PlayPauseCommand = new CommandHandler(PlayPause);

        NextSongCommand = new CommandHandler(NextSong);

        ToggleRepeatSingleSongCommand = new CommandHandler(ToggleRepeatSingleSongEnabled);

        ToggleMuteEnabledCommand = new CommandHandler(ToggleMuteEnabled);

        _compositeDisposable.Add(Jukebox.CurrentSongTitleObservable.Subscribe(currentSongTitle => CurrentSongTitle = currentSongTitle));
        _compositeDisposable.Add(Jukebox.CurrentPositionObservable.Subscribe(currentPosition => CurrentPosition = currentPosition));
        _compositeDisposable.Add(Jukebox.SongLengthObservable.Subscribe(songLength => SongLength = songLength));
        _compositeDisposable.Add(Jukebox.VolumeObservable.Subscribe(volume => Volume = volume));
    }

    public ICommand ToggleShuffleCommand { get; }

    public ICommand PreviousSongCommand { get; }

    public ICommand PlayPauseCommand { get; }

    public ICommand NextSongCommand { get; }

    public ICommand ToggleRepeatSingleSongCommand { get; }

    public ICommand ToggleMuteEnabledCommand { get; }

    public string CurrentSongTitle
    {
        get => _currentSongTitle;
        set
        {
            if (value == _currentSongTitle) return;

            _currentSongTitle = value;

            OnPropertyChanged();
        }
    }

    public double CurrentPosition
    {
        get => _currentPosition;
        set
        {
            if (!(Math.Abs(_currentPosition - value) > 0.1)) return;

            _currentPosition = value;

            OnPropertyChanged();
            OnPropertyChanged(nameof(CurrentPositionString));
        }
    }

    public string CurrentPositionString => TimeSpan.FromSeconds(CurrentPosition).ToString(@"mm\:ss");

    public double SongLength
    {
        get => _songLength.TotalSeconds;
        set
        {
            if (Math.Abs(_songLength.TotalSeconds - value) < 0.1) return;

            _songLength = TimeSpan.FromSeconds(value);

            OnPropertyChanged();
            OnPropertyChanged(nameof(SongLengthString));
        }
    }

    public string SongLengthString => TimeSpan.FromSeconds(SongLength).ToString(@"mm\:ss");

    public bool ShuffleEnabled
    {
        get => _shuffleEnabled;
        set
        {
            if (value == _shuffleEnabled) return;

            _shuffleEnabled = value;

            if (_shuffleEnabled)
            {
                Jukebox.EnableShuffle();
            }
            else
            {
                Jukebox.DisableShuffle();
            }

            OnPropertyChanged();
        }
    }

    public bool RepeatSingleSongEnabled
    {
        get => _repeatSingleSongEnabled;
        set
        {
            if (value == _repeatSingleSongEnabled) return;

            _repeatSingleSongEnabled = value;

            if (_repeatSingleSongEnabled)
            {
                Jukebox.RepeatCurrentSong();
            }
            else
            {
                Jukebox.PlayAllSongs();
            }

            OnPropertyChanged();
        }
    }

    public bool MuteEnabled
    {
        get => _muteEnabled;
        set
        {
            if (value == _muteEnabled) return;

            _muteEnabled = value;

            OnPropertyChanged();
        }
    }

    public float Volume
    {
        get => (float)Math.Round(Jukebox.Volume * 100);
        set
        {
            if (Math.Abs(Jukebox.Volume - value) < 0.001) return;

            Jukebox.Volume = value / 100;

            OnPropertyChanged();
        }
    }

    private void ToggleShuffle()
    {
        ShuffleEnabled = !ShuffleEnabled;

        if (ShuffleEnabled)
        {
            Jukebox.EnableShuffle();
        }
        else
        {
            Jukebox.DisableShuffle();
        }
    }

    private void PreviousSong()
    {
        Jukebox.PreviousSong();
    }

    private void PlayPause()
    {
        Jukebox.PlayPause();
    }

    private void NextSong()
    {
        Jukebox.NextSong();
    }

    private void ToggleRepeatSingleSongEnabled()
    {
        RepeatSingleSongEnabled = !RepeatSingleSongEnabled;

        if (RepeatSingleSongEnabled)
        {
            Jukebox.RepeatCurrentSong();
        }
        else
        {
            Jukebox.PlayAllSongs();
        }
    }

    private void ToggleMuteEnabled()
    {
        if (Volume == 0)
        {
            if (!MuteEnabled) return;

            MuteEnabled = false;

            Jukebox.UnMute();
        }
        else
        {
            Jukebox.Mute();

            MuteEnabled = true;
        }
    }

    public void Dispose()
    {
        _compositeDisposable.Dispose();
    }
}