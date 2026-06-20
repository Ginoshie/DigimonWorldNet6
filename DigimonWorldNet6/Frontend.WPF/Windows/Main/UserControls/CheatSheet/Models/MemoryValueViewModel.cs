using System;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.CheatSheet.Models;

public sealed class MemoryValueViewModel(string label, Func<bool> getter, Action<bool> setter) : BaseViewModel, IRefreshable
{
    private bool _lastNotifiedValue = getter();

    public string Label { get; } = label;

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
        bool current = getter();
        if (current == _lastNotifiedValue)
        {
            return;
        }

        _lastNotifiedValue = current;
        OnPropertyChanged(nameof(Value));
    }
}
