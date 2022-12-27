using DigimonWorld.Evolution.Calculator.Core.DataObjects;

namespace DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;

public interface ICriteriaCalculator
{
    public bool CriteriaIsMet(Digimon digimon, IEvolutionCriteria evolutionCriteria);
}