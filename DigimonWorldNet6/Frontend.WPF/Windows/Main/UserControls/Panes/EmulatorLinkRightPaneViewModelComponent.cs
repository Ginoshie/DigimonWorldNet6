using System;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Input;
using System.Windows.Media;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using ReactiveUI;
using Shared.Enums;
using Shared.Services;
using Shared.Services.Events;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.Panes;

public class EmulatorLinkRightPaneViewModelComponent : PaneBaseViewModel, IDisposable
{
    private const double PANEL_DISABLED_X_OFFSET = -466;
    private const double PANEL_OPENED_X_OFFSET = -12;
    private const double PANEL_CLOSED_X_OFFSET = -420;

    private readonly CompositeDisposable _disposable;

    public EmulatorLinkRightPaneViewModelComponent()
    {
        ToggleRightPaneCommand = new CommandHandler(ToggleRightPane);

        EmulatorLinkSyncModeIsManual = UserConfigurationManager.EmulatorLinkConfig.EmulatorLinkSyncMode == EmulatorLinkSyncMode.Manual;

        _disposable = new CompositeDisposable(
            EmulatorLinkEventHub.EmulatorConnectedObservable.Subscribe(_ => OnEmulatorConnected()),
            EmulatorLinkEventHub.EmulatorDisconnectedObservable.Subscribe(_ => OnEmulatorDisconnected()),
            EmulatorLinkEventHub.EmulatorLinkSyncModeChangedObservable.Subscribe(OnEmulatorSyncModeChanged)
        );

        PaneOffset = GetTargetOffset();

        this.WhenAnyValue(x => x.PaneIsOpen)
            .Merge(this.WhenAnyValue(x => x.PaneEnabled))
            .Select(_ => GetTargetOffset())
            .DistinctUntilChanged()
            .SelectMany(targetOffset => AnimateOffset(PaneOffset, targetOffset))
            .ObserveOn(SynchronizationContext.Current!)
            .Subscribe(v => PaneOffset = v)
            .DisposeWith(_disposable);
    }

    public ICommand ToggleRightPaneCommand { get; }

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

    public bool EmulatorConnected
    {
        get;
        set
        {
            SetField(ref field, value);

            OnPropertyChanged(nameof(PaneEnabled));
            OnPropertyChanged(nameof(CurrentEmulatorLinkModeText));
            OnPropertyChanged(nameof(CurrentEmulatorLinkModeBrush));
        }
    }

    public bool EmulatorLinkSyncModeIsManual
    {
        get;
        set
        {
            SetField(ref field, value);

            OnPropertyChanged(nameof(PaneEnabled));
            OnPropertyChanged(nameof(CurrentEmulatorLinkModeText));
            OnPropertyChanged(nameof(CurrentEmulatorLinkModeBrush));
        }
    }

    public bool PaneEnabled => EmulatorConnected && EmulatorLinkSyncModeIsManual;

    public string CurrentEmulatorLinkModeText
    {
        get
        {
            string mode = "Emulator mode: ";

            if (EmulatorConnected)
            {
                if (EmulatorLinkSyncModeIsManual)
                {
                    mode += nameof(EmulatorLinkSyncMode.Manual);
                }
                else
                {
                    mode += nameof(EmulatorLinkSyncMode.Auto);
                }
            }
            else
            {
                mode += "No connection";
            }

            return mode;
        }
    }

    public Brush CurrentEmulatorLinkModeBrush
    {
        get
        {
            if (EmulatorConnected)
            {
                return EmulatorLinkSyncModeIsManual ? Brushes.Yellow : Brushes.Green;
            }

            return Brushes.Red;
        }
    }

    private void ToggleRightPane() => PaneIsOpen = !PaneIsOpen;

    private void OnEmulatorConnected() => EmulatorConnected = true;

    private void OnEmulatorDisconnected() => EmulatorConnected = false;

    private void OnEmulatorSyncModeChanged(EmulatorLinkSyncMode mode) => EmulatorLinkSyncModeIsManual = mode == EmulatorLinkSyncMode.Manual;

    private double GetTargetOffset()
    {
        if (!PaneEnabled)
        {
            return PANEL_DISABLED_X_OFFSET;
        }

        return PaneIsOpen
            ? PANEL_OPENED_X_OFFSET
            : PANEL_CLOSED_X_OFFSET;
    }

    public void Dispose() => _disposable.Dispose();
}