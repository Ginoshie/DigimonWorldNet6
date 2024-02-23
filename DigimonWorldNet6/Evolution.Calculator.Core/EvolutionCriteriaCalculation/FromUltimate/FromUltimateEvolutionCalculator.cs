using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromUltimate;

public sealed class FromUltimateEvolutionCalculator : IEvolutionCalculator
{
    public EvolutionResult DetermineEvolutionResult(Digimon digimon) => EvolutionResult.None;
}