namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.TamerVision;

public partial class TamerVisionUserControl
{
    public TamerVisionUserControl()
    {
        InitializeComponent();

        DataContext = new TamerVisionViewModel();
    }
}