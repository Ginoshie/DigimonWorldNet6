namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EmulatorLink;

public partial class EmulatorLinkUserControl
{
    public EmulatorLinkUserControl()
    {
        InitializeComponent();
        
        DataContext = new EmulatorLinkViewModel();
    }
}