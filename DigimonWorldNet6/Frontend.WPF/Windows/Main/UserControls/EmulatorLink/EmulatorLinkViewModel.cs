using System;
using System.Reactive.Disposables;
using System.Windows.Input;
using System.Windows.Threading;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using MemoryAccess;
using Shared.Services;
using Shared.Services.Events;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EmulatorLink;

public class EmulatorLinkViewModel : BaseViewModel, IDisposable
{
    private readonly CompositeDisposable _disposable;
    private DispatcherTimer _memorySyncTimer = null!;

    public EmulatorLinkViewModel()
    {
        // Profile stats
        SignalSetAllEmulatorProfileStatsCommand = new CommandHandler(DigimonStatsEventHub.SignalSetAllEmulatorProfileStats);
        SetDigimonTypeFromEmulator = new CommandHandler(DigimonStatsEventHub.SignalSetEmulatorDigimonType);
        SetDigimonWeightFromEmulator = new CommandHandler(DigimonStatsEventHub.SignalSetEmulatorWeight);

        // Parameter stats
        SignalSetAllEmulatorParameterStatsCommand = new CommandHandler(DigimonStatsEventHub.SignalSetAllEmulatorParameterStats);
        SignalSetEmulatorHPCommand = new CommandHandler(DigimonStatsEventHub.SignalSetEmulatorHP);
        SignalSetEmulatorMPCommand = new CommandHandler(DigimonStatsEventHub.SignalSetEmulatorMP);
        SignalSetEmulatorOffCommand = new CommandHandler(DigimonStatsEventHub.SignalSetEmulatorOff);
        SignalSetEmulatorDefCommand = new CommandHandler(DigimonStatsEventHub.SignalSetEmulatorDef);
        SignalSetEmulatorSpdCommand = new CommandHandler(DigimonStatsEventHub.SignalSetEmulatorSpd);
        SignalSetEmulatorBrnCommand = new CommandHandler(DigimonStatsEventHub.SignalSetEmulatorBrn);

        // Condition stats
        SignalSetAllEmulatorConditionStatsCommand = new CommandHandler(DigimonStatsEventHub.SignalSetAllEmulatorConditionStats);
        SetDigimonHappinessFromEmulator = new CommandHandler(DigimonStatsEventHub.SignalSetEmulatorHappiness);
        SetDigimonDisciplineFromEmulator = new CommandHandler(DigimonStatsEventHub.SignalSetEmulatorDiscipline);
        SetDigimonCareMistakesFromEmulator = new CommandHandler(DigimonStatsEventHub.SignalSetEmulatorCareMistakes);
        SetDigimonTechniquesCountFromEmulator = new CommandHandler(DigimonStatsEventHub.SignalSetEmulatorTechniqueCount);
        SetDigimonBattlesCountFromEmulator = new CommandHandler(DigimonStatsEventHub.SignalSetEmulatorBattlesCount);

        SetupMemorySyncTimer();

        _disposable = new CompositeDisposable(
            EmulatorLinkEventHub.EmulatorConnectedObservable.Subscribe(_ => OnEmulatorConnected()),
            EmulatorLinkEventHub.EmulatorDisconnectedObservable.Subscribe(_ => OnEmulatorDisconnected())
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