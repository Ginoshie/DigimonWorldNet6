namespace DigimonWorld.Frontend.WPF.Windows.MainWindow;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();

        MainWindowViewModel dataContext = new(this);
            
        DataContext = dataContext;
    }
}