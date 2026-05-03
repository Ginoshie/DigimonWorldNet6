using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Dialogs;

public class JijimonYesNoDialogViewModel : INotifyPropertyChanged
{
    private readonly JijimonYesNoDialog _dialog;

    public JijimonYesNoDialogViewModel(string message, JijimonYesNoDialog dialog)
    {
        Message = message;
        _dialog = dialog;
        YesCommand = new CommandHandler(OnYes);
    }

    public string Message
    {
        get;
        private set
        {
            if (field != value)
            {
                field = value;
                OnPropertyChanged();
            }
        }
    }

    public bool ButtonsVisible
    {
        get;
        private set
        {
            if (field != value)
            {
                field = value;
                OnPropertyChanged();
            }
        }
    } = true;

    public ICommand YesCommand { get; }

    private void OnYes()
    {
        _dialog.Result = true;
        _dialog.Close();
    }

    public void HideButtons()
    {
        ButtonsVisible = false;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
