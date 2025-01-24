using System;
using System.Reactive.Disposables;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.Services;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.UserControls.MusicPlayer;

public class MusicPlayerViewModel : BaseViewModel, IDisposable
{
    private readonly SpeakingSimulator _speakingSimulator;
    private readonly CompositeDisposable _compositeDisposable;

    private string _currentSongTitle = string.Empty;
    private double _currentPosition;
    private TimeSpan _songLength;
    private bool _shuffleEnabled;
    private bool _repeatSingleSongEnabled;
    private bool _muteEnabled;
    private string _giromonText = string.Empty;

    public MusicPlayerViewModel()
    {
        _speakingSimulator = new SpeakingSimulator();

        ToggleShuffleCommand = new CommandHandler(ToggleShuffle);

        PreviousSongCommand = new CommandHandler(PreviousSong);

        PlayPauseCommand = new CommandHandler(PlayPause);

        NextSongCommand = new CommandHandler(NextSong);

        ToggleRepeatSingleSongCommand = new CommandHandler(ToggleRepeatSingleSongEnabled);

        ToggleMuteEnabledCommand = new CommandHandler(ToggleMuteEnabled);

        _compositeDisposable = new CompositeDisposable(
            _speakingSimulator,
            Jukebox.CurrentSongTitleObservable.Subscribe(currentSongTitle => CurrentSongTitle = currentSongTitle),
            Jukebox.CurrentPositionObservable.Subscribe(currentPosition => CurrentPosition = currentPosition),
            Jukebox.SongLengthObservable.Subscribe(songLength => SongLength = songLength),
            Jukebox.VolumeObservable.Subscribe(volume => Volume = volume),
            EventHub.OnLeftPaneOpened.Subscribe(async void (_) => await OnMusicPlayerOpened()),
            EventHub.OnLeftPaneClosed.Subscribe(async void (_) => await OnMusicPlayerClosed()),
            EventHub.OnLeomonsThemeStarted.Subscribe(async void (_) => await OnLeomonSongStarted()),
            EventHub.OnShuffleDisabled.Subscribe(async void (_) => await OnShuffleDisabled()),
            EventHub.OnShuffleEnabled.Subscribe(async void (_) => await OnShuffleEnabled()),
            EventHub.OnPreviousSongStarted.Subscribe(async void (_) => await OnPreviousSongStarted()),
            EventHub.OnCurrentSongStarted.Subscribe(async void (_) => await OnCurrentSongStarted()),
            EventHub.OnPause.Subscribe(async void (_) => await OnPaused()),
            EventHub.OnNextSongStarted.Subscribe(async void (_) => await OnNextSongStarted()),
            EventHub.OnRepeatCurrentSongMode.Subscribe(async void (_) => await OnRepeatCurrentSongMode()),
            EventHub.OnPlayAllSongsMode.Subscribe(async void (_) => await OnPlayAllSongsMode()),
            EventHub.OnMute.Subscribe(async void (_) => await OnMute()),
            EventHub.OnUnmute.Subscribe(async void (_) => await OnUnmute()));

        InstantDisplayCommand = new CommandHandler(InstantDisplay);
    }

    public ICommand ToggleShuffleCommand { get; }

    public ICommand PreviousSongCommand { get; }

    public ICommand PlayPauseCommand { get; }

    public ICommand NextSongCommand { get; }

    public ICommand ToggleRepeatSingleSongCommand { get; }

    public ICommand ToggleMuteEnabledCommand { get; }

    public ICommand InstantDisplayCommand { get; }

    public string CurrentSongTitle
    {
        get => _currentSongTitle;
        set => SetField(ref _currentSongTitle, value);
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
        set => SetField(ref _muteEnabled, value);
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

    public string GiromonText
    {
        get => _giromonText;
        set => SetField(ref _giromonText, value);
    }

    public string ShuffleButtonText => _shuffleEnabled ? "Shuffle mode ON.\n\nClick to turn OFF shuffle mode." : "Shuffle mode OFF.\n\nClick to turn ON shuffle mode.";

    public string PreviousSongButtonText { get; } = "Skip to previous song";

    public string PlayPauseButtonText => Jukebox.IsPaused ? "Music is paused.\n\nClick to start playing music." : "Music is playing.\n\nClick to pause music.";

    public string NextSongButtonText { get; } = "Skip to next song.";

    public string RepeatSingleSongText => _repeatSingleSongEnabled ? "Repeat single song mode ON.\n\nClick to turn ON play all songs mode." : "Play all songs mode ON.\n\nClick to turn ON repeat single song mode.";

    public string MuteButtonText => _muteEnabled ? "Mute is ON.\n\nClick to turn OFF mute." : "Mute is OFF.\n\nClick to turn ON mute.";

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

    private void InstantDisplay()
    {
        _speakingSimulator.RequestInstantDisplay();
    }

    private async Task OnMusicPlayerOpened() => await Task
        .Delay(750)
        .WaitAsync(CancellationToken.None)
        .ContinueWith(_ => _speakingSimulator.WriteTextAsSpeechAsync(GiromonJukeboxNarratorText.IntroText, textOutput => GiromonText = textOutput));

    private async Task OnMusicPlayerClosed() => await _speakingSimulator.WriteTextAsSpeechAsync(string.Empty, textOutput => GiromonText = textOutput);

    private async Task OnLeomonSongStarted() => await _speakingSimulator.WriteTextAsSpeechAsync(GiromonJukeboxNarratorText.LeomonsTheme, textOutput => GiromonText = textOutput);
    private async Task OnShuffleDisabled() => await _speakingSimulator.WriteTextAsSpeechAsync(GiromonJukeboxNarratorText.ShuffleDisabled, textOutput => GiromonText = textOutput);
    private async Task OnShuffleEnabled() => await _speakingSimulator.WriteTextAsSpeechAsync(GiromonJukeboxNarratorText.ShuffleEnabled, textOutput => GiromonText = textOutput);
    private async Task OnPreviousSongStarted() => await _speakingSimulator.WriteTextAsSpeechAsync(GiromonJukeboxNarratorText.PreviousSong, textOutput => GiromonText = textOutput);
    private async Task OnCurrentSongStarted() => await _speakingSimulator.WriteTextAsSpeechAsync(GiromonJukeboxNarratorText.Play, textOutput => GiromonText = textOutput);
    private async Task OnPaused() => await _speakingSimulator.WriteTextAsSpeechAsync(GiromonJukeboxNarratorText.Pause, textOutput => GiromonText = textOutput);
    private async Task OnNextSongStarted() => await _speakingSimulator.WriteTextAsSpeechAsync(GiromonJukeboxNarratorText.NextSong, textOutput => GiromonText = textOutput);
    private async Task OnRepeatCurrentSongMode() => await _speakingSimulator.WriteTextAsSpeechAsync(GiromonJukeboxNarratorText.RepeatCurrent, textOutput => GiromonText = textOutput);
    private async Task OnPlayAllSongsMode() => await _speakingSimulator.WriteTextAsSpeechAsync(GiromonJukeboxNarratorText.PlayAllSongs, textOutput => GiromonText = textOutput);
    private async Task OnMute() => await _speakingSimulator.WriteTextAsSpeechAsync(GiromonJukeboxNarratorText.Mute, textOutput => GiromonText = textOutput);
    private async Task OnUnmute() => await _speakingSimulator.WriteTextAsSpeechAsync(GiromonJukeboxNarratorText.Unmute, textOutput => GiromonText = textOutput);

    public void Dispose()
    {
        _compositeDisposable.Dispose();
    }
}