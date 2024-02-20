using System.Collections.Generic;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.InTraining;

public sealed class InTrainingEvolutionMapper
{
    private readonly Dictionary<DigimonType, EvolutionResult> _freshAndInTrainingEvolutionMappings = new();

    public InTrainingEvolutionMapper()
    {
        _freshAndInTrainingEvolutionMappings[DigimonType.Poyomon] = EvolutionResult.Tokomon;
    }

    public EvolutionResult this[DigimonType digimonType]
    {
        get
        {
            if (_freshAndInTrainingEvolutionMappings.TryGetValue(digimonType, out var evolutionResult))
            {
                return evolutionResult;
            }
            else
            {
                throw new KeyNotFoundException($"Evolution mapping for {digimonType} was not found in {nameof(InTrainingEvolutionMapper)}");
            }
        }
    }
}