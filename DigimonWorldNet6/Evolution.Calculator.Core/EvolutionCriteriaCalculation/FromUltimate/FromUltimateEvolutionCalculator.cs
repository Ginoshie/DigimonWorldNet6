using System;
using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromUltimate;

public sealed class FromUltimateEvolutionCalculator : IEvolutionCalculator
{
    public EvolutionResult DetermineEvolutionResult(Digimon digimon)
    {
        if (digimon.EvolutionStage != EvolutionStage.Ultimate) 
            throw new ArgumentException($"{digimon.DigimonType} is not a {nameof(EvolutionStage.Ultimate)} stage digimon.");
        
        return EvolutionResult.None;
    }
}