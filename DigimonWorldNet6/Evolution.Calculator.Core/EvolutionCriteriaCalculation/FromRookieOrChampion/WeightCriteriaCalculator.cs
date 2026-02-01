using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;

public sealed class WeightCriteriaCalculator
{
    public bool CriteriaIsMet(UserDigimon userDigimon, MainCriteriaWeight weightCriteria)
    {
        return userDigimon.Weight >= weightCriteria.LowerWeightLimit &&
               userDigimon.Weight <= weightCriteria.UpperWeightLimit;
    }
}