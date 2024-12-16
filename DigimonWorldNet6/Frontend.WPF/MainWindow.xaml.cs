using DigimonWorld.Frontend.WPF.UserControls.EvolutionTool;

namespace DigimonWorld.Frontend.WPF;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();

        EvolutionToolViewModel dataContext = new(this);
            
        DataContext = dataContext;
    }
}