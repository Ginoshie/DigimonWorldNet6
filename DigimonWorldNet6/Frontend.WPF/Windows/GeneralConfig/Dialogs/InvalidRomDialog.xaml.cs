namespace DigimonWorld.Frontend.WPF.Windows.GeneralConfig.Dialogs;

public partial class InvalidRomDialog
{
    public InvalidRomDialog()
    {
        InitializeComponent();
        DataContext = new InvalidRomDialogViewModel();
    }
}
