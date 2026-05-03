namespace DigimonWorld.Frontend.WPF.Windows.Dialogs;

public partial class JijimonYesNoDialog
{
    public JijimonYesNoDialog(string message)
    {
        InitializeComponent();
        DataContext = new JijimonYesNoDialogViewModel(message, this);
    }

    public bool Result { get; set; }
}
