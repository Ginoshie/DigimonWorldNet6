using System.ComponentModel;
using System.Diagnostics;
using System.Reactive.Disposables;
using MemoryAccess.Core;
using MemoryAccess.MemoryValues;
using Shared.Configuration;
using Shared.Services;

namespace MemoryAccess;

public sealed class LiveMemoryReader : INotifyPropertyChanged, IDisposable
{
    private static readonly Lazy<LiveMemoryReader> _instance = new(() => new LiveMemoryReader());

    private readonly CompositeDisposable _disposables;

    private CancellationTokenSource? _cts;
    private string _emulatorProcessName = "duckstation-qt-x64-ReleaseLTCG";

    public static LiveMemoryReader Instance => _instance.Value;

    private LiveMemoryReader()
    {
        _disposables = new CompositeDisposable(
            UserConfigurationManager.CurrentEmulatorLinkConfig.Subscribe(OnEmulatorLinkConfigurationChanged)
        );
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
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = new CancellationTokenSource();

        Task.Run(() => MonitorEmulatorAsync(_cts.Token));
    }

    public void Stop()
    {
        _cts?.Cancel();

        _cts?.Dispose();

        _cts = null;
    }

    private void OnEmulatorLinkConfigurationChanged(EmulatorLinkConfig emulatorLinkConfig)
    {
        Stop();

        _emulatorProcessName = emulatorLinkConfig.SelectedProcessName;

        Start();
    }

    private async Task MonitorEmulatorAsync(CancellationToken token)
    {
        Process? attachedProcess = null;

        while (!token.IsCancellationRequested)
        {
            try
            {
                Process? proc = Process.GetProcessesByName(_emulatorProcessName)
                    .FirstOrDefault(p => !p.HasExited);

                if (proc == null)
                {
                    if (Connected)
                    {
                        Connected = false;
                        attachedProcess = null;
                    }

                    await Task.Delay(1000, token);
                    continue;
                }

                if (attachedProcess == null || attachedProcess.HasExited)
                {
                    Attach(proc);
                    Connected = true;
                    attachedProcess = proc;
                }

                await Task.Delay(1000, token);
            }
            catch (TaskCanceledException)
            {
                Connected = false;
                await Task.Delay(1000, token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LiveMemoryReader exception: {ex}");
                Connected = false;
                await Task.Delay(1000, token);
            }
        }

        Connected = false;
    }

    private void Attach(Process proc)
    {
        ProcessMemory mem = new(proc);
        PsxRam ram = new(mem);

        DigimonParameterStats = new DigimonParameterStats(mem, ram);
        DigimonConditionStats = new DigimonConditionStats(mem, ram);
        DigimonProfileStats = new DigimonProfileStats(mem, ram);
        DigimonTechniqueStats = new DigimonTechniqueStats(mem, ram);
    }

    public DigimonParameterStats DigimonParameterStats { get; private set; } = DigimonParameterStats.Empty;
    public DigimonConditionStats DigimonConditionStats { get; private set; } = DigimonConditionStats.Empty;
    public DigimonProfileStats DigimonProfileStats { get; private set; } = DigimonProfileStats.Empty;
    public DigimonTechniqueStats DigimonTechniqueStats { get; private set; } = DigimonTechniqueStats.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    public void Dispose()
    {
        _disposables.Dispose();
        _cts?.Cancel();
        _cts?.Dispose();
    }
}