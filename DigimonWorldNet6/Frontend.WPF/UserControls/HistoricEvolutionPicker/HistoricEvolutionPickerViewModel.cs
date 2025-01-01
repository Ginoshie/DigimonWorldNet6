using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DigimonWorld.Evolution.Calculator.Core;
using DigimonWorld.Frontend.WPF.Models;
using Generics.Enums;

namespace DigimonWorld.Frontend.WPF.UserControls.HistoricEvolutionPicker;

public class HistoricEvolutionPickerViewModel : INotifyPropertyChanged
{
    public HistoricEvolutionPickerViewModel()
    {
        HistoricEvolutionClickedCommand = new RelayCommand(UpdateHistoricEvolution);
    }
    
    public ICommand HistoricEvolutionClickedCommand { get; }
    
    public IList<DigimonType> HistoricEvolutions { get; } = Session.HistoricEvolutions;
    
    private void UpdateHistoricEvolution(object parameter)
    {
        if (parameter is not DigimonIcon digimonIcon) return;
        
        if (!HistoricEvolutions.Remove(digimonIcon.DigimonType))
        {
            HistoricEvolutions.Add(digimonIcon.DigimonType);
        }
        
        OnPropertyChanged(nameof(HistoricEvolutions));
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}