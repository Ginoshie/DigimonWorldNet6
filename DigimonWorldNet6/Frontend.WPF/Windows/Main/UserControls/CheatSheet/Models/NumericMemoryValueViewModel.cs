using System;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.CheatSheet.Models;

public sealed class NumericMemoryValueViewModel : BaseViewModel, IRefreshable, IEditableValue
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

        int current = _getter();
        if (current == _lastNotifiedValue)
        {
            return;
        }

        _lastNotifiedValue = current;
        OnPropertyChanged(nameof(Value));
    }
}
