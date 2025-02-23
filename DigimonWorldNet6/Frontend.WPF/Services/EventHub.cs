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
    
    private static readonly BehaviorSubject<ShuffleMode> ShuffleModeSubject = new(ShuffleMode.Shuffle);
    
    private static readonly Subject<Unit> PreviousSongStartedSubject = new();
    
    private static readonly Subject<PlayMode> PlayModeSubject = new();
    
    private static readonly Subject<Unit> NextSongStartedSubject = new();
    
    private static readonly BehaviorSubject<RepeatMode> RepeatModeSubject = new(RepeatMode.All);
    
    private static readonly BehaviorSubject<MuteMode> MuteModeSubject = new(MuteMode.Unmuted);
    
    public static IObservable<Unit> MusicPlayerOpenedObservable { get; } = MusicPlayerOpenedSubject.AsObservable();
    
    public static IObservable<Unit> MusicPlayerClosedObservable { get; } =  MusicPlayerClosedSubject.AsObservable();
    
    public static IObservable<Unit> LeomonsThemeStartedObservable { get; } = LeomonsThemeStartedSubject.AsObservable();
    
    public static IObservable<PlayMode> PlayModeObservable { get; } = PlayModeSubject.AsObservable();
    
    public static void SignalMusicPlayerOpened() => MusicPlayerOpenedSubject.OnNext(Unit.Default);
    
    public static void SignalMusicPlayerClosed() => MusicPlayerClosedSubject.OnNext(Unit.Default);
    
    public static void SignalLeomonsThemeStarted() => LeomonsThemeStartedSubject.OnNext(Unit.Default);
    
    public static void SignalShuffleMode(ShuffleMode shuffleMode) => ShuffleModeSubject.OnNext(shuffleMode);
    
    public static void SignalPreviousSongStarted() => PreviousSongStartedSubject.OnNext(Unit.Default);
    
    public static void SignalNextSongStarted() => NextSongStartedSubject.OnNext(Unit.Default);
    
    public static void SignalPlayModeChanged(PlayMode playMode) => PlayModeSubject.OnNext(playMode);
    
    public static void SignalRepeatMode(RepeatMode repeatMode) => RepeatModeSubject.OnNext(repeatMode);
    
    public static void SignalMuteMode(MuteMode muteMode) => MuteModeSubject.OnNext(muteMode);
}