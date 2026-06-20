using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;

public sealed class StatsCriteriaCalculator
{
    public bool CriteriaIsMet(EvolutionCalculationInput evolutionCalculationInput, MainCriteriaStats statsCriteria)
    {
        return evolutionCalculationInput.Hp >= statsCriteria.HP &&
               evolutionCalculationInput.Mp >= statsCriteria.MP &&
               evolutionCalculationInput.Off >= statsCriteria.Off &&
               evolutionCalculationInput.Speed >= statsCriteria.Speed &&
               evolutionCalculationInput.Def >= statsCriteria.Def &&
               evolutionCalculationInput.Brains >= statsCriteria.Brains;
    }
}