using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using DigimonWorld.Frontend.WPF.Constants;

namespace DigimonWorld.Frontend.WPF.Services;

public static class EventHub
{
    private static readonly Subject<Unit> MusicPlayerOpenedSubject = new();
    
    private static readonly Subject<Unit> MusicPlayerClosedSubject = new();
    
    private static readonly Subject<Unit> LeomonsThemeStartedSubject = new();
    
    private static readonly Subject<ShuffleMode> ShuffleModeChangedSubject = new();
    
    private static readonly Subject<Unit> PreviousSongStartedSubject = new();
    
    private static readonly Subject<PlayMode> PlayModeChangedSubject = new();
    
    private static readonly Subject<Unit> NextSongStartedSubject = new();
    
    private static readonly Subject<RepeatMode> RepeatModeChangedSubject = new();
    
    private static readonly Subject<MuteMode> MuteModeChangedSubject = new();
    
    private static readonly Subject<float> VolumeChangedSubject = new();
    
    public static IObservable<Unit> MusicPlayerOpenedObservable { get; } = MusicPlayerOpenedSubject.AsObservable();
    
    public static IObservable<Unit> MusicPlayerClosedObservable { get; } =  MusicPlayerClosedSubject.AsObservable();
    
    public static IObservable<Unit> LeomonsThemeStartedObservable { get; } = LeomonsThemeStartedSubject.AsObservable();
    
    public static IObservable<Unit> PreviousSongStartedObservable { get; } = PreviousSongStartedSubject.AsObservable();
    
    public static IObservable<PlayMode> PlayModeChangedObservable { get; } = PlayModeChangedSubject.AsObservable();
    
    public static IObservable<Unit> NextSongStartedObservable { get; } = NextSongStartedSubject.AsObservable();
    
    public static IObservable<ShuffleMode> ShuffleModeChangedObservable { get; } = ShuffleModeChangedSubject.AsObservable();
    
    public static IObservable<RepeatMode> RepeatModeChangedObservable { get; } = RepeatModeChangedSubject.AsObservable();
    
    public static IObservable<MuteMode> MuteModeChangedObservable { get; } = MuteModeChangedSubject.AsObservable();
    
    public static IObservable<float> VolumeChangedObservable { get; } = VolumeChangedSubject.AsObservable();
    
    public static void SignalMusicPlayerOpened() => MusicPlayerOpenedSubject.OnNext(Unit.Default);
    
    public static void SignalMusicPlayerClosed() => MusicPlayerClosedSubject.OnNext(Unit.Default);
    
    public static void SignalLeomonsThemeStarted() => LeomonsThemeStartedSubject.OnNext(Unit.Default);
    
    public static void SignalShuffleMode(ShuffleMode shuffleMode) => ShuffleModeChangedSubject.OnNext(shuffleMode);
    
    public static void SignalPreviousSongStarted() => PreviousSongStartedSubject.OnNext(Unit.Default);
    
    public static void SignalNextSongStarted() => NextSongStartedSubject.OnNext(Unit.Default);
    
    public static void SignalPlayModeChanged(PlayMode playMode) => PlayModeChangedSubject.OnNext(playMode);
    
    public static void SignalRepeatMode(RepeatMode repeatMode) => RepeatModeChangedSubject.OnNext(repeatMode);
    
    public static void SignalMuteMode(MuteMode muteMode) => MuteModeChangedSubject.OnNext(muteMode);
    
    public static void SignalVolumeChanged(float newVolume) => VolumeChangedSubject.OnNext(newVolume);
}