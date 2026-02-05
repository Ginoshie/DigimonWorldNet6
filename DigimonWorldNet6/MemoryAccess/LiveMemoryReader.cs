using System.ComponentModel;
using System.Diagnostics;
using MemoryAccess.Core;
using MemoryAccess.MemoryValues;
using Shared.Services;

namespace MemoryAccess;

public sealed class LiveMemoryReader : INotifyPropertyChanged
{
    private static readonly Lazy<LiveMemoryReader> _instance =
        new(() => new LiveMemoryReader());

    public static LiveMemoryReader Instance => _instance.Value;

    private CancellationTokenSource? _cts;

    private LiveMemoryReader()
    {
    }

    public bool Connected
    {
        get;
        private set
        {
            if (field == value)
            {
                return;
            }

            field = value;

            if (field)
            {
                EventHub.SignalEmulatorConnected();
            }
            else
            {
                EventHub.SignalEmulatorDisconnected();
            }
                
            OnPropertyChanged(nameof(Connected));
        }
    }

    public void Start()
    {
        _cts ??= new CancellationTokenSource();
        Task.Run(() => MonitorEmulatorAsync(_cts.Token));
    }

    public void Stop() => _cts?.Cancel();

    private async Task MonitorEmulatorAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            try
            {
                Process? proc = FindDuckStation();

                if (proc == null)
                {
                    Connected = false;
                    await Task.Delay(1000, token);
                    continue;
                }

                Attach(proc);
                Connected = true;

                await WaitForExitAsync(proc, token);

                Connected = false;
            }
            catch
            {
                Connected = false;
                await Task.Delay(1000, token);
            }
        }
    }

    private static Process? FindDuckStation() =>
        Process
            .GetProcessesByName("duckstation-qt-x64-ReleaseLTCG")
            .FirstOrDefault();

    private void Attach(Process proc)
    {
        ProcessMemory mem = new(proc);
        PsxRam ram = new(mem);

        DigimonParameterStats = new DigimonParameterStats(mem, ram);
        DigimonConditionStats = new DigimonConditionStats(mem, ram);
        DigimonProfileStats = new DigimonProfileStats(mem, ram);
        DigimonTechniqueStats = new DigimonTechniqueStats(mem, ram);
    }

    private static Task WaitForExitAsync(Process process, CancellationToken token)
    {
        TaskCompletionSource tcs = new();

        process.EnableRaisingEvents = true;
        process.Exited += (_, _) => tcs.TrySetResult();

        token.Register(() => tcs.TrySetCanceled());

        return tcs.Task;
    }

    public DigimonParameterStats DigimonParameterStats { get; private set; } = DigimonParameterStats.Empty;
    public DigimonConditionStats DigimonConditionStats { get; private set; } = DigimonConditionStats.Empty;
    public DigimonProfileStats DigimonProfileStats { get; private set; } = DigimonProfileStats.Empty;
    public DigimonTechniqueStats DigimonTechniqueStats { get; private set; } = DigimonTechniqueStats.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}