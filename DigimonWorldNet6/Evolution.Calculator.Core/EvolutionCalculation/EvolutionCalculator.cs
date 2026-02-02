using System;
using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromFresh;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromInTraining;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromUltimate;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCalculation;

public sealed class EvolutionCalculator
{
    public EvolutionResult CalculateEvolutionResult(UserDigimon userDigimon)
    {
        IEvolutionCalculator evolutionCalculator = userDigimon.EvolutionStage switch
        {
            EvolutionStage.Fresh => new FromFreshEvolutionCalculator(),
            EvolutionStage.InTraining => new FromInTrainingEvolutionCalculator(),
            EvolutionStage.Rookie or EvolutionStage.Champion => new FromRookieOrChampionEvolutionCalculator(),
            EvolutionStage.Ultimate => new FromUltimateEvolutionCalculator(),
            _ => throw new ArgumentOutOfRangeException(nameof(userDigimon.EvolutionStage), $"{userDigimon.EvolutionStage} not supported by {nameof(EvolutionCalculator)}")
        };

        EvolutionResult evolutionResult = evolutionCalculator.DetermineEvolutionResult(userDigimon);

        return evolutionResult;
    }
}