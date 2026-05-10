 using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Media;
using DigimonWorld.Evolution.Calculator.Core;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using DigimonWorld.Frontend.WPF.Services;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvoTreeHelper.Models;
using Domain;
using Shared.Constants;
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
    private const double COLUMN_SPACING = 160;
    private const double ROW_SPACING = -7;

    private static readonly Brush _lightBlue = new SolidColorBrush(Color.FromRgb(0x5E, 0xD6, 0xD6));
    private static readonly Brush _yellow = new SolidColorBrush(Color.FromRgb(0xE8, 0xA8, 0x35));
    private static readonly Brush _green = new SolidColorBrush(Color.FromRgb(0x6D, 0xC5, 0x6D));
    private static readonly Brush _blue = new SolidColorBrush(Color.FromRgb(0x4A, 0x9F, 0xC5));
    private static readonly Brush _pink = new SolidColorBrush(Color.FromRgb(0xBB, 0x7A, 0xD6));
    private static readonly Brush _red = new SolidColorBrush(Color.FromRgb(0xC5, 0x4A, 0x4A));

    private static Brush[] GetConnectionColors(int count) => count switch
    {
        1 => [_lightBlue],
        2 => [_lightBlue, _blue],
        3 => [_green, _lightBlue, _blue],
        4 => [_yellow, _green, _lightBlue, _blue],
        5 => [_yellow, _green, _lightBlue, _blue, _pink],
        _ => [_yellow, _green, _lightBlue, _blue, _pink, _red],
    };

    private readonly CompositeDisposable _disposables;
    private DigimonName _currentDigimon = DigimonName.Agumon;
    private GameVariant _gameVariant = GameVariant.Original;
    private Dictionary<DigimonName, IEvolutionCriteria> _criteriaMap = new();
    private List<DigimonName> _forwardEvolutions = [];

    public EvoTreeHelperViewModel()
    {
        _disposables = new CompositeDisposable(
            EmulatorLinkEventHub.DigimonProfileStatsSynchronizedObservable.Subscribe(_ => OnProfileStatsSynced()),
            EmulatorLinkEventHub.DigimonParameterStatsSynchronizedObservable.Subscribe(_ => RefreshUserStats()),
            UserConfigurationManager.CurrentEvolutionCalculatorConfig.Subscribe(config =>
            {
                _gameVariant = config.GameVariant;
                TryReadCurrentDigimon();
            })
        );

        TryReadCurrentDigimon();
        RefreshUserStats();
        BuildGraph();
    }

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

    public double NodeSize => NODE_SIZE;
    public double CanvasWidth => CANVAS_WIDTH;
    public double CanvasHeight => CANVAS_HEIGHT;

    private EvolutionCriteriaDisplay? _criteria0;
    public EvolutionCriteriaDisplay? Criteria0 { get => _criteria0; private set => SetField(ref _criteria0, value); }
    private EvolutionCriteriaDisplay? _criteria1;
    public EvolutionCriteriaDisplay? Criteria1 { get => _criteria1; private set => SetField(ref _criteria1, value); }
    private EvolutionCriteriaDisplay? _criteria2;
    public EvolutionCriteriaDisplay? Criteria2 { get => _criteria2; private set => SetField(ref _criteria2, value); }
    private EvolutionCriteriaDisplay? _criteria3;
    public EvolutionCriteriaDisplay? Criteria3 { get => _criteria3; private set => SetField(ref _criteria3, value); }
    private EvolutionCriteriaDisplay? _criteria4;
    public EvolutionCriteriaDisplay? Criteria4 { get => _criteria4; private set => SetField(ref _criteria4, value); }
    private EvolutionCriteriaDisplay? _criteria5;
    public EvolutionCriteriaDisplay? Criteria5 { get => _criteria5; private set => SetField(ref _criteria5, value); }

    private string _currentIconPath = "";
    public string CurrentIconPath { get => _currentIconPath; private set => SetField(ref _currentIconPath, value); }
    private string _currentDigimonDisplayName = "";
    public string CurrentDigimonName { get => _currentDigimonDisplayName; private set => SetField(ref _currentDigimonDisplayName, value); }

    private string _currentHP = "0";
    public string CurrentHP { get => _currentHP; private set => SetField(ref _currentHP, value); }
    private string _currentMP = "0";
    public string CurrentMP { get => _currentMP; private set => SetField(ref _currentMP, value); }
    private string _currentOff = "0";
    public string CurrentOff { get => _currentOff; private set => SetField(ref _currentOff, value); }
    private string _currentDef = "0";
    public string CurrentDef { get => _currentDef; private set => SetField(ref _currentDef, value); }
    private string _currentSpeed = "0";
    public string CurrentSpeed { get => _currentSpeed; private set => SetField(ref _currentSpeed, value); }
    private string _currentBrains = "0";
    public string CurrentBrains { get => _currentBrains; private set => SetField(ref _currentBrains, value); }
    private string _currentWeight = "0";
    public string CurrentWeight { get => _currentWeight; private set => SetField(ref _currentWeight, value); }
    private string _currentCareMistakes = "0";
    public string CurrentCareMistakes { get => _currentCareMistakes; private set => SetField(ref _currentCareMistakes, value); }
    private string _currentHappiness = "0";
    public string CurrentHappiness { get => _currentHappiness; private set => SetField(ref _currentHappiness, value); }
    private string _currentDiscipline = "0";
    public string CurrentDiscipline { get => _currentDiscipline; private set => SetField(ref _currentDiscipline, value); }
    private string _currentBattles = "0";
    public string CurrentBattles { get => _currentBattles; private set => SetField(ref _currentBattles, value); }
    private string _currentTechniqueCount = "0";
    public string CurrentTechniqueCount { get => _currentTechniqueCount; private set => SetField(ref _currentTechniqueCount, value); }

    private void RefreshUserStats()
    {
        try
        {
            UserDigimon d = Session.UserDigimon;
            CurrentHP = d.HP.ToString();
            CurrentMP = d.MP.ToString();
            CurrentOff = d.Off.ToString();
            CurrentDef = d.Def.ToString();
            CurrentSpeed = d.Speed.ToString();
            CurrentBrains = d.Brains.ToString();
            CurrentWeight = d.Weight.ToString();
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

    private void RefreshCriteria()
    {
        UserDigimon d = Session.UserDigimon;
        int userHp = d.HP, userMp = d.MP, userOff = d.Off, userDef = d.Def;
        int userSpeed = d.Speed, userBrains = d.Brains, userWeight = d.Weight;
        int userCareMistakes = d.CareMistakes, userHappiness = d.Happiness;
        int userDiscipline = d.Discipline, userBattles = d.Battles, userTechniqueCount = d.TechniqueCount;

        EvolutionCriteriaDisplay?[] existing = [Criteria0, Criteria1, Criteria2, Criteria3, Criteria4, Criteria5];
        List<EvolutionCriteriaDisplay> criteriaList = [];
        int index = 0;
        foreach (DigimonName evo in _forwardEvolutions)
        {
            if (_criteriaMap.TryGetValue(evo, out IEvolutionCriteria? criteria))
            {
                EvolutionCriteriaDisplay? current = index < existing.Length ? existing[index] : null;
                if (current != null && current.Name == evo.ToString())
                {
                    current.IsWinningEvolution = false;
                    current.UpdateUserStats(userHp, userMp, userOff, userDef, userSpeed, userBrains, userWeight, userCareMistakes, userHappiness, userDiscipline, userBattles, userTechniqueCount);
                    criteriaList.Add(current);
                } else
                {
                    criteriaList.Add(new EvolutionCriteriaDisplay(evo.ToString(), DigimonIconFactory.Create(evo).IconPath, criteria, userHp, userMp, userOff, userDef, userSpeed, userBrains, userWeight, userCareMistakes, userHappiness, userDiscipline, userBattles, userTechniqueCount));
                }
                index++;
            }
        }

        List<(bool IsEnabled, int ScoreTotal, int StatCount)> evolutionData = criteriaList.Select(c => (c.IsEnabled, c.ScoreTotal, c.StatCount)).ToList();
        bool useCarriedOverStats = _gameVariant == GameVariant.Original;
        int winnerIndex = FromRookieOrChampionEvolutionScoreCalculator.DetermineWinningEvolutionIndex(evolutionData, useCarriedOverStats);

        if (winnerIndex >= 0)
        {
            criteriaList[winnerIndex].IsWinningEvolution = true;
        }

        Criteria0 = criteriaList.Count > 0 ? criteriaList[0] : null;
        Criteria1 = criteriaList.Count > 1 ? criteriaList[1] : null;
        Criteria2 = criteriaList.Count > 2 ? criteriaList[2] : null;
        Criteria3 = criteriaList.Count > 3 ? criteriaList[3] : null;
        Criteria4 = criteriaList.Count > 4 ? criteriaList[4] : null;
        Criteria5 = criteriaList.Count > 5 ? criteriaList[5] : null;
    }

    private void OnProfileStatsSynced()
    {
        try
        {
            byte digimonType = ServiceRelay.LiveMemoryReader.ProfileStats.DigimonType;
            if (digimonType == 0)
            {
                return;
            }

            Digimon digimon = DigimonTypes.Get(digimonType, _gameVariant);

            if (digimon.DigimonName != _currentDigimon)
            {
                _currentDigimon = digimon.DigimonName;
                Application.Current.Dispatcher.Invoke(BuildGraph);
            }
        }
        catch
        {
            // Emulator may not be connected or data not available yet
        }
    }

    private void TryReadCurrentDigimon()
    {
        try
        {
            byte digimonType = ServiceRelay.LiveMemoryReader.ProfileStats.DigimonType;
            if (digimonType == 0)
            {
                return;
            }

            Digimon digimon = DigimonTypes.Get(digimonType, _gameVariant);
            _currentDigimon = digimon.DigimonName;
        }
        catch
        {
            // Emulator may not be connected or data not available yet
        }
    }

    private void BuildGraph()
    {
        DigimonName center = _currentDigimon;

        ObservableCollection<EvoTreeNode> newNodes = [];
        ObservableCollection<ResolvedConnection> newConnections = [];
        HashSet<DigimonName> visited = [center];

        CurrentIconPath = DigimonIconFactory.Create(center).IconPath;
        CurrentDigimonName = center.ToString();

        List<List<DigimonName>> forwardColumns = ExpandColumns(
            [center], visited, EvolutionPathProvider.GetEvolutions, maxDepth: 1);

        int totalColumns = 1 + forwardColumns.Count;
        double treeWidth = (totalColumns - 1) * COLUMN_SPACING + NODE_SIZE;
        double centerColumnX = (CANVAS_WIDTH - treeWidth) / 2.0;

        Dictionary<DigimonName, EvoTreeNode> nodeMap = new();

        const double centerY = DATA_ROWS_CENTER - NODE_SIZE / 2.0;
        EvoTreeNode centerNode = CreateNode(center, centerColumnX, centerY);
        newNodes.Add(centerNode);
        nodeMap[center] = centerNode;

        _criteriaMap = GetEvolutionCriteriaMap(center);

        for (int col = 0; col < forwardColumns.Count; col++)
        {
            double x = centerColumnX + (col + 1) * COLUMN_SPACING;
            PlaceColumn(forwardColumns[col], x, newNodes, nodeMap);
        }

        _forwardEvolutions = forwardColumns.SelectMany(c => c).ToList();
        RefreshCriteria();

        AddConnections(
            [center], forwardColumns, nodeMap, newConnections, EvolutionPathProvider.GetEvolutions);

        Nodes = newNodes;
        Connections = newConnections;
        SpecialEvolutions = new ObservableCollection<SpecialEvolutionInfo>(
            SpecialEvolutionProvider.GetAvailableSpecialEvolutions(center));
    }

    private static List<List<DigimonName>> ExpandColumns(
        List<DigimonName> startColumn,
        HashSet<DigimonName> visited,
        Func<DigimonName, DigimonName[]> getRelated,
        int maxDepth = int.MaxValue)
    {
        List<List<DigimonName>> columns = [];
        List<DigimonName> currentColumn = startColumn;
        int depth = 0;

        while (currentColumn.Count > 0 && depth < maxDepth)
        {
            List<DigimonName> nextColumn = [];
            foreach (DigimonName digimon in currentColumn)
            {
                foreach (DigimonName related in getRelated(digimon))
                {
                    if (visited.Add(related))
                    {
                        nextColumn.Add(related);
                    }
                }
            }

            if (nextColumn.Count > 0)
            {
                columns.Add(nextColumn);
            }
            currentColumn = nextColumn;
            depth++;
        }

        return columns;
    }

    private void PlaceColumn(
        List<DigimonName> column,
        double x,
        ObservableCollection<EvoTreeNode> nodes,
        Dictionary<DigimonName, EvoTreeNode> nodeMap)
    {
        double totalHeight = (column.Count - 1) * (NODE_SIZE + ROW_SPACING) + NODE_SIZE;
        double startY = DATA_ROWS_CENTER - totalHeight / 2.0;

        for (int i = 0; i < column.Count; i++)
        {
            double y = startY + i * (NODE_SIZE + ROW_SPACING);
            EvoTreeNode node = CreateNode(column[i], x, y);
            nodes.Add(node);
            nodeMap[column[i]] = node;
        }
    }

    private void AddConnections(
        List<DigimonName> startColumn,
        List<List<DigimonName>> columns,
        Dictionary<DigimonName, EvoTreeNode> nodeMap,
        ObservableCollection<ResolvedConnection> connections,
        Func<DigimonName, DigimonName[]> getRelated,
        bool reverse = false)
    {
        List<List<DigimonName>> allColumns = [startColumn, .. columns];
        for (int col = 0; col < allColumns.Count - 1; col++)
        {
            foreach (DigimonName digimon in allColumns[col])
            {
                List<DigimonName> relatedInGraph = [];
                foreach (DigimonName related in getRelated(digimon))
                {
                    if (nodeMap.ContainsKey(related))
                    {
                        relatedInGraph.Add(related);
                    }
                }

                relatedInGraph.Sort((a, b) => nodeMap[a].Y.CompareTo(nodeMap[b].Y));

                Brush[] colors = GetConnectionColors(relatedInGraph.Count);
                double lineSpacing = 6.0;
                double totalOffset = (relatedInGraph.Count - 1) * lineSpacing;
                double startOffset = -totalOffset / 2.0;

                for (int i = 0; i < relatedInGraph.Count; i++)
                {
                    double sourceYOffset = startOffset + i * lineSpacing;
                    Brush color = colors[i % colors.Length];

                    int stubTier;
                    if (color == _lightBlue || color == _blue)
                    {
                        stubTier = 1;
                    } else if (color == _green || color == _pink)
                    {
                        stubTier = 2;
                    } else
                    {
                        stubTier = 3;
                    }

                    EvoTreeNode sourceNode = reverse ? nodeMap[relatedInGraph[i]] : nodeMap[digimon];
                    EvoTreeNode targetNode = reverse ? nodeMap[digimon] : nodeMap[relatedInGraph[i]];
                    connections.Add(new ResolvedConnection(sourceNode, targetNode, color, sourceYOffset, stubTier));
                }
            }
        }
    }

    private static EvoTreeNode CreateNode(DigimonName digimon, double x, double y)
    {
        string iconPath = DigimonIconFactory.Create(digimon).IconPath;
        return new EvoTreeNode(x, y, iconPath, digimon);
    }

    private Dictionary<DigimonName, IEvolutionCriteria> GetEvolutionCriteriaMap(DigimonName center)
    {
        Dictionary<DigimonName, IEvolutionCriteria> map = new();
        try
        {
            FromRookieOrChampionEvolutionMapper mapper = new();
            List<IEvolutionCriteria> criteriaList = mapper.GetEvolutionCriteria(center);
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