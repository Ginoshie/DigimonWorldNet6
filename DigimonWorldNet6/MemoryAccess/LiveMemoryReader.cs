using System.ComponentModel;
using System.Diagnostics;
using System.Reactive.Disposables;
using MemoryAccess.Core;
using MemoryAccess.MemoryValues;
using MemoryAccess.MemoryValues.Digimon;
using MemoryAccess.MemoryValues.Evolution;
using Shared.Services.Events;

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
            EmulatorLinkEventHub.EmulatorProcessNameChangedObservable.Subscribe(OnEmulatorProcessNameChanged)
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
                EmulatorLinkEventHub.SignalEmulatorConnected();
            }
            else
            {
                EmulatorLinkEventHub.SignalEmulatorDisconnected();
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

    private void OnEmulatorProcessNameChanged(string emulatorProcessName)
    {
        if (_emulatorProcessName == emulatorProcessName)
        {
            return;
        }

        Stop();

        _emulatorProcessName = emulatorProcessName;

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

        ParameterStats = new ParameterStats(mem, ram);
        ConditionStats = new ConditionStats(mem, ram);
        ProfileStats = new ProfileStats(mem, ram);
        CareStats = new CareStats(mem, ram);
        TechniqueStats = new TechniqueStats(mem, ram);
        HistoricEvolutions = new HistoricEvolutions(mem, ram);
    }

    public ParameterStats ParameterStats { get; private set; } = ParameterStats.Empty;
    public ConditionStats ConditionStats { get; private set; } = ConditionStats.Empty;
    public ProfileStats ProfileStats { get; private set; } = ProfileStats.Empty;
    public CareStats CareStats { get; private set; } = CareStats.Empty;
    public TechniqueStats TechniqueStats { get; private set; } = TechniqueStats.Empty;
    public HistoricEvolutions HistoricEvolutions { get; private set; } = HistoricEvolutions.Empty;

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