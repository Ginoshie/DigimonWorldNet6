using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvoTreeHelper.Models;

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

        BonusCriteria b = criteria.BonusCriteria;
        Happiness = b.Happiness >= 0 ? b.Happiness.ToString() : "";
        Discipline = b.Discipline >= 0 ? b.Discipline.ToString() : "";
        string batOp = b.IsBattlesCriteriaAMaximum ? "≤ " : "≥ ";
        Battles = b.Battles >= 0 ? $"{batOp}{b.Battles}" : "";
        TechniqueCount = b.TechniqueCount > 0 ? b.TechniqueCount.ToString() : "";

        UpdateUserStats(userHp, userMp, userOff, userDef, userSpeed, userBrains, userWeight, userCareMistakes, userHappiness, userDiscipline, userBattles, userTechniqueCount);
    }

    public void UpdateUserStats(int userHp, int userMp, int userOff, int userDef, int userSpeed, int userBrains, int userWeight, int userCareMistakes, int userHappiness, int userDiscipline, int userBattles, int userTechniqueCount)
    {
        IEvolutionCriteria criteria = _criteria;
        BonusCriteria b = criteria.BonusCriteria;

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

        EvolutionScoreCalculationResult scoreResult = FromRookieOrChampionEvolutionScoreCalculator.CalculateEvolutionScore(
            userHp, userMp, userOff, userDef, userSpeed, userBrains,
            criteria.Stats, 0, 0, false);
        ScoreTotal = scoreResult.CarriedOverStatTotal;
        StatCount = scoreResult.CarriedOverCount;
        EvolutionScore = scoreResult.EvolutionScore;
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

    public string UserHP
    {
        get;
        private set => SetField(ref field, value);
    } = "";

    public string UserMP
    {
        get;
        private set => SetField(ref field, value);
    } = "";

    public string UserOff
    {
        get;
        private set => SetField(ref field, value);
    } = "";

    public string UserDef
    {
        get;
        private set => SetField(ref field, value);
    } = "";

    public string UserSpeed
    {
        get;
        private set => SetField(ref field, value);
    } = "";

    public string UserBrains
    {
        get;
        private set => SetField(ref field, value);
    } = "";

    public string UserWeight
    {
        get;
        private set => SetField(ref field, value);
    } = "";

    public string UserCareMistakes
    {
        get;
        private set => SetField(ref field, value);
    } = "";

    public string Happiness { get; }
    public string Discipline { get; }
    public string Battles { get; }
    public string TechniqueCount { get; }

    public string UserHappiness
    {
        get;
        private set => SetField(ref field, value);
    } = "";

    public string UserDiscipline
    {
        get;
        private set => SetField(ref field, value);
    } = "";

    public string UserBattles
    {
        get;
        private set => SetField(ref field, value);
    } = "";

    public string UserTechniqueCount
    {
        get;
        private set => SetField(ref field, value);
    } = "";

    public bool? IsHPMet
    {
        get;
        private set => SetField(ref field, value);
    }

    public bool? IsMPMet
    {
        get;
        private set => SetField(ref field, value);
    }

    public bool? IsOffMet
    {
        get;
        private set => SetField(ref field, value);
    }

    public bool? IsDefMet
    {
        get;
        private set => SetField(ref field, value);
    }

    public bool? IsSpeedMet
    {
        get;
        private set => SetField(ref field, value);
    }

    public bool? IsBrainsMet
    {
        get;
        private set => SetField(ref field, value);
    }

    public bool IsWeightMet
    {
        get;
        private set => SetField(ref field, value);
    }

    public bool? IsCareMistakesMet
    {
        get;
        private set => SetField(ref field, value);
    }

    public bool? IsHappinessMet
    {
        get;
        private set => SetField(ref field, value);
    }

    public bool? IsDisciplineMet
    {
        get;
        private set => SetField(ref field, value);
    }

    public bool? IsBattlesMet
    {
        get;
        private set => SetField(ref field, value);
    }

    public bool? IsTechniqueCountMet
    {
        get;
        private set => SetField(ref field, value);
    }

    public bool IsEnabled
    {
        get;
        private set => SetField(ref field, value);
    }

    public int EvolutionScore
    {
        get;
        private set => SetField(ref field, value);
    }

    public int ScoreTotal
    {
        get;
        private set => SetField(ref field, value);
    }

    public int StatCount
    {
        get;
        private set => SetField(ref field, value);
    }

    public bool IsWinningEvolution
    {
        get;
        set => SetField(ref field, value);
    }
}
