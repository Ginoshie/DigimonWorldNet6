using System;
using System.Linq;
using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using Shared.Enums;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvoTreeHelper.UserControls;

public class CriteriaColumnViewModel : BaseViewModel
{
    public CriteriaColumnViewModel(DigimonName userDigimonDigimonName, string name, string iconPath, IEvolutionCriteria criteria, int userHp, int userMp, int userOff, int userDef, int userSpeed, int userBrains, int userWeight, int userCareMistakes,
        int userHappiness, int userDiscipline, int userBattles, int userTechniqueCount)
    {
        UserDigimonDigimonName = userDigimonDigimonName;
        Criteria = criteria;
        Name = name;
        IconPath = iconPath;
        Hp = criteria.Stats.Hp > 0 ? criteria.Stats.Hp.ToString() : "";
        Mp = criteria.Stats.Mp > 0 ? criteria.Stats.Mp.ToString() : "";
        Off = criteria.Stats.Off > 0 ? criteria.Stats.Off.ToString() : "";
        Def = criteria.Stats.Def > 0 ? criteria.Stats.Def.ToString() : "";
        Speed = criteria.Stats.Speed > 0 ? criteria.Stats.Speed.ToString() : "";
        Brains = criteria.Stats.Brains > 0 ? criteria.Stats.Brains.ToString() : "";
        Weight = criteria.Weight.WeightTarget > 0 ? $"{criteria.Weight.LowerWeightLimit} - {criteria.Weight.UpperWeightLimit}" : "";

        string careMistakeOperator = criteria.CareMistakes.IsCareMistakesCriteriaAMaximum ? "≤ " : "≥ ";
        CareMistakes = criteria.CareMistakes.CareMistakes >= 0
            ? $"{careMistakeOperator}{criteria.CareMistakes.CareMistakes}"
            : "";

        BonusCriteria b = criteria.BonusCriteria;
        Happiness = b.Happiness >= 0 ? b.Happiness.ToString() : "";
        Discipline = b.Discipline >= 0 ? b.Discipline.ToString() : "";
        string batOp = b.IsBattlesCriteriaAMaximum ? "≤ " : "≥ ";
        Battles = b.Battles >= 0 ? $"{batOp}{b.Battles}" : "";
        TechniqueCount = b.TechniqueCount > 0 ? b.TechniqueCount.ToString() : "";
        Precursor = criteria.BonusCriteria.PrecursorDigimon.ToString();

        UpdateUserStats(userHp, userMp, userOff, userDef, userSpeed, userBrains, userWeight, userCareMistakes, userHappiness, userDiscipline, userBattles, userTechniqueCount);
    }

    public DigimonName UserDigimonDigimonName { get; }
    public IEvolutionCriteria Criteria { get; }
    public string Name { get; }
    public string IconPath { get; }
    public string Hp { get; }
    public string Mp { get; }
    public string Off { get; }
    public string Def { get; }
    public string Speed { get; }
    public string Brains { get; }
    public string Weight { get; }
    public string CareMistakes { get; }

    public string Happiness { get; }
    public string Discipline { get; }
    public string Battles { get; }
    public string TechniqueCount { get; }
    public string? Precursor { get; }

    public bool? IsHpMet
    {
        get;
        private set => SetField(ref field, value);
    }

    public bool? IsMpMet
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


    public bool? IsPrecursorMet
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

    public void UpdateUserStats(int userHp, int userMp, int userOff, int userDef, int userSpeed, int userBrains, int userWeight, int userCareMistakes, int userHappiness, int userDiscipline, int userBattles, int userTechniqueCount)
    {
        switch (Criteria.EvolutionStage)
        {
            case EvolutionStage.InTraining:
                IsEnabled = true;
                break;
            case EvolutionStage.Rookie:
                SetRookieEvolutionStats(userHp, userMp, userOff, userDef, userSpeed, userBrains);
                break;
            case EvolutionStage.Champion:
            case EvolutionStage.Ultimate:
                SetChampionAndUltimateEvolutionStats(userHp, userMp, userOff, userDef, userSpeed, userBrains, userWeight, userCareMistakes, userHappiness, userDiscipline, userBattles, userTechniqueCount);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void SetRookieEvolutionStats(int userHp, int userMp, int userOff, int userDef, int userSpeed, int userBrains)
    {
        const string hp = "hp", mp = "mp", off = "off", def = "def", speed = "speed", brains = "brains";

        (string statName, int statValue)[] prioOrderedEvoCriteriaStats =
        [
            (hp, userHp),
            (mp, userMp),
            (off, userOff),
            (def, userDef),
            (speed, userSpeed),
            (brains, userBrains)
        ];

        (string statName, int statValue) highestEvoStat = prioOrderedEvoCriteriaStats.MaxBy(s => s.statValue);

        IsHpMet = Criteria.Stats.Hp > 0 ? highestEvoStat.statName == hp : null;
        IsMpMet = Criteria.Stats.Mp > 0 ? highestEvoStat.statName == mp : null;
        IsOffMet = Criteria.Stats.Off > 0 ? highestEvoStat.statName == off : null;
        IsDefMet = Criteria.Stats.Def > 0 ? highestEvoStat.statName == def : null;
        IsSpeedMet = Criteria.Stats.Speed > 0 ? highestEvoStat.statName == speed : null;
        IsBrainsMet = Criteria.Stats.Brains > 0 ? highestEvoStat.statName == brains : null;

        if (IsHpMet == true || IsMpMet == true)
        {
            EvolutionScore = highestEvoStat.statValue / 10;

            IsEnabled = true;
        }

        if (IsOffMet == true ||
            IsDefMet == true ||
            IsSpeedMet == true ||
            IsBrainsMet == true)
        {
            EvolutionScore = highestEvoStat.statValue;

            IsEnabled = true;
        }
    }

    private void SetChampionAndUltimateEvolutionStats(int userHp, int userMp, int userOff, int userDef, int userSpeed, int userBrains, int userWeight, int userCareMistakes, int userHappiness, int userDiscipline, int userBattles,
        int userTechniqueCount)
    {
        BonusCriteria bonusCriteria = Criteria.BonusCriteria;

        IsHpMet = Criteria.Stats.Hp > 0 ? userHp >= Criteria.Stats.Hp : null;
        IsMpMet = Criteria.Stats.Mp > 0 ? userMp >= Criteria.Stats.Mp : null;
        IsOffMet = Criteria.Stats.Off > 0 ? userOff >= Criteria.Stats.Off : null;
        IsDefMet = Criteria.Stats.Def > 0 ? userDef >= Criteria.Stats.Def : null;
        IsSpeedMet = Criteria.Stats.Speed > 0 ? userSpeed >= Criteria.Stats.Speed : null;
        IsBrainsMet = Criteria.Stats.Brains > 0 ? userBrains >= Criteria.Stats.Brains : null;
        IsWeightMet = userWeight >= Criteria.Weight.LowerWeightLimit && userWeight <= Criteria.Weight.UpperWeightLimit;

        if (Criteria.CareMistakes.CareMistakes < 0)
        {
            IsCareMistakesMet = null;
        }
        else if (Criteria.CareMistakes.IsCareMistakesCriteriaAMaximum)
        {
            IsCareMistakesMet = userCareMistakes <= Criteria.CareMistakes.CareMistakes;
        }
        else
        {
            IsCareMistakesMet = userCareMistakes >= Criteria.CareMistakes.CareMistakes;
        }

        IsHappinessMet = bonusCriteria.Happiness >= 0 ? userHappiness >= bonusCriteria.Happiness : null;
        IsDisciplineMet = bonusCriteria.Discipline >= 0 ? userDiscipline >= bonusCriteria.Discipline : null;

        if (bonusCriteria.Battles < 0)
        {
            IsBattlesMet = null;
        }
        else if (bonusCriteria.IsBattlesCriteriaAMaximum)
        {
            IsBattlesMet = userBattles <= bonusCriteria.Battles;
        }
        else
        {
            IsBattlesMet = userBattles >= bonusCriteria.Battles;
        }

        IsTechniqueCountMet = bonusCriteria.TechniqueCount > 0 ? userTechniqueCount >= bonusCriteria.TechniqueCount : null;

        IsPrecursorMet = bonusCriteria.PrecursorDigimon == UserDigimonDigimonName;

        bool statsMet = userHp >= Criteria.Stats.Hp
                        && userMp >= Criteria.Stats.Mp
                        && userOff >= Criteria.Stats.Off
                        && userDef >= Criteria.Stats.Def
                        && userSpeed >= Criteria.Stats.Speed
                        && userBrains >= Criteria.Stats.Brains;

        bool careMistakesMet = (Criteria.CareMistakes.IsCareMistakesCriteriaAMaximum
            ? userCareMistakes <= Criteria.CareMistakes.CareMistakes
            : userCareMistakes >= Criteria.CareMistakes.CareMistakes);

        bool weightMet = IsWeightMet;

        bool bonusMet = (bonusCriteria.Happiness >= 0 && userHappiness >= bonusCriteria.Happiness)
                        || (bonusCriteria.Discipline >= 0 && userDiscipline >= bonusCriteria.Discipline)
                        || (bonusCriteria.Battles >= 0 && (bonusCriteria.IsBattlesCriteriaAMaximum
                            ? userBattles <= bonusCriteria.Battles
                            : userBattles >= bonusCriteria.Battles))
                        || userTechniqueCount >= bonusCriteria.TechniqueCount
                        || !string.IsNullOrWhiteSpace(bonusCriteria.PrecursorDigimon.ToString()) && bonusCriteria.PrecursorDigimon.ToString() == Precursor;

        int criteriaMetCount = (statsMet ? 1 : 0) + (careMistakesMet ? 1 : 0)
                                                  + (weightMet ? 1 : 0) + (bonusMet ? 1 : 0);
        IsEnabled = criteriaMetCount >= 3;

        if (Criteria.EvolutionStage != EvolutionStage.Champion && Criteria.EvolutionStage != EvolutionStage.Ultimate)
        {
            return;
        }

        EvolutionScoreCalculationResult scoreResult = FromRookieOrChampionEvolutionScoreCalculator.CalculateEvolutionScore(userHp, userMp, userOff, userDef, userSpeed, userBrains, Criteria.Stats, 0, 0, false);

        ScoreTotal = scoreResult.StatTotal;
        StatCount = scoreResult.StatCount;
        EvolutionScore = scoreResult.EvolutionScore;
    }
}