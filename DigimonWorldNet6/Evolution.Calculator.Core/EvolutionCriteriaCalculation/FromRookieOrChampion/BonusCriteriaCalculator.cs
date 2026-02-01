using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;

public sealed class BonusCriteriaCalculator
{
    public bool CriteriaIsMet(UserDigimon userDigimon, BonusCriteria bonusCriteria)
    {
        if (HappinessCriteriaEnabled(bonusCriteria) && HappinessCriteriaIsMet(userDigimon, bonusCriteria))
        {
            return true;
        }

        if (DisciplineCriteriaEnabled(bonusCriteria) && DisciplineCriteriaIsMet(userDigimon, bonusCriteria))
        {
            return true;
        }

        if (BattleCriteriaEnabled(bonusCriteria) && BattleCriteriaIsMet(userDigimon, bonusCriteria))
        {
            return true;
        }

        if (TechniqueCriteriaIsMet(userDigimon, bonusCriteria))
        {
            return true;
        }

        if (PrecursorDigimonCriteriaIsEnabled(bonusCriteria) && PrecursorDigimonCriteriaIsMet(userDigimon, bonusCriteria))
        {
            return true;
        }

        return false;
    }

    private bool HappinessCriteriaEnabled(BonusCriteria bonusCriteria)
    {
        return bonusCriteria.Happiness >= 0;
    }

    private bool HappinessCriteriaIsMet(UserDigimon userDigimon, BonusCriteria bonusCriteria)
    {
        return userDigimon.Happiness >= bonusCriteria.Happiness;
    }

    private bool DisciplineCriteriaEnabled(BonusCriteria bonusCriteria)
    {
        return bonusCriteria.Discipline >= 0;
    }

    private bool DisciplineCriteriaIsMet(UserDigimon userDigimon, BonusCriteria bonusCriteria)
    {
        return userDigimon.Discipline >= bonusCriteria.Discipline;
    }

    private bool BattleCriteriaEnabled(BonusCriteria bonusCriteria)
    {
        return bonusCriteria.Battles >= 0;
    }

    private bool BattleCriteriaIsMet(UserDigimon userDigimon, BonusCriteria bonusCriteria)
    {
        return (userDigimon.Battles <= bonusCriteria.Battles && bonusCriteria.IsBattlesCriteriaAMaximum) ||
               (userDigimon.Battles >= bonusCriteria.Battles && !bonusCriteria.IsBattlesCriteriaAMaximum);
    }

    private bool TechniqueCriteriaIsMet(UserDigimon userDigimon, BonusCriteria bonusCriteria)
    {
        return userDigimon.TechniqueCount >= bonusCriteria.TechniqueCount;
    }

    private bool PrecursorDigimonCriteriaIsEnabled(BonusCriteria bonusCriteria)
    {
        return bonusCriteria.PrecursorDigimon != null;
    }

    private bool PrecursorDigimonCriteriaIsMet(UserDigimon userDigimon, BonusCriteria bonusCriteria)
    {
        return userDigimon.DigimonName == bonusCriteria.PrecursorDigimon;
    }
}