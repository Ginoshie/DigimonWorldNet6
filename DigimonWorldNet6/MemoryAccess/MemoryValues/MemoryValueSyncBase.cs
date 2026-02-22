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
            EmulatorLinkEventHub.EmulatorConnectedObservable.Subscribe(OnEmulatorConnected)
        );
    }

    private void OnEmulatorConnected(bool isConnected)
    {
        if (isConnected)
        {
            _updateDataSubscription.Disposable = Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(_ => OnUpdateData());
        }
        else
        {
            _updateDataSubscription.Dispose();
        }
    }

    protected virtual void OnUpdateData()
    {
    }

    public void UpdateData()
    {
        OnUpdateData();
    }

    public void Dispose()
    {
        _updateDataSubscription.Dispose();
        _compositeDisposable.Dispose();
    }
}