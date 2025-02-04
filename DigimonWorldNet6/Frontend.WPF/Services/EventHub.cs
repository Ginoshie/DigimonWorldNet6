using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using DigimonWorld.Frontend.WPF.Constants;

namespace DigimonWorld.Frontend.WPF.Services;

public static class EventHub
{
    private static readonly Subject<Unit> LeftPaneOpenedSubject = new();
    
    private static readonly Subject<Unit> LeftPaneClosedSubject = new();
    
    private static readonly Subject<Unit> LeomonsThemeStartedSubject = new();
    
    private static readonly BehaviorSubject<ShuffleMode> ShuffleModeSubject = new(ShuffleMode.Shuffle);
    
    private static readonly Subject<Unit> PreviousSongStartedSubject = new();
    
    private static readonly Subject<PlayMode> PlayModeSubject = new();
    
    private static readonly Subject<Unit> NextSongStartedSubject = new();
    
    private static readonly BehaviorSubject<RepeatMode> RepeatModeSubject = new(RepeatMode.All);
    
    private static readonly BehaviorSubject<MuteMode> MuteModeSubject = new(MuteMode.Unmuted);
    
    public static IObservable<Unit> LeftPaneOpenedObservable { get; } = LeftPaneOpenedSubject.AsObservable();
    
    public static IObservable<Unit> LeftPaneClosedObservable { get; } =  LeftPaneClosedSubject.AsObservable();
    
    public static IObservable<Unit> LeomonsThemeStartedObservable { get; } = LeomonsThemeStartedSubject.AsObservable();
    
    public static IObservable<ShuffleMode> ShuffleModeObservable { get; } = ShuffleModeSubject.AsObservable();
    
    public static IObservable<Unit> PreviousSongStartedObservable { get; } = PreviousSongStartedSubject.AsObservable();
    
    public static IObservable<Unit> NextSongStartedObservable { get; } = NextSongStartedSubject.AsObservable();
    
    public static IObservable<PlayMode> PlayModeObservable { get; } = PlayModeSubject.AsObservable();
    
    public static IObservable<RepeatMode> RepeatModeObservable { get; } = RepeatModeSubject.AsObservable();
    
    public static IObservable<MuteMode> MuteModeObservable { get; } = MuteModeSubject.AsObservable();
    
    public static void SignalLeftPaneOpened() => LeftPaneOpenedSubject.OnNext(Unit.Default);
    
    public static void SignalLeftPaneClosed() => LeftPaneClosedSubject.OnNext(Unit.Default);
    
    public static void SignalLeomonsThemeStarted() => LeomonsThemeStartedSubject.OnNext(Unit.Default);
    
    public static void SignalShuffleMode(ShuffleMode shuffleMode) => ShuffleModeSubject.OnNext(shuffleMode);
    
    public static void SignalPreviousSongStarted() => PreviousSongStartedSubject.OnNext(Unit.Default);
    
    public static void SignalNextSongStarted() => NextSongStartedSubject.OnNext(Unit.Default);
    
    public static void SignalPlayMode(PlayMode playMode) => PlayModeSubject.OnNext(playMode);
    
    public static void SignalRepeatMode(RepeatMode repeatMode) => RepeatModeSubject.OnNext(repeatMode);
    
    public static void SignalMuteMode(MuteMode muteMode) => MuteModeSubject.OnNext(muteMode);
}