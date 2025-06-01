namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.HistoricEvolutionPicker;

public partial class HistoricEvolutionPickerUserControl
{
    public HistoricEvolutionPickerUserControl()
    {
        InitializeComponent();
        
        DataContext = new HistoricEvolutionPickerViewModel();
    }
}