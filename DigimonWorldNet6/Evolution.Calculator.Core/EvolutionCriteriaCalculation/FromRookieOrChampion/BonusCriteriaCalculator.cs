using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;

public sealed class BonusCriteriaCalculator
{
    public bool CriteriaIsMet(EvolutionCalculationInput evolutionCalculationInput, BonusCriteria bonusCriteria)
    {
        if (HappinessCriteriaEnabled(bonusCriteria) && HappinessCriteriaIsMet(evolutionCalculationInput, bonusCriteria))
        {
            return true;
        }

        if (DisciplineCriteriaEnabled(bonusCriteria) && DisciplineCriteriaIsMet(evolutionCalculationInput, bonusCriteria))
        {
            return true;
        }

        if (BattleCriteriaEnabled(bonusCriteria) && BattleCriteriaIsMet(evolutionCalculationInput, bonusCriteria))
        {
            return true;
        }

        if (TechniqueCriteriaIsMet(evolutionCalculationInput, bonusCriteria))
        {
            return true;
        }

        if (PrecursorDigimonCriteriaIsEnabled(bonusCriteria) && PrecursorDigimonCriteriaIsMet(evolutionCalculationInput, bonusCriteria))
        {
            return true;
        }

        return false;
    }

    private bool HappinessCriteriaEnabled(BonusCriteria bonusCriteria)
    {
        return bonusCriteria.Happiness >= 0;
    }

    private bool HappinessCriteriaIsMet(EvolutionCalculationInput evolutionCalculationInput, BonusCriteria bonusCriteria)
    {
        return evolutionCalculationInput.Happiness >= bonusCriteria.Happiness;
    }

    private bool DisciplineCriteriaEnabled(BonusCriteria bonusCriteria)
    {
        return bonusCriteria.Discipline >= 0;
    }

    private bool DisciplineCriteriaIsMet(EvolutionCalculationInput evolutionCalculationInput, BonusCriteria bonusCriteria)
    {
        return evolutionCalculationInput.Discipline >= bonusCriteria.Discipline;
    }

    private bool BattleCriteriaEnabled(BonusCriteria bonusCriteria)
    {
        return bonusCriteria.Battles >= 0;
    }

    private bool BattleCriteriaIsMet(EvolutionCalculationInput evolutionCalculationInput, BonusCriteria bonusCriteria)
    {
        return (evolutionCalculationInput.Battles <= bonusCriteria.Battles && bonusCriteria.IsBattlesCriteriaAMaximum) ||
               (evolutionCalculationInput.Battles >= bonusCriteria.Battles && !bonusCriteria.IsBattlesCriteriaAMaximum);
    }

    private bool TechniqueCriteriaIsMet(EvolutionCalculationInput evolutionCalculationInput, BonusCriteria bonusCriteria)
    {
        return evolutionCalculationInput.TechniqueCount >= bonusCriteria.TechniqueCount;
    }

    private bool PrecursorDigimonCriteriaIsEnabled(BonusCriteria bonusCriteria)
    {
        return bonusCriteria.PrecursorDigimon != null;
    }

    private bool PrecursorDigimonCriteriaIsMet(EvolutionCalculationInput evolutionCalculationInput, BonusCriteria bonusCriteria)
    {
        return evolutionCalculationInput.DigimonName == bonusCriteria.PrecursorDigimon;
    }
}