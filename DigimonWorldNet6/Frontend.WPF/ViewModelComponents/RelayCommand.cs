using System;
using System.Windows.Input;

namespace DigimonWorld.Frontend.WPF.ViewModelComponents;

public class RelayCommand<T>(Action<T> execute) : ICommand
{
    private readonly Action<T> _execute = execute ?? throw new ArgumentNullException(nameof(execute));

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter) => _execute((T)parameter!);

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}