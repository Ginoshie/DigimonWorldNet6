using System;
using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromFresh;

public sealed class FromFreshEvolutionCalculator : IEvolutionCalculator
{
    private readonly FromFreshEvolutionMapper _fromFreshEvolutionMapper = new();
    
    public EvolutionResult DetermineEvolutionResult(UserDigimon userDigimon)
    {
        if (userDigimon.EvolutionStage != EvolutionStage.Fresh)
        {
            throw new ArgumentException($"{userDigimon.DigimonName} is not a {nameof(EvolutionStage.Fresh)} stage digimon.");
        }

        return _fromFreshEvolutionMapper[userDigimon.DigimonName];
    }
}