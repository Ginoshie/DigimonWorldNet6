using System;
using System.Reactive.Disposables;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.Services;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using DigimonWorld.Frontend.WPF.Windows.BaseClasses;

namespace DigimonWorld.Frontend.WPF.Windows.MusicPlayer;

public class MusicPlayerViewModel : BaseWindowViewModel, IDisposable
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

    public MusicPlayerViewModel(Window window) : base(window)
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
            Services.MusicPlayer.CurrentSongTitleObservable.Subscribe(currentSongTitle => CurrentSongTitle = currentSongTitle),
            Services.MusicPlayer.CurrentPositionObservable.Subscribe(currentPosition => CurrentPosition = currentPosition),
            Services.MusicPlayer.SongLengthObservable.Subscribe(songLength => SongLength = songLength),
            EventHub.MusicPlayerOpenedObservable.Subscribe(async void (_) => await OnMusicPlayerOpened()),
            EventHub.MusicPlayerClosedObservable.Subscribe(async void (_) => await OnMusicPlayerClosed()),
            EventHub.LeomonsThemeStartedObservable.Subscribe(async void (_) => await OnLeomonSongStarted()), 
            EventHub.PlayModeObservable.Subscribe(async void (playMode) => await OnPlayModeChanged(playMode))
        );

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

            Services.MusicPlayer.SetShuffleMode(_shuffleEnabled ? ShuffleMode.Shuffle : ShuffleMode.Chronological);

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

            Services.MusicPlayer.SetRepeatMode(_repeatSingleSongEnabled ? RepeatMode.Single : RepeatMode.All);

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
        get => (float)Math.Round(Services.MusicPlayer.Volume * 100);
        set
        {
            if (Math.Abs(Services.MusicPlayer.Volume - value) < 0.001) return;

            Services.MusicPlayer.Volume = value / 100;

            OnPropertyChanged();
        }
    }

    public string GiromonText
    {
        get => _giromonText;
        set => SetField(ref _giromonText, value);
    }

    protected override void CloseApplication()
    {
        EventHub.SignalMusicPlayerClosed();

        base.CloseApplication();
    }

    private void ToggleShuffle()
    {
        ShuffleEnabled = !ShuffleEnabled;

        ShuffleMode shuffleMode = ShuffleEnabled ? ShuffleMode.Shuffle : ShuffleMode.Chronological;

        Services.MusicPlayer.SetShuffleMode(shuffleMode);

        _ = OnShuffleModeChanged(shuffleMode);
    }

    private void PreviousSong()
    {
        Services.MusicPlayer.PreviousSong();

        _ = OnPreviousSongStarted();
    }

    private void PlayPause() => Services.MusicPlayer.PlayPause();

    private void NextSong()
    {
        Services.MusicPlayer.NextSong();

        _ = OnNextSongStarted();
    }

    private void ToggleRepeatSingleSongEnabled()
    {
        RepeatSingleSongEnabled = !RepeatSingleSongEnabled;

        RepeatMode repeatMode = RepeatSingleSongEnabled ? RepeatMode.Single : RepeatMode.All;
        
        Services.MusicPlayer.SetRepeatMode(repeatMode);

        _ = OnRepeatModeChanged(repeatMode);
    }

    private void ToggleMuteEnabled()
    {
        if (Volume == 0)
        {
            if (!MuteEnabled) return;

            MuteEnabled = false;

            Services.MusicPlayer.UnMute();

            _ = OnMuteModeChanged(MuteMode.Unmuted);
        }
        else
        {
            Services.MusicPlayer.Mute();

            MuteEnabled = true;

            _ = OnMuteModeChanged(MuteMode.Mute);
        }
    }

    private void InstantDisplay() => _speakingSimulator.RequestInstantDisplay();

    private async Task OnMusicPlayerOpened()
    {
        int initialDelay = GeneralConfigurationManager.SpeakingSimulatorConfig.NarratorMode == NarratorMode.Instant ? 0 : 750;

        await Task
            .Delay(initialDelay)
            .WaitAsync(CancellationToken.None)
            .ContinueWith(_ => _speakingSimulator.WriteTextAsSpeechAsync(GiromonMusicPlayerNarratorText.IntroText, textOutput => GiromonText = textOutput));
    }

    private async Task OnMusicPlayerClosed() => await _speakingSimulator.WriteTextAsSpeechAsync(string.Empty, textOutput => GiromonText = textOutput);

    private async Task OnLeomonSongStarted() => await _speakingSimulator.WriteTextAsSpeechAsync(GiromonMusicPlayerNarratorText.LeomonsTheme, textOutput => GiromonText = textOutput);

    private async Task OnShuffleModeChanged(ShuffleMode shuffleMode)
    {
        switch (shuffleMode)
        {
            case ShuffleMode.Shuffle:
                ShuffleEnabled = true;
                await _speakingSimulator.WriteTextAsSpeechAsync(GiromonMusicPlayerNarratorText.ShuffleEnabled, textOutput => GiromonText = textOutput);
                return;
            case ShuffleMode.Chronological:
                ShuffleEnabled = false;
                await _speakingSimulator.WriteTextAsSpeechAsync(GiromonMusicPlayerNarratorText.ShuffleDisabled, textOutput => GiromonText = textOutput);
                return;
            default:
                throw new ArgumentOutOfRangeException(nameof(shuffleMode), shuffleMode, null);
        }
    }

    private async Task OnPreviousSongStarted() => await _speakingSimulator.WriteTextAsSpeechAsync(GiromonMusicPlayerNarratorText.PreviousSong, textOutput => GiromonText = textOutput);

    private async Task OnPlayModeChanged(PlayMode playMode)
    {
        switch (playMode)
        {
            case PlayMode.Paused:
                await _speakingSimulator.WriteTextAsSpeechAsync(GiromonMusicPlayerNarratorText.Pause, textOutput => GiromonText = textOutput);
                return;
            case PlayMode.Playing:
                await _speakingSimulator.WriteTextAsSpeechAsync(GiromonMusicPlayerNarratorText.Play, textOutput => GiromonText = textOutput);
                return;
            case PlayMode.Stopped:
            default:
                throw new ArgumentOutOfRangeException(nameof(playMode), playMode, null);
        }
    }

    private async Task OnNextSongStarted() => await _speakingSimulator.WriteTextAsSpeechAsync(GiromonMusicPlayerNarratorText.NextSong, textOutput => GiromonText = textOutput);

    private async Task OnRepeatModeChanged(RepeatMode repeatMode)
    {
        switch (repeatMode)
        {
            case RepeatMode.All:
                RepeatSingleSongEnabled = false;
                await _speakingSimulator.WriteTextAsSpeechAsync(GiromonMusicPlayerNarratorText.PlayAllSongs, textOutput => GiromonText = textOutput);
                return;
            case RepeatMode.Single:
                RepeatSingleSongEnabled = true;
                await _speakingSimulator.WriteTextAsSpeechAsync(GiromonMusicPlayerNarratorText.RepeatCurrent, textOutput => GiromonText = textOutput);
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
                await _speakingSimulator.WriteTextAsSpeechAsync(GiromonMusicPlayerNarratorText.Mute, textOutput => GiromonText = textOutput);
                return;
            case MuteMode.Unmuted:
                MuteEnabled = false;
                await _speakingSimulator.WriteTextAsSpeechAsync(GiromonMusicPlayerNarratorText.Unmute, textOutput => GiromonText = textOutput);
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