using System.Windows.Input;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Dialogs;

public class JijimonYesNoDialogViewModel
{
    private readonly JijimonYesNoDialog _dialog;

    public JijimonYesNoDialogViewModel(string message, JijimonYesNoDialog dialog)
    {
        Message = message;
        _dialog = dialog;
        YesCommand = new CommandHandler(OnYes);
    }

    public string Message { get; }
    public ICommand YesCommand { get; }

    private void OnYes()
    {
        _dialog.Result = true;
        _dialog.Close();
    }
}
