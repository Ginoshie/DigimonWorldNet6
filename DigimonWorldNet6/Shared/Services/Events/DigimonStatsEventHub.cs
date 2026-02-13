using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Shared.Services.Events;

public static class DigimonStatsEventHub
{
    private static readonly Subject<Unit> _syncAllEmulatorProfileStatsSubject = new();
    private static readonly Subject<Unit> _syncEmulatorDigimonTypeSubject = new();
    private static readonly Subject<Unit> _syncEmulatorWeightSubject = new();
    private static readonly Subject<Unit> _syncAllEmulatorParameterStatsSubject = new();
    private static readonly Subject<Unit> _syncEmulatorHPSubject = new();
    private static readonly Subject<Unit> _syncEmulatorMPSubject = new();
    private static readonly Subject<Unit> _syncEmulatorOffSubject = new();
    private static readonly Subject<Unit> _syncEmulatorDefSubject = new();
    private static readonly Subject<Unit> _syncEmulatorSpdSubject = new();
    private static readonly Subject<Unit> _syncEmulatorBrnSubject = new();
    private static readonly Subject<Unit> _syncAllEmulatorConditionStatsSubject = new();
    private static readonly Subject<Unit> _syncEmulatorHappinessSubject = new();
    private static readonly Subject<Unit> _syncEmulatorDisciplineSubject = new();
    private static readonly Subject<Unit> _syncEmulatorCareMistakesSubject = new();
    private static readonly Subject<Unit> _syncEmulatorTechniqueCountSubject = new();
    private static readonly Subject<Unit> _syncEmulatorBattlesCountSubject = new();

    public static IObservable<Unit> SyncAllEmulatorProfileStatsObservable => _syncAllEmulatorProfileStatsSubject.AsObservable();
    public static IObservable<Unit> SyncEmulatorDigimonTypeObservable => _syncEmulatorDigimonTypeSubject.AsObservable();
    public static IObservable<Unit> SyncEmulatorWeightObservable => _syncEmulatorWeightSubject.AsObservable();
    public static IObservable<Unit> SyncAllEmulatorParameterStatsObservable => _syncAllEmulatorParameterStatsSubject.AsObservable();
    public static IObservable<Unit> SyncEmulatorHPObservable => _syncEmulatorHPSubject.AsObservable();
    public static IObservable<Unit> SyncEmulatorMPObservable => _syncEmulatorMPSubject.AsObservable();
    public static IObservable<Unit> SyncEmulatorOffObservable => _syncEmulatorOffSubject.AsObservable();
    public static IObservable<Unit> SyncEmulatorDefObservable => _syncEmulatorDefSubject.AsObservable();
    public static IObservable<Unit> SyncEmulatorSpdObservable => _syncEmulatorSpdSubject.AsObservable();
    public static IObservable<Unit> SyncEmulatorBrnObservable => _syncEmulatorBrnSubject.AsObservable();
    public static IObservable<Unit> SyncAllEmulatorConditionStatsObservable => _syncAllEmulatorConditionStatsSubject.AsObservable();
    public static IObservable<Unit> SyncEmulatorHappinessObservable => _syncEmulatorHappinessSubject.AsObservable();
    public static IObservable<Unit> SyncEmulatorDisciplineObservable => _syncEmulatorDisciplineSubject.AsObservable();
    public static IObservable<Unit> SyncEmulatorCareMistakesObservable => _syncEmulatorCareMistakesSubject.AsObservable();
    public static IObservable<Unit> SyncEmulatorTechniqueCountObservable => _syncEmulatorTechniqueCountSubject.AsObservable();
    public static IObservable<Unit> SyncEmulatorBattlesCountObservable => _syncEmulatorBattlesCountSubject.AsObservable();

    public static void SignalSyncAllEmulatorProfileStats() => _syncAllEmulatorProfileStatsSubject.OnNext(Unit.Default);
    public static void SignalSyncEmulatorDigimonType() => _syncEmulatorDigimonTypeSubject.OnNext(Unit.Default);
    public static void SignalSyncEmulatorWeight() => _syncEmulatorWeightSubject.OnNext(Unit.Default);
    public static void SignalSyncAllEmulatorParameterStats() => _syncAllEmulatorParameterStatsSubject.OnNext(Unit.Default);
    public static void SignalSyncEmulatorHP() => _syncEmulatorHPSubject.OnNext(Unit.Default);
    public static void SignalSyncEmulatorMP() => _syncEmulatorMPSubject.OnNext(Unit.Default);
    public static void SignalSyncEmulatorOff() => _syncEmulatorOffSubject.OnNext(Unit.Default);
    public static void SignalSyncEmulatorDef() => _syncEmulatorDefSubject.OnNext(Unit.Default);
    public static void SignalSyncEmulatorSpd() => _syncEmulatorSpdSubject.OnNext(Unit.Default);
    public static void SignalSyncEmulatorBrn() => _syncEmulatorBrnSubject.OnNext(Unit.Default);
    public static void SignalSyncAllEmulatorConditionStats() => _syncAllEmulatorConditionStatsSubject.OnNext(Unit.Default);
    public static void SignalSyncEmulatorHappiness() => _syncEmulatorHappinessSubject.OnNext(Unit.Default);
    public static void SignalSyncEmulatorDiscipline() => _syncEmulatorDisciplineSubject.OnNext(Unit.Default);
    public static void SignalSyncEmulatorCareMistakes() => _syncEmulatorCareMistakesSubject.OnNext(Unit.Default);
    public static void SignalSyncEmulatorTechniqueCount() => _syncEmulatorTechniqueCountSubject.OnNext(Unit.Default);
    public static void SignalSyncEmulatorBattlesCount() => _syncEmulatorBattlesCountSubject.OnNext(Unit.Default);
}