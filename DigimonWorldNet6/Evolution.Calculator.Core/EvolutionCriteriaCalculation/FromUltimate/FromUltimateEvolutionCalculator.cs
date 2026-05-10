using System;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromUltimate;

public sealed class FromUltimateEvolutionCalculator : IEvolutionCalculator
{
    public EvolutionResult DetermineEvolutionResult(EvolutionCalculationInput evolutionCalculationInput)
    {
        if (evolutionCalculationInput.EvolutionStage != EvolutionStage.Ultimate)
        {
            throw new ArgumentException($"{evolutionCalculationInput.DigimonName} is not an {nameof(EvolutionStage.Ultimate)} stage digimon.");
        }

        return EvolutionResult.None;
    }
}