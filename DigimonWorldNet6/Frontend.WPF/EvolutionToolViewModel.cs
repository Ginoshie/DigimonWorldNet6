using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCalculation;
using Generics.Enums;

namespace DigimonWorld.Frontend.WPF;

public sealed class EvolutionToolViewModel
{
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

    private readonly EvolutionCalculator _evolutionCalculator = new ();
    private DigimonType _digimonType;

    public EvolutionToolViewModel()
    {
        SetEvolutionResult = new CommandHandler(CalculateEvolutionResult);
    }

    public ICommand SetEvolutionResult { get; }

    public DigimonType DigimonType
    {
        get => _digimonType;
        set
        {
            _digimonType = value;

            OnPropertyChanged();
        }
    }

    public int HP
    {
        get => _hp;
        set
        {
            _hp = value;

            OnPropertyChanged();
        }
    }

    public int MP
    {
        get => _mp;
        set
        {
            _mp = value;

            OnPropertyChanged();
        }
    }

    public int Off
    {
        get => _off;
        set
        {
            _off = value;

            OnPropertyChanged();
        }
    }

    public int Def
    {
        get => _def;
        set
        {
            _def = value;

            OnPropertyChanged();
        }
    }

    public int Speed
    {
        get => _speed;
        set
        {
            _speed = value;

            OnPropertyChanged();
        }
    }

    public int Brains
    {
        get => _brains;
        set
        {
            _brains = value;
            
            OnPropertyChanged();
        }
    }

    public int CareMistakes
    {
        get => _careMistakes;
        set
        {
            _careMistakes = value;
            
            OnPropertyChanged();
        }
    }

    public int Weight
    {
        get => _weight;
        set
        {
            _weight = value;
            
            OnPropertyChanged();
        }
    }

    public int Happiness
    {
        get => _happiness;
        set
        {
            _happiness = value;
            
            OnPropertyChanged();
        }
    }

    public int Discipline
    {
        get => _discipline;
        set
        {
            _discipline = value;
            
            OnPropertyChanged();
        }
    }

    public int Battles
    {
        get => _battles;
        set
        {
            _battles = value;
            
            OnPropertyChanged();
        }
    }

    public int Techniques
    {
        get => _techniques;
        set
        {
            _techniques = value;
            
            OnPropertyChanged();
        }
    }
    
    public EvolutionResult EvolutionResult { get; private set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void CalculateEvolutionResult()
    {
        var currentDigimon = new Digimon(DigimonType, HP, MP, Off, Def, Speed, Brains, CareMistakes, Weight, Happiness, Discipline, Battles, _techniques);
        EvolutionResult = _evolutionCalculator.CalculateEvolutionResult(currentDigimon);
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}