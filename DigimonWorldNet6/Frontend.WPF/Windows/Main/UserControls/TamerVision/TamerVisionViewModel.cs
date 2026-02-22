using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DigimonWorld.Evolution.Calculator.Core;
using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.Enums;
using DigimonWorld.Frontend.WPF.Models;
using DigimonWorld.Frontend.WPF.Services;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using MemoryAccess.MemoryValues.Digimon;
using Shared;
using Shared.Constants;
using Shared.Enums;
using Shared.Extensions;
using Shared.Services;
using Shared.Services.Events;
using GameVariant = Shared.Enums.GameVariant;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.TamerVision;

public class TamerVisionViewModel : BaseViewModel, IDisposable
{
    private const string BACKGROUND = "pack://application:,,,/Images/Misc/background{0}";
    private const string HIDE_EVO = "pack://application:,,,/Images/Misc/hidden-background.jpg";
    private const string DIGIMON_IMAGE = "pack://application:,,,/Images/Digimon/{0}{1}";
    private const string NONE_VARIANT_SUFFIX = ".png";
    private const string BLURRED_VARIANT_SUFFIX = ".png";
    private const string DIGI_GUESS_VARIANT_SUFFIX = "-silhouette.png";
    private const string ANONYMOUS_VARIANT_SUFFIX = "-anon.png";

    private readonly CompositeDisposable _disposables;
    private readonly SpeakingSimulator _speakingSimulator = new();
    private readonly Brush _defaultEvolutionResultBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#354A5B"));

    private GameVariant _gameVariant = GameVariant.Original;
    private Digimon _synchedDigimon;
    private EvoResultMask _currentEvoResultMask = EvoResultMask.None;

    public TamerVisionViewModel()
    {
        InstantDisplayCommand = new CommandHandler(InstantDisplay);
        SetShowEvoCommand = new CommandHandler(SetShowEvo);
        SetHideEvoCommand = new CommandHandler(SetHideEvo);
        SetEvoResultMaskNoneCommand = new CommandHandler(() => SetEvoResultMaskCommand(EvoResultMask.None));
        SetEvoResultMaskBlurredCommand = new CommandHandler(() => SetEvoResultMaskCommand(EvoResultMask.Blurred));
        SetEvoResultMaskDigiGuessCommand = new CommandHandler(() => SetEvoResultMaskCommand(EvoResultMask.DigiGuess));
        SetEvoResultMaskAnonymousCommand = new CommandHandler(() => SetEvoResultMaskCommand(EvoResultMask.Anonymous));

        EvoResultBackground = _defaultEvolutionResultBackground;

        SpeechDelay initialDelay = UserConfigurationManager.SpeakingSimulatorConfig.NarratorMode == NarratorMode.Instant ? SpeechDelay.None : SpeechDelay.Long;

        _ = _speakingSimulator.SpeakAsync(
            AnalogmanTamerVisionNarratorText.IntroText,
            textOutput => AnalogmanText = textOutput,
            initialDelay);

        _disposables = new CompositeDisposable(
            _speakingSimulator,
            UserConfigurationManager.CurrentEvolutionCalculatorConfig.Subscribe(evolutionCalculatorConfig => _gameVariant = evolutionCalculatorConfig.GameVariant),
            EmulatorLinkEventHub.DigimonConditionStatsSynchronizedObservable.Subscribe(_ => OnDigimonConditionStatsSynchronized()),
            EmulatorLinkEventHub.DigimonParameterStatsSynchronizedObservable.Subscribe(_ => OnDigimonParameterStatsSynchronized()),
            EmulatorLinkEventHub.DigimonProfileStatsSynchronizedObservable.Subscribe(_ => OnDigimonProfileStatsSynchronized()),
            EmulatorLinkEventHub.DigimonCareStatsSynchronizedObservable.Subscribe(_ => OnCareStatsSynchronized()),
            EmulatorLinkEventHub.DigimonTechniqueStatsSynchronizedObservable.Subscribe(_ => OnDigimonTechniqueStatsSynchronized()),
            EmulatorLinkEventHub.HistoricEvolutionsSynchronizedObservable.Subscribe(_ => OnHistoricEvolutionsSynchronized()),
            Observable.Interval(TimeSpan.FromSeconds(1)).ObserveOn(SynchronizationContext.Current!).Subscribe(_ => CalculateEvolutionResult())
        );
        
        UpdateEvoResult();
    }

    public string AnalogmanText
    {
        get;
        set => SetField(ref field, value);
    } = string.Empty;

    public ICommand InstantDisplayCommand { get; }

    public ICommand SetShowEvoCommand { get; }
    public ICommand SetHideEvoCommand { get; }
    public ICommand SetEvoResultMaskNoneCommand { get; }
    public ICommand SetEvoResultMaskBlurredCommand { get; }
    public ICommand SetEvoResultMaskDigiGuessCommand { get; }
    public ICommand SetEvoResultMaskAnonymousCommand { get; }

    public bool ShowEvo
    {
        get;
        set
        {
            SetField(ref field, value);

            OnPropertyChanged(nameof(HideEvo));
            OnPropertyChanged(nameof(EvoResultDigiGuessIsShown));
            OnPropertyChanged(nameof(EvoResultBlurredIsShown));
        }
    }

    public bool HideEvo => !ShowEvo;

    public EvolutionResult EvolutionResult
    {
        get;
        set
        {
            if (SetField(ref field, value))
            {
                UpdateEvoResult();
            }
        }
    } = EvolutionResult.None;

    public Brush EvoResultBackground
    {
        get;
        set => SetField(ref field, value);
    }

    public ImageSource EvoResultImage
    {
        get;
        set => SetField(ref field, value);
    } = new BitmapImage(new Uri(string.Format(DIGIMON_IMAGE, nameof(EvolutionResult.None), NONE_VARIANT_SUFFIX)));

    public string DigimonName
    {
        get;
        set => SetField(ref field, value);
    } = nameof(EvolutionResult.None);

    public bool EvoResultMaskNoneIsSelected => _currentEvoResultMask == EvoResultMask.None;
    public bool EvoResultMaskBlurredIsSelected => _currentEvoResultMask == EvoResultMask.Blurred;
    public bool EvoResultMaskDigiGuessIsSelected => _currentEvoResultMask == EvoResultMask.DigiGuess;
    public bool EvoResultMaskAnonymousIsSelected => _currentEvoResultMask == EvoResultMask.Anonymous;
    public bool EvoResultDigiGuessIsShown => EvoResultMaskDigiGuessIsSelected && ShowEvo;
    public bool EvoResultBlurredIsShown => EvoResultMaskBlurredIsSelected && ShowEvo;
    
    public string DigimonType
    {
        get;
        set
        {
            if (SetField(ref field, value))
            {
                OnDigimonTypeChanged();
            }
        }
    } = string.Empty;

    public int Age
    {
        get;
        set => SetField(ref field, value);
    }

    public int EvoAge
    {
        get;
        set => SetField(ref field, value);
    }

    public int EvoReadyAge
    {
        get;
        set => SetField(ref field, value);
    }

    public TypeIcon Type
    {
        get;
        set => SetField(ref field, value);
    }

    public string Active
    {
        get;
        set => SetField(ref field, value);
    } = string.Empty;

    public ObservableCollection<SpecialIcon> SpecialIcons
    {
        get;
        set => SetField(ref field, value);
    } = [];

    public int Weight
    {
        get;
        set => SetField(ref field, value);
    }

    public int CareMistakes
    {
        get;
        set => SetField(ref field, value);
    }

    public int Battles
    {
        get;
        set => SetField(ref field, value);
    }

    public int PoopLevel
    {
        get;
        set => SetField(ref field, value);
    }

    public int PoopingTimer
    {
        get;
        set => SetField(ref field, value);
    }

    public int Tiredness
    {
        get;
        set => SetField(ref field, value);
    }

    public int EnergyLevel
    {
        get;
        set => SetField(ref field, value);
    }

    public int HungryTimer
    {
        get;
        set => SetField(ref field, value);
    }


    public int StarvationTimer
    {
        get;
        set => SetField(ref field, value);
    }

    public int TechniqueCount
    {
        get;
        set => SetField(ref field, value);
    }

    public int Happiness
    {
        get;
        set => SetField(ref field, value);
    }

    public int Discipline
    {
        get;
        set => SetField(ref field, value);
    }

    public int Virus
    {
        get;
        set => SetField(ref field, value);
    }

    public int HP
    {
        get;
        set => SetField(ref field, value);
    }

    public int CurrentHP
    {
        get;
        set => SetField(ref field, value);
    }

    public int MP
    {
        get;
        set => SetField(ref field, value);
    }


    public int CurrentMP
    {
        get;
        set => SetField(ref field, value);
    }

    public int Offense
    {
        get;
        set => SetField(ref field, value);
    }

    public int Defence
    {
        get;
        set => SetField(ref field, value);
    }

    public int Speed
    {
        get;
        set => SetField(ref field, value);
    }

    public int Brains
    {
        get;
        set => SetField(ref field, value);
    }

    private void CalculateEvolutionResult()
    {
        UserDigimon currentUserDigimon = new(_synchedDigimon.DigimonName, HP, MP, Offense, Defence, Speed, Brains, CareMistakes, Weight, Happiness, Discipline, Battles, TechniqueCount);

        EvolutionResult evolutionResult = currentUserDigimon.DigimonName.EvolutionStage() == EvolutionStage.Ultimate
            ? EvolutionResult.NotApplicable
            : ServiceRelay.CalculateEvolutionResult(currentUserDigimon);

        EvolutionResult = evolutionResult;
    }

    private void OnDigimonTypeChanged()
    {
        ProfileStats profileStats = ServiceRelay.LiveMemoryReader.ProfileStats;

        List<SpecialIcon> specialIcons =
        [
            SpecialIconFactory.Create(profileStats.FirstSpecial.ToSpecial()),
            SpecialIconFactory.Create(profileStats.SecondSpecial.ToSpecial()),
            SpecialIconFactory.Create(profileStats.ThirdSpecial.ToSpecial())
        ];

        SpecialIcons = new ObservableCollection<SpecialIcon>(specialIcons.Where(i => i.Special != Special.None));

        Type = TypeIconFactory.Create(profileStats.Type.ToDigimonType());

        EvoReadyAge = EvolutionStageMapper.Get(DigimonType) switch
        {
            EvolutionStage.Fresh => 6,
            EvolutionStage.InTraining => 24,
            EvolutionStage.Rookie => 72,
            EvolutionStage.Champion => 144,
            EvolutionStage.Ultimate => throw new ArgumentOutOfRangeException(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public void OnDigimonConditionStatsSynchronized()
    {
        ConditionStats conditionStats = ServiceRelay.LiveMemoryReader.ConditionStats;

        CareMistakes = conditionStats.CareMistakes;
        Happiness = conditionStats.Happiness;
        Discipline = conditionStats.Discipline;
        Battles = conditionStats.Battles;
    }

    public void OnDigimonParameterStatsSynchronized()
    {
        ParameterStats parameterStats = ServiceRelay.LiveMemoryReader.ParameterStats;

        HP = parameterStats.HP;
        CurrentHP = parameterStats.CurrentHP;
        MP = parameterStats.MP;
        CurrentMP = parameterStats.CurrentMP;
        Offense = parameterStats.Offense;
        Defence = parameterStats.Defense;
        Speed = parameterStats.Speed;
        Brains = parameterStats.Brains;
    }

    public void OnDigimonProfileStatsSynchronized()
    {
        ProfileStats profileStats = ServiceRelay.LiveMemoryReader.ProfileStats;
        _synchedDigimon = DigimonTypes.Get(profileStats.DigimonType, _gameVariant);

        Virus = profileStats.VirusBar;
        Weight = profileStats.Weight;

        DigimonName = profileStats.Name;
        DigimonType = _synchedDigimon.DigimonName.ToString();
        Age = profileStats.AgeInDays;
        EvoAge = profileStats.EvolutionAgeInHours;

        int awakeFromHour = profileStats.WakeUpHour;
        int awakeTillHour = profileStats.SleepyHour;
        int standardAwakeTime = profileStats.StandardAwakeTime;
        ActiveTime activeType = ActiveTimeMapper.GetActiveTime(awakeFromHour, awakeTillHour, standardAwakeTime);

        Active = activeType is ActiveTime.Baby1 or ActiveTime.Baby2
            ? $"Active until {awakeTillHour} o'clock and again after {awakeFromHour} o'clock ({activeType})"
            : $"Active from {awakeFromHour} o'clock till {awakeTillHour} o'clock ({activeType})";
    }

    private void OnCareStatsSynchronized()
    {
        CareStats careStats = ServiceRelay.LiveMemoryReader.CareStats;

        PoopLevel = careStats.PoopLevel;
        PoopingTimer = careStats.PoopingTimer;
        Tiredness = careStats.Tiredness;
        EnergyLevel = careStats.EnergyLevel;
        HungryTimer = careStats.HungryTimer;
        StarvationTimer = careStats.StarvationTimer;
    }

    private void OnDigimonTechniqueStatsSynchronized()
    {
        TechniqueStats techniqueStats = ServiceRelay.LiveMemoryReader.TechniqueStats;

        TechniqueCount = techniqueStats.LearnedTechniqueCount();
    }

    private void OnHistoricEvolutionsSynchronized()
    {
    }

    private void InstantDisplay() => _speakingSimulator.RequestInstantDisplay();

    private void SetShowEvo()
    {
        ShowEvo = true;

        UpdateEvoResult();
    }

    private void SetHideEvo()
    {
        ShowEvo = false;

        UpdateEvoResult();
    }

    private void SetEvoResultMaskCommand(EvoResultMask evoResultMask)
    {
        _currentEvoResultMask = evoResultMask;

        OnPropertyChanged(nameof(EvoResultMaskNoneIsSelected));
        OnPropertyChanged(nameof(EvoResultMaskBlurredIsSelected));
        OnPropertyChanged(nameof(EvoResultMaskDigiGuessIsSelected));
        OnPropertyChanged(nameof(EvoResultMaskAnonymousIsSelected));
        OnPropertyChanged(nameof(EvoResultDigiGuessIsShown));
        OnPropertyChanged(nameof(EvoResultBlurredIsShown));

        if (ShowEvo)
        {
            UpdateEvoResult();
        }
    }

    private void UpdateEvoResult()
    {
        if (HideEvo)
        {
            EvoResultImage = new BitmapImage(new Uri(string.Format(HIDE_EVO)));
            return;
        }

        EvoResultImage = new BitmapImage(new Uri(string.Format(DIGIMON_IMAGE, EvolutionResult, CurrentEvoResultMaskSuffix())));

        if (_currentEvoResultMask == EvoResultMask.DigiGuess)
        {
            EvoResultBackground = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(string.Format(BACKGROUND, CurrentEvoResultMaskSuffix()))),
                Stretch = Stretch.UniformToFill
            };
        }
        else
        {
            EvoResultBackground = _defaultEvolutionResultBackground;
        }
    }

    private string CurrentEvoResultMaskSuffix() =>
        _currentEvoResultMask switch
        {
            EvoResultMask.None => NONE_VARIANT_SUFFIX,
            EvoResultMask.Blurred => BLURRED_VARIANT_SUFFIX,
            EvoResultMask.DigiGuess => DIGI_GUESS_VARIANT_SUFFIX,
            EvoResultMask.Anonymous => ANONYMOUS_VARIANT_SUFFIX,
            _ => throw new ArgumentOutOfRangeException()
        };

    public void Dispose() => _disposables.Dispose();
}