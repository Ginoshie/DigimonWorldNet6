using System.Collections.Generic;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.InTraining;

public sealed class InTrainingEvolutionMapper
{
    private readonly Dictionary<DigimonType, DigimonType> _freshAndInTrainingEvolutionMappings = new();

    public InTrainingEvolutionMapper()
    {
        _freshAndInTrainingEvolutionMappings[DigimonType.Poyomon] = DigimonType.Tokomon;
    }

    public DigimonType this[DigimonType digimonType]
    {
        get
        {
            if (_freshAndInTrainingEvolutionMappings.TryGetValue(digimonType, out digimonType))
            {
                return digimonType;
            }
            else
            {
                throw new KeyNotFoundException($"Evolution mapping for {digimonType} was not found in {nameof(InTrainingEvolutionMapper)}");
            }
        }
    }
}