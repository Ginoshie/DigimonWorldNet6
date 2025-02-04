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
            EventHub.LeftPaneOpenedObservable.Subscribe(async void (_) => await OnMusicPlayerOpened()),
            EventHub.LeftPaneClosedObservable.Subscribe(async void (_) => await OnMusicPlayerClosed()),
            EventHub.LeomonsThemeStartedObservable.Subscribe(async void (_) => await OnLeomonSongStarted()),
            EventHub.RepeatModeObservable.Subscribe(async void (repeatMode) => await OnRepeatModeChanged(repeatMode)),
            EventHub.PreviousSongStartedObservable.Subscribe(async void (_) => await OnPreviousSongStarted()),
            EventHub.PlayModeObservable.Subscribe(async void (playMode) => await OnPlayModeChanged(playMode)),
            EventHub.NextSongStartedObservable.Subscribe(async void (_) => await OnNextSongStarted()),
            EventHub.ShuffleModeObservable.Subscribe(async void (shuffleMode) => await OnShuffleModeChanged(shuffleMode)),
            EventHub.MuteModeObservable.Subscribe(async void (muteMode) => await OnMuteModeChanged(muteMode)));

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
        private set => SetField(ref _currentSongTitle, value);
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
        private set
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

            Jukebox.SetShuffleMode(_shuffleEnabled ? ShuffleMode.Shuffle : ShuffleMode.Chronological);

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

            Jukebox.SetRepeatMode(_repeatSingleSongEnabled ? RepeatMode.Single : RepeatMode.All);

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

    private void ToggleShuffle()
    {
        ShuffleEnabled = !ShuffleEnabled;

        Jukebox.SetShuffleMode(ShuffleEnabled ? ShuffleMode.Shuffle : ShuffleMode.Chronological);
    }

    private void PreviousSong() => Jukebox.PreviousSong();

    private void PlayPause() => Jukebox.PlayPause();

    private void NextSong() => Jukebox.NextSong();

    private void ToggleRepeatSingleSongEnabled()
    {
        RepeatSingleSongEnabled = !RepeatSingleSongEnabled;

        Jukebox.SetRepeatMode(RepeatSingleSongEnabled ? RepeatMode.Single : RepeatMode.All);
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

    private void InstantDisplay() => _speakingSimulator.RequestInstantDisplay();

    private async Task OnMusicPlayerOpened() => await Task
        .Delay(750)
        .WaitAsync(CancellationToken.None)
        .ContinueWith(_ => _speakingSimulator.WriteTextAsSpeechAsync(GiromonJukeboxNarratorText.IntroText, textOutput => GiromonText = textOutput));

    private async Task OnMusicPlayerClosed() => await _speakingSimulator.WriteTextAsSpeechAsync(string.Empty, textOutput => GiromonText = textOutput);

    private async Task OnLeomonSongStarted() => await _speakingSimulator.WriteTextAsSpeechAsync(GiromonJukeboxNarratorText.LeomonsTheme, textOutput => GiromonText = textOutput);

    private async Task OnShuffleModeChanged(ShuffleMode shuffleMode)
    {
        switch (shuffleMode)
        {
            case ShuffleMode.Shuffle:
                ShuffleEnabled = true;
                await _speakingSimulator.WriteTextAsSpeechAsync(GiromonJukeboxNarratorText.ShuffleEnabled, textOutput => GiromonText = textOutput);
                return;
            case ShuffleMode.Chronological:
                ShuffleEnabled = false;
                await _speakingSimulator.WriteTextAsSpeechAsync(GiromonJukeboxNarratorText.ShuffleDisabled, textOutput => GiromonText = textOutput);
                return;
            default:
                throw new ArgumentOutOfRangeException(nameof(shuffleMode), shuffleMode, null);
        }
    }

    private async Task OnPreviousSongStarted() => await _speakingSimulator.WriteTextAsSpeechAsync(GiromonJukeboxNarratorText.PreviousSong, textOutput => GiromonText = textOutput);

    private async Task OnPlayModeChanged(PlayMode playMode)
    {
        switch (playMode)
        {
            case PlayMode.Paused:
                await _speakingSimulator.WriteTextAsSpeechAsync(GiromonJukeboxNarratorText.Pause, textOutput => GiromonText = textOutput);
                return;
            case PlayMode.Playing:
                await _speakingSimulator.WriteTextAsSpeechAsync(GiromonJukeboxNarratorText.Play, textOutput => GiromonText = textOutput);
                return;
            case PlayMode.Stopped:
            default:
                throw new ArgumentOutOfRangeException(nameof(playMode), playMode, null);
        }
    }

    private async Task OnNextSongStarted() => await _speakingSimulator.WriteTextAsSpeechAsync(GiromonJukeboxNarratorText.NextSong, textOutput => GiromonText = textOutput);

    private async Task OnRepeatModeChanged(RepeatMode repeatMode)
    {
        switch (repeatMode)
        {
            case RepeatMode.All:
                RepeatSingleSongEnabled = false;
                await _speakingSimulator.WriteTextAsSpeechAsync(GiromonJukeboxNarratorText.PlayAllSongs, textOutput => GiromonText = textOutput);
                return;
            case RepeatMode.Single:
                RepeatSingleSongEnabled = true;
                await _speakingSimulator.WriteTextAsSpeechAsync(GiromonJukeboxNarratorText.RepeatCurrent, textOutput => GiromonText = textOutput);
                return;
            default:
                throw new ArgumentOutOfRangeException(nameof(repeatMode), repeatMode, null);
        }
    }

    private async Task OnMuteModeChanged(MuteMode muteMode)
    {
        switch (muteMode)
        {
            case MuteMode.Mute:
                MuteEnabled = true;
                await _speakingSimulator.WriteTextAsSpeechAsync(GiromonJukeboxNarratorText.Mute, textOutput => GiromonText = textOutput);
                return;
            case MuteMode.Unmuted:
                MuteEnabled = false;
                await _speakingSimulator.WriteTextAsSpeechAsync(GiromonJukeboxNarratorText.Unmute, textOutput => GiromonText = textOutput);
                return;
            default:
                throw new ArgumentOutOfRangeException(nameof(muteMode), muteMode, null);
        }
    }

    public void Dispose()
    {
        _compositeDisposable.Dispose();
    }
}