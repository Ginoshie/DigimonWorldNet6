using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromFresh;

public sealed class FromFreshEvolutionCalculator : IEvolutionCalculator
{
    private readonly FromFreshEvolutionMapper _fromFreshEvolutionMapper = new();
    
    public EvolutionResult DetermineEvolutionResult(Digimon digimon) => _fromFreshEvolutionMapper[digimon.DigimonType];
}