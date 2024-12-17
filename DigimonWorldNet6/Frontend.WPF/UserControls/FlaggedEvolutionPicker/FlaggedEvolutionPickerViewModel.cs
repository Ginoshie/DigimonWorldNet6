using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Generics.Enums;

namespace DigimonWorld.Frontend.WPF.UserControls.FlaggedEvolutionPicker;

public class FlaggedEvolutionPickerViewModel : INotifyPropertyChanged
{
    private bool _componentIsOpen;

    public bool ComponentIsOpen
    {
        get => _componentIsOpen;
        set
        {
            if (_componentIsOpen == value) return;
            
            _componentIsOpen = value;
            OnPropertyChanged();
        }
    }

    public IEnumerable<DigimonType> FlaggedDigimonTypes { get; set; } = [];
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}