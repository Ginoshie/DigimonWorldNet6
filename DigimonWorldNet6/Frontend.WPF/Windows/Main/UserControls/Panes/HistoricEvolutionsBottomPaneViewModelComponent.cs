using System;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using Shared.Services.Events;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.Panes;

public class HistoricEvolutionsBottomPaneViewModelComponent : BaseViewModel, IDisposable
{
    private readonly CompositeDisposable _disposable;

    public HistoricEvolutionsBottomPaneViewModelComponent()
    {
        ToggleBottomPaneCommand = new CommandHandler(ToggleBottomPane);

        SignalSyncFreshStageHistoricEvolutionsCommand = new CommandHandler(SignalSyncFreshStageHistoricEvolutions);
        SignalSyncInTrainingStageHistoricEvolutionsCommand = new CommandHandler(SignalSyncInTrainingStageHistoricEvolutions);
        SignalSyncRookieStageHistoricEvolutionsCommand = new CommandHandler(SignalSyncRookieStageHistoricEvolutions);
        SignalSyncChampionStageHistoricEvolutionsCommand = new CommandHandler(SignalSyncChampionStageHistoricEvolutions);
        SignalSyncUltimateStageHistoricEvolutionsCommand = new CommandHandler(SignalSyncUltimateStageHistoricEvolutions);
        SignalSyncAllStagesHistoricEvolutionsCommand = new CommandHandler(SignalSyncAllStagesHistoricEvolutions);

        _disposable = new CompositeDisposable(
            EmulatorLinkEventHub.EmulatorConnectedObservable.Subscribe(_ => OnEmulatorConnected()),
            EmulatorLinkEventHub.EmulatorDisconnectedObservable.Subscribe(_ => OnEmulatorDisconnected())
        );
    }

    public ICommand ToggleBottomPaneCommand { get; }
    public ICommand SignalSyncFreshStageHistoricEvolutionsCommand { get; }

    public ICommand SignalSyncInTrainingStageHistoricEvolutionsCommand { get; }

    public ICommand SignalSyncRookieStageHistoricEvolutionsCommand { get; }

    public ICommand SignalSyncChampionStageHistoricEvolutionsCommand { get; }

    public ICommand SignalSyncUltimateStageHistoricEvolutionsCommand { get; }

    public ICommand SignalSyncAllStagesHistoricEvolutionsCommand { get; }

    public bool BottomPaneIsOpen
    {
        get;
        private set
        {
            if (SetField(ref field, value))
            {
                _ = NotifyEmulatorSyncButtonSectionDelayedAsync();
            }
        }
    }

    private async Task NotifyEmulatorSyncButtonSectionDelayedAsync()
    {
        await Task.Delay(500);
        OnPropertyChanged(nameof(EmulatorSyncButtonSectionIsOpen));
    }


    public bool EmulatorConnected
    {
        get;
        set
        {
            SetField(ref field, value);

            OnPropertyChanged(nameof(EmulatorSyncButtonSectionIsOpen));
        }
    }

    public bool EmulatorSyncButtonSectionIsOpen => BottomPaneIsOpen && EmulatorConnected;

    private void ToggleBottomPane() => BottomPaneIsOpen = !BottomPaneIsOpen;

    private void SignalSyncFreshStageHistoricEvolutions() => HistoricEvolutionEventhub.SignalSyncFreshStageHistoricEvolutions();

    private void SignalSyncInTrainingStageHistoricEvolutions() => HistoricEvolutionEventhub.SignalSyncInTrainingStageHistoricEvolutions();

    private void SignalSyncRookieStageHistoricEvolutions() => HistoricEvolutionEventhub.SignalSyncRookieStageHistoricEvolutions();

    private void SignalSyncChampionStageHistoricEvolutions() => HistoricEvolutionEventhub.SignalSyncChampionStageHistoricEvolutions();

    private void SignalSyncUltimateStageHistoricEvolutions() => HistoricEvolutionEventhub.SignalSyncUltimateStageHistoricEvolutions();

    private void SignalSyncAllStagesHistoricEvolutions() => HistoricEvolutionEventhub.SignalSyncAllStagesHistoricEvolutions();

    private void OnEmulatorConnected() => EmulatorConnected = true;

    private void OnEmulatorDisconnected() => EmulatorConnected = false;

    public void Dispose() => _disposable.Dispose();
}