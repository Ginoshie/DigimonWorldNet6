using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculators;

public class WeightCriteriaCalculator : ICriteriaCalculator
{
    public bool CriteriaIsMet(Digimon digimon, IEvolutionCriteria evolutionCriteria)
    {
        var weightCriteria = evolutionCriteria.Weight;
        
        return digimon.Weight >= weightCriteria.LowerWeightLimit &&
               digimon.Weight <= weightCriteria.UpperWeightLimit;
    }
}