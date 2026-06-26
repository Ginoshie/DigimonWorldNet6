using System;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.CheatSheet.Models;

public sealed class MemoryValueViewModel(string label, Func<bool> getter, Action<bool> setter) : BaseViewModel, IRefreshable, ILockableValue
{
    private bool _lastNotifiedValue = getter();

    public string Label { get; } = label;

    public bool IsLocked
    {
        get;
        set => SetField(ref field, value);
    }

    public bool Value
    {
        get => getter();
        set
        {
            setter(value);
            _lastNotifiedValue = value;
            OnPropertyChanged();
        }
    }

    public void Refresh()
    {
        if (IsLocked)
        {
            return;
        }

        bool current = getter();
        if (current == _lastNotifiedValue)
        {
            return;
        }

        _lastNotifiedValue = current;
        OnPropertyChanged(nameof(Value));
    }

    public void PushLockedValueToMemory()
    {
        if (!IsLocked)
        {
            return;
        }

        setter(_lastNotifiedValue);
    }
}
