using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculators;

public class CareMistakeCriteriaCalculator : ICriteriaCalculator<MainCriteriaCareMistakes>
{
    public bool CriteriaIsMet(Digimon digimon, MainCriteriaCareMistakes careMistakeCriteria)
    {
        return (digimon.CareMistakes <= careMistakeCriteria.CareMistakes && careMistakeCriteria.IsCareMistakesCriteriaAMaximum) ||
               (digimon.CareMistakes >= careMistakeCriteria.CareMistakes && !careMistakeCriteria.IsCareMistakesCriteriaAMaximum);
    }
}