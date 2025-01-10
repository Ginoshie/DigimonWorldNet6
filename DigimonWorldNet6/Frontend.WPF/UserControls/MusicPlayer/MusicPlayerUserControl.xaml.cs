using System.Windows.Controls;

namespace DigimonWorld.Frontend.WPF.UserControls.MusicPlayer;

public partial class MusicPlayerUserControl : UserControl
{
    public MusicPlayerUserControl()
    {
        InitializeComponent();

        DataContext = new MusicPlayerViewModel();
    }
}