using System;
using System.Windows;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.BaseClasses;

public abstract class BaseWindowViewModel : BaseViewModel
{
    protected readonly Window Window;

    protected BaseWindowViewModel(Window window)
    {
        Window = window;

        MinimizeCommand = new CommandHandler(() => Window.WindowState = WindowState.Minimized);

        CloseCommand = new CommandHandler(CloseApplication);

        DragCommand = new CommandHandler(() => DragWindow(window));
    }

    public ICommand MinimizeCommand { get; }

    public ICommand CloseCommand { get; }

    public ICommand DragCommand { get; }

    protected virtual void DragWindow(Window window)
    {
        if (Mouse.PrimaryDevice.LeftButton != MouseButtonState.Pressed)
        {
            return;
        }

        try
        {
            window.DragMove();
        }
        catch (InvalidOperationException)
        {
            // Ignore exceptions caused by improper DragMove calls
        }
    }

    protected virtual void CloseApplication() => Window.Close();
}