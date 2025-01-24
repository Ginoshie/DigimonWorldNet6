using System.Windows.Input;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.GeneralConfig;

public class GeneralConfigWindowViewModel : BaseViewModel
{
    private readonly GeneralConfigWindow _window;

    public GeneralConfigWindowViewModel(GeneralConfigWindow window)
    {
        _window = window;
        
        SaveCommand = new CommandHandler(Save);
        
        CancelCommand  = new CommandHandler(Cancel);
    }
    
    public ICommand SaveCommand { get; private set; }
    
    public ICommand CancelCommand { get; private set; }

    private void Save()
    {
        _window.Close();
    }

    private void Cancel()
    {
        _window.Close();
    }
}