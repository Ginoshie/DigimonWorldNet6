using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;

public sealed class CareMistakeCriteriaCalculator
{
    public bool CriteriaIsMet(UserDigimon userDigimon, MainCriteriaCareMistakes careMistakeCriteria)
    {
        return (userDigimon.CareMistakes <= careMistakeCriteria.CareMistakes && careMistakeCriteria.IsCareMistakesCriteriaAMaximum) ||
               (userDigimon.CareMistakes >= careMistakeCriteria.CareMistakes && !careMistakeCriteria.IsCareMistakesCriteriaAMaximum);
    }
}