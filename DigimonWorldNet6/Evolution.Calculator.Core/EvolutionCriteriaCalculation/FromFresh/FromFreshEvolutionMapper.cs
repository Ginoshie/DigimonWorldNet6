using System.Collections.Generic;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromFresh;

public sealed class FromFreshEvolutionMapper
{
    private readonly Dictionary<DigimonType, EvolutionResult> _fromFreshEvolutionMappings = new();

    public FromFreshEvolutionMapper()
    {
        _fromFreshEvolutionMappings[DigimonType.Botamon] = EvolutionResult.Koromon;
        _fromFreshEvolutionMappings[DigimonType.Poyomon] = EvolutionResult.Tokomon;
        _fromFreshEvolutionMappings[DigimonType.Punimon] = EvolutionResult.Tsunomon;
        _fromFreshEvolutionMappings[DigimonType.Yuramon] = EvolutionResult.Tanemon;
    }

    public EvolutionResult this[DigimonType digimonType]
    {
        get
        {
            if (_fromFreshEvolutionMappings.TryGetValue(digimonType, out var evolutionResult))
            {
                return evolutionResult;
            }
            else
            {
                throw new KeyNotFoundException($"Evolution mapping for {digimonType} was not found in {nameof(FromFreshEvolutionMapper)}");
            }
        }
    }
}