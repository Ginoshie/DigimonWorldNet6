using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Shared.Enums;

namespace Shared.Services.Events;

public static class MusicPlayerEventHub
{
    private static readonly Subject<Unit> _musicPlayerOpenedSubject = new();
    private static readonly Subject<Unit> _musicPlayerClosedSubject = new();
    private static readonly Subject<Unit> _leomonsThemeStartedSubject = new();
    private static readonly Subject<ShuffleMode> _shuffleModeChangedSubject = new();
    private static readonly Subject<Unit> _previousSongStartedSubject = new();
    private static readonly Subject<PlayMode> _playModeChangedSubject = new();
    private static readonly Subject<Unit> _nextSongStartedSubject = new();
    private static readonly Subject<RepeatMode> _repeatModeChangedSubject = new();
    private static readonly Subject<MuteMode> _muteModeChangedSubject = new();
    private static readonly Subject<float> _volumeChangedSubject = new();

    public static IObservable<Unit> MusicPlayerOpenedObservable { get; } = _musicPlayerOpenedSubject.AsObservable();
    public static IObservable<Unit> MusicPlayerClosedObservable { get; } = _musicPlayerClosedSubject.AsObservable();
    public static IObservable<Unit> LeomonsThemeStartedObservable { get; } = _leomonsThemeStartedSubject.AsObservable();
    public static IObservable<Unit> PreviousSongStartedObservable { get; } = _previousSongStartedSubject.AsObservable();
    public static IObservable<PlayMode> PlayModeChangedObservable { get; } = _playModeChangedSubject.AsObservable();
    public static IObservable<Unit> NextSongStartedObservable { get; } = _nextSongStartedSubject.AsObservable();
    public static IObservable<ShuffleMode> ShuffleModeChangedObservable { get; } = _shuffleModeChangedSubject.AsObservable();
    public static IObservable<RepeatMode> RepeatModeChangedObservable { get; } = _repeatModeChangedSubject.AsObservable();
    public static IObservable<MuteMode> MuteModeChangedObservable { get; } = _muteModeChangedSubject.AsObservable();
    public static IObservable<float> VolumeChangedObservable { get; } = _volumeChangedSubject.AsObservable();

    public static void SignalMusicPlayerOpened() => _musicPlayerOpenedSubject.OnNext(Unit.Default);
    public static void SignalMusicPlayerClosed() => _musicPlayerClosedSubject.OnNext(Unit.Default);
    public static void SignalLeomonsThemeStarted() => _leomonsThemeStartedSubject.OnNext(Unit.Default);
    public static void SignalShuffleMode(ShuffleMode shuffleMode) => _shuffleModeChangedSubject.OnNext(shuffleMode);
    public static void SignalPreviousSongStarted() => _previousSongStartedSubject.OnNext(Unit.Default);
    public static void SignalNextSongStarted() => _nextSongStartedSubject.OnNext(Unit.Default);
    public static void SignalPlayModeChanged(PlayMode playMode) => _playModeChangedSubject.OnNext(playMode);
    public static void SignalRepeatMode(RepeatMode repeatMode) => _repeatModeChangedSubject.OnNext(repeatMode);
    public static void SignalMuteMode(MuteMode muteMode) => _muteModeChangedSubject.OnNext(muteMode);
    public static void SignalVolumeChanged(float newVolume) => _volumeChangedSubject.OnNext(newVolume);
}