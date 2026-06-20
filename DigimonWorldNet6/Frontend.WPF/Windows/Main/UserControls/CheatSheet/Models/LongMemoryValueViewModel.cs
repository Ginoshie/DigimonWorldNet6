using System;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.CheatSheet.Models;

public sealed class LongMemoryValueViewModel : BaseViewModel, IRefreshable, IEditableValue
{
    private readonly Func<long> _getter;
    private readonly Action<long> _setter;
    private long _lastNotifiedValue;

    public LongMemoryValueViewModel(string label, Func<long> getter, Action<long> setter)
    {
        Label = label;
        _getter = getter;
        _setter = setter;
        _lastNotifiedValue = getter();
    }

    public string Label { get; }

    public bool IsEditing { get; set; }

    public long Value
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

        long current = _getter();
        if (current == _lastNotifiedValue)
        {
            return;
        }

        _lastNotifiedValue = current;
        OnPropertyChanged(nameof(Value));
    }
}
