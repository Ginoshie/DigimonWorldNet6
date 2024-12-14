using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCalculation;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.Services;
using Generics.Enums;
using Generics.Extensions;

namespace DigimonWorld.Frontend.WPF;

public sealed class EvolutionToolViewModel : INotifyPropertyChanged
{
    private string _jijimonText = JijimonNarratorText.EvolutionCalculatorIntroText;

    private int _hp;
    private int _mp;
    private int _off;
    private int _def;
    private int _speed;
    private int _brains;
    private int _careMistakes;
    private int _weight;
    private int _happiness;
    private int _discipline;
    private int _battles;
    private int _techniques;
    private DigimonType _digimonType;
    private EvolutionResult _evolutionResult = EvolutionResult.Unknown;
    private bool _flipToRight;

    private readonly EvolutionCalculator _evolutionCalculator = new();

    public EvolutionToolViewModel(Window window)
    {
        SetEvolutionResult = new CommandHandler(CalculateEvolutionResult);
        
        InstantDisplayCommand = new CommandHandler(InstantDisplay);
        
        MinimizeCommand = new CommandHandler(() => window.WindowState = WindowState.Minimized);
        
        CloseCommand = new CommandHandler(window.Close);

        DragCommand = new CommandHandler(() => DragWindow(window));
    }

    public ICommand SetEvolutionResult { get; }
    
    public ICommand InstantDisplayCommand { get; }
    public ICommand MinimizeCommand { get; }
    
    public ICommand CloseCommand { get; }
    
    public ICommand DragCommand { get; }
    
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

    public int HP
    {
        get => _hp;
        set
        {
            if (_hp == value) return;

            _hp = value;

            OnPropertyChanged();
        }
    }

    public int MP
    {
        get => _mp;
        set
        {
            if (_mp == value) return;

            _mp = value;

            OnPropertyChanged();
        }
    }

    public int Off
    {
        get => _off;
        set
        {
            if (_off == value) return;

            _off = value;

            OnPropertyChanged();
        }
    }

    public int Def
    {
        get => _def;
        set
        {
            if (_def == value) return;

            _def = value;

            OnPropertyChanged();
        }
    }

    public int Speed
    {
        get => _speed;
        set
        {
            if (_speed == value) return;

            _speed = value;

            OnPropertyChanged();
        }
    }

    public int Brains
    {
        get => _brains;
        set
        {
            if (_brains == value) return;

            _brains = value;

            OnPropertyChanged();
        }
    }

    public int CareMistakes
    {
        get => _careMistakes;
        set
        {
            if (_careMistakes == value) return;

            _careMistakes = value;

            OnPropertyChanged();
        }
    }

    public int Weight
    {
        get => _weight;
        set
        {
            if (_weight == value) return;

            _weight = value;

            OnPropertyChanged();
        }
    }

    public int Happiness
    {
        get => _happiness;
        set
        {
            if (_happiness == value) return;

            _happiness = value;

            OnPropertyChanged();
        }
    }

    public int Discipline
    {
        get => _discipline;
        set
        {
            if (_discipline == value) return;

            _discipline = value;

            OnPropertyChanged();
        }
    }

    public int Battles
    {
        get => _battles;
        set
        {
            if (_battles == value) return;

            _battles = value;

            OnPropertyChanged();
        }
    }

    public int Techniques
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
            _ = SpeakingSimulator.WriteAsSpeech(JijimonNarratorText.EvolutionResultCalculated(value), textOutput => JijimonText = textOutput);

            OnPropertyChanged();
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

    public event PropertyChangedEventHandler? PropertyChanged;

    private void CalculateEvolutionResult()
    {
        Digimon currentDigimon = new(DigimonType, HP, MP, Off, Def, Speed, Brains, CareMistakes, Weight, Happiness, Discipline, Battles, _techniques);

        EvolutionResult = currentDigimon.DigimonType.EvolutionStage() == EvolutionStage.Ultimate 
            ? EvolutionResult.NotApplicable 
            : _evolutionCalculator.CalculateEvolutionResult(currentDigimon);

        FlipToRight = true;
    }

    private void InstantDisplay()
    {
        SpeakingSimulator.RequestInstantDisplay();
    }

    private void DragWindow(Window window)
    {
        if (Mouse.PrimaryDevice.LeftButton != MouseButtonState.Pressed) return;
        
        MouseEventArgs? mouseEventArgs = Mouse.PrimaryDevice.Target as MouseEventArgs;

        if (mouseEventArgs?.OriginalSource is Button or TextBox)
        {
            return;
        }

        try
        {
            window.DragMove();
        }
        catch (InvalidOperationException)
        {
            // Ignore exceptions caused by improper DragMove calls
        }
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}