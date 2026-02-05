using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using DigimonWorld.Frontend.WPF.Windows.GeneralConfig.Utility;
using Shared.Services;

namespace DigimonWorld.Frontend.WPF.Windows.GeneralConfig.Dialogs;

public class EmulatorProcessPickerViewModel : BaseViewModel
{
    private readonly Window _owner;

    public EmulatorProcessPickerViewModel(Window owner)
    {
        _owner = owner;

        SetEmulatorProcessNameCommand = new CommandHandler(SetEmulatorProcessName);

        CancelCommand = new CommandHandler(Cancel);
    }

    public ObservableCollection<ProcessItemViewModel> Processes { get; } = [];

    public ProcessItemViewModel? SelectedProcess
    {
        get ;
        set => SetField(ref field, value);
    }

    public ICommand SetEmulatorProcessNameCommand { get; }
    public ICommand CancelCommand { get; }

    public void LoadProcesses()
    {
        Processes.Clear();

        foreach (Process p in Process
                     .GetProcesses()
                     .Where(p => p.MainWindowHandle != IntPtr.Zero)
                     .OrderBy(p => p.ProcessName))
        {
            Processes.Add(new ProcessItemViewModel(p));
        }
    }

    private void SetEmulatorProcessName()
    {
        if (SelectedProcess == null)
        {
            return;
        }

        UserConfigurationManager.SetEmulatorProcessName(SelectedProcess.Name);

        _owner.DialogResult = true;
    }

    private void Cancel() => _owner.DialogResult = false;
}