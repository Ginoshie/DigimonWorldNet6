using System;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromFresh;

public sealed class FromFreshEvolutionCalculator : IEvolutionCalculator
{
    private readonly FromFreshEvolutionMapper _fromFreshEvolutionMapper = new();
    
    public EvolutionResult DetermineEvolutionResult(EvolutionCalculationInput evolutionCalculationInput)
    {
        if (evolutionCalculationInput.EvolutionStage != EvolutionStage.Fresh)
        {
            throw new ArgumentException($"{evolutionCalculationInput.DigimonName} is not a {nameof(EvolutionStage.Fresh)} stage digimon.");
        }

        return _fromFreshEvolutionMapper[evolutionCalculationInput.DigimonName];
    }
}