using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromFresh;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromInTraining;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.Enums;
using DigimonWorld.Frontend.WPF.Services;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvoTreeHelper.Models;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvoTreeHelper.UserControls;
using Domain;
using Shared.Enums;
using Shared.Extensions;
using Shared.Services;
using Shared.Services.Events;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvoTreeHelper;

public class EvoTreeHelperViewModel : BaseViewModel, IDisposable
{
    private const double NODE_SIZE = 58;
    private const double CANVAS_WIDTH = 400;
    private const double CANVAS_HEIGHT = 340;
    private const double DATA_ROWS_CENTER = CANVAS_HEIGHT / 2.0 + 6;
    private const double ROW_SPACING = -7;
    private const double CONNECTION_LINE_SPACING = 6.0;
    private const double EVOLUTION_NODES_X_POSITION = 251;
    private const double CURRENT_DIGIMON_NODE_X_POSITION = 91;
    private const double CURRENT_DIGIMON_NODE_Y_POSITION = 147;


    private readonly Brush _lightBlue = new SolidColorBrush(Color.FromRgb(0x5E, 0xD6, 0xD6));
    private readonly Brush _yellow = new SolidColorBrush(Color.FromRgb(0xE8, 0xA8, 0x35));
    private readonly Brush _green = new SolidColorBrush(Color.FromRgb(0x6D, 0xC5, 0x6D));
    private readonly Brush _blue = new SolidColorBrush(Color.FromRgb(0x4A, 0x9F, 0xC5));
    private readonly Brush _pink = new SolidColorBrush(Color.FromRgb(0xBB, 0x7A, 0xD6));
    private readonly Brush _red = new SolidColorBrush(Color.FromRgb(0xC5, 0x4A, 0x4A));

    private readonly CompositeDisposable _disposables;
    private readonly SpeakingSimulator _speakingSimulator;

    private GameVariant _gameVariant = GameVariant.Original;
    private SpeechDelay _speechDelay;
    private Dictionary<DigimonName, IEvolutionCriteria> _criteriaMap = new();
    private List<DigimonName> _evolutions = [];

    public EvoTreeHelperViewModel()
    {
        SynchronizationContext uiSynchronizationContext = SynchronizationContext.Current!;

        _speakingSimulator = new SpeakingSimulator();

        InstantDisplayCommand = new CommandHandler(InstantDisplay);

        _disposables = new CompositeDisposable(
            _speakingSimulator,
            UserDigimonEventHub.ProfileStatsSynchronizedObservable
                .ObserveOn(uiSynchronizationContext)
                .Subscribe(_ => OnProfileStatsSynced()),
            UserDigimonEventHub.ParameterStatsSynchronizedObservable
                .ObserveOn(uiSynchronizationContext)
                .Subscribe(_ => OnParameterStatsSynchronized()),
            UserDigimonEventHub.ConditionStatsSynchronizedObservable
                .ObserveOn(uiSynchronizationContext)
                .Subscribe(_ => OnConditionStatsSynchronized()),
            UserConfigurationManager.CurrentEvolutionCalculatorConfig
                .ObserveOn(uiSynchronizationContext)
                .Subscribe(config => _gameVariant = config.GameVariant),
            UserConfigurationManager.CurrentSpeakingSimulatorConfig
                .ObserveOn(uiSynchronizationContext)
                .Subscribe(config => _speechDelay = config.NarratorMode == NarratorMode.Instant ? SpeechDelay.None : SpeechDelay.Short),
            EmulatorLinkEventHub.EmulatorConnectedObservable
                .ObserveOn(uiSynchronizationContext)
                .Subscribe(OnEmulatorConnectedChanged));

        _speechDelay = UserConfigurationManager.SpeakingSimulatorConfig.NarratorMode == NarratorMode.Instant ? SpeechDelay.None : SpeechDelay.Short;

        SpeakGabumonTextAsync(GabumonEvoTreeHelperNarratorText.EMULATOR_NOT_CONNECTED_TEXT, _speechDelay).ConfigureAwait(false);

        CurrentDigimon = DigimonName.Gabumon;

        CurrentHp = "0";
        CurrentMp = "0";
        CurrentOff = "0";
        CurrentDef = "0";
        CurrentSpeed = "0";
        CurrentBrains = "0";
        CurrentWeight = "0";
        CurrentCareMistakes = "0";
        CurrentHappiness = "0";
        CurrentDiscipline = "0";
        CurrentBattles = "0";
        CurrentTechniqueCount = "0";
    }

    public double CanvasWidth => CANVAS_WIDTH;
    public double CanvasHeight => CANVAS_HEIGHT;

    public ICommand InstantDisplayCommand { get; }

    public ObservableCollection<EvoTreeNode> Nodes
    {
        get;
        private set => SetField(ref field, value);
    } = [];

    public ObservableCollection<ResolvedConnection> Connections
    {
        get;
        private set => SetField(ref field, value);
    } = [];

    public ObservableCollection<SpecialEvolutionInfo> SpecialEvolutions
    {
        get;
        private set => SetField(ref field, value);
    } = [];

    public string GabumonText
    {
        get;
        set => SetField(ref field, value);
    } = string.Empty;

    private Brush[] GetConnectionColors(int count) => count switch
    {
        1 => [_lightBlue],
        2 => [_lightBlue, _blue],
        3 => [_green, _lightBlue, _blue],
        4 => [_yellow, _green, _lightBlue, _blue],
        5 => [_yellow, _green, _lightBlue, _blue, _pink],
        _ => [_yellow, _green, _lightBlue, _blue, _pink, _red]
    };

    public DigimonName CurrentDigimon
    {
        get;
        private set
        {
            if (SetField(ref field, value))
            {
                BuildGraph();
            }
        }
    } = DigimonName.None;

    public CriteriaColumnViewModel? Evolution1
    {
        get;
        private set => SetField(ref field, value);
    }

    public CriteriaColumnViewModel? Evolution2
    {
        get;
        private set => SetField(ref field, value);
    }

    public CriteriaColumnViewModel? Evolution3
    {
        get;
        private set => SetField(ref field, value);
    }

    public CriteriaColumnViewModel? Evolution4
    {
        get;
        private set => SetField(ref field, value);
    }

    public CriteriaColumnViewModel? Evolution5
    {
        get;
        private set => SetField(ref field, value);
    }

    public CriteriaColumnViewModel? Evolution6
    {
        get;
        private set => SetField(ref field, value);
    }

    public string CurrentIconPath
    {
        get;
        private set => SetField(ref field, value);
    } = "";

    public string CurrentDigimonName
    {
        get;
        private set => SetField(ref field, value);
    } = "";

    public string CurrentHp
    {
        get;
        private set => SetField(ref field, value);
    } = "0";

    public string CurrentMp
    {
        get;
        private set => SetField(ref field, value);
    } = "0";

    public string CurrentOff
    {
        get;
        private set => SetField(ref field, value);
    } = "0";

    public string CurrentDef
    {
        get;
        private set => SetField(ref field, value);
    } = "0";

    public string CurrentSpeed
    {
        get;
        private set => SetField(ref field, value);
    } = "0";

    public string CurrentBrains
    {
        get;
        private set => SetField(ref field, value);
    } = "0";

    public string CurrentWeight
    {
        get;
        private set => SetField(ref field, value);
    } = "0";

    public string CurrentCareMistakes
    {
        get;
        private set => SetField(ref field, value);
    } = "0";

    public string CurrentHappiness
    {
        get;
        private set => SetField(ref field, value);
    } = "0";

    public string CurrentDiscipline
    {
        get;
        private set => SetField(ref field, value);
    } = "0";

    public string CurrentBattles
    {
        get;
        private set => SetField(ref field, value);
    } = "0";

    public string CurrentTechniqueCount
    {
        get;
        private set => SetField(ref field, value);
    } = "0";

    private void InstantDisplay() => _speakingSimulator.RequestInstantDisplay();

    private void OnProfileStatsSynced()
    {
        try
        {
            UserDigimon userDigimon = UserDigimon.Instance;

            CurrentDigimon = userDigimon.DigimonName;
            CurrentWeight = userDigimon.Weight.ToString();

            Application.Current.Dispatcher.Invoke(RefreshCriteria);
        }
        catch
        {
            // Emulator may not be connected or data not available yet
        }
    }

    private void OnParameterStatsSynchronized()
    {
        try
        {
            UserDigimon d = Session.UserDigimon;
            CurrentHp = d.Hp.ToString();
            CurrentMp = d.Mp.ToString();
            CurrentOff = d.Off.ToString();
            CurrentDef = d.Def.ToString();
            CurrentSpeed = d.Speed.ToString();
            CurrentBrains = d.Brains.ToString();
        }
        catch
        {
            // Emulator may not be connected
        }

        RefreshCriteria();
    }

    private void OnConditionStatsSynchronized()
    {
        try
        {
            UserDigimon d = Session.UserDigimon;
            CurrentCareMistakes = d.CareMistakes.ToString();
            CurrentHappiness = d.Happiness.ToString();
            CurrentDiscipline = d.Discipline.ToString();
            CurrentBattles = d.Battles.ToString();
            CurrentTechniqueCount = d.TechniqueCount.ToString();
        }
        catch
        {
            // Emulator may not be connected
        }

        RefreshCriteria();
    }

    private void OnEmulatorConnectedChanged(bool isConnected)
    {
        if (isConnected)
        {
            SpeakGabumonTextAsync(GabumonEvoTreeHelperNarratorText.EMULATOR_CONNECTED_TEXT, _speechDelay).ConfigureAwait(false);
        }
        else
        {
            SpeakGabumonTextAsync(GabumonEvoTreeHelperNarratorText.EMULATOR_NOT_CONNECTED_TEXT, _speechDelay).ConfigureAwait(false);

            CurrentDigimon = DigimonName.Gabumon;

            CurrentHp = "0";
            CurrentMp = "0";
            CurrentOff = "0";
            CurrentDef = "0";
            CurrentSpeed = "0";
            CurrentBrains = "0";
            CurrentWeight = "0";
            CurrentCareMistakes = "0";
            CurrentHappiness = "0";
            CurrentDiscipline = "0";
            CurrentBattles = "0";
            CurrentTechniqueCount = "0";
        }
    }

    private void RefreshCriteria()
    {
        UserDigimon userDigimon = Session.UserDigimon;
        int userHp = userDigimon.Hp, userMp = userDigimon.Mp, userOff = userDigimon.Off, userDef = userDigimon.Def;
        int userSpeed = userDigimon.Speed, userBrains = userDigimon.Brains, userWeight = userDigimon.Weight;
        int userCareMistakes = userDigimon.CareMistakes, userHappiness = userDigimon.Happiness;
        int userDiscipline = userDigimon.Discipline, userBattles = userDigimon.Battles, userTechniqueCount = userDigimon.TechniqueCount;

        List<CriteriaColumnViewModel> evolutionCriteriaDisplayViewModels = [];
        foreach (DigimonName evolutionCandidate in _evolutions)
        {
            if (!_criteriaMap.TryGetValue(evolutionCandidate, out IEvolutionCriteria? criteria))
            {
                continue;
            }

            CriteriaColumnViewModel criteriaColumnViewModel = new(userDigimon.DigimonName, evolutionCandidate.ToString(), DigimonIconFactory.Create(evolutionCandidate).IconPath, criteria, userHp, userMp, userOff, userDef,
                userSpeed, userBrains, userWeight, userCareMistakes, userHappiness, userDiscipline, userBattles, userTechniqueCount);

            evolutionCriteriaDisplayViewModels.Add(criteriaColumnViewModel);
        }

        int winnerIndex = -1;

        switch (userDigimon.EvolutionStage)
        {
            case EvolutionStage.Fresh:
                winnerIndex = 0;
                break;
            case EvolutionStage.InTraining:
                winnerIndex = evolutionCriteriaDisplayViewModels.FindIndex(x =>
                    x.IsHpMet == true ||
                    x.IsMpMet == true ||
                    x.IsOffMet == true ||
                    x.IsDefMet == true ||
                    x.IsSpeedMet == true ||
                    x.IsBrainsMet == true
                );
                break;
            case EvolutionStage.Rookie or EvolutionStage.Champion:
            {
                List<(bool IsEnabled, int ScoreTotal, int StatCount)> evolutionData = evolutionCriteriaDisplayViewModels.Select(c => (c.IsEnabled, c.ScoreTotal, c.StatCount)).ToList();
                bool useCarriedOverStats = _gameVariant == GameVariant.Original;
                winnerIndex = FromRookieOrChampionEvolutionScoreCalculator.DetermineWinningEvolutionIndex(evolutionData, useCarriedOverStats);
                break;
            }
        }

        if (winnerIndex >= 0 && winnerIndex < evolutionCriteriaDisplayViewModels.Count)
        {
            evolutionCriteriaDisplayViewModels[winnerIndex].IsWinningEvolution = true;
        }

        Evolution1 = evolutionCriteriaDisplayViewModels.Count > 0 ? evolutionCriteriaDisplayViewModels[0] : null;
        Evolution2 = evolutionCriteriaDisplayViewModels.Count > 1 ? evolutionCriteriaDisplayViewModels[1] : null;
        Evolution3 = evolutionCriteriaDisplayViewModels.Count > 2 ? evolutionCriteriaDisplayViewModels[2] : null;
        Evolution4 = evolutionCriteriaDisplayViewModels.Count > 3 ? evolutionCriteriaDisplayViewModels[3] : null;
        Evolution5 = evolutionCriteriaDisplayViewModels.Count > 4 ? evolutionCriteriaDisplayViewModels[4] : null;
        Evolution6 = evolutionCriteriaDisplayViewModels.Count > 5 ? evolutionCriteriaDisplayViewModels[5] : null;
    }

    private Task SpeakGabumonTextAsync(string text, SpeechDelay delayMs = SpeechDelay.None) => _speakingSimulator.SpeakAsync(text, output => GabumonText = output, delayMs);

    private void BuildGraph()
    {
        DigimonName currentDigimon = CurrentDigimon;

        ObservableCollection<EvoTreeNode> newNodes = [];

        CurrentIconPath = DigimonIconFactory.Create(currentDigimon).IconPath;
        CurrentDigimonName = currentDigimon.ToString();

        _evolutions = EvolutionPathProvider.GetEvolutions(currentDigimon).ToList();

        Dictionary<DigimonName, EvoTreeNode> nodeMap = new();

        EvoTreeNode centerNode = CreateNode(currentDigimon, CURRENT_DIGIMON_NODE_X_POSITION, CURRENT_DIGIMON_NODE_Y_POSITION);
        newNodes.Add(centerNode);
        nodeMap[currentDigimon] = centerNode;

        _criteriaMap = GetEvolutionCriteriaMap(currentDigimon);

        PlaceColumn(_evolutions, newNodes, nodeMap);

        RefreshCriteria();

        AddConnections(_evolutions, nodeMap);

        Nodes = newNodes;
        SpecialEvolutions = new ObservableCollection<SpecialEvolutionInfo>(SpecialEvolutionProvider.GetAvailableSpecialEvolutions(currentDigimon));
    }

    private void PlaceColumn(
        List<DigimonName> column,
        ObservableCollection<EvoTreeNode> nodes,
        Dictionary<DigimonName, EvoTreeNode> nodeMap)
    {
        double totalHeight = (column.Count - 1) * (NODE_SIZE + ROW_SPACING) + NODE_SIZE;
        double startY = DATA_ROWS_CENTER - totalHeight / 2.0;

        for (int i = 0; i < column.Count; i++)
        {
            double y = startY + i * (NODE_SIZE + ROW_SPACING);
            EvoTreeNode node = CreateNode(column[i], EVOLUTION_NODES_X_POSITION, y);
            nodes.Add(node);
            nodeMap[column[i]] = node;
        }
    }

    private void AddConnections(
        List<DigimonName> evolutions,
        Dictionary<DigimonName, EvoTreeNode> nodeMap)
    {
        ObservableCollection<ResolvedConnection> newConnections = [];

        Brush[] colors = GetConnectionColors(evolutions.Count);

        double startOffset = -((_evolutions.Count - 1) * CONNECTION_LINE_SPACING) / 2.0;

        for (int index = 0; index < _evolutions.Count; index++)
        {
            Brush color = colors[index];

            newConnections.Add(
                new ResolvedConnection(
                    nodeMap[CurrentDigimon],
                    nodeMap[_evolutions[index]],
                    color,
                    startOffset + index * CONNECTION_LINE_SPACING,
                    GetStubTier(color, evolutions.Count)));
        }

        Connections = newConnections;
    }

    private int GetStubTier(Brush color, int evolutionCount) =>
        color switch
        {
            _ when color == _lightBlue && evolutionCount == 1 => 1,

            _ when color == _lightBlue && evolutionCount == 2 => 1,
            _ when color == _blue && evolutionCount == 2 => 1,

            _ when color == _green && evolutionCount == 3 => 2,
            _ when color == _lightBlue && evolutionCount == 3 => 1,
            _ when color == _blue && evolutionCount == 3 => 2,

            _ when color == _yellow && evolutionCount == 4 => 2,
            _ when color == _green && evolutionCount == 4 => 1,
            _ when color == _lightBlue && evolutionCount == 4 => 1,
            _ when color == _blue && evolutionCount == 4 => 2,

            _ when color == _yellow && evolutionCount == 5 => 3,
            _ when color == _green && evolutionCount == 5 => 1,
            _ when color == _lightBlue && evolutionCount == 5 => 1,
            _ when color == _blue && evolutionCount == 5 => 1,
            _ when color == _pink && evolutionCount == 5 => 3,

            _ when color == _yellow && evolutionCount == 6 => 3,
            _ when color == _green && evolutionCount == 6 => 2,
            _ when color == _lightBlue && evolutionCount == 6 => 1,
            _ when color == _blue && evolutionCount == 6 => 1,
            _ when color == _pink && evolutionCount == 6 => 2,
            _ when color == _red && evolutionCount == 6 => 3,

            _ when color != _yellow && color != _green && color != _lightBlue && color != _blue && color != _pink && color != _red => throw new ArgumentOutOfRangeException(nameof(color), color, "Color is not supported as a subtier"),
            _ when evolutionCount is > 6 or < 1 => throw new ArgumentOutOfRangeException(nameof(evolutionCount), evolutionCount, "Evolution count must be between 1 and 6"),
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, "Unexpected error while determining stub tier")
        };
    // 1 => [_lightBlue],
    // 2 => [_lightBlue, _blue],
    // 3 => [_green, _lightBlue, _blue],
    // 4 => [_yellow, _green, _lightBlue, _blue],
    // 5 => [_yellow, _green, _lightBlue, _blue, _pink],
    // _ => [_yellow, _green, _lightBlue, _blue, _pink, _red],

    private EvoTreeNode CreateNode(DigimonName digimon, double x, double y)
    {
        string iconPath = DigimonIconFactory.Create(digimon).IconPath;
        return new EvoTreeNode(x, y, iconPath, digimon);
    }

    private Dictionary<DigimonName, IEvolutionCriteria> GetEvolutionCriteriaMap(DigimonName center)
    {
        Dictionary<DigimonName, IEvolutionCriteria> map = new();
        try
        {
            List<IEvolutionCriteria> criteriaList = [];

            switch (center.EvolutionStage())
            {
                case EvolutionStage.Fresh:
                    FromFreshEvolutionMapper fromFreshEvolutionMapper = new();
                    criteriaList = [fromFreshEvolutionMapper[center]];
                    break;
                case EvolutionStage.InTraining:
                    FromInTrainingEvolutionMapper fromInTrainingEvolutionMapper = new();
                    criteriaList = fromInTrainingEvolutionMapper[center].ToList();
                    break;
                case EvolutionStage.Rookie:
                case EvolutionStage.Champion:
                    FromRookieOrChampionEvolutionMapper fromRookieOrChampionEvolutionMapper = new();
                    criteriaList = fromRookieOrChampionEvolutionMapper.GetEvolutionCriteria(center);
                    break;
                case EvolutionStage.Ultimate:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            foreach (IEvolutionCriteria criteria in criteriaList)
            {
                DigimonName evoName = criteria.EvolutionResult.ToDigimonType();
                map[evoName] = criteria;
            }
        }
        catch
        {
            // Criteria may not be available for this digimon stage
        }

        return map;
    }

    public void Dispose() => _disposables.Dispose();
}