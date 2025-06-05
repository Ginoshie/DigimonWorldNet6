using System;
using System.Reactive.Disposables;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using DigimonWorld.Evolution.Calculator.Core;
using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.Services;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using Generics.Enums;
using Generics.Extensions;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvolutionCalculator;

public sealed class EvolutionCalculatorViewModel : BaseViewModel, IDisposable
{
    private readonly SpeakingSimulator _speakingSimulator;
    private readonly CompositeDisposable _compositeDisposable;

    private string _jijimonText = string.Empty;

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
    private DigimonType _digimonType;
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

        Task
            .Delay(initialDelay)
            .WaitAsync(CancellationToken.None)
            .ContinueWith(_ => _speakingSimulator.WriteTextAsSpeechAsync(JijimonEvolutionCalculatorNarratorText.IntroText, textOutput => JijimonText = textOutput));
    }

    public ICommand SetEvolutionResult { get; }

    public ICommand InstantDisplayCommand { get; }

    public string CalculateButtonText => UiText.CalculateButtonText;

    public string JijimonText
    {
        get => _jijimonText;
        set
        {
            if (_jijimonText == value) return;

            _jijimonText = value;

            OnPropertyChanged();
        }
    }

    public DigimonType DigimonType
    {
        get => _digimonType;
        set
        {
            if (_digimonType == value) return;

            _digimonType = value;

            EvolutionResult = EvolutionResult.Unknown;

            FlipToRight = false;

            OnPropertyChanged();
        }
    }

    public string HP
    {
        get => _hp;
        set
        {
            if (_hp == value) return;

            _hp = value;

            OnPropertyChanged();
        }
    }

    public string MP
    {
        get => _mp;
        set
        {
            if (_mp == value) return;

            _mp = value;

            OnPropertyChanged();
        }
    }

    public string Off
    {
        get => _off;
        set
        {
            if (_off == value) return;

            _off = value;

            OnPropertyChanged();
        }
    }

    public string Def
    {
        get => _def;
        set
        {
            if (_def == value) return;

            _def = value;

            OnPropertyChanged();
        }
    }

    public string Speed
    {
        get => _speed;
        set
        {
            if (_speed == value) return;

            _speed = value;

            OnPropertyChanged();
        }
    }

    public string Brains
    {
        get => _brains;
        set
        {
            if (_brains == value) return;

            _brains = value;

            OnPropertyChanged();
        }
    }

    public string CareMistakes
    {
        get => _careMistakes;
        set
        {
            if (_careMistakes == value) return;

            _careMistakes = value;

            OnPropertyChanged();
        }
    }

    public string Weight
    {
        get => _weight;
        set
        {
            if (_weight == value) return;

            _weight = value;

            OnPropertyChanged();
        }
    }

    public string Happiness
    {
        get => _happiness;
        set
        {
            if (_happiness == value) return;

            _happiness = value;

            OnPropertyChanged();
        }
    }

    public string Discipline
    {
        get => _discipline;
        set
        {
            if (_discipline == value) return;

            _discipline = value;

            OnPropertyChanged();
        }
    }

    public string Battles
    {
        get => _battles;
        set
        {
            if (_battles == value) return;

            _battles = value;

            OnPropertyChanged();
        }
    }

    public string Techniques
    {
        get => _techniques;
        set
        {
            if (_techniques == value) return;

            _techniques = value;

            OnPropertyChanged();
        }
    }

    public EvolutionResult EvolutionResult
    {
        get => _evolutionResult;
        private set
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

    private void CalculateEvolutionResult()
    {
        _evolutionResult = EvolutionResult.Unknown;

        OnPropertyChanged(nameof(EvolutionResult));

        Digimon currentDigimon = new(DigimonType, int.Parse(HP), int.Parse(MP), int.Parse(Off), int.Parse(Def), int.Parse(Speed), int.Parse(Brains), int.Parse(CareMistakes), int.Parse(Weight), int.Parse(Happiness), int.Parse(Discipline),
            int.Parse(Battles), int.Parse(Techniques));

        EvolutionResult = currentDigimon.DigimonType.EvolutionStage() == EvolutionStage.Ultimate
            ? EvolutionResult.NotApplicable
            : ServiceRelay.CalculateEvolutionResult(currentDigimon);

        FlipToRight = true;
    }

    private void InstantDisplay()
    {
        _speakingSimulator.RequestInstantDisplay();
    }

    public void Dispose()
    {
        _speakingSimulator.Dispose();
        _compositeDisposable.Dispose();
    }
}