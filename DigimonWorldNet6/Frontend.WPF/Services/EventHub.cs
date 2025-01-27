using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace DigimonWorld.Frontend.WPF.Services;

public static class EventHub
{
    private static readonly Subject<Unit> LeftPaneOpenedSubject = new();
    
    private static readonly Subject<Unit> LeftPaneClosedSubject = new();
    
    private static readonly Subject<Unit> LeomonsThemeStartedSubject = new();
    
    private static readonly Subject<Unit> ShuffleEnabledSubject = new();
    
    private static readonly Subject<Unit> ShuffleDisabledSubject = new();
    
    private static readonly Subject<Unit> PreviousSongStartedSubject = new();
    
    private static readonly Subject<Unit> CurrentSongStartedSubject = new();
    
    private static readonly Subject<Unit> PauseSubject = new();
    
    private static readonly Subject<Unit> NextSongStartedSubject = new();
    
    private static readonly Subject<Unit> RepeatCurrentSongSubject = new();
    
    private static readonly Subject<Unit> PlayAllSongsSubject = new();
    
    private static readonly Subject<Unit> MuteSubject = new();
    
    private static readonly Subject<Unit> UnmuteSubject = new();
    
    public static IObservable<Unit> LeftPaneOpened { get; } = LeftPaneOpenedSubject.AsObservable();
    
    public static IObservable<Unit> LeftPaneClosed { get; } =  LeftPaneClosedSubject.AsObservable();
    
    public static IObservable<Unit> LeomonsThemeStarted { get; } = LeomonsThemeStartedSubject.AsObservable();
    
    public static IObservable<Unit> ShuffleEnabled { get; } = ShuffleEnabledSubject.AsObservable();
    
    public static IObservable<Unit> ShuffleDisabled { get; } = ShuffleDisabledSubject.AsObservable();
    
    public static IObservable<Unit> PreviousSongStarted { get; } = PreviousSongStartedSubject.AsObservable();
    
    public static IObservable<Unit> CurrentSongStarted { get; } = CurrentSongStartedSubject.AsObservable();
    
    public static IObservable<Unit> NextSongStarted { get; } = NextSongStartedSubject.AsObservable();
    
    public static IObservable<Unit> MusicPaused { get; } = PauseSubject.AsObservable();
    
    public static IObservable<Unit> RepeatCurrentSongModeEnabled { get; } = RepeatCurrentSongSubject.AsObservable();
    
    public static IObservable<Unit> PlayAllSongsModeEnabled { get; } = PlayAllSongsSubject.AsObservable();
    
    public static IObservable<Unit> MuteMusic { get; } = MuteSubject.AsObservable();
    
    public static IObservable<Unit> UnmuteMusic { get; } = UnmuteSubject.AsObservable();
    
    public static void SignalLeftPaneOpened() => LeftPaneOpenedSubject.OnNext(Unit.Default);
    
    public static void SignalLeftPaneClosed() => LeftPaneClosedSubject.OnNext(Unit.Default);
    
    public static void SignalLeomonsThemeStarted() => LeomonsThemeStartedSubject.OnNext(Unit.Default);
    
    public static void SignalShuffleEnabled() => ShuffleEnabledSubject.OnNext(Unit.Default);
    
    public static void SignalShuffleDisabled() => ShuffleDisabledSubject.OnNext(Unit.Default);
    
    public static void SignalPreviousSongStarted() => PreviousSongStartedSubject.OnNext(Unit.Default);
    
    public static void SignalCurrentSongStarted() => CurrentSongStartedSubject.OnNext(Unit.Default);
    
    public static void SignalNextSongStarted() => NextSongStartedSubject.OnNext(Unit.Default);
    
    public static void SignalPause() => PauseSubject.OnNext(Unit.Default);
    
    public static void SignalRepeatCurrentSongMode() => RepeatCurrentSongSubject.OnNext(Unit.Default);
    
    public static void SignalPlayAllSongsMode() => PlayAllSongsSubject.OnNext(Unit.Default);
    
    public static void SignalMute() => MuteSubject.OnNext(Unit.Default);
    
    public static void SignalUnmute() => UnmuteSubject.OnNext(Unit.Default);
}