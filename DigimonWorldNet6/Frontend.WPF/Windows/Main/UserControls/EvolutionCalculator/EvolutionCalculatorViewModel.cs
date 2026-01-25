using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using DigimonWorld.Evolution.Calculator.Core;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.Services;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using Generics.Configuration;
using Generics.Constants;
using Generics.Enums;
using Generics.Extensions;
using Generics.Services;
using Digimon = DigimonWorld.Evolution.Calculator.Core.DataObjects.Digimon;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvolutionCalculator;

public sealed class EvolutionCalculatorViewModel : BaseViewModel, IDisposable
{
    private readonly SpeakingSimulator _speakingSimulator;
    private readonly CompositeDisposable _compositeDisposable;

    private string _jijimonText = string.Empty;

    private IEnumerable<DigimonName> _availableDigimonTypes;
    private string _hp = "0";
    private string _mp = "0";
    private string _off = "0";
    private string _def = "0";
    private string _speed = "0";
    private string _brains = "0";
    private string _careMistakes = "0";
    private string _weight = "0";
    private string _happiness = "0";
    private string _discipline = "0";
    private string _battles = "0";
    private string _techniques = "0";
    private DigimonName _playerDigimonType;
    private EvolutionResult _evolutionResult = EvolutionResult.Unknown;
    private bool _flipToRight;

    public EvolutionCalculatorViewModel()
    {
        _speakingSimulator = new SpeakingSimulator();

        _compositeDisposable = new CompositeDisposable(
            _speakingSimulator
        );

        SetEvolutionResult = new CommandHandler(CalculateEvolutionResult);

        InstantDisplayCommand = new CommandHandler(InstantDisplay);

        int initialDelay = UserConfigurationManager.SpeakingSimulatorConfig.NarratorMode == NarratorMode.Instant ? 0 : 1500;

        Task.Delay(initialDelay)
            .WaitAsync(CancellationToken.None)
            .ContinueWith(_ => _speakingSimulator.WriteTextAsSpeechAsync(JijimonEvolutionCalculatorNarratorText.IntroText, textOutput => JijimonText = textOutput));

        _availableDigimonTypes = DigimonTypes.Get(UserConfigurationManager.EvolutionCalculatorConfig.EvolutionCalculatorMode);
        UserConfigurationManager.CurrentEvolutionCalculatorConfig.Subscribe(OnEvolutionCalculatorConfigChanged);
    }

    public ICommand SetEvolutionResult { get; }

    public ICommand InstantDisplayCommand { get; }

    public string CalculateButtonText => UiText.CalculateButtonText;

    public string JijimonText
    {
        get => _jijimonText;
        set => SetField(ref _jijimonText, value);
    }

    public DigimonName PlayerDigimonType
    {
        get => _playerDigimonType;
        set
        {
            if (_playerDigimonType == value) return;

            _playerDigimonType = value;

            EvolutionResult = EvolutionResult.Unknown;

            FlipToRight = false;

            OnPropertyChanged();
        }
    }

    public string HP
    {
        get => _hp;
        set => SetField(ref _hp, value);
    }

    public string MP
    {
        get => _mp;
        set => SetField(ref _mp, value);
    }

    public string Off
    {
        get => _off;
        set => SetField(ref _off, value);
    }

    public string Def
    {
        get => _def;
        set => SetField(ref _def, value);
    }

    public string Speed
    {
        get => _speed;
        set => SetField(ref _speed, value);
    }

    public string Brains
    {
        get => _brains;
        set => SetField(ref _brains, value);
    }

    public string CareMistakes
    {
        get => _careMistakes;
        set => SetField(ref _careMistakes, value);
    }

    public string Weight
    {
        get => _weight;
        set => SetField(ref _weight, value);
    }

    public string Happiness
    {
        get => _happiness;
        set => SetField(ref _happiness, value);
    }

    public string Discipline
    {
        get => _discipline;
        set => SetField(ref _discipline, value);
    }

    public string Battles
    {
        get => _battles;
        set => SetField(ref _battles, value);
    }

    public string Techniques
    {
        get => _techniques;
        set => SetField(ref _techniques, value);
    }

    public EvolutionResult EvolutionResult
    {
        get => _evolutionResult;
        set
        {
            if (_evolutionResult == value) return;

            _evolutionResult = value;

            _ = _speakingSimulator.WriteEvolutionTextAsSpeech(JijimonEvolutionCalculatorNarratorText.EvolutionResultCalculated(value), textOutput => JijimonText = textOutput, () => OnPropertyChanged());
        }
    }

    public bool FlipToRight
    {
        get => _flipToRight;
        set
        {
            if (_flipToRight == value) return;
            _flipToRight = value;
            OnPropertyChanged();
        }
    }

    public IEnumerable<DigimonName> AvailableDigimonTypes
    {
        get => _availableDigimonTypes;
        private set
        {
            if(Equals(_availableDigimonTypes, value)) return;
            
            _availableDigimonTypes = value;
            OnPropertyChanged();
        }
    }

    private void CalculateEvolutionResult()
    {
        _evolutionResult = EvolutionResult.Unknown;

        OnPropertyChanged(nameof(EvolutionResult));

        Digimon currentDigimon = new(PlayerDigimonType, int.Parse(HP), int.Parse(MP), int.Parse(Off), int.Parse(Def), int.Parse(Speed), int.Parse(Brains), int.Parse(CareMistakes), int.Parse(Weight), int.Parse(Happiness), int.Parse(Discipline),
            int.Parse(Battles), int.Parse(Techniques));

        EvolutionResult = currentDigimon.DigimonName.EvolutionStage() == EvolutionStage.Ultimate
            ? EvolutionResult.NotApplicable
            : ServiceRelay.CalculateEvolutionResult(currentDigimon);

        FlipToRight = true;
    }

    private void InstantDisplay() => _speakingSimulator.RequestInstantDisplay();

    private void OnEvolutionCalculatorConfigChanged(EvolutionCalculatorConfig evolutionCalculatorConfig)
    {
        AvailableDigimonTypes = DigimonTypes.Get(UserConfigurationManager.EvolutionCalculatorConfig.EvolutionCalculatorMode);

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