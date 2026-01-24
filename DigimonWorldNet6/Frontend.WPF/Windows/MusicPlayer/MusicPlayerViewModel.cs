using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.Services;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using DigimonWorld.Frontend.WPF.Windows.BaseClasses;
using Generics.Enums;

namespace DigimonWorld.Frontend.WPF.Windows.MusicPlayer;

public class MusicPlayerViewModel : BaseWindowViewModel, IDisposable
{
    private readonly SpeakingSimulator _speakingSimulator;
    private readonly CompositeDisposable _compositeDisposable;

    private string _currentSongTitle = string.Empty;
    private double _currentPosition;
    private TimeSpan _songLength;
    private bool _shuffleEnabled = Services.MusicPlayer.ShuffleMode == ShuffleMode.Shuffle;
    private bool _repeatSingleSongEnabled = Services.MusicPlayer.RepeatMode == RepeatMode.Single;
    private bool _muteEnabled = Services.MusicPlayer.Volume > 0;
    private int _volume = (int)(Services.MusicPlayer.Volume * 100f);
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

        InstantDisplayCommand = new CommandHandler(InstantDisplay);

        _compositeDisposable = new CompositeDisposable(
            _speakingSimulator,
            Services.MusicPlayer.CurrentSongTitleObservable.Subscribe(currentSongTitle => CurrentSongTitle = currentSongTitle),
            Services.MusicPlayer.CurrentPositionObservable.Subscribe(currentPosition => CurrentPosition = currentPosition),
            Services.MusicPlayer.SongLengthObservable.Subscribe(songLength => SongLength = songLength),
            EventHub.MusicPlayerOpenedObservable
                .SelectMany(_ => Observable.FromAsync(OnMusicPlayerOpened))
                .Subscribe(),
            EventHub.MusicPlayerClosedObservable
                .SelectMany(_ => Observable.FromAsync(OnMusicPlayerClosed))
                .Subscribe(),
            EventHub.LeomonsThemeStartedObservable
                .SelectMany(_ => Observable.FromAsync(OnLeomonSongStarted))
                .Subscribe(),
            EventHub.ShuffleModeChangedObservable
                .SelectMany(mode => Observable.FromAsync(() => OnShuffleModeChanged(mode)))
                .Subscribe(),
            EventHub.PreviousSongStartedObservable
                .SelectMany(Observable.FromAsync(OnPreviousSongStarted))
                .Subscribe(),
            EventHub.PlayModeChangedObservable
                .SelectMany(mode => Observable.FromAsync(() => OnPlayModeChanged(mode)))
                .Subscribe(),
            EventHub.NextSongStartedObservable
                .SelectMany(Observable.FromAsync(OnNextSongStarted))
                .Subscribe(),
            EventHub.RepeatModeChangedObservable
                .SelectMany(mode => Observable.FromAsync(() => OnRepeatModeChanged(mode)))
                .Subscribe(),
            EventHub.MuteModeChangedObservable
                .SelectMany(mode => Observable.FromAsync(() => OnMuteModeChanged(mode)))
                .Subscribe(),
            EventHub.VolumeChangedObservable.Subscribe(OnVolumeChanged)
        );
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
        private set
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
        private set
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
        get => _volume;
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
        ShuffleMode toggledShuffleMode = !ShuffleEnabled ? ShuffleMode.Shuffle : ShuffleMode.Chronological;
        Services.MusicPlayer.SetShuffleMode(toggledShuffleMode);
    }

    private void PreviousSong() => Services.MusicPlayer.PreviousSong();

    private void PlayPause() => Services.MusicPlayer.PlayPause();

    private void NextSong() => Services.MusicPlayer.NextSong();

    private void ToggleRepeatSingleSongEnabled()
    {
        RepeatMode toggledRepeatMode = !RepeatSingleSongEnabled ? RepeatMode.Single : RepeatMode.All;
        Services.MusicPlayer.SetRepeatMode(toggledRepeatMode);
    }

    private void ToggleMuteEnabled()
    {
        if (Volume == 0)
        {
            Services.MusicPlayer.UnMute();
        }
        else
        {
            Services.MusicPlayer.Mute();
        }
    }

    private void InstantDisplay() => _speakingSimulator.RequestInstantDisplay();

    private async Task OnMusicPlayerOpened()
    {
        int initialDelay = UserConfigurationManager.SpeakingSimulatorConfig.NarratorMode == NarratorMode.Instant ? 0 : 750;

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
        if (MuteEnabled && muteMode == MuteMode.Mute || !MuteEnabled && muteMode == MuteMode.Unmuted) return;

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

    private void OnVolumeChanged(float newVolume)
    {
        _volume = (int)(newVolume * 100f);

        OnPropertyChanged(nameof(Volume));
    }

    public void Dispose()
    {
        _compositeDisposable.Dispose();
    }
}