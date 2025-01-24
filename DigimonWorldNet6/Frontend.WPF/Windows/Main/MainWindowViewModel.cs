using System;
using System.Windows;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Services;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using DigimonWorld.Frontend.WPF.Windows.GeneralConfig;

namespace DigimonWorld.Frontend.WPF.Windows.Main;

public class MainWindowViewModel : BaseViewModel
{
    private bool _leftPaneIsOpen;
    private bool _bottomPaneIsOpen;
    private readonly Window _window;

    public MainWindowViewModel(Window window)
    {
        _window = window;

        MinimizeCommand = new CommandHandler(() => _window.WindowState = WindowState.Minimized);

        CloseCommand = new CommandHandler(CloseApplication);

        DragCommand = new CommandHandler(() => DragWindow(window));

        ToggleLeftPaneCommand = new CommandHandler(ToggleLeftPane);

        ToggleBottomPaneCommand = new CommandHandler(ToggleBottomPane);
        
        OpenConfigurationMenuCommand = new CommandHandler(OpenConfigurationMenu);
    }

    private void CloseApplication()
    {
        Jukebox.Dispose();

        _window.Close();
    }

    public bool LeftPaneIsOpen
    {
        get => _leftPaneIsOpen;
        private set
        {
            if (_leftPaneIsOpen == value) return;

            _leftPaneIsOpen = value;

            if (_leftPaneIsOpen)
            {
                EventHub.SignalLeftPaneOpened();
            }
            else
            {
                EventHub.SignalLeftPaneClosed();
            }

            OnPropertyChanged();
        }
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

    public ICommand MinimizeCommand { get; }

    public ICommand CloseCommand { get; }

    public ICommand DragCommand { get; }

    public ICommand ToggleLeftPaneCommand { get; }

    public ICommand ToggleBottomPaneCommand { get; }

    public ICommand OpenConfigurationMenuCommand { get; }

    private void DragWindow(Window window)
    {
        if (Mouse.PrimaryDevice.LeftButton != MouseButtonState.Pressed) return;

        try
        {
            window.DragMove();
        }
        catch (InvalidOperationException)
        {
            // Ignore exceptions caused by improper DragMove calls
        }
    }

    private void ToggleLeftPane()
    {
        LeftPaneIsOpen = !LeftPaneIsOpen;
    }

    private void ToggleBottomPane()
    {
        BottomPaneIsOpen = !BottomPaneIsOpen;
    }

    private void OpenConfigurationMenu()
    {
        GeneralConfigWindow configWindow = new()
        {
            Owner = Application.Current.MainWindow
        };

        GeneralConfigWindowViewModel configViewModel = new(configWindow);

        configWindow.DataContext = configViewModel;

        configWindow.ShowDialog();
    }
}