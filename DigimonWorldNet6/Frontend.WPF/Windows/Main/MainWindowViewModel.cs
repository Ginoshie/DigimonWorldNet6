using System;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Services;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using DigimonWorld.Frontend.WPF.Windows.AboutAndCredits;
using DigimonWorld.Frontend.WPF.Windows.BaseClasses;
using DigimonWorld.Frontend.WPF.Windows.GeneralConfig;
using DigimonWorld.Frontend.WPF.Windows.MusicPlayer;

namespace DigimonWorld.Frontend.WPF.Windows.Main;

public class MainWindowViewModel : BaseWindowViewModel, IDisposable
{
    private readonly CompositeDisposable _compositeDisposable;
    
    private bool _bottomPaneIsOpen;
    private bool _musicPlayerIsOpen;

    public MainWindowViewModel(Window window) : base(window)
    {
        ToggleBottomPaneCommand = new CommandHandler(ToggleBottomPane);
        
        OpenConfigurationWindowCommand = new CommandHandler(OpenConfigurationWindow);
        
        OpenAboutAndCreditsWindowCommand = new CommandHandler(OpenAboutAndCreditsWindow);

        OpenMusicPlayerWindowCommand = new CommandHandler(OpenMusicPlayerWindow);

        _compositeDisposable = new CompositeDisposable(
            EventHub.MusicPlayerClosedObservable.Subscribe(_ => _musicPlayerIsOpen = false)
        );
    }

    public bool BottomPaneIsOpen
    {
        get => _bottomPaneIsOpen;
        private set
        {
            if (_bottomPaneIsOpen == value) return;

            _bottomPaneIsOpen = value;

            OnPropertyChanged();
        }
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

    private void ToggleBottomPane()
    {
        BottomPaneIsOpen = !BottomPaneIsOpen;
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

        musicPlayerWindow.Show();
        
        EventHub.SignalMusicPlayerOpened();

        _musicPlayerIsOpen = true;
    }

    public void Dispose()
    {
        _compositeDisposable.Dispose();
    }
}