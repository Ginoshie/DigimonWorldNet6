using System;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using DigimonWorld.Frontend.WPF.Windows.AboutAndCredits;
using DigimonWorld.Frontend.WPF.Windows.BaseClasses;
using DigimonWorld.Frontend.WPF.Windows.GeneralConfig;
using DigimonWorld.Frontend.WPF.Windows.Dialogs;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.Panes;
using DigimonWorld.Frontend.WPF.Windows.MusicPlayer;
using MemoryAccess;
using MemoryAccess.MemoryValues.World;
using Shared;
using Shared.Services;
using Shared.Services.Events;

namespace DigimonWorld.Frontend.WPF.Windows.Main;

public class MainWindowViewModel : BaseWindowViewModel, IDisposable
{
    private const double CLOCK_HIDDEN_OFFSET = 220;
    private const double CLOCK_VISIBLE_OFFSET = 0;

    private readonly CompositeDisposable _compositeDisposable;
    private readonly SerialDisposable _worldTimeSyncSubscription = new();
    private readonly SerialDisposable _clockAnimationSubscription = new();
    private readonly SynchronizationContextScheduler _uiScheduler;

    private bool _musicPlayerIsOpen;
    private UpdateCheckResult? _cachedUpdateResult;

    public MainWindowViewModel(Window window) : base(window)
    {
        _uiScheduler = new SynchronizationContextScheduler(SynchronizationContext.Current!);

        OpenConfigurationWindowCommand = new CommandHandler(OpenConfigurationWindow);

        OpenAboutAndCreditsWindowCommand = new CommandHandler(OpenAboutAndCreditsWindow);

        OpenMusicPlayerWindowCommand = new CommandHandler(OpenMusicPlayerWindow);

        CheckForUpdateCommand = new CommandHandler(CheckForUpdate);

        _compositeDisposable = new CompositeDisposable(
            MusicPlayerEventHub.MusicPlayerClosedObservable.Subscribe(_ => _musicPlayerIsOpen = false),
            EmulatorLinkEventHub.EmulatorConnectedObservable.Subscribe(OnEmulatorConnectedChanged),
            _worldTimeSyncSubscription,
            _clockAnimationSubscription
        );

        LeftPaneViewModelComponent = new NavigationLeftPaneViewModelComponent(uc => CurrentSelectedMainWindowContent = uc);

        CurrentSelectedMainWindowContent = LeftPaneViewModelComponent.InitialContent;

        BottomPaneViewModelComponent = new HistoricEvolutionsBottomPaneViewModelComponent();

        RightPaneViewModelComponent = new EmulatorLinkRightPaneViewModelComponent();

        CheckForUpdateOnStartup();
    }

    public NavigationLeftPaneViewModelComponent LeftPaneViewModelComponent { get; private set; }
    public HistoricEvolutionsBottomPaneViewModelComponent BottomPaneViewModelComponent { get; private set; }
    public EmulatorLinkRightPaneViewModelComponent RightPaneViewModelComponent { get; private set; }

    public string ClockTime
    {
        get;
        set
        {
            if (SetField(ref field, value))
            {
                OnPropertyChanged(nameof(ClockRotationAngle));
            }
        }
    } = "00:00";

    public double ClockRotationAngle
    {
        get
        {
            if (TimeSpan.TryParse(ClockTime, out TimeSpan time))
            {
                return -(time.Hours / 24.0 * 360.0);
            }
            return 0;
        }
    }

    public bool ClockIsVisible
    {
        get;
        private set => SetField(ref field, value);
    }

    public bool UpdateIsAvailable
    {
        get;
        private set => SetField(ref field, value);
    }

    public double ClockOffset
    {
        get;
        private set => SetField(ref field, value);
    } = CLOCK_HIDDEN_OFFSET;

    private void AnimateClockOffset(double target)
    {
        const int fps = 60;
        const int durationMs = 600;
        const int steps = durationMs * fps / 1000;
        double start = ClockOffset;

        _clockAnimationSubscription.Disposable = Observable
            .Interval(TimeSpan.FromMilliseconds(1000.0 / fps))
            .Take(steps + 1)
            .Select(i =>
            {
                double t = (double)i / steps;
                t = 1 - Math.Pow(1 - t, 2);
                return start + (target - start) * t;
            })
            .ObserveOn(_uiScheduler)
            .Subscribe(v => ClockOffset = v);
    }

    private void OnEmulatorConnectedChanged(bool isConnected)
    {
        if (isConnected)
        {
            ClockIsVisible = true;
            SyncWorldTime();
            _worldTimeSyncSubscription.Disposable = Observable.Interval(TimeSpan.FromSeconds(1))
                .ObserveOn(_uiScheduler)
                .Subscribe(_ => SyncWorldTime());
            AnimateClockOffset(CLOCK_VISIBLE_OFFSET);
        } else
        {
            _worldTimeSyncSubscription.Disposable = null;
            AnimateClockOffset(CLOCK_HIDDEN_OFFSET);
            ClockIsVisible = false;
        }
    }

    private void SyncWorldTime()
    {
        WorldTime worldTime = LiveMemoryReader.Instance.WorldTime;
        ClockTime = $"{worldTime.Hour:D2}:{worldTime.Minute:D2}";
    }

    public UserControl CurrentSelectedMainWindowContent
    {
        get;
        private set => SetField(ref field, value);
    }

    public ICommand OpenConfigurationWindowCommand { get; }

    public ICommand OpenAboutAndCreditsWindowCommand { get; }

    public CommandHandler OpenMusicPlayerWindowCommand { get; }

    public ICommand CheckForUpdateCommand { get; }

    protected override void CloseApplication()
    {
        Services.MusicPlayer.Dispose();

        Window.Close();
    }

    private void OpenConfigurationWindow()
    {
        GeneralConfigWindow configWindow = new()
        {
            Owner = Application.Current.MainWindow
        };

        GeneralConfigWindowViewModel configViewModel = new(configWindow);

        configWindow.DataContext = configViewModel;

        configWindow.ShowDialog();

        configViewModel.Dispose();
    }

    private void OpenAboutAndCreditsWindow()
    {
        AboutAndCreditsWindow aboutAndCreditsWindow = new()
        {
            Owner = Application.Current.MainWindow
        };

        AboutAndCreditsWindowViewModel aboutAndCreditsWindowViewModel = new(aboutAndCreditsWindow);

        aboutAndCreditsWindow.DataContext = aboutAndCreditsWindowViewModel;

        aboutAndCreditsWindow.ShowDialog();
    }

    private void OpenMusicPlayerWindow()
    {
        if (_musicPlayerIsOpen)
        {
            return;
        }

        MusicPlayerWindow musicPlayerWindow = new()
        {
            Owner = Application.Current.MainWindow
        };

        MusicPlayerViewModel musicPlayerViewModel = new(musicPlayerWindow);

        musicPlayerWindow.DataContext = musicPlayerViewModel;

        musicPlayerWindow.Closed += (_, _) =>
        {
            musicPlayerViewModel.Dispose();
            _musicPlayerIsOpen = false;
        };

        musicPlayerWindow.Show();

        MusicPlayerEventHub.SignalMusicPlayerOpened();

        _musicPlayerIsOpen = true;
    }

    private async void CheckForUpdateOnStartup()
    {
        try
        {
            _cachedUpdateResult = await UpdateService.CheckForUpdateAsync();
            UpdateIsAvailable = _cachedUpdateResult.HasUpdate;
        } catch
        {
            // Silently ignore update check failures on startup
        }
    }

    private async void CheckForUpdate()
    {
        try
        {
            UpdateCheckResult result = _cachedUpdateResult ?? await UpdateService.CheckForUpdateAsync();
            _cachedUpdateResult = null;

            if (!result.HasUpdate || result.UpdateInfo == null)
            {
                return;
            }

            JijimonYesNoDialog updateDialog = new($"Well well. . . what do we have here! A shiny new version {result.NewVersion} has arrived!\n\nYou're currently on {AppVersion.Current}. Shall we fetch this update?")
            {
                Owner = Application.Current.MainWindow
            };
            updateDialog.ShowDialog();

            if (!updateDialog.Result)
            {
                return;
            }

            JijimonYesNoDialog progressDialog = new("Hold on now, let me fetch that for you...")
            {
                Owner = Application.Current.MainWindow
            };
            ((JijimonYesNoDialogViewModel)progressDialog.DataContext).HideButtons();

            _ = System.Threading.Tasks.Task.Run(async () =>
            {
                await UpdateService.DownloadAndApplyUpdateAsync(result.UpdateInfo);
            });

            progressDialog.ShowDialog();
        } catch (Exception)
        {
            NanimonErrorDialog dialog = new("Nanimon", "Naniiii?! The update broke!\n\nEven I couldn't punch through whatever went wrong there. Try again later or smash that button below to grab it yourself!", "https://github.com/Ginoshie/DigimonWorldNet6/releases")
            {
                Owner = Application.Current.MainWindow
            };
            dialog.ShowDialog();
        }
    }

    public void Dispose() => _compositeDisposable.Dispose();
}