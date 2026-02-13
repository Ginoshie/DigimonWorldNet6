using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Shared.Services.Events;

public static class EmulatorLinkEventHub
{
    private static readonly Subject<Unit> _emulatorConnectedSubject = new();
    private static readonly Subject<Unit> _emulatorDisconnectedSubject = new();

    public static IObservable<Unit> EmulatorConnectedObservable => _emulatorConnectedSubject.AsObservable();
    public static IObservable<Unit> EmulatorDisconnectedObservable => _emulatorDisconnectedSubject.AsObservable();

    public static void SignalEmulatorConnected() => _emulatorConnectedSubject.OnNext(Unit.Default);
    public static void SignalEmulatorDisconnected() => _emulatorDisconnectedSubject.OnNext(Unit.Default);
}