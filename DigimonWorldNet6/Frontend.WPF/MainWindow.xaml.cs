using System.Windows;

namespace DigimonWorld.Frontend.WPF;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var dataContext = new EvolutionToolViewModel();
            
        DataContext = dataContext;
    }
}