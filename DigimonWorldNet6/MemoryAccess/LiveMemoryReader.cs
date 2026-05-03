using System.ComponentModel;
using System.Diagnostics;
using System.Reactive.Disposables;
using MemoryAccess.Core;
using MemoryAccess.MemoryValues.Digimon;
using MemoryAccess.MemoryValues.Evolution;
using MemoryAccess.MemoryValues.World;
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
            EmulatorLinkEventHub.EmulatorProcessNameChangedObservable.Subscribe(OnEmulatorProcessNameChanged),
            EmulatorLinkEventHub.EmulatorReconnectRequestedObservable.Subscribe(_ => Reconnect())
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

            EmulatorLinkEventHub.SignalEmulatorConnected(field);

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

    private void Reconnect()
    {
        Stop();
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

                    if (!AreParameterStatsWithinExpectedRanges())
                    {
                        ResetStats();
                        EmulatorLinkEventHub.SignalEmulatorInvalidRomDetected();
                        break;
                    }

                    Connected = true;
                    attachedProcess = proc;
                }

                await Task.Delay(1000, token);
            }
            catch (TaskCanceledException)
            {
                break;
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
        ParameterStats.UpdateData();
        ConditionStats = new ConditionStats(mem, ram);
        ConditionStats.UpdateData();
        ProfileStats = new ProfileStats(mem, ram);
        ProfileStats.UpdateData();
        CareStats = new CareStats(mem, ram);
        CareStats.UpdateData();
        TechniqueStats = new TechniqueStats(mem, ram);
        TechniqueStats.UpdateData();
        HistoricEvolutions = new HistoricEvolutions(mem, ram);
        HistoricEvolutions.UpdateData();

        try
        {
            WorldTime = new WorldTime(mem, ram);
            WorldTime.UpdateData();
        } catch
        {
            WorldTime = WorldTime.Empty;
        }
    }

    public ParameterStats ParameterStats { get; private set; } = ParameterStats.Empty;
    public ConditionStats ConditionStats { get; private set; } = ConditionStats.Empty;
    public ProfileStats ProfileStats { get; private set; } = ProfileStats.Empty;
    public CareStats CareStats { get; private set; } = CareStats.Empty;
    public TechniqueStats TechniqueStats { get; private set; } = TechniqueStats.Empty;
    public HistoricEvolutions HistoricEvolutions { get; private set; } = HistoricEvolutions.Empty;
    public WorldTime WorldTime { get; private set; } = WorldTime.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    private bool AreParameterStatsWithinExpectedRanges()
    {
        short hp = ParameterStats.HP;
        short mp = ParameterStats.MP;
        short off = ParameterStats.Offense;
        short def = ParameterStats.Defense;
        short spd = ParameterStats.Speed;
        short brn = ParameterStats.Brains;

        return hp is >= 1 and <= 9999
            && mp is >= 1 and <= 9999
            && off is >= 1 and <= 999
            && def is >= 1 and <= 999
            && spd is >= 1 and <= 999
            && brn is >= 1 and <= 999;
    }

    private void ResetStats()
    {
        ParameterStats = ParameterStats.Empty;
        ConditionStats = ConditionStats.Empty;
        ProfileStats = ProfileStats.Empty;
        CareStats = CareStats.Empty;
        TechniqueStats = TechniqueStats.Empty;
        HistoricEvolutions = HistoricEvolutions.Empty;
        WorldTime = WorldTime.Empty;
    }

    public void Dispose()
    {
        _disposables.Dispose();
        _cts?.Cancel();
        _cts?.Dispose();
    }
}