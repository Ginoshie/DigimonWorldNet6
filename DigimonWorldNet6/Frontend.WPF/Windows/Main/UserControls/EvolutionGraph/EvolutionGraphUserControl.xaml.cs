namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvolutionGraph;

public partial class EvolutionGraphUserControl
{
    public EvolutionGraphUserControl()
    {
        DataContext = new EvolutionGraphViewModel();
        InitializeComponent();
    }
}
