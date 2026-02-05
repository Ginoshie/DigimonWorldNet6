using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Shared.Enums;

namespace Shared.Services;

public static class EventHub
{
    #region Musicplayer

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

    #endregion

    #region EvolutionCalculator

    // Subjects
    private static readonly Subject<Unit> _setAllEmulatorProfileStatsSubject = new();
    private static readonly Subject<Unit> _setEmulatorDigimonTypeSubject = new();
    private static readonly Subject<Unit> _setEmulatorWeightSubject = new();
    
    private static readonly Subject<Unit> _setAllEmulatorParameterStatsSubject = new();
    private static readonly Subject<Unit> _setEmulatorHPSubject = new();
    private static readonly Subject<Unit> _setEmulatorMPSubject = new();
    private static readonly Subject<Unit> _setEmulatorOffSubject = new();
    private static readonly Subject<Unit> _setEmulatorDefSubject = new();
    private static readonly Subject<Unit> _setEmulatorSpdSubject = new();
    private static readonly Subject<Unit> _setEmulatorBrnSubject = new();
    
    private static readonly Subject<Unit> _setAllEmulatorConditionStatsSubject = new();
    private static readonly Subject<Unit> _setEmulatorHappinessSubject = new();
    private static readonly Subject<Unit> _setEmulatorDisciplineSubject = new();
    private static readonly Subject<Unit> _setEmulatorCareMistakesSubject = new();
    private static readonly Subject<Unit> _setEmulatorTechniqueCountSubject = new();
    private static readonly Subject<Unit> _setEmulatorBattlesCountSubject = new();

    // Observables
    public static IObservable<Unit> SetAllEmulatorProfileStatsObservable => _setAllEmulatorProfileStatsSubject.AsObservable();
    public static IObservable<Unit> SetEmulatorDigimonTypeObservable => _setEmulatorDigimonTypeSubject.AsObservable();
    public static IObservable<Unit> SetEmulatorWeightObservable => _setEmulatorWeightSubject.AsObservable();
    
    public static IObservable<Unit> SetAllEmulatorParameterStatsObservable => _setAllEmulatorParameterStatsSubject.AsObservable();
    public static IObservable<Unit> SetEmulatorHPObservable => _setEmulatorHPSubject.AsObservable();
    public static IObservable<Unit> SetEmulatorMPObservable => _setEmulatorMPSubject.AsObservable();
    public static IObservable<Unit> SetEmulatorOffObservable => _setEmulatorOffSubject.AsObservable();
    public static IObservable<Unit> SetEmulatorDefObservable => _setEmulatorDefSubject.AsObservable();
    public static IObservable<Unit> SetEmulatorSpdObservable => _setEmulatorSpdSubject.AsObservable();
    public static IObservable<Unit> SetEmulatorBrnObservable => _setEmulatorBrnSubject.AsObservable();
    
    public static IObservable<Unit> SetAllEmulatorConditionStatsObservable => _setAllEmulatorConditionStatsSubject.AsObservable();
    public static IObservable<Unit> SetEmulatorHappinessObservable => _setEmulatorHappinessSubject.AsObservable();
    public static IObservable<Unit> SetEmulatorDisciplineObservable => _setEmulatorDisciplineSubject.AsObservable();
    public static IObservable<Unit> SetEmulatorCareMistakesObservable => _setEmulatorCareMistakesSubject.AsObservable();
    public static IObservable<Unit> SetEmulatorTechniqueCountObservable => _setEmulatorTechniqueCountSubject.AsObservable();
    public static IObservable<Unit> SetEmulatorBattlesCountObservable => _setEmulatorBattlesCountSubject.AsObservable();

    // OnNexts
    public static void SignalSetAllEmulatorProfileStats() => _setAllEmulatorProfileStatsSubject.OnNext(Unit.Default);
    public static void SignalSetEmulatorDigimonType() => _setEmulatorDigimonTypeSubject.OnNext(Unit.Default);
    public static void SignalSetEmulatorWeight() => _setEmulatorWeightSubject.OnNext(Unit.Default);
    
    public static void SignalSetAllEmulatorParameterStats() => _setAllEmulatorParameterStatsSubject.OnNext(Unit.Default);
    public static void SignalSetEmulatorHP() => _setEmulatorHPSubject.OnNext(Unit.Default);
    public static void SignalSetEmulatorMP() => _setEmulatorMPSubject.OnNext(Unit.Default);
    public static void SignalSetEmulatorOff() => _setEmulatorOffSubject.OnNext(Unit.Default);
    public static void SignalSetEmulatorDef() => _setEmulatorDefSubject.OnNext(Unit.Default);
    public static void SignalSetEmulatorSpd() => _setEmulatorSpdSubject.OnNext(Unit.Default);
    public static void SignalSetEmulatorBrn() => _setEmulatorBrnSubject.OnNext(Unit.Default);
    
    public static void SignalSetAllEmulatorConditionStats() => _setAllEmulatorConditionStatsSubject.OnNext(Unit.Default);
    public static void SignalSetEmulatorHappiness() => _setEmulatorHappinessSubject.OnNext(Unit.Default);
    public static void SignalSetEmulatorDiscipline() => _setEmulatorDisciplineSubject.OnNext(Unit.Default);
    public static void SignalSetEmulatorCareMistakes() => _setEmulatorCareMistakesSubject.OnNext(Unit.Default);
    public static void SignalSetEmulatorTechniqueCount() => _setEmulatorTechniqueCountSubject.OnNext(Unit.Default);
    public static void SignalSetEmulatorBattlesCount() => _setEmulatorBattlesCountSubject.OnNext(Unit.Default);

    #endregion

    #region EmulatorLink

    private static readonly Subject<Unit> _emulatorConnected = new();
    private static readonly Subject<Unit> _emulatorDisconnected = new();

    public static IObservable<Unit> EmulatorConnectedObservable => _emulatorConnected.AsObservable();
    public static IObservable<Unit> EmulatorDisconnectedObservable => _emulatorDisconnected.AsObservable();

    public static void SignalEmulatorConnected() => _emulatorConnected.OnNext(Unit.Default);
    public static void SignalEmulatorDisconnected() => _emulatorDisconnected.OnNext(Unit.Default);

    #endregion
}