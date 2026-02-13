using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Shared.Services.Events;

public static class EmulatorLinkEventHub
{
    private static readonly Subject<Unit> _emulatorConnected = new();
    private static readonly Subject<Unit> _emulatorDisconnected = new();

    public static IObservable<Unit> EmulatorConnectedObservable => _emulatorConnected.AsObservable();
    public static IObservable<Unit> EmulatorDisconnectedObservable => _emulatorDisconnected.AsObservable();

    public static void SignalEmulatorConnected() => _emulatorConnected.OnNext(Unit.Default);
    public static void SignalEmulatorDisconnected() => _emulatorDisconnected.OnNext(Unit.Default);
}