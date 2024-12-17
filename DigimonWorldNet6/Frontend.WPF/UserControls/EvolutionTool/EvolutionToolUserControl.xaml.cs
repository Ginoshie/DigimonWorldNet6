namespace DigimonWorld.Frontend.WPF.UserControls.EvolutionTool;

public partial class EvolutionToolUserControl
{
    public EvolutionToolUserControl()
    {
        InitializeComponent();
        
        DataContext = new EvolutionToolViewModel();
    }
}