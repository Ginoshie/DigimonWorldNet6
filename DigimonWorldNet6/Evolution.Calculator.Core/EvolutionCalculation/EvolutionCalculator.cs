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
    
    private readonly IEvolutionCalculator _fromFreshEvolutionCalculator = new FromFreshEvolutionCalculator();
    private readonly IEvolutionCalculator _fromInTrainingEvolutionCalculator = new FromInTrainingEvolutionCalculator();
    private readonly IEvolutionCalculator _fromRookieOrChampionEvolutionCalculator = new FromRookieOrChampionEvolutionCalculator();
    private readonly IEvolutionCalculator _fromUltimateEvolutionCalculator = new FromUltimateEvolutionCalculator();

    public static EvolutionCalculator Instance => _instance.Value;
    
    private EvolutionCalculator() { }
    
    public EvolutionResult CalculateEvolutionResult(EvolutionCalculationInput evolutionCalculationInput)
    {
        IEvolutionCalculator evolutionCalculator = evolutionCalculationInput.EvolutionStage switch
        {
            EvolutionStage.Fresh => _fromFreshEvolutionCalculator,
            EvolutionStage.InTraining => _fromInTrainingEvolutionCalculator,
            EvolutionStage.Rookie or EvolutionStage.Champion => _fromRookieOrChampionEvolutionCalculator,
            EvolutionStage.Ultimate => _fromUltimateEvolutionCalculator,
            _ => throw new ArgumentOutOfRangeException(nameof(evolutionCalculationInput.EvolutionStage), $"{evolutionCalculationInput.EvolutionStage} not supported by {nameof(EvolutionCalculator)}")
        };

        EvolutionResult evolutionResult = evolutionCalculator.DetermineEvolutionResult(evolutionCalculationInput);

        return evolutionResult;
    }
}