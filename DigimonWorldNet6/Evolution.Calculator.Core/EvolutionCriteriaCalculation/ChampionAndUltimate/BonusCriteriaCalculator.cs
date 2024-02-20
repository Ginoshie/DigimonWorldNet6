using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.ChampionAndUltimate;

public sealed class BonusCriteriaCalculator
{
    public bool CriteriaIsMet(Digimon digimon, BonusCriteria bonusCriteria)
    {
        if (HappinessCriteriaEnabled(bonusCriteria) && HappinessCriteriaIsMet(digimon, bonusCriteria)) return true;

        if (DisciplineCriteriaEnabled(bonusCriteria) && DisciplineCriteriaIsMet(digimon, bonusCriteria)) return true;

        if (BattleCriteriaEnabled(bonusCriteria) && BattleCriteriaIsMet(digimon, bonusCriteria)) return true;

        if (TechniqueCriteriaIsMet(digimon, bonusCriteria)) return true;

        if (PrecursorDigimonCriteriaIsEnabled(bonusCriteria) && PrecursorDigimonCriteriaIsMet(digimon, bonusCriteria)) return true;

        return false;
    }

    private bool HappinessCriteriaEnabled(BonusCriteria bonusCriteria)
    {
        return bonusCriteria.Happiness >= 0;
    }

    private bool HappinessCriteriaIsMet(Digimon digimon, BonusCriteria bonusCriteria)
    {
        return digimon.Happiness >= bonusCriteria.Happiness;
    }

    private bool DisciplineCriteriaEnabled(BonusCriteria bonusCriteria)
    {
        return bonusCriteria.Discipline >= 0;
    }

    private bool DisciplineCriteriaIsMet(Digimon digimon, BonusCriteria bonusCriteria)
    {
        return digimon.Discipline >= bonusCriteria.Discipline;
    }

    private bool BattleCriteriaEnabled(BonusCriteria bonusCriteria)
    {
        return bonusCriteria.Battles >= 0;
    }

    private bool BattleCriteriaIsMet(Digimon digimon, BonusCriteria bonusCriteria)
    {
        return (digimon.Battles <= bonusCriteria.Battles && bonusCriteria.IsBattlesCriteriaAMaximum) ||
               (digimon.Battles >= bonusCriteria.Battles && !bonusCriteria.IsBattlesCriteriaAMaximum);
    }

    private bool TechniqueCriteriaIsMet(Digimon digimon, BonusCriteria bonusCriteria)
    {
        return digimon.TechniqueCount >= bonusCriteria.TechniqueCount;
    }

    private bool PrecursorDigimonCriteriaIsEnabled(BonusCriteria bonusCriteria)
    {
        return bonusCriteria.PrecursorDigimon != null;
    }

    private bool PrecursorDigimonCriteriaIsMet(Digimon digimon, BonusCriteria bonusCriteria)
    {
        return digimon.DigimonType == bonusCriteria.PrecursorDigimon;
    }
}