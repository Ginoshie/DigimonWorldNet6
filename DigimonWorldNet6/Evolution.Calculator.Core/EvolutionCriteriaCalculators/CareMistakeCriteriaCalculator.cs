using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculators;

public class CareMistakeCriteriaCalculator : ICriteriaCalculator
{
    public bool CriteriaIsMet(Digimon digimon, IEvolutionCriteria evolutionCriteria)
    {
        var careMistakeCriteria = evolutionCriteria.CareMistakes;
        
        return (digimon.CareMistakes <= careMistakeCriteria.CareMistakes && careMistakeCriteria.IsCareMistakesCriteriaAMaximum) ||
               (digimon.CareMistakes >= careMistakeCriteria.CareMistakes && !careMistakeCriteria.IsCareMistakesCriteriaAMaximum);
    }
}