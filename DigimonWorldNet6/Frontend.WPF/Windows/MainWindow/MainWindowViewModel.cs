using System;
using System.Windows;
using System.Windows.Input;

namespace DigimonWorld.Frontend.WPF.Windows.MainWindow;

public class MainWindowViewModel
{
    public MainWindowViewModel(Window window)
    {
        MinimizeCommand = new CommandHandler(() => window.WindowState = WindowState.Minimized);
        
        CloseCommand = new CommandHandler(window.Close);

        DragCommand = new CommandHandler(() => DragWindow(window));
    }
    
    public ICommand MinimizeCommand { get; }
    
    public ICommand CloseCommand { get; }
    
    public ICommand DragCommand { get; }

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
}