using System.Windows.Input;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.Panes;

public class EmulatorLinkRightPaneViewModelComponent : BaseViewModel
{
    public EmulatorLinkRightPaneViewModelComponent()
    {
        ToggleRightPaneCommand = new CommandHandler(ToggleRightPane);
    }

    public ICommand ToggleRightPaneCommand { get; }

    public bool RightPaneIsOpen
    {
        get;
        private set => SetField(ref field, value);
    }

    private void ToggleRightPane() => RightPaneIsOpen = !RightPaneIsOpen;
}