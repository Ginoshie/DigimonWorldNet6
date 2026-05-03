namespace DigimonWorld.Frontend.WPF.Windows.Dialogs;

public partial class NanimonErrorDialog
{
    public NanimonErrorDialog(string narratorName, string message, string? linkUrl = null)
    {
        InitializeComponent();
        DataContext = new NanimonErrorDialogViewModel(narratorName, message, linkUrl);
    }
}

