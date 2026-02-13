using System;
using System.Windows.Controls;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvolutionCalculator;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.Panes;

public class NavigationLeftPaneViewModelComponent : BaseViewModel
{
    private readonly Action<UserControl> _setCurrentSelectedMainWindowContent;

    public NavigationLeftPaneViewModelComponent(Action<UserControl> setCurrentSelectedMainWindowContent)
    {
        _setCurrentSelectedMainWindowContent = setCurrentSelectedMainWindowContent;
        ToggleLeftPaneCommand = new CommandHandler(ToggleLeftPane);

        ShowEvolutionCalculatorCommand = new CommandHandler(ShowEvolutionCalculator);

        ShowDigiWikiCommand = new CommandHandler(ShowDigiWiki);
    }

    public ICommand ToggleLeftPaneCommand { get; }

    public ICommand ShowEvolutionCalculatorCommand { get; }

    public ICommand ShowDigiWikiCommand { get; }

    private void ShowEvolutionCalculator() => _setCurrentSelectedMainWindowContent(new EvolutionCalculatorUserControl());

    private void ShowDigiWiki() => _setCurrentSelectedMainWindowContent(new DigiWikiUserControl());

    public bool LeftPaneIsOpen
    {
        get;
        private set => SetField(ref field, value);
    }

    private void ToggleLeftPane() => LeftPaneIsOpen = !LeftPaneIsOpen;
}