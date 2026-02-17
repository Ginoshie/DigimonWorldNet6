using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using DigimonWorld.Frontend.WPF.Windows.GeneralConfig.Utility;
using Shared.Services;

namespace DigimonWorld.Frontend.WPF.Windows.GeneralConfig.Dialogs;

public class EmulatorProcessPickerViewModel : BaseViewModel, IDisposable
{
    private readonly Window _owner;

    private readonly IDisposable? _memorySyncSubscription;

    public EmulatorProcessPickerViewModel(Window owner)
    {
        _owner = owner;

        LoadProcesses();

        _memorySyncSubscription = Observable
            .Interval(TimeSpan.FromSeconds(1))
            .ObserveOn(SynchronizationContext.Current!)
            .Subscribe(_ => LoadProcesses());

        AttachEmulatorProcessCommand = new CommandHandler(AttachEmulatorProcess);

        CancelCommand = new CommandHandler(Cancel);
    }

    public ObservableCollection<ProcessItemViewModel> Processes { get; } = [];

    public ProcessItemViewModel? SelectedProcess
    {
        get;
        set => SetField(ref field, value);
    }

    public ICommand AttachEmulatorProcessCommand { get; }
    public ICommand CancelCommand { get; }

    public void LoadProcesses()
    {
        List<Process> currentProcesses = Process
            .GetProcesses()
            .Where(p => p.MainWindowHandle != IntPtr.Zero && p.MainWindowTitle != "Digimon World Tool")
            .OrderBy(p => p.ProcessName)
            .ToList();

        for (int i = Processes.Count - 1; i >= 0; i--)
        {
            if (currentProcesses.All(p => p.Id != Processes[i].Process.Id))
            {
                Processes.RemoveAt(i);
            }
        }

        foreach (Process process in currentProcesses.Where(process => Processes.All(p => p.Process.Id != process.Id)))
        {
            Processes.Add(new ProcessItemViewModel(process));
        }
    }

    private void AttachEmulatorProcess()
    {
        _memorySyncSubscription?.Dispose();

        if (SelectedProcess == null)
        {
            return;
        }

        UserConfigurationManager.SetEmulatorProcessName(SelectedProcess.Name);

        _owner.DialogResult = true;
    }

    private void Cancel()
    {
        _memorySyncSubscription?.Dispose();

        _owner.DialogResult = false;
    }

    public void Dispose() => _memorySyncSubscription?.Dispose();
}