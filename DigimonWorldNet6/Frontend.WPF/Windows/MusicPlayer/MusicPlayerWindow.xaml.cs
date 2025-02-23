namespace DigimonWorld.Frontend.WPF.Windows.MusicPlayer;

public partial class MusicPlayerWindow
{
    public MusicPlayerWindow()
    {
        InitializeComponent();

        MusicPlayerViewModel viewModel = new(this);    
        
        DataContext = viewModel;
        
        Closed += (_, _) => viewModel.Dispose();
    }
}