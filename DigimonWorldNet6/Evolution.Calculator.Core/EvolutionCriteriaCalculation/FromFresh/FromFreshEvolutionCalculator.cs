using System;
using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromFresh;

public sealed class FromFreshEvolutionCalculator : IEvolutionCalculator
{
    private readonly FromFreshEvolutionMapper _fromFreshEvolutionMapper = new();
    
    public EvolutionResult DetermineEvolutionResult(Digimon digimon)
    {
        if (digimon.EvolutionStage != EvolutionStage.Fresh) 
            throw new ArgumentException($"{digimon.DigimonName} is not a {nameof(EvolutionStage.Fresh)} stage digimon.");
        
        return _fromFreshEvolutionMapper[digimon.DigimonName];
    }
}