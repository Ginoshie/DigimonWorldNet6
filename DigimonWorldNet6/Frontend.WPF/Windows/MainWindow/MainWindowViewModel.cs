using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace DigimonWorld.Frontend.WPF.Windows.MainWindow;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private bool _leftPaneIsOpen;

    public MainWindowViewModel(Window window)
    {
        MinimizeCommand = new CommandHandler(() => window.WindowState = WindowState.Minimized);

        CloseCommand = new CommandHandler(window.Close);

        DragCommand = new CommandHandler(() => DragWindow(window));
        
        ToggleLeftPaneCommand = new CommandHandler(ToggleLeftPane);
    }

    public bool LeftPaneIsOpen
    {
        get => _leftPaneIsOpen;
        private set
        {
            if (_leftPaneIsOpen == value) return;

            _leftPaneIsOpen = value;
            OnPropertyChanged();
        }
    }

    public ICommand MinimizeCommand { get; }

    public ICommand CloseCommand { get; }

    public ICommand DragCommand { get; }
    
    public ICommand ToggleLeftPaneCommand { get; }

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

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}