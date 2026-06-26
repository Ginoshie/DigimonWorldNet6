using System;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.CheatSheet.Models;

public sealed class NumericMemoryValueViewModel : BaseViewModel, IRefreshable, IEditableValue, ILockableValue
{
    private readonly Func<int> _getter;
    private readonly Action<int> _setter;
    private int _lastNotifiedValue;

    public NumericMemoryValueViewModel(string label, Func<int> getter, Action<int> setter)
    {
        Label = label;
        _getter = getter;
        _setter = setter;
        _lastNotifiedValue = getter();
    }

    public string Label { get; }

    public bool IsEditing { get; set; }

    public bool IsLocked
    {
        get;
        set => SetField(ref field, value);
    }

    public int Value
    {
        get => _getter();
        set
        {
            _setter(value);
            _lastNotifiedValue = value;
            OnPropertyChanged();
        }
    }

    public void Refresh()
    {
        if (IsEditing)
        {
            return;
        }

        if (IsLocked)
        {
            return;
        }

        int current = _getter();
        if (current == _lastNotifiedValue)
        {
            return;
        }

        _lastNotifiedValue = current;
        OnPropertyChanged(nameof(Value));
    }

    public void PushLockedValueToMemory()
    {
        if (!IsLocked || IsEditing)
        {
            return;
        }

        _setter(_lastNotifiedValue);
    }
}
