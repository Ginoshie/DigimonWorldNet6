using System;
using System.Windows.Input;

namespace DigimonWorld.Frontend.WPF.ViewModelComponents;

public sealed class CommandHandler(Action action) : ICommand
{
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        action();
    }
}