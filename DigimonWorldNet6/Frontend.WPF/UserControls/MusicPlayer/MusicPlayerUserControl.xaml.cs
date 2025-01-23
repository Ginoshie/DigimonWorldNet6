namespace DigimonWorld.Frontend.WPF.UserControls.MusicPlayer;

public partial class MusicPlayerUserControl
{
    public MusicPlayerUserControl()
    {
        InitializeComponent();

        DataContext = new MusicPlayerViewModel();
    }
}