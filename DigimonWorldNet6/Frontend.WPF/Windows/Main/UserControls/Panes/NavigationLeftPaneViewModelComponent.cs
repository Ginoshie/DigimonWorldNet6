using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvolutionCalculator;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvolutionGraph;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.TamerVision;
using ReactiveUI;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.Panes;

public class NavigationLeftPaneViewModelComponent : PaneBaseViewModel, IDisposable
{
    private const double PANEL_OPENED_X_OFFSET = 10;
    private const double PANEL_CLOSED_X_OFFSET = 120;

    private readonly Action<UserControl> _setCurrentSelectedMainWindowContent;
    private readonly CompositeDisposable _disposable;

    private readonly Lazy<EvolutionCalculatorUserControl> _evolutionCalculatorUserControl = new();
    private readonly Lazy<DigiWikiUserControl> _digiWikiUserControl = new();
    private readonly Lazy<TamerVisionUserControl> _tamerVisionUserControl = new();
    private readonly Lazy<EvolutionGraphUserControl> _evolutionGraphUserControl = new();

    public NavigationLeftPaneViewModelComponent(Action<UserControl> setCurrentSelectedMainWindowContent)
    {
        _setCurrentSelectedMainWindowContent = setCurrentSelectedMainWindowContent;
        ToggleLeftPaneCommand = new CommandHandler(ToggleLeftPane);

        ShowEvolutionCalculatorCommand = new CommandHandler(ShowEvolutionCalculator);

        ShowDigiWikiCommand = new CommandHandler(ShowDigiWiki);

        ShowTamerVisionCommand = new CommandHandler(ShowTamerWiki);

        ShowEvolutionGraphCommand = new CommandHandler(ShowEvolutionGraph);

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

    public ICommand ShowTamerVisionCommand { get; }

    public ICommand ShowEvolutionGraphCommand { get; }

    public UserControl InitialContent => _evolutionCalculatorUserControl.Value;

    private void ShowEvolutionCalculator() => _setCurrentSelectedMainWindowContent(_evolutionCalculatorUserControl.Value);

    private void ShowDigiWiki() => _setCurrentSelectedMainWindowContent(_digiWikiUserControl.Value);

    private void ShowTamerWiki() => _setCurrentSelectedMainWindowContent(_tamerVisionUserControl.Value);

    private void ShowEvolutionGraph() => _setCurrentSelectedMainWindowContent(_evolutionGraphUserControl.Value);


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