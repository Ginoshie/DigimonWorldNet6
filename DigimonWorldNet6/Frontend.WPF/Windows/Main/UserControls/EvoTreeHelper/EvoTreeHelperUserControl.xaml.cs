namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvoTreeHelper;

public partial class EvoTreeHelperUserControl
{
    public EvoTreeHelperUserControl()
    {
        DataContext = new EvoTreeHelperViewModel();
        InitializeComponent();
    }
}
