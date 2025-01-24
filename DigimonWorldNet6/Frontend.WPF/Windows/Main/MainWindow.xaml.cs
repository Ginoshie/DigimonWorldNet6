namespace DigimonWorld.Frontend.WPF.Windows.Main;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new MainWindowViewModel(this);
    }
}