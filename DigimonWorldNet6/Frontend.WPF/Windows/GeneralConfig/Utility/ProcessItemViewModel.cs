using System.Diagnostics;
using System.Windows.Media;

namespace DigimonWorld.Frontend.WPF.Windows.GeneralConfig.Utility;

public class ProcessItemViewModel(Process process)
{
    public Process Process { get; } = process;
    public string Name => Process.ProcessName;
    public string Title => Process.MainWindowTitle;
    public ImageSource? Icon { get; } = ProcessIconHelper.GetIcon(process);
}