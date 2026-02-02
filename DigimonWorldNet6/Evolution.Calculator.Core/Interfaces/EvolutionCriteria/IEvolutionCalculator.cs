using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;

public interface IEvolutionCalculator
{
    public EvolutionResult DetermineEvolutionResult(UserDigimon userDigimon);
}