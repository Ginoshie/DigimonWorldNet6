using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculators;

public sealed class BonusCriteriaCalculator : ICriteriaCalculator<BonusCriteria>
{
    public bool CriteriaIsMet(Digimon digimon, BonusCriteria bonusCriteria)
    {
        return (digimon.Battles <= bonusCriteria.Battles && bonusCriteria.IsBattlesCriteriaAMaximum) ||
               (digimon.Battles >= bonusCriteria.Battles && !bonusCriteria.IsBattlesCriteriaAMaximum);
    }
}