using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using DigimonWorld.Evolution.Calculator.Core;
using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.Services;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using Generics.Configuration;
using Generics.Constants;
using Generics.Enums;
using Generics.Extensions;
using Generics.Services;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvolutionCalculator;

public sealed class EvolutionCalculatorViewModel : BaseViewModel, IDisposable
{
    private readonly SpeakingSimulator _speakingSimulator;
    private readonly CompositeDisposable _compositeDisposable;

    private EvolutionResult _evolutionResult = EvolutionResult.Unknown;

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

        UserConfigurationManager.CurrentEvolutionCalculatorConfig.Subscribe(OnEvolutionCalculatorConfigChanged);
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

            _ = _speakingSimulator.WriteEvolutionTextAsSpeech(JijimonEvolutionCalculatorNarratorText.EvolutionResultCalculated(value), textOutput => JijimonText = textOutput, () => OnPropertyChanged());
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

        UserDigimon currentUserDigimon = new(PlayerDigimonType, int.Parse(HP), int.Parse(MP), int.Parse(Off), int.Parse(Def), int.Parse(Speed), int.Parse(Brains), int.Parse(CareMistakes), int.Parse(Weight), int.Parse(Happiness), int.Parse(Discipline),
            int.Parse(Battles), int.Parse(Techniques));

        EvolutionResult = currentUserDigimon.DigimonName.EvolutionStage() == EvolutionStage.Ultimate
            ? EvolutionResult.NotApplicable
            : ServiceRelay.CalculateEvolutionResult(currentUserDigimon);

        FlipToRight = true;
    }

    private void InstantDisplay() => _speakingSimulator.RequestInstantDisplay();

    private void OnEvolutionCalculatorConfigChanged(EvolutionCalculatorConfig evolutionCalculatorConfig)
    {
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