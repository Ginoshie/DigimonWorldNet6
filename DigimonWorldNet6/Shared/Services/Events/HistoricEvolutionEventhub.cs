using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Shared.Services.Events;

public class HistoricEvolutionEventhub
{
    private static readonly Subject<Unit> _syncFreshStageHistoricEvolutions = new();
    private static readonly Subject<Unit> _syncInTrainingStageHistoricEvolutions = new();
    private static readonly Subject<Unit> _syncRookieStageHistoricEvolutions = new();
    private static readonly Subject<Unit> _syncChampionStageHistoricEvolutions = new();
    private static readonly Subject<Unit> _syncUltimateStageHistoricEvolutions = new();
    private static readonly Subject<Unit> _syncAllStagesHistoricEvolutions = new();

    public static IObservable<Unit> SyncFreshStageHistoricEvolutionsObservable => _syncFreshStageHistoricEvolutions.AsObservable();
    public static IObservable<Unit> SyncInTrainingStageHistoricEvolutionsObservable => _syncInTrainingStageHistoricEvolutions.AsObservable();
    public static IObservable<Unit> SyncRookieStageHistoricEvolutionsObservable => _syncRookieStageHistoricEvolutions.AsObservable();
    public static IObservable<Unit> SyncChampionStageHistoricEvolutionsObservable => _syncChampionStageHistoricEvolutions.AsObservable();
    public static IObservable<Unit> SyncUltimateStageHistoricEvolutionsObservable => _syncUltimateStageHistoricEvolutions.AsObservable();
    public static IObservable<Unit> SyncAllStagesHistoricEvolutionsObservable => _syncAllStagesHistoricEvolutions.AsObservable();

    public static void SignalSyncFreshStageHistoricEvolutions() => _syncFreshStageHistoricEvolutions.OnNext(Unit.Default);
    public static void SignalSyncInTrainingStageHistoricEvolutions() => _syncInTrainingStageHistoricEvolutions.OnNext(Unit.Default);
    public static void SignalSyncRookieStageHistoricEvolutions() => _syncRookieStageHistoricEvolutions.OnNext(Unit.Default);
    public static void SignalSyncChampionStageHistoricEvolutions() => _syncChampionStageHistoricEvolutions.OnNext(Unit.Default);
    public static void SignalSyncUltimateStageHistoricEvolutions() => _syncUltimateStageHistoricEvolutions.OnNext(Unit.Default);
    public static void SignalSyncAllStagesHistoricEvolutions() => _syncAllStagesHistoricEvolutions.OnNext(Unit.Default);
}