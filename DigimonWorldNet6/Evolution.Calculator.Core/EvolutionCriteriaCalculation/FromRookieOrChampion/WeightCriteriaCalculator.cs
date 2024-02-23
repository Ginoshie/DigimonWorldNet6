using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;

public sealed class WeightCriteriaCalculator
{
    public bool CriteriaIsMet(Digimon digimon, MainCriteriaWeight weightCriteria)
    {
        return digimon.Weight >= weightCriteria.LowerWeightLimit &&
               digimon.Weight <= weightCriteria.UpperWeightLimit;
    }
}