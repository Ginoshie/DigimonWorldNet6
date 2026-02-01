using DigimonWorld.Evolution.Calculator.Core.DataObjects;

namespace DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;

public interface ICriteriaCalculator<T>
{
    public bool CriteriaIsMet(UserDigimon userDigimon, T evolutionCriteria);
}