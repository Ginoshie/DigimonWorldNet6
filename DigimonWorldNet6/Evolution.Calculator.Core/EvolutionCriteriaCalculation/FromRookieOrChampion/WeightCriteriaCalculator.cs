using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;

public sealed class WeightCriteriaCalculator
{
    public bool CriteriaIsMet(EvolutionCalculationInput evolutionCalculationInput, MainCriteriaWeight weightCriteria)
    {
        return evolutionCalculationInput.Weight >= weightCriteria.LowerWeightLimit &&
               evolutionCalculationInput.Weight <= weightCriteria.UpperWeightLimit;
    }
}