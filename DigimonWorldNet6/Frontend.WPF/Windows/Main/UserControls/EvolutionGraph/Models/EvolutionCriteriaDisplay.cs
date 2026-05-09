using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvolutionGraph.Models;

public class EvolutionCriteriaDisplay : BaseViewModel
{
    private readonly IEvolutionCriteria _criteria;

    public EvolutionCriteriaDisplay(string name, string iconPath, IEvolutionCriteria criteria, int userHp, int userMp, int userOff, int userDef, int userSpeed, int userBrains, int userWeight, int userCareMistakes, int userHappiness, int userDiscipline, int userBattles, int userTechniqueCount)
    {
        _criteria = criteria;
        Name = name;
        IconPath = iconPath;
        HP = criteria.Stats.HP > 0 ? criteria.Stats.HP.ToString() : "";
        MP = criteria.Stats.MP > 0 ? criteria.Stats.MP.ToString() : "";
        Off = criteria.Stats.Off > 0 ? criteria.Stats.Off.ToString() : "";
        Def = criteria.Stats.Def > 0 ? criteria.Stats.Def.ToString() : "";
        Speed = criteria.Stats.Speed > 0 ? criteria.Stats.Speed.ToString() : "";
        Brains = criteria.Stats.Brains > 0 ? criteria.Stats.Brains.ToString() : "";
        Weight = $"{criteria.Weight.LowerWeightLimit} - {criteria.Weight.UpperWeightLimit}";

        string cmOp = criteria.CareMistakes.IsCareMistakesCriteriaAMaximum ? "≤ " : "≥ ";
        CareMistakes = criteria.CareMistakes.CareMistakes >= 0
            ? $"{cmOp}{criteria.CareMistakes.CareMistakes}"
            : "";

        var b = criteria.BonusCriteria;
        Happiness = b.Happiness >= 0 ? b.Happiness.ToString() : "";
        Discipline = b.Discipline >= 0 ? b.Discipline.ToString() : "";
        string batOp = b.IsBattlesCriteriaAMaximum ? "≤ " : "≥ ";
        Battles = b.Battles >= 0 ? $"{batOp}{b.Battles}" : "";
        TechniqueCount = b.TechniqueCount > 0 ? b.TechniqueCount.ToString() : "";

        UpdateUserStats(userHp, userMp, userOff, userDef, userSpeed, userBrains, userWeight, userCareMistakes, userHappiness, userDiscipline, userBattles, userTechniqueCount);
    }

    public void UpdateUserStats(int userHp, int userMp, int userOff, int userDef, int userSpeed, int userBrains, int userWeight, int userCareMistakes, int userHappiness, int userDiscipline, int userBattles, int userTechniqueCount)
    {
        var criteria = _criteria;
        var b = criteria.BonusCriteria;

        UserHP = userHp.ToString();
        UserMP = userMp.ToString();
        UserOff = userOff.ToString();
        UserDef = userDef.ToString();
        UserSpeed = userSpeed.ToString();
        UserBrains = userBrains.ToString();
        UserWeight = userWeight.ToString();
        UserCareMistakes = userCareMistakes.ToString();
        UserHappiness = userHappiness.ToString();
        UserDiscipline = userDiscipline.ToString();
        UserBattles = userBattles.ToString();
        UserTechniqueCount = userTechniqueCount.ToString();

        IsHPMet = criteria.Stats.HP > 0 ? userHp >= criteria.Stats.HP : null;
        IsMPMet = criteria.Stats.MP > 0 ? userMp >= criteria.Stats.MP : null;
        IsOffMet = criteria.Stats.Off > 0 ? userOff >= criteria.Stats.Off : null;
        IsDefMet = criteria.Stats.Def > 0 ? userDef >= criteria.Stats.Def : null;
        IsSpeedMet = criteria.Stats.Speed > 0 ? userSpeed >= criteria.Stats.Speed : null;
        IsBrainsMet = criteria.Stats.Brains > 0 ? userBrains >= criteria.Stats.Brains : null;
        IsWeightMet = userWeight >= criteria.Weight.LowerWeightLimit && userWeight <= criteria.Weight.UpperWeightLimit;

        if (criteria.CareMistakes.CareMistakes < 0)
        {
            IsCareMistakesMet = null;
        } else if (criteria.CareMistakes.IsCareMistakesCriteriaAMaximum)
        {
            IsCareMistakesMet = userCareMistakes <= criteria.CareMistakes.CareMistakes;
        } else
        {
            IsCareMistakesMet = userCareMistakes >= criteria.CareMistakes.CareMistakes;
        }

        IsHappinessMet = b.Happiness >= 0 ? userHappiness >= b.Happiness : null;
        IsDisciplineMet = b.Discipline >= 0 ? userDiscipline >= b.Discipline : null;

        if (b.Battles < 0)
        {
            IsBattlesMet = null;
        } else if (b.IsBattlesCriteriaAMaximum)
        {
            IsBattlesMet = userBattles <= b.Battles;
        } else
        {
            IsBattlesMet = userBattles >= b.Battles;
        }

        IsTechniqueCountMet = b.TechniqueCount > 0 ? userTechniqueCount >= b.TechniqueCount : null;

        bool statsMet = userHp >= criteria.Stats.HP
                        && userMp >= criteria.Stats.MP
                        && userOff >= criteria.Stats.Off
                        && userDef >= criteria.Stats.Def
                        && userSpeed >= criteria.Stats.Speed
                        && userBrains >= criteria.Stats.Brains;

        bool careMistakesMet = (criteria.CareMistakes.IsCareMistakesCriteriaAMaximum
            ? userCareMistakes <= criteria.CareMistakes.CareMistakes
            : userCareMistakes >= criteria.CareMistakes.CareMistakes);

        bool weightMet = IsWeightMet;

        bool bonusMet = (b.Happiness >= 0 && userHappiness >= b.Happiness)
                        || (b.Discipline >= 0 && userDiscipline >= b.Discipline)
                        || (b.Battles >= 0 && (b.IsBattlesCriteriaAMaximum
                            ? userBattles <= b.Battles
                            : userBattles >= b.Battles))
                        || userTechniqueCount >= b.TechniqueCount;

        int criteriaMetCount = (statsMet ? 1 : 0) + (careMistakesMet ? 1 : 0)
                               + (weightMet ? 1 : 0) + (bonusMet ? 1 : 0);
        IsEnabled = criteriaMetCount >= 3;

        int scoreTotal = 0;
        int scoreCount = 0;
        if (criteria.Stats.HP > 0) { scoreTotal += userHp / 10; scoreCount++; }
        if (criteria.Stats.MP > 0) { scoreTotal += userMp / 10; scoreCount++; }
        if (criteria.Stats.Off > 0) { scoreTotal += userOff; scoreCount++; }
        if (criteria.Stats.Def > 0) { scoreTotal += userDef; scoreCount++; }
        if (criteria.Stats.Speed > 0) { scoreTotal += userSpeed; scoreCount++; }
        if (criteria.Stats.Brains > 0) { scoreTotal += userBrains; scoreCount++; }
        ScoreTotal = scoreTotal;
        StatCount = scoreCount;
        EvolutionScore = scoreCount > 0 ? scoreTotal / scoreCount : 0;
    }

    public string Name { get; }
    public string IconPath { get; }
    public string HP { get; }
    public string MP { get; }
    public string Off { get; }
    public string Def { get; }
    public string Speed { get; }
    public string Brains { get; }
    public string Weight { get; }
    public string CareMistakes { get; }

    private string _userHP = "";
    public string UserHP { get => _userHP; private set => SetField(ref _userHP, value); }
    private string _userMP = "";
    public string UserMP { get => _userMP; private set => SetField(ref _userMP, value); }
    private string _userOff = "";
    public string UserOff { get => _userOff; private set => SetField(ref _userOff, value); }
    private string _userDef = "";
    public string UserDef { get => _userDef; private set => SetField(ref _userDef, value); }
    private string _userSpeed = "";
    public string UserSpeed { get => _userSpeed; private set => SetField(ref _userSpeed, value); }
    private string _userBrains = "";
    public string UserBrains { get => _userBrains; private set => SetField(ref _userBrains, value); }
    private string _userWeight = "";
    public string UserWeight { get => _userWeight; private set => SetField(ref _userWeight, value); }
    private string _userCareMistakes = "";
    public string UserCareMistakes { get => _userCareMistakes; private set => SetField(ref _userCareMistakes, value); }

    public string Happiness { get; }
    public string Discipline { get; }
    public string Battles { get; }
    public string TechniqueCount { get; }

    private string _userHappiness = "";
    public string UserHappiness { get => _userHappiness; private set => SetField(ref _userHappiness, value); }
    private string _userDiscipline = "";
    public string UserDiscipline { get => _userDiscipline; private set => SetField(ref _userDiscipline, value); }
    private string _userBattles = "";
    public string UserBattles { get => _userBattles; private set => SetField(ref _userBattles, value); }
    private string _userTechniqueCount = "";
    public string UserTechniqueCount { get => _userTechniqueCount; private set => SetField(ref _userTechniqueCount, value); }

    private bool? _isHPMet;
    public bool? IsHPMet { get => _isHPMet; private set => SetField(ref _isHPMet, value); }
    private bool? _isMPMet;
    public bool? IsMPMet { get => _isMPMet; private set => SetField(ref _isMPMet, value); }
    private bool? _isOffMet;
    public bool? IsOffMet { get => _isOffMet; private set => SetField(ref _isOffMet, value); }
    private bool? _isDefMet;
    public bool? IsDefMet { get => _isDefMet; private set => SetField(ref _isDefMet, value); }
    private bool? _isSpeedMet;
    public bool? IsSpeedMet { get => _isSpeedMet; private set => SetField(ref _isSpeedMet, value); }
    private bool? _isBrainsMet;
    public bool? IsBrainsMet { get => _isBrainsMet; private set => SetField(ref _isBrainsMet, value); }
    private bool _isWeightMet;
    public bool IsWeightMet { get => _isWeightMet; private set => SetField(ref _isWeightMet, value); }
    private bool? _isCareMistakesMet;
    public bool? IsCareMistakesMet { get => _isCareMistakesMet; private set => SetField(ref _isCareMistakesMet, value); }
    private bool? _isHappinessMet;
    public bool? IsHappinessMet { get => _isHappinessMet; private set => SetField(ref _isHappinessMet, value); }
    private bool? _isDisciplineMet;
    public bool? IsDisciplineMet { get => _isDisciplineMet; private set => SetField(ref _isDisciplineMet, value); }
    private bool? _isBattlesMet;
    public bool? IsBattlesMet { get => _isBattlesMet; private set => SetField(ref _isBattlesMet, value); }
    private bool? _isTechniqueCountMet;
    public bool? IsTechniqueCountMet { get => _isTechniqueCountMet; private set => SetField(ref _isTechniqueCountMet, value); }
    private bool _isEnabled;
    public bool IsEnabled { get => _isEnabled; private set => SetField(ref _isEnabled, value); }
    private int _evolutionScore;
    public int EvolutionScore { get => _evolutionScore; private set => SetField(ref _evolutionScore, value); }
    private int _scoreTotal;
    public int ScoreTotal { get => _scoreTotal; private set => SetField(ref _scoreTotal, value); }
    private int _statCount;
    public int StatCount { get => _statCount; private set => SetField(ref _statCount, value); }
    private bool _isWinningEvolution;
    public bool IsWinningEvolution { get => _isWinningEvolution; set => SetField(ref _isWinningEvolution, value); }
}
