using System;
using System.Reactive.Disposables;
using System.Windows.Input;
using System.Windows.Threading;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using MemoryAccess;
using Shared.Services;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EmulatorLink;

public class EmulatorLinkViewModel : BaseViewModel, IDisposable
{
    private readonly CompositeDisposable _disposable;
    private DispatcherTimer _memorySyncTimer = null!;

    public EmulatorLinkViewModel()
    {
        // Profile stats
        SignalSetAllEmulatorProfileStatsCommand = new CommandHandler(EventHub.SignalSetAllEmulatorProfileStats);
        SetDigimonTypeFromEmulator = new CommandHandler(EventHub.SignalSetEmulatorDigimonType);
        SetDigimonWeightFromEmulator = new CommandHandler(EventHub.SignalSetEmulatorWeight);

        // Parameter stats
        SignalSetAllEmulatorParameterStatsCommand = new CommandHandler(EventHub.SignalSetAllEmulatorParameterStats);
        SignalSetEmulatorHPCommand = new CommandHandler(EventHub.SignalSetEmulatorHP);
        SignalSetEmulatorMPCommand = new CommandHandler(EventHub.SignalSetEmulatorMP);
        SignalSetEmulatorOffCommand = new CommandHandler(EventHub.SignalSetEmulatorOff);
        SignalSetEmulatorDefCommand = new CommandHandler(EventHub.SignalSetEmulatorDef);
        SignalSetEmulatorSpdCommand = new CommandHandler(EventHub.SignalSetEmulatorSpd);
        SignalSetEmulatorBrnCommand = new CommandHandler(EventHub.SignalSetEmulatorBrn);

        // Condition stats
        SignalSetAllEmulatorConditionStatsCommand = new CommandHandler(EventHub.SignalSetAllEmulatorConditionStats);
        SetDigimonHappinessFromEmulator = new CommandHandler(EventHub.SignalSetEmulatorHappiness);
        SetDigimonDisciplineFromEmulator = new CommandHandler(EventHub.SignalSetEmulatorDiscipline);
        SetDigimonCareMistakesFromEmulator = new CommandHandler(EventHub.SignalSetEmulatorCareMistakes);
        SetDigimonTechniquesCountFromEmulator = new CommandHandler(EventHub.SignalSetEmulatorTechniqueCount);
        SetDigimonBattlesCountFromEmulator = new CommandHandler(EventHub.SignalSetEmulatorBattlesCount);

        SetupMemorySyncTimer();

        _disposable = new CompositeDisposable(
            EventHub.EmulatorConnectedObservable.Subscribe(_ => OnEmulatorConnected()),
            EventHub.EmulatorDisconnectedObservable.Subscribe(_ => OnEmulatorDisconnected())
        );
    }
    
    public bool EmulatorConnected
    {
        get;
        private set => SetField(ref field, value);
    }

    // Profile stats
    public ICommand SignalSetAllEmulatorProfileStatsCommand { get; }
    public ICommand SetDigimonTypeFromEmulator { get; }
    public ICommand SetDigimonWeightFromEmulator { get; }


    // Parameter stats
    public ICommand SignalSetAllEmulatorParameterStatsCommand { get; }
    public ICommand SignalSetEmulatorHPCommand { get; }
    public ICommand SignalSetEmulatorMPCommand { get; }
    public ICommand SignalSetEmulatorOffCommand { get; }
    public ICommand SignalSetEmulatorDefCommand { get; }
    public ICommand SignalSetEmulatorSpdCommand { get; }
    public ICommand SignalSetEmulatorBrnCommand { get; }

    // Condition stats
    public ICommand SignalSetAllEmulatorConditionStatsCommand { get; }
    public ICommand SetDigimonHappinessFromEmulator { get; }
    public ICommand SetDigimonDisciplineFromEmulator { get; }
    public ICommand SetDigimonCareMistakesFromEmulator { get; }
    public ICommand SetDigimonTechniquesCountFromEmulator { get; }
    public ICommand SetDigimonBattlesCountFromEmulator { get; }
    
    public double SyncColumnWidth => 120;
    public double RectangularButtonWidth => 97;
    public double SquareButtonWidth => 45;
    public double ButtonHeight => 45;

    public bool IsHappy
    {
        get;
        private set => SetField(ref field, value);
    }

    public bool IsDisciplined
    {
        get;
        private set => SetField(ref field, value);
    }

    private void SetupMemorySyncTimer()
    {
        _memorySyncTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(500)
        };

        _memorySyncTimer.Tick += (_, _) => SyncFromMemory();
    }

    private void OnEmulatorConnected()
    {
        SyncFromMemory();
        _memorySyncTimer.Start();

        EmulatorConnected = true;
    }

    private void OnEmulatorDisconnected()
    {
        _memorySyncTimer.Stop();

        EmulatorConnected = false;
    }

    private void SyncFromMemory()
    {
        DispatcherTimer timer = new()
        {
            Interval = TimeSpan.FromMilliseconds(500)
        };

        timer.Tick += (_, _) => RefreshFromMemory();
        timer.Start();
    }

    private void RefreshFromMemory()
    {
        IsHappy = LiveMemoryReader.Instance.DigimonConditionStats.Happiness >= 0;
        IsDisciplined = LiveMemoryReader.Instance.DigimonConditionStats.Discipline >= 50;
    }

    public void Dispose() => _disposable.Dispose();
}