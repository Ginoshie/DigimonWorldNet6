using System;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Services;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using DigimonWorld.Frontend.WPF.Windows.AboutAndCredits;
using DigimonWorld.Frontend.WPF.Windows.BaseClasses;
using DigimonWorld.Frontend.WPF.Windows.GeneralConfig;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvolutionCalculator;
using DigimonWorld.Frontend.WPF.Windows.MusicPlayer;

namespace DigimonWorld.Frontend.WPF.Windows.Main;

public class MainWindowViewModel : BaseWindowViewModel, IDisposable
{
    private readonly CompositeDisposable _compositeDisposable;

    private bool _bottomPaneIsOpen;
    private bool _leftPaneIsOpen;
    private bool _musicPlayerIsOpen;
    private UserControl _currentSelectedMainWindowContent;

    public MainWindowViewModel(Window window) : base(window)
    {
        ToggleBottomPaneCommand = new CommandHandler(ToggleBottomPane);

        ToggleLeftPaneCommand = new CommandHandler(ToggleLeftPane);

        ShowEvolutionCalculatorCommand = new CommandHandler(ShowEvolutionCalculator);

        ShowDigiWikiCommand = new CommandHandler(ShowDigiWiki);

        OpenConfigurationWindowCommand = new CommandHandler(OpenConfigurationWindow);

        OpenAboutAndCreditsWindowCommand = new CommandHandler(OpenAboutAndCreditsWindow);

        OpenMusicPlayerWindowCommand = new CommandHandler(OpenMusicPlayerWindow);

        _compositeDisposable = new CompositeDisposable(
            EventHub.MusicPlayerClosedObservable.Subscribe(_ => _musicPlayerIsOpen = false)
        );

        _currentSelectedMainWindowContent = new EvolutionCalculatorUserControl();
    }

    public UserControl CurrentSelectedMainWindowContent
    {
        get => _currentSelectedMainWindowContent;
        private set => SetField(ref _currentSelectedMainWindowContent, value);
    }

    public bool LeftPaneIsOpen
    {
        get => _leftPaneIsOpen;
        private set => SetField(ref _leftPaneIsOpen, value);
    }

    public bool BottomPaneIsOpen
    {
        get => _bottomPaneIsOpen;
        private set => SetField(ref _bottomPaneIsOpen, value);
    }

    public ICommand ToggleLeftPaneCommand { get; }

    public ICommand ShowEvolutionCalculatorCommand { get; }

    public ICommand ShowDigiWikiCommand { get; }

    public ICommand ToggleBottomPaneCommand { get; }

    public ICommand OpenConfigurationWindowCommand { get; }

    public ICommand OpenAboutAndCreditsWindowCommand { get; }

    public CommandHandler OpenMusicPlayerWindowCommand { get; }

    protected override void CloseApplication()
    {
        Services.MusicPlayer.Dispose();

        Window.Close();
    }

    private void ToggleLeftPane() => LeftPaneIsOpen = !LeftPaneIsOpen;

    private void ToggleBottomPane() => BottomPaneIsOpen = !BottomPaneIsOpen;

    private void ShowEvolutionCalculator() => CurrentSelectedMainWindowContent = new EvolutionCalculatorUserControl();

    private void ShowDigiWiki() => CurrentSelectedMainWindowContent = new DigiWikiUserControl();

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
        if (_musicPlayerIsOpen) return;

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

        EventHub.SignalMusicPlayerOpened();

        _musicPlayerIsOpen = true;
    }

    public void Dispose() => _compositeDisposable.Dispose();
}