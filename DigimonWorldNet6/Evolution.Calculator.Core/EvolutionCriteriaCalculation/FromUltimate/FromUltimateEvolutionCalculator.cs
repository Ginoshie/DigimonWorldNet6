using System;
using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromUltimate;

public sealed class FromUltimateEvolutionCalculator : IEvolutionCalculator
{
    public EvolutionResult DetermineEvolutionResult(UserDigimon userDigimon)
    {
        if (userDigimon.EvolutionStage != EvolutionStage.Ultimate)
        {
            throw new ArgumentException($"{userDigimon.DigimonName} is not an {nameof(EvolutionStage.Ultimate)} stage digimon.");
        }

        return EvolutionResult.None;
    }
}