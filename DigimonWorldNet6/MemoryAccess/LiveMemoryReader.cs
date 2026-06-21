using System.ComponentModel;
using System.Diagnostics;
using System.Reactive.Disposables;
using MemoryAccess.Core;
using MemoryAccess.MemoryValues.Digimon;
using MemoryAccess.MemoryValues.Evolution;
using MemoryAccess.MemoryValues.Tamer;
using MemoryAccess.MemoryValues.Technical;
using MemoryAccess.MemoryValues.World;
using Shared.Services.Events;

namespace MemoryAccess;

public sealed class LiveMemoryReader : INotifyPropertyChanged, IDisposable
{
    private static readonly Lazy<LiveMemoryReader> _instance = new(() => new LiveMemoryReader());

    private readonly CompositeDisposable _disposables;

    private CancellationTokenSource? _cts;
    private string _emulatorProcessName = "duckstation-qt-x64-ReleaseLTCG";

    private readonly SerialDisposable _parameterStats = new();
    private readonly SerialDisposable _conditionStats = new();
    private readonly SerialDisposable _profileStats = new();
    private readonly SerialDisposable _combatStats = new();
    private readonly SerialDisposable _inventoryStats = new();
    private readonly SerialDisposable _careStats = new();
    private readonly SerialDisposable _techniqueStats = new();
    private readonly SerialDisposable _historicEvolutions = new();
    private readonly SerialDisposable _worldTime = new();
    private readonly SerialDisposable _recruitment = new();
    private readonly SerialDisposable _tamer = new();
    private readonly SerialDisposable _technical = new();

    public static LiveMemoryReader Instance => _instance.Value;

    private LiveMemoryReader()
    {
        _disposables = new CompositeDisposable(
            EmulatorLinkEventHub.EmulatorProcessNameChangedObservable.Subscribe(OnEmulatorProcessNameChanged),
            EmulatorLinkEventHub.EmulatorReconnectRequestedObservable.Subscribe(_ => Start())
        );

        _parameterStats.Disposable = ParameterStats.Empty;
        _conditionStats.Disposable = ConditionStats.Empty;
        _profileStats.Disposable = ProfileStats.Empty;
        _combatStats.Disposable = CombatStats.Empty;
        _inventoryStats.Disposable = InventoryStats.Empty;
        _careStats.Disposable = CareStats.Empty;
        _techniqueStats.Disposable = TechniqueStats.Empty;
        _historicEvolutions.Disposable = HistoricEvolutions.Empty;
        _worldTime.Disposable = WorldTime.Empty;
        _recruitment.Disposable = Recruitment.Empty;
        _tamer.Disposable = Tamer.Empty;
        _technical.Disposable = Technical.Empty;
    }

    public ParameterStats ParameterStats
    {
        get => (ParameterStats)_parameterStats.Disposable!;
        private set => _parameterStats.Disposable = value;
    }

    public ConditionStats ConditionStats
    {
        get => (ConditionStats)_conditionStats.Disposable!;
        private set => _conditionStats.Disposable = value;
    }

    public ProfileStats ProfileStats
    {
        get => (ProfileStats)_profileStats.Disposable!;
        private set => _profileStats.Disposable = value;
    }

    public CombatStats CombatStats
    {
        get => (CombatStats)_combatStats.Disposable!;
        private set => _combatStats.Disposable = value;
    }

    public InventoryStats InventoryStats
    {
        get => (InventoryStats)_inventoryStats.Disposable!;
        private set => _inventoryStats.Disposable = value;
    }

    public CareStats CareStats
    {
        get => (CareStats)_careStats.Disposable!;
        private set => _careStats.Disposable = value;
    }

    public TechniqueStats TechniqueStats
    {
        get => (TechniqueStats)_techniqueStats.Disposable!;
        private set => _techniqueStats.Disposable = value;
    }

    public HistoricEvolutions HistoricEvolutions
    {
        get => (HistoricEvolutions)_historicEvolutions.Disposable!;
        private set => _historicEvolutions.Disposable = value;
    }

    public WorldTime WorldTime
    {
        get => (WorldTime)_worldTime.Disposable!;
        private set => _worldTime.Disposable = value;
    }

    public Recruitment Recruitment
    {
        get => (Recruitment)_recruitment.Disposable!;
        private set => _recruitment.Disposable = value;
    }

    public Tamer Tamer
    {
        get => (Tamer)_tamer.Disposable!;
        private set => _tamer.Disposable = value;
    }

    public Technical Technical
    {
        get => (Technical)_technical.Disposable!;
        private set => _technical.Disposable = value;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

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

    private void OnEmulatorProcessNameChanged(string emulatorProcessName)
    {
        if (_emulatorProcessName == emulatorProcessName)
        {
            return;
        }

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
                        ResetMemorySyncs();
                        attachedProcess = null;
                    }

                    await Task.Delay(1000, token);
                    continue;
                }

                if (attachedProcess == null ||
                    attachedProcess.HasExited ||
                    attachedProcess.Id != proc.Id)
                {
                    Attach(proc);

                    if (!AreParameterStatsWithinExpectedRanges())
                    {
                        ResetMemorySyncs();
                        EmulatorLinkEventHub.SignalEmulatorInvalidRomDetected();
                        break;
                    }

                    Connected = true;
                    attachedProcess = proc;
                }

                if (!AreParameterStatsWithinExpectedRanges())
                {
                    Start();
                }
                
                await Task.Delay(1000, token);
            }
            catch (TaskCanceledException)
            {
                break;
            }
            catch (Exception)
            {
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
        CombatStats = new CombatStats(mem, ram);
        CombatStats.UpdateData();
        InventoryStats = new InventoryStats(mem, ram);
        InventoryStats.UpdateData();
        CareStats = new CareStats(mem, ram);
        CareStats.UpdateData();
        TechniqueStats = new TechniqueStats(mem, ram);
        TechniqueStats.UpdateData();
        HistoricEvolutions = new HistoricEvolutions(mem, ram);
        HistoricEvolutions.UpdateData();
        Recruitment = new Recruitment(mem, ram);
        Recruitment.UpdateData();
        Tamer = new Tamer(mem, ram);
        Tamer.UpdateData();
        Technical = new Technical(mem, ram);
        Technical.UpdateData();

        try
        {
            WorldTime = new WorldTime(mem, ram);
            WorldTime.UpdateData();
        }
        catch
        {
            WorldTime = WorldTime.Empty;
        }
    }

    private void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    private bool AreParameterStatsWithinExpectedRanges()
    {
        short hp = ParameterStats.Hp;
        short mp = ParameterStats.Mp;
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

    private void ResetMemorySyncs()
    {
        ParameterStats = ParameterStats.Empty;
        ConditionStats = ConditionStats.Empty;
        ProfileStats = ProfileStats.Empty;
        CombatStats = CombatStats.Empty;
        InventoryStats = InventoryStats.Empty;
        CareStats = CareStats.Empty;
        TechniqueStats = TechniqueStats.Empty;
        HistoricEvolutions = HistoricEvolutions.Empty;
        WorldTime = WorldTime.Empty;
        Tamer = Tamer.Empty;
        Technical = Technical.Empty;
    }

    public void Dispose()
    {
        _disposables.Dispose();
        _cts?.Cancel();
        _cts?.Dispose();
    }
}