using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.Enums;
using DigimonWorld.Frontend.WPF.Services;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using DigimonWorld.Frontend.WPF.Windows.BaseClasses;
using Shared.Enums;
using Shared.Services;

namespace DigimonWorld.Frontend.WPF.Windows.MusicPlayer;

public class MusicPlayerViewModel : BaseWindowViewModel, IDisposable
{
    private readonly SpeakingSimulator _speakingSimulator;
    private readonly CompositeDisposable _compositeDisposable;

    private TimeSpan _songLength;
    private int _volume = (int)(Services.MusicPlayer.Volume * 100f);

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
        get;
        private set => SetField(ref field, value);
    } = string.Empty;

    public double CurrentPosition
    {
        get;
        set
        {
            if (!(Math.Abs(field - value) > 0.1))
            {
                return;
            }

            field = value;

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
            if (Math.Abs(_songLength.TotalSeconds - value) < 0.1)
            {
                return;
            }

            _songLength = TimeSpan.FromSeconds(value);

            OnPropertyChanged();
            OnPropertyChanged(nameof(SongLengthString));
        }
    }

    public string SongLengthString => TimeSpan.FromSeconds(SongLength).ToString(@"mm\:ss");

    public bool ShuffleEnabled
    {
        get;
        private set
        {
            if (value == field)
            {
                return;
            }

            field = value;

            Services.MusicPlayer.SetShuffleMode(field ? ShuffleMode.Shuffle : ShuffleMode.Chronological);

            OnPropertyChanged();
        }
    } = Services.MusicPlayer.ShuffleMode == ShuffleMode.Shuffle;

    public bool RepeatSingleSongEnabled
    {
        get;
        private set
        {
            if (value == field)
            {
                return;
            }

            field = value;
            Services.MusicPlayer.SetRepeatMode(field ? RepeatMode.Single : RepeatMode.All);
            OnPropertyChanged();
        }
    } = Services.MusicPlayer.RepeatMode == RepeatMode.Single;

    public bool MuteEnabled
    {
        get;
        set => SetField(ref field, value);
    } = Services.MusicPlayer.Volume > 0;

    public float Volume
    {
        get => _volume;
        set
        {
            if (Math.Abs(Services.MusicPlayer.Volume - value) < 0.001)
            {
                return;
            }

            Services.MusicPlayer.Volume = value / 100;

            OnPropertyChanged();
        }
    }

    public string GiromonText
    {
        get;
        set => SetField(ref field, value);
    } = string.Empty;

    protected override void CloseApplication()
    {
        EventHub.SignalMusicPlayerClosed();

        base.CloseApplication();
    }

    private void ToggleShuffle() => Services.MusicPlayer.SetShuffleMode(!ShuffleEnabled ? ShuffleMode.Shuffle : ShuffleMode.Chronological);

    private void PreviousSong() => Services.MusicPlayer.PreviousSong();

    private void PlayPause() => Services.MusicPlayer.PlayPause();

    private void NextSong() => Services.MusicPlayer.NextSong();

    private void ToggleRepeatSingleSongEnabled() => Services.MusicPlayer.SetRepeatMode(!RepeatSingleSongEnabled ? RepeatMode.Single : RepeatMode.All);

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

    private Task SpeakGiromonTextAsync(string text, SpeechDelay delayMs = SpeechDelay.None) => _speakingSimulator.SpeakAsync(text, output => GiromonText = output, delayMs);

    private async Task OnMusicPlayerOpened()
    {
        SpeechDelay delay = UserConfigurationManager.SpeakingSimulatorConfig.NarratorMode == NarratorMode.Instant ? SpeechDelay.None : SpeechDelay.Short;

        await SpeakGiromonTextAsync(GiromonMusicPlayerNarratorText.IntroText, delay);
    }

    private Task OnMusicPlayerClosed() => SpeakGiromonTextAsync(string.Empty);

    private Task OnLeomonSongStarted() => SpeakGiromonTextAsync(GiromonMusicPlayerNarratorText.LeomonsTheme);
    private Task OnPreviousSongStarted() => SpeakGiromonTextAsync(GiromonMusicPlayerNarratorText.PreviousSong);

    private Task OnNextSongStarted() => SpeakGiromonTextAsync(GiromonMusicPlayerNarratorText.NextSong);

    private async Task OnShuffleModeChanged(ShuffleMode shuffleMode)
    {
        ShuffleEnabled = shuffleMode == ShuffleMode.Shuffle;

        string text = shuffleMode == ShuffleMode.Shuffle ? GiromonMusicPlayerNarratorText.ShuffleEnabled : GiromonMusicPlayerNarratorText.ShuffleDisabled;

        await SpeakGiromonTextAsync(text);
    }

    private async Task OnPlayModeChanged(PlayMode playMode)
    {
        string text = playMode switch
        {
            PlayMode.Paused => GiromonMusicPlayerNarratorText.Pause,
            PlayMode.Playing => GiromonMusicPlayerNarratorText.Play,
            _ => throw new ArgumentOutOfRangeException(nameof(playMode), playMode, null)
        };

        await SpeakGiromonTextAsync(text);
    }

    private async Task OnRepeatModeChanged(RepeatMode repeatMode)
    {
        RepeatSingleSongEnabled = repeatMode == RepeatMode.Single;

        string text = repeatMode == RepeatMode.Single ? GiromonMusicPlayerNarratorText.RepeatCurrent : GiromonMusicPlayerNarratorText.PlayAllSongs;

        await SpeakGiromonTextAsync(text);
    }

    private async Task OnMuteModeChanged(MuteMode muteMode)
    {
        if ((MuteEnabled && muteMode == MuteMode.Mute) || (!MuteEnabled && muteMode == MuteMode.Unmuted))
        {
            return;
        }

        MuteEnabled = muteMode == MuteMode.Mute;

        string text = muteMode == MuteMode.Mute ? GiromonMusicPlayerNarratorText.Mute : GiromonMusicPlayerNarratorText.Unmute;

        await SpeakGiromonTextAsync(text);
    }

    private void OnVolumeChanged(float newVolume)
    {
        _volume = (int)(newVolume * 100f);
        OnPropertyChanged(nameof(Volume));
    }

    public void Dispose() => _compositeDisposable.Dispose();
}