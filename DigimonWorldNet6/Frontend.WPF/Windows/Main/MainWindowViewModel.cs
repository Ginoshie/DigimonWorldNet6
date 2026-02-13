using System;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using DigimonWorld.Frontend.WPF.Windows.AboutAndCredits;
using DigimonWorld.Frontend.WPF.Windows.BaseClasses;
using DigimonWorld.Frontend.WPF.Windows.GeneralConfig;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvolutionCalculator;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.Panes;
using DigimonWorld.Frontend.WPF.Windows.MusicPlayer;
using Shared.Services;
using Shared.Services.Events;

namespace DigimonWorld.Frontend.WPF.Windows.Main;

public class MainWindowViewModel : BaseWindowViewModel, IDisposable
{
    private readonly CompositeDisposable _compositeDisposable;

    private bool _musicPlayerIsOpen;

    public MainWindowViewModel(Window window) : base(window)
    {
        ToggleBottomPaneCommand = new CommandHandler(ToggleBottomPane);

        OpenConfigurationWindowCommand = new CommandHandler(OpenConfigurationWindow);

        OpenAboutAndCreditsWindowCommand = new CommandHandler(OpenAboutAndCreditsWindow);

        OpenMusicPlayerWindowCommand = new CommandHandler(OpenMusicPlayerWindow);

        _compositeDisposable = new CompositeDisposable(
            MusicPlayerEventHub.MusicPlayerClosedObservable.Subscribe(_ => _musicPlayerIsOpen = false)
        );
        
        CurrentSelectedMainWindowContent = new EvolutionCalculatorUserControl();

        LeftPaneViewModelComponent = new NavigationLeftPaneViewModelComponent(uc => CurrentSelectedMainWindowContent = uc);
        RightPaneViewModelComponent = new EmulatorLinkRightPaneViewModelComponent();
    }
    
    public EmulatorLinkRightPaneViewModelComponent RightPaneViewModelComponent { get; private set; }
    public NavigationLeftPaneViewModelComponent LeftPaneViewModelComponent { get; private set; }

    public UserControl CurrentSelectedMainWindowContent
    {
        get;
        private set => SetField(ref field, value);
    }

    public bool BottomPaneIsOpen
    {
        get;
        private set => SetField(ref field, value);
    }

    public ICommand ToggleBottomPaneCommand { get; }

    public ICommand OpenConfigurationWindowCommand { get; }

    public ICommand OpenAboutAndCreditsWindowCommand { get; }

    public CommandHandler OpenMusicPlayerWindowCommand { get; }

    protected override void CloseApplication()
    {
        Services.MusicPlayer.Dispose();

        Window.Close();
    }

    private void ToggleBottomPane() => BottomPaneIsOpen = !BottomPaneIsOpen;

    private void OpenConfigurationWindow()
    {
        GeneralConfigWindow configWindow = new()
        {
            Owner = Application.Current.MainWindow
        };

        GeneralConfigWindowViewModel configViewModel = new(configWindow);

        configWindow.DataContext = configViewModel;

        configWindow.ShowDialog();
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

    public void Dispose() => _compositeDisposable.Dispose();
}