using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Input;
using DigimonWorld.Evolution.Calculator.Core;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.Enums;
using DigimonWorld.Frontend.WPF.Services;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using Shared.Configuration;
using Shared.Constants;
using Shared.Enums;
using Shared.Services;
using Shared.Services.Events;
using GameVariant = Shared.Enums.GameVariant;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvolutionCalculator;

public sealed class EvolutionCalculatorViewModel : BaseViewModel, IDisposable
{
    private readonly SpeakingSimulator _speakingSimulator;
    private readonly CompositeDisposable _compositeDisposable;
    private readonly SynchronizationContext _uiSynchronizationContext;

    private EvolutionResult _evolutionResult = EvolutionResult.Unknown;
    private GameVariant _gameVariant = GameVariant.Original;
    private bool _emulatorIsConnected;
    private EmulatorLinkSyncMode _emulatorSyncMode;

    public EvolutionCalculatorViewModel()
    {
        _uiSynchronizationContext = SynchronizationContext.Current!;
        _speakingSimulator = new SpeakingSimulator();

        _compositeDisposable = new CompositeDisposable(
            _speakingSimulator,

            // Profile
            DigimonStatsEventHub.SyncAllEmulatorProfileStatsObservable.Subscribe(_ => SyncAllProfileStats()),
            DigimonStatsEventHub.SyncEmulatorDigimonTypeObservable.Subscribe(_ => SetEmulatorDigimonType()),
            DigimonStatsEventHub.SyncEmulatorWeightObservable.Subscribe(_ => SetEmulatorWeight()),

            // Parameter
            DigimonStatsEventHub.SyncAllEmulatorParameterStatsObservable.Subscribe(_ => SyncAllEmulatorCombatStats()),
            DigimonStatsEventHub.SyncEmulatorHpObservable.Subscribe(_ => SyncEmulatorHp()),
            DigimonStatsEventHub.SyncEmulatorMpObservable.Subscribe(_ => SyncEmulatorMp()),
            DigimonStatsEventHub.SyncEmulatorOffObservable.Subscribe(_ => SyncEmulatorOff()),
            DigimonStatsEventHub.SyncEmulatorDefObservable.Subscribe(_ => SyncEmulatorDef()),
            DigimonStatsEventHub.SyncEmulatorSpdObservable.Subscribe(_ => SyncEmulatorSpeed()),
            DigimonStatsEventHub.SyncEmulatorBrnObservable.Subscribe(_ => SyncEmulatorBrains()),

            // Condition
            DigimonStatsEventHub.SyncAllEmulatorConditionStatsObservable.Subscribe(_ => SyncAllEmulatorConditionStats()),
            DigimonStatsEventHub.SyncEmulatorHappinessObservable.Subscribe(_ => SyncEmulatorHappiness()),
            DigimonStatsEventHub.SyncEmulatorDisciplineObservable.Subscribe(_ => SyncEmulatorDiscipline()),
            DigimonStatsEventHub.SyncEmulatorCareMistakesObservable.Subscribe(_ => SyncEmulatorCareMistakes()),
            DigimonStatsEventHub.SyncEmulatorTechniqueCountObservable.Subscribe(_ => SyncEmulatorTechniqueCount()),
            DigimonStatsEventHub.SyncEmulatorBattlesCountObservable.Subscribe(_ => SyncEmulatorBattlesCount()),

            // Emulator Link
            EmulatorLinkEventHub.EmulatorLinkSyncModeChangedObservable.Subscribe(UpdateEmulatorLinkSyncMode),
            EmulatorLinkEventHub.EmulatorConnectedObservable.Subscribe(OnEmulatorConnectedChanged),

            // UserConfig
            UserConfigurationManager.CurrentEvolutionCalculatorConfig.Subscribe(UpdateAvailableDigimon),

            // Memory sync
            _memorySyncDisposable
        );

        SetEvolutionResult = new CommandHandler(CalculateEvolutionResult);

        InstantDisplayCommand = new CommandHandler(InstantDisplay);

        SpeechDelay initialDelay = UserConfigurationManager.SpeakingSimulatorConfig.NarratorMode == NarratorMode.Instant ? SpeechDelay.None : SpeechDelay.Long;

        _emulatorSyncMode = UserConfigurationManager.EmulatorLinkConfig.EmulatorLinkSyncMode;

        SyncAllFromCurrentMemoryState();

        _ = _speakingSimulator.SpeakAsync(
            JijimonEvolutionCalculatorNarratorText.INTRO_TEXT,
            textOutput => JijimonText = textOutput,
            initialDelay);
    }

    private void SyncAllFromCurrentMemoryState()
    {
        if (!ServiceRelay.LiveMemoryReader.Connected)
        {
            return;
        }

        _emulatorIsConnected = true;

        if (_emulatorSyncMode != EmulatorLinkSyncMode.Auto)
        {
            return;
        }

        try
        {
            SyncAllProfileStats();
            SyncAllEmulatorCombatStats();
            SyncAllEmulatorConditionStats();
        }
        catch (Exception)
        {
            // Memory data may not be available yet — that's fine, the sync timer will pick it up
        }

        StartMemorySync();
    }

    public ICommand SetEvolutionResult { get; }

    public ICommand InstantDisplayCommand { get; }

    public string CalculateButtonText => UiText.CALCULATE_BUTTON_TEXT;

    public string JijimonText
    {
        get;
        set => SetField(ref field, value);
    } = string.Empty;

    public DigimonName PlayerDigimonType
    {
        get;
        set
        {
            if (field == value)
            {
                return;
            }

            field = value;

            EvolutionResult = EvolutionResult.Unknown;

            FlipToRight = false;

            OnPropertyChanged();
        }
    }

    public string Hp
    {
        get;
        set => SetField(ref field, value);
    } = "0";

    public string Mp
    {
        get;
        set => SetField(ref field, value);
    } = "0";

    public string Off
    {
        get;
        set => SetField(ref field, value);
    } = "0";

    public string Def
    {
        get;
        set => SetField(ref field, value);
    } = "0";

    public string Speed
    {
        get;
        set => SetField(ref field, value);
    } = "0";

    public string Brains
    {
        get;
        set => SetField(ref field, value);
    } = "0";

    public string CareMistakes
    {
        get;
        set => SetField(ref field, value);
    } = "0";

    public string Weight
    {
        get;
        set => SetField(ref field, value);
    } = "0";

    public string Happiness
    {
        get;
        set => SetField(ref field, value);
    } = "0";

    public string Discipline
    {
        get;
        set => SetField(ref field, value);
    } = "0";

    public string Battles
    {
        get;
        set => SetField(ref field, value);
    } = "0";

    public string Techniques
    {
        get;
        set => SetField(ref field, value);
    } = "0";

    public EvolutionResult EvolutionResult
    {
        get => _evolutionResult;
        set
        {
            if (_evolutionResult == value)
            {
                return;
            }

            _evolutionResult = value;

            _ = _speakingSimulator.SpeakAsync(JijimonEvolutionCalculatorNarratorText.EvolutionResultCalculated(value), textOutput => JijimonText = textOutput, SpeechDelay.None, () => OnPropertyChanged());
        }
    }

    public bool FlipToRight
    {
        get;
        set
        {
            if (field == value)
            {
                return;
            }

            field = value;
            OnPropertyChanged();
        }
    }

    public IEnumerable<DigimonName> AvailableDigimonTypes
    {
        get;
        private set
        {
            if (Equals(field, value))
            {
                return;
            }

            field = value;
            OnPropertyChanged();
        }
    } = [];

    private void CalculateEvolutionResult()
    {
        _evolutionResult = EvolutionResult.Unknown;

        OnPropertyChanged(nameof(EvolutionResult));

        EvolutionCalculationInput input = new(PlayerDigimonType, int.Parse(Hp), int.Parse(Mp), int.Parse(Off), int.Parse(Def), int.Parse(Speed), int.Parse(Brains), int.Parse(CareMistakes), int.Parse(Weight), int.Parse(Happiness),
            int.Parse(Discipline),
            int.Parse(Battles), int.Parse(Techniques));

        EvolutionResult = input.EvolutionStage == EvolutionStage.Ultimate
            ? EvolutionResult.NotApplicable
            : ServiceRelay.CalculateEvolutionResult(input);

        FlipToRight = true;
    }

    private void InstantDisplay() => _speakingSimulator.RequestInstantDisplay();

    // Profile
    private void SyncAllProfileStats()
    {
        SetEmulatorWeight();
        SetEmulatorDigimonType();
    }

    private void SetEmulatorWeight() => Weight = ServiceRelay.LiveMemoryReader.ProfileStats.Weight.ToString();
    private void SetEmulatorDigimonType()
    {
        if (ServiceRelay.LiveMemoryReader.ProfileStats.DigimonType != 0)
        {
            PlayerDigimonType = DigimonTypes.Get(ServiceRelay.LiveMemoryReader.ProfileStats.DigimonType, _gameVariant).DigimonName;
        }
    }

    // Parameter
    private void SyncAllEmulatorCombatStats()
    {
        SyncEmulatorHp();
        SyncEmulatorMp();
        SyncEmulatorOff();
        SyncEmulatorDef();
        SyncEmulatorSpeed();
        SyncEmulatorBrains();
    }

    private void SyncEmulatorHp() => Hp = ServiceRelay.LiveMemoryReader.ParameterStats.Hp.ToString();
    private void SyncEmulatorMp() => Mp = ServiceRelay.LiveMemoryReader.ParameterStats.Mp.ToString();
    private void SyncEmulatorOff() => Off = ServiceRelay.LiveMemoryReader.ParameterStats.Offense.ToString();
    private void SyncEmulatorDef() => Def = ServiceRelay.LiveMemoryReader.ParameterStats.Defense.ToString();
    private void SyncEmulatorSpeed() => Speed = ServiceRelay.LiveMemoryReader.ParameterStats.Speed.ToString();
    private void SyncEmulatorBrains() => Brains = ServiceRelay.LiveMemoryReader.ParameterStats.Brains.ToString();

    // Condition
    private void SyncAllEmulatorConditionStats()
    {
        SyncEmulatorHappiness();
        SyncEmulatorDiscipline();
        SyncEmulatorCareMistakes();
        SyncEmulatorTechniqueCount();
        SyncEmulatorBattlesCount();
    }

    private void SyncEmulatorHappiness() => Happiness = ServiceRelay.LiveMemoryReader.ConditionStats.Happiness.ToString();
    private void SyncEmulatorDiscipline() => Discipline = ServiceRelay.LiveMemoryReader.ConditionStats.Discipline.ToString();
    private void SyncEmulatorCareMistakes() => CareMistakes = ServiceRelay.LiveMemoryReader.ConditionStats.CareMistakes.ToString();
    private void SyncEmulatorTechniqueCount() => Techniques = ServiceRelay.LiveMemoryReader.TechniqueStats.LearnedTechniqueCount().ToString();
    private void SyncEmulatorBattlesCount() => Battles = ServiceRelay.LiveMemoryReader.ConditionStats.Battles.ToString();

    private void UpdateEmulatorLinkSyncMode(EmulatorLinkSyncMode mode)
    {
        _emulatorSyncMode = mode;
        
        switch (mode)
        {
            case EmulatorLinkSyncMode.Auto:
            {
                if (_emulatorIsConnected)
                {
                    StartMemorySync();
                }

                break;
            }
            case EmulatorLinkSyncMode.Manual:
                StopMemorySync();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private readonly SerialDisposable _memorySyncDisposable = new();

    private void StartMemorySync()
    {
        _memorySyncDisposable.Disposable = Observable
            .Interval(TimeSpan.FromSeconds(1))
            .ObserveOn(_uiSynchronizationContext)
            .TakeUntil(_ => !_emulatorIsConnected)
            .Subscribe(_ =>
            {
                if (!_emulatorIsConnected)
                {
                    return;
                }

                try
                {
                    SyncAllProfileStats();
                    SyncAllEmulatorCombatStats();
                    SyncAllEmulatorConditionStats();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error syncing stats: {ex}");
                }
            });
    }

    private void StopMemorySync()
    {
        _memorySyncDisposable.Disposable = null;
    }

    private void OnEmulatorConnectedChanged(bool isConnected)
    {
        if (isConnected)
        {
            _emulatorIsConnected = true;

            if (_emulatorSyncMode == EmulatorLinkSyncMode.Auto)
            {
                StartMemorySync();
            }
        }
        else
        {
            _emulatorIsConnected = false;
            StopMemorySync();
        }
    }


    private void UpdateAvailableDigimon(EvolutionCalculatorConfig evolutionCalculatorConfig)
    {
        _gameVariant = evolutionCalculatorConfig.GameVariant;

        AvailableDigimonTypes = DigimonTypes.Get(evolutionCalculatorConfig.GameVariant);

        IEnumerable<DigimonName> availableDigimonTypes = AvailableDigimonTypes as DigimonName[] ?? AvailableDigimonTypes.ToArray();
        if (!availableDigimonTypes.Contains(PlayerDigimonType))
        {
            PlayerDigimonType = availableDigimonTypes.First();
        }
    }

    public void Dispose() => _compositeDisposable.Dispose();
}