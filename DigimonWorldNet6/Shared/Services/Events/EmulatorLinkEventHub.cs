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

    public static IObservable<Unit> EmulatorConnectedObservable => _emulatorConnectedSubject.AsObservable();
    public static IObservable<Unit> EmulatorDisconnectedObservable => _emulatorDisconnectedSubject.AsObservable();
    public static IObservable<EmulatorLinkSyncMode> EmulatorLinkSyncModeChangedObservable => _emulatorSyncModeChangedSubject.AsObservable();
    public static IObservable<string> EmulatorProcessNameChangedObservable => _emulatorProcessNameChangedSubject.AsObservable();

    public static void SignalEmulatorConnected() => _emulatorConnectedSubject.OnNext(Unit.Default);
    public static void SignalEmulatorDisconnected() => _emulatorDisconnectedSubject.OnNext(Unit.Default);
    public static void SignalEmulatorLinkSyncModeChanged(EmulatorLinkSyncMode mode) => _emulatorSyncModeChangedSubject.OnNext(mode);
    public static void SignalEmulatorProcessNameChanged(string processName) => _emulatorProcessNameChangedSubject.OnNext(processName);
}