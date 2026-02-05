using System.Diagnostics;
using System.Windows.Media;

namespace DigimonWorld.Frontend.WPF.Windows.GeneralConfig.Utility;

public class ProcessItemViewModel
{
    public Process Process { get; }
    public string Name => Process.ProcessName;
    public string Title => Process.MainWindowTitle;
    public ImageSource? Icon { get; }

    public ProcessItemViewModel(Process process)
    {
        Process = process;
        Icon = ProcessIconHelper.GetIcon(process);
    }
}