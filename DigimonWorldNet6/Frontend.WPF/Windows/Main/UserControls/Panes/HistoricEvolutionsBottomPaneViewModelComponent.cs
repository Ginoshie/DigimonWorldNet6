using System;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using ReactiveUI;
using Shared.Enums;
using Shared.Services;
using Shared.Services.Events;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.Panes;

public class HistoricEvolutionsBottomPaneViewModelComponent : PaneBaseViewModel, IDisposable
{
    private const double PANEL_OPENED_X_OFFSET = -12;
    private const double PANEL_CLOSED_X_OFFSET = -475;
    private const double EMULATOR_SYNC_BUTTON_SECTION_OPENED_X_OFFSET = -10;
    private const double EMULATOR_SYNC_BUTTON_SECTION_CLOSED_X_OFFSET = -65;

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
            EmulatorLinkEventHub.EmulatorDisconnectedObservable.Subscribe(_ => OnEmulatorDisconnected()),
            EmulatorLinkEventHub.EmulatorLinkSyncModeChangedObservable.Subscribe(UpdateEmulatorLinkSyncMode)
        );

        PaneOffset = PaneIsOpen ? PANEL_OPENED_X_OFFSET : PANEL_CLOSED_X_OFFSET;

        this.WhenAnyValue(x => x.PaneIsOpen)
            .Select(paneIsOpen => paneIsOpen ? PANEL_OPENED_X_OFFSET : PANEL_CLOSED_X_OFFSET)
            .DistinctUntilChanged()
            .SelectMany(targetOffset => AnimateOffset(PaneOffset, targetOffset))
            .ObserveOn(SynchronizationContext.Current!)
            .Subscribe(v => PaneOffset = v)
            .DisposeWith(_disposable);

        EmulatorButtonSectionOffset = EmulatorSyncButtonSectionIsOpen ? EMULATOR_SYNC_BUTTON_SECTION_OPENED_X_OFFSET : EMULATOR_SYNC_BUTTON_SECTION_CLOSED_X_OFFSET;

        this.WhenAnyValue(x => x.EmulatorSyncButtonSectionIsOpen)
            .Select(emulatorSyncButtonSectionIsOpen => emulatorSyncButtonSectionIsOpen ? EMULATOR_SYNC_BUTTON_SECTION_OPENED_X_OFFSET : EMULATOR_SYNC_BUTTON_SECTION_CLOSED_X_OFFSET)
            .DistinctUntilChanged()
            .SelectMany(targetOffset => AnimateOffset(EmulatorButtonSectionOffset, targetOffset))
            .ObserveOn(SynchronizationContext.Current!)
            .Subscribe(v => EmulatorButtonSectionOffset = v)
            .DisposeWith(_disposable);

        EmulatorLinkSyncMode = UserConfigurationManager.EmulatorLinkConfig.EmulatorLinkSyncMode;
    }

    public ICommand ToggleBottomPaneCommand { get; }
    public ICommand SignalSyncFreshStageHistoricEvolutionsCommand { get; }

    public ICommand SignalSyncInTrainingStageHistoricEvolutionsCommand { get; }

    public ICommand SignalSyncRookieStageHistoricEvolutionsCommand { get; }

    public ICommand SignalSyncChampionStageHistoricEvolutionsCommand { get; }

    public ICommand SignalSyncUltimateStageHistoricEvolutionsCommand { get; }

    public ICommand SignalSyncAllStagesHistoricEvolutionsCommand { get; }

    public bool PaneIsOpen
    {
        get;
        private set
        {
            if (!SetField(ref field, value))
            {
                return;
            }

            if (value)
            {
                _ = NotifyEmulatorSyncButtonSectionDelayedAsync();
            }
            else
            {
                OnPropertyChanged(nameof(EmulatorSyncButtonSectionIsOpen));
            }
        }
    }

    public double PaneOffset
    {
        get;
        private set => SetField(ref field, value);
    }

    public double EmulatorButtonSectionOffset
    {
        get;
        private set => SetField(ref field, value);
    }

    private async Task NotifyEmulatorSyncButtonSectionDelayedAsync()
    {
        await Task.Delay(AnimationDurationInMs);
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

    public EmulatorLinkSyncMode EmulatorLinkSyncMode
    {
        get;
        set
        {
            SetField(ref field, value);
        }
    } = UserConfigurationManager.EmulatorLinkConfig.EmulatorLinkSyncMode;

    public bool EmulatorSyncButtonSectionIsOpen => PaneIsOpen && EmulatorConnected && EmulatorLinkSyncMode == EmulatorLinkSyncMode.Manual;

    private void ToggleBottomPane() => PaneIsOpen = !PaneIsOpen;

    private void SignalSyncFreshStageHistoricEvolutions() => HistoricEvolutionEventhub.SignalSyncFreshStageHistoricEvolutions();

    private void SignalSyncInTrainingStageHistoricEvolutions() => HistoricEvolutionEventhub.SignalSyncInTrainingStageHistoricEvolutions();

    private void SignalSyncRookieStageHistoricEvolutions() => HistoricEvolutionEventhub.SignalSyncRookieStageHistoricEvolutions();

    private void SignalSyncChampionStageHistoricEvolutions() => HistoricEvolutionEventhub.SignalSyncChampionStageHistoricEvolutions();

    private void SignalSyncUltimateStageHistoricEvolutions() => HistoricEvolutionEventhub.SignalSyncUltimateStageHistoricEvolutions();

    private void SignalSyncAllStagesHistoricEvolutions() => HistoricEvolutionEventhub.SignalSyncAllStagesHistoricEvolutions();

    private void OnEmulatorConnected()
    {
        EmulatorConnected = true;

        if (EmulatorLinkSyncMode == EmulatorLinkSyncMode.Auto)
        {
            StartMemorySync();
        }
    }

    private void OnEmulatorDisconnected()
    {
        EmulatorConnected = false;

        StopMemorySync();
    }

    private void UpdateEmulatorLinkSyncMode(EmulatorLinkSyncMode mode)
    {
        EmulatorLinkSyncMode = mode;
        
        OnPropertyChanged(nameof(EmulatorSyncButtonSectionIsOpen));

        switch (mode)
        {
            case EmulatorLinkSyncMode.Auto:
            {
                if (EmulatorConnected)
                {
                    StartMemorySync();
                }

                break;
            }
            case EmulatorLinkSyncMode.Manual:
                StopMemorySync();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private readonly SerialDisposable _memorySyncDisposable = new();

    private void StartMemorySync()
    {
        _memorySyncDisposable.Disposable = Observable
            .Interval(TimeSpan.FromSeconds(2))
            .TakeUntil(EmulatorLinkEventHub.EmulatorDisconnectedObservable)
            .Subscribe(_ =>
            {
                if (!EmulatorConnected) return;

                try
                {
                    SignalSyncAllStagesHistoricEvolutions();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error syncing historic evolutions: {ex}");
                }
            });
    }

    private void StopMemorySync()
    {
        _memorySyncDisposable.Disposable = null;
    }

    public void Dispose() => _disposable.Dispose();
}