using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvolutionCalculator;
using ReactiveUI;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.Panes;

public class NavigationLeftPaneViewModelComponent : PaneBaseViewModel, IDisposable
{
    private const double PANEL_OPENED_X_OFFSET = 10;
    private const double PANEL_CLOSED_X_OFFSET = 120;

    private readonly Action<UserControl> _setCurrentSelectedMainWindowContent;
    private readonly CompositeDisposable _disposable;

    public NavigationLeftPaneViewModelComponent(Action<UserControl> setCurrentSelectedMainWindowContent)
    {
        _setCurrentSelectedMainWindowContent = setCurrentSelectedMainWindowContent;
        ToggleLeftPaneCommand = new CommandHandler(ToggleLeftPane);

        ShowEvolutionCalculatorCommand = new CommandHandler(ShowEvolutionCalculator);

        ShowDigiWikiCommand = new CommandHandler(ShowDigiWiki);

        PaneOffset = PaneIsOpen ? PANEL_OPENED_X_OFFSET : PANEL_CLOSED_X_OFFSET;

        _disposable = new CompositeDisposable(
            this.WhenAnyValue(x => x.PaneIsOpen)
                .Select(paneIsOpen => paneIsOpen ? PANEL_OPENED_X_OFFSET : PANEL_CLOSED_X_OFFSET)
                .DistinctUntilChanged()
                .SelectMany(targetOffset => AnimateOffset(PaneOffset, targetOffset))
                .ObserveOn(SynchronizationContext.Current!)
                .Subscribe(v => PaneOffset = v));
    }

    public ICommand ToggleLeftPaneCommand { get; }

    public ICommand ShowEvolutionCalculatorCommand { get; }

    public ICommand ShowDigiWikiCommand { get; }

    private void ShowEvolutionCalculator() => _setCurrentSelectedMainWindowContent(new EvolutionCalculatorUserControl());

    private void ShowDigiWiki() => _setCurrentSelectedMainWindowContent(new DigiWikiUserControl());

    public bool PaneIsOpen
    {
        get;
        private set => SetField(ref field, value);
    }

    public double PaneOffset
    {
        get;
        private set => SetField(ref field, value);
    }

    private void ToggleLeftPane() => PaneIsOpen = !PaneIsOpen;

    public void Dispose() => _disposable.Dispose();
}