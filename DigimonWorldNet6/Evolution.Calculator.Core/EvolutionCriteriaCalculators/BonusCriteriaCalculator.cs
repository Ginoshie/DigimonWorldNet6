using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculators;

public class BonusCriteriaCalculator : ICriteriaCalculator
{
    public bool CriteriaIsMet(Digimon digimon, IEvolutionCriteria evolutionCriteria)
    {
        var bonusCriteria = evolutionCriteria.BonusCriteria;

        return BattleCriteriaIsMet(digimon, bonusCriteria);
    }

    private bool BattleCriteriaIsMet(Digimon digimon, IBonusCriteria bonusCriteria)
    {
        return (digimon.Battles <= bonusCriteria.Battles && bonusCriteria.IsBattlesCriteriaAMaximum) ||
               (digimon.Battles >= bonusCriteria.Battles && !bonusCriteria.IsBattlesCriteriaAMaximum);
    }
}