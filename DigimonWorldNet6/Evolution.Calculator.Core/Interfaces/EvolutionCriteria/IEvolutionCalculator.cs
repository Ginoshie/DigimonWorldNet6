using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;

public interface IEvolutionCalculator
{
    public DigimonType DetermineEvolutionResult(Digimon digimon);
}