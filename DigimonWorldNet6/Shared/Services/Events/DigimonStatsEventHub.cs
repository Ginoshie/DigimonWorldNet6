using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Shared.Services.Events;

public static class DigimonStatsEventHub
{
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
}