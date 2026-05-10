using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;

public sealed class CareMistakeCriteriaCalculator
{
    public bool CriteriaIsMet(EvolutionCalculationInput evolutionCalculationInput, MainCriteriaCareMistakes careMistakeCriteria)
    {
        return (evolutionCalculationInput.CareMistakes <= careMistakeCriteria.CareMistakes && careMistakeCriteria.IsCareMistakesCriteriaAMaximum) ||
               (evolutionCalculationInput.CareMistakes >= careMistakeCriteria.CareMistakes && !careMistakeCriteria.IsCareMistakesCriteriaAMaximum);
    }
}