using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using MemoryAccess;
using Shared.Services.Events;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EmulatorLink;

public class EmulatorLinkViewModel : BaseViewModel, IDisposable
{
    private readonly CompositeDisposable _disposable;

    private IDisposable? _memorySyncSubscription;

    public EmulatorLinkViewModel()
    {
        // Profile stats
        SignalSetAllEmulatorProfileStatsCommand = new CommandHandler(DigimonStatsEventHub.SignalSyncAllEmulatorProfileStats);
        SetDigimonTypeFromEmulator = new CommandHandler(DigimonStatsEventHub.SignalSyncEmulatorDigimonType);
        SetDigimonWeightFromEmulator = new CommandHandler(DigimonStatsEventHub.SignalSyncEmulatorWeight);

        // Parameter stats
        SignalSetAllEmulatorParameterStatsCommand = new CommandHandler(DigimonStatsEventHub.SignalSyncAllEmulatorParameterStats);
        SignalSetEmulatorHPCommand = new CommandHandler(DigimonStatsEventHub.SignalSyncEmulatorHP);
        SignalSetEmulatorMPCommand = new CommandHandler(DigimonStatsEventHub.SignalSyncEmulatorMP);
        SignalSetEmulatorOffCommand = new CommandHandler(DigimonStatsEventHub.SignalSyncEmulatorOff);
        SignalSetEmulatorDefCommand = new CommandHandler(DigimonStatsEventHub.SignalSyncEmulatorDef);
        SignalSetEmulatorSpdCommand = new CommandHandler(DigimonStatsEventHub.SignalSyncEmulatorSpd);
        SignalSetEmulatorBrnCommand = new CommandHandler(DigimonStatsEventHub.SignalSyncEmulatorBrn);

        // Condition stats
        SignalSetAllEmulatorConditionStatsCommand = new CommandHandler(DigimonStatsEventHub.SignalSyncAllEmulatorConditionStats);
        SetDigimonHappinessFromEmulator = new CommandHandler(DigimonStatsEventHub.SignalSyncEmulatorHappiness);
        SetDigimonDisciplineFromEmulator = new CommandHandler(DigimonStatsEventHub.SignalSyncEmulatorDiscipline);
        SetDigimonCareMistakesFromEmulator = new CommandHandler(DigimonStatsEventHub.SignalSyncEmulatorCareMistakes);
        SetDigimonTechniquesCountFromEmulator = new CommandHandler(DigimonStatsEventHub.SignalSyncEmulatorTechniqueCount);
        SetDigimonBattlesCountFromEmulator = new CommandHandler(DigimonStatsEventHub.SignalSyncEmulatorBattlesCount);

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

    private void OnEmulatorConnected()
    {
        _memorySyncSubscription = Observable.Interval(TimeSpan.FromMilliseconds(500)).Subscribe(_ => SyncFromEmulator());

        EmulatorConnected = true;
    }

    private void OnEmulatorDisconnected()
    {
        _memorySyncSubscription?.Dispose();
        
        EmulatorConnected = false;
    }

    private void SyncFromEmulator()
    {
        IsHappy = LiveMemoryReader.Instance.DigimonConditionStats.Happiness >= 0;
        IsDisciplined = LiveMemoryReader.Instance.DigimonConditionStats.Discipline >= 50;
    }

    public void Dispose()
    {
        _disposable.Dispose();
        
        _memorySyncSubscription?.Dispose();
    }
}