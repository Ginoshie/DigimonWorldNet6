using System;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromFresh;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromInTraining;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromUltimate;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCalculation;

public sealed class EvolutionCalculator
{
    private static readonly Lazy<EvolutionCalculator> _instance = new(() => new EvolutionCalculator());

    public static EvolutionCalculator Instance => _instance.Value;
    
    private EvolutionCalculator() { }
    
    public EvolutionResult CalculateEvolutionResult(EvolutionCalculationInput evolutionCalculationInput)
    {
        IEvolutionCalculator evolutionCalculator = evolutionCalculationInput.EvolutionStage switch
        {
            EvolutionStage.Fresh => new FromFreshEvolutionCalculator(),
            EvolutionStage.InTraining => new FromInTrainingEvolutionCalculator(),
            EvolutionStage.Rookie or EvolutionStage.Champion => new FromRookieOrChampionEvolutionCalculator(),
            EvolutionStage.Ultimate => new FromUltimateEvolutionCalculator(),
            _ => throw new ArgumentOutOfRangeException(nameof(evolutionCalculationInput.EvolutionStage), $"{evolutionCalculationInput.EvolutionStage} not supported by {nameof(EvolutionCalculator)}")
        };

        EvolutionResult evolutionResult = evolutionCalculator.DetermineEvolutionResult(evolutionCalculationInput);

        return evolutionResult;
    }
}