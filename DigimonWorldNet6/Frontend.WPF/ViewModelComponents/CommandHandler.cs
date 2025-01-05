using System;
using System.Windows.Input;

namespace DigimonWorld.Frontend.WPF.ViewModelComponents;

public sealed class CommandHandler(Action action) : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        action();
    }
}