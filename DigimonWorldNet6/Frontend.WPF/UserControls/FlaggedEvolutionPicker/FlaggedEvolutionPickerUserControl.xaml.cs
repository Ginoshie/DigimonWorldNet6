using System.Windows.Controls;

namespace DigimonWorld.Frontend.WPF.UserControls.FlaggedEvolutionPicker;

public partial class FlaggedEvolutionPickerUserControl : UserControl
{
    public FlaggedEvolutionPickerUserControl()
    {
        InitializeComponent();
        
        DataContext = new FlaggedEvolutionPickerViewModel();
    }
}