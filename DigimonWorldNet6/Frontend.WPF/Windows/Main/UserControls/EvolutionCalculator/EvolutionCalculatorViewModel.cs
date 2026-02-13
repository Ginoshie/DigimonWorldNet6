using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Windows.Input;
using DigimonWorld.Evolution.Calculator.Core;
using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.Enums;
using DigimonWorld.Frontend.WPF.Services;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using Shared.Configuration;
using Shared.Constants;
using Shared.Enums;
using Shared.Extensions;
using Shared.Services;
using Shared.Services.Events;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvolutionCalculator;

public sealed class EvolutionCalculatorViewModel : BaseViewModel, IDisposable
{
    private readonly SpeakingSimulator _speakingSimulator;
    private readonly CompositeDisposable _compositeDisposable;

    private EvolutionResult _evolutionResult = EvolutionResult.Unknown;
    private GameVariant _gameVariant = GameVariant.Original;

    public EvolutionCalculatorViewModel()
    {
        _speakingSimulator = new SpeakingSimulator();

        _compositeDisposable = new CompositeDisposable(
            _speakingSimulator,
            // Profile
            DigimonStatsEventHub.SetAllEmulatorProfileStatsObservable.Subscribe(_ => SetAllProfileStats()),
            DigimonStatsEventHub.SetEmulatorDigimonTypeObservable.Subscribe(_ => SetEmulatorDigimonType()),
            DigimonStatsEventHub.SetEmulatorWeightObservable.Subscribe(_ => SetEmulatorWeight()),

            // Parameter
            DigimonStatsEventHub.SetAllEmulatorParameterStatsObservable.Subscribe(_ => OnSetAllEmulatorCombatStats()),
            DigimonStatsEventHub.SetEmulatorHPObservable.Subscribe(_ => SetEmulatorHP()),
            DigimonStatsEventHub.SetEmulatorMPObservable.Subscribe(_ => SetEmulatorMP()),
            DigimonStatsEventHub.SetEmulatorOffObservable.Subscribe(_ => SetEmulatorOff()),
            DigimonStatsEventHub.SetEmulatorDefObservable.Subscribe(_ => SetEmulatorDef()),
            DigimonStatsEventHub.SetEmulatorSpdObservable.Subscribe(_ => SetEmulatorSpeed()),
            DigimonStatsEventHub.SetEmulatorBrnObservable.Subscribe(_ => SetEmulatorBrains()),

            // Condition
            DigimonStatsEventHub.SetAllEmulatorConditionStatsObservable.Subscribe(_ => OnSetAllEmulatorConditionStats()),
            DigimonStatsEventHub.SetEmulatorHappinessObservable.Subscribe(_ => SetEmulatorHappiness()),
            DigimonStatsEventHub.SetEmulatorDisciplineObservable.Subscribe(_ => SetEmulatorDiscipline()),
            DigimonStatsEventHub.SetEmulatorCareMistakesObservable.Subscribe(_ => SetEmulatorCareMistakes()),
            DigimonStatsEventHub.SetEmulatorTechniqueCountObservable.Subscribe(_ => SetEmulatorTechniqueCount()),
            DigimonStatsEventHub.SetEmulatorBattlesCountObservable.Subscribe(_ => SetEmulatorBattlesCount()),

            // UserConfig
            UserConfigurationManager.CurrentEvolutionCalculatorConfig.Subscribe(OnEvolutionCalculatorConfigChanged)
        );

        SetEvolutionResult = new CommandHandler(CalculateEvolutionResult);

        InstantDisplayCommand = new CommandHandler(InstantDisplay);

        SpeechDelay initialDelay = UserConfigurationManager.SpeakingSimulatorConfig.NarratorMode == NarratorMode.Instant ? SpeechDelay.None : SpeechDelay.Long;

        _ = _speakingSimulator.SpeakAsync(
            JijimonEvolutionCalculatorNarratorText.IntroText,
            textOutput => JijimonText = textOutput,
            initialDelay);
    }

    public ICommand SetEvolutionResult { get; }

    public ICommand InstantDisplayCommand { get; }

    public string CalculateButtonText => UiText.CalculateButtonText;

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
    
    // Profile
    private void SetAllProfileStats()
    {
        SetEmulatorWeight();
        SetEmulatorDigimonType();
    }
    private void SetEmulatorWeight() => Weight = ServiceRelay.LiveMemoryReader.DigimonProfileStats.Weight.ToString();
    private void SetEmulatorDigimonType() => PlayerDigimonType = DigimonTypes.Get(ServiceRelay.LiveMemoryReader.DigimonProfileStats.DigimonType, _gameVariant).Digimon;
    
    
    // Parameter
    private void SetEmulatorHP() => HP = ServiceRelay.LiveMemoryReader.DigimonParameterStats.HP.ToString();
    private void SetEmulatorMP() => MP = ServiceRelay.LiveMemoryReader.DigimonParameterStats.MP.ToString();
    private void SetEmulatorOff() => Off = ServiceRelay.LiveMemoryReader.DigimonParameterStats.Offense.ToString();
    private void SetEmulatorDef() => Def = ServiceRelay.LiveMemoryReader.DigimonParameterStats.Defense.ToString();
    private void SetEmulatorSpeed() => Speed = ServiceRelay.LiveMemoryReader.DigimonParameterStats.Speed.ToString();
    private void SetEmulatorBrains() => Brains = ServiceRelay.LiveMemoryReader.DigimonParameterStats.Brains.ToString();
    
    // Condition
    private void OnSetAllEmulatorConditionStats()
    {
        SetEmulatorHappiness();
        SetEmulatorDiscipline();
        SetEmulatorCareMistakes();
        SetEmulatorTechniqueCount();
        SetEmulatorBattlesCount();
    }
    private void SetEmulatorHappiness() => Discipline = ServiceRelay.LiveMemoryReader.DigimonConditionStats.Discipline.ToString();
    private void SetEmulatorDiscipline() => Happiness = ServiceRelay.LiveMemoryReader.DigimonConditionStats.Happiness.ToString();
    private void SetEmulatorCareMistakes() => CareMistakes = ServiceRelay.LiveMemoryReader.DigimonConditionStats.CareMistakes.ToString();
    private void SetEmulatorTechniqueCount() => Techniques = ServiceRelay.LiveMemoryReader.DigimonTechniqueStats.LearnedTechniqueCount().ToString();
    private void SetEmulatorBattlesCount() => Battles = ServiceRelay.LiveMemoryReader.DigimonConditionStats.Battles.ToString();

    public string HP
    {
        get;
        set => SetField(ref field, value);
    } = "0";

    public string MP
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

            _ = _speakingSimulator.SpeakAsync(JijimonEvolutionCalculatorNarratorText.EvolutionResultCalculated(value), textOutput => JijimonText = textOutput, 0, () => OnPropertyChanged());
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

        UserDigimon currentUserDigimon = new(PlayerDigimonType, int.Parse(HP), int.Parse(MP), int.Parse(Off), int.Parse(Def), int.Parse(Speed), int.Parse(Brains), int.Parse(CareMistakes), int.Parse(Weight), int.Parse(Happiness),
            int.Parse(Discipline),
            int.Parse(Battles), int.Parse(Techniques));

        EvolutionResult = currentUserDigimon.DigimonName.EvolutionStage() == EvolutionStage.Ultimate
            ? EvolutionResult.NotApplicable
            : ServiceRelay.CalculateEvolutionResult(currentUserDigimon);

        FlipToRight = true;
    }

    private void InstantDisplay() => _speakingSimulator.RequestInstantDisplay();

    private void OnSetAllEmulatorCombatStats()
    {
        SetEmulatorHP();
        SetEmulatorMP();
        SetEmulatorOff();
        SetEmulatorDef();
        SetEmulatorSpeed();
        SetEmulatorBrains();
    }

    private void OnEvolutionCalculatorConfigChanged(EvolutionCalculatorConfig evolutionCalculatorConfig)
    {
        _gameVariant = UserConfigurationManager.EvolutionCalculatorConfig.GameVariant;

        AvailableDigimonTypes = DigimonTypes.Get(UserConfigurationManager.EvolutionCalculatorConfig.GameVariant);

        IEnumerable<DigimonName> availableDigimonTypes = AvailableDigimonTypes as DigimonName[] ?? AvailableDigimonTypes.ToArray();
        if (!availableDigimonTypes.Contains(PlayerDigimonType))
        {
            PlayerDigimonType = availableDigimonTypes.First();
        }
    }

    public void Dispose()
    {
        _speakingSimulator.Dispose();
        _compositeDisposable.Dispose();
    }
}