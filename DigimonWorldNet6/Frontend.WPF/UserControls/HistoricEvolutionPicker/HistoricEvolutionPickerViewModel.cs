using System.Collections.Generic;
using System.Windows.Input;
using DigimonWorld.Evolution.Calculator.Core;
using DigimonWorld.Frontend.WPF.Models;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using Generics.Enums;

namespace DigimonWorld.Frontend.WPF.UserControls.HistoricEvolutionPicker;

public class HistoricEvolutionPickerViewModel : BaseViewModel
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
}