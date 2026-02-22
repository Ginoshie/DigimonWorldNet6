using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Shared.Enums;

namespace Shared.Services.Events;

public static class EmulatorLinkEventHub
{
    private static readonly Subject<Unit> _emulatorConnectedSubject = new();
    private static readonly Subject<Unit> _emulatorDisconnectedSubject = new();
    private static readonly Subject<EmulatorLinkSyncMode> _emulatorSyncModeChangedSubject = new();
    private static readonly Subject<string> _emulatorProcessNameChangedSubject = new();
    private static readonly Subject<Unit> _digimonConditionStatsSynchronizedSubject = new();
    private static readonly Subject<Unit> _digimonParameterStatsSynchronizedSubject = new();
    private static readonly Subject<Unit> _digimonProfileStatsSynchronizedSubject = new();
    private static readonly Subject<Unit> _digimonCareStatsSynchronizedSubject = new();
    private static readonly Subject<Unit> _digimonTechniqueStatsSynchronizedSubject = new();
    private static readonly Subject<Unit> _historicEvolutionsSynchronizedSubject = new();

    public static IObservable<Unit> EmulatorConnectedObservable => _emulatorConnectedSubject.AsObservable();
    public static IObservable<Unit> EmulatorDisconnectedObservable => _emulatorDisconnectedSubject.AsObservable();
    public static IObservable<EmulatorLinkSyncMode> EmulatorLinkSyncModeChangedObservable => _emulatorSyncModeChangedSubject.AsObservable();
    public static IObservable<string> EmulatorProcessNameChangedObservable => _emulatorProcessNameChangedSubject.AsObservable();
    public static IObservable<Unit> DigimonConditionStatsSynchronizedObservable => _digimonConditionStatsSynchronizedSubject.AsObservable();
    public static IObservable<Unit> DigimonParameterStatsSynchronizedObservable => _digimonParameterStatsSynchronizedSubject.AsObservable();
    public static IObservable<Unit> DigimonProfileStatsSynchronizedObservable => _digimonProfileStatsSynchronizedSubject.AsObservable();
    public static IObservable<Unit> DigimonCareStatsSynchronizedObservable => _digimonCareStatsSynchronizedSubject.AsObservable();
    public static IObservable<Unit> DigimonTechniqueStatsSynchronizedObservable => _digimonTechniqueStatsSynchronizedSubject.AsObservable();
    public static IObservable<Unit> HistoricEvolutionsSynchronizedObservable => _historicEvolutionsSynchronizedSubject.AsObservable();

    public static void SignalEmulatorConnected() => _emulatorConnectedSubject.OnNext(Unit.Default);
    public static void SignalEmulatorDisconnected() => _emulatorDisconnectedSubject.OnNext(Unit.Default);
    public static void SignalEmulatorLinkSyncModeChanged(EmulatorLinkSyncMode mode) => _emulatorSyncModeChangedSubject.OnNext(mode);
    public static void SignalEmulatorProcessNameChanged(string processName) => _emulatorProcessNameChangedSubject.OnNext(processName);
    public static void SignalDigimonConditionStatsSynchronized() => _digimonConditionStatsSynchronizedSubject.OnNext(Unit.Default);
    public static void SignalDigimonParameterStatsSynchronized() => _digimonParameterStatsSynchronizedSubject.OnNext(Unit.Default);
    public static void SignalDigimonProfileStatsSynchronized() => _digimonProfileStatsSynchronizedSubject.OnNext(Unit.Default);
    public static void SignalDigimonCareStatsSynchronized() => _digimonCareStatsSynchronizedSubject.OnNext(Unit.Default);
    public static void SignalDigimonTechniqueStatsSynchronized() => _digimonTechniqueStatsSynchronizedSubject.OnNext(Unit.Default);
    public static void SignalHistoricEvolutionsSynchronized() => _historicEvolutionsSynchronizedSubject.OnNext(Unit.Default);
}