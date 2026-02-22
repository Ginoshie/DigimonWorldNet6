using System.Reactive.Disposables;
using System.Reactive.Linq;
using Shared.Services.Events;

namespace MemoryAccess.MemoryValues;

public abstract class MemoryValueSyncBase : IDisposable
{
    private readonly CompositeDisposable _compositeDisposable;
    private readonly SerialDisposable _updateDataSubscription = new();

    protected MemoryValueSyncBase()
    {
        _compositeDisposable = new CompositeDisposable(
            EmulatorLinkEventHub.EmulatorConnectedObservable.Subscribe(_ => OnEmulatorConnected()),
            EmulatorLinkEventHub.EmulatorDisconnectedObservable.Subscribe(_ => OnEmulatorDisconnected())
        );
    }

    private void OnEmulatorConnected() => _updateDataSubscription.Disposable = Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(_ => UpdateData());

    private void OnEmulatorDisconnected() => Dispose();

    protected virtual void UpdateData()
    {
    }

    public void Dispose()
    {
        _updateDataSubscription.Dispose();
        _compositeDisposable.Dispose();
    }
}