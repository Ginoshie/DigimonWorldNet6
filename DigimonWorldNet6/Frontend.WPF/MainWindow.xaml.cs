namespace DigimonWorld.Frontend.WPF;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();

        EvolutionToolViewModel dataContext = new();
            
        DataContext = dataContext;
    }
}