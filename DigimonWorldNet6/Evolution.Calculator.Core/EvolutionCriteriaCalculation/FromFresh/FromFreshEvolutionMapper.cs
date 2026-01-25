using System.Collections.Generic;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromFresh;

public sealed class FromFreshEvolutionMapper
{
    private readonly Dictionary<DigimonName, EvolutionResult> _fromFreshEvolutionMappings = new();

    public FromFreshEvolutionMapper()
    {
        _fromFreshEvolutionMappings[DigimonName.Botamon] = EvolutionResult.Koromon;
        _fromFreshEvolutionMappings[DigimonName.Poyomon] = EvolutionResult.Tokomon;
        _fromFreshEvolutionMappings[DigimonName.Punimon] = EvolutionResult.Tsunomon;
        _fromFreshEvolutionMappings[DigimonName.Yuramon] = EvolutionResult.Tanemon;
    }

    public EvolutionResult this[DigimonName digimonName]
    {
        get
        {
            if (_fromFreshEvolutionMappings.TryGetValue(digimonName, out EvolutionResult evolutionResult))
            {
                return evolutionResult;
            }

            throw new KeyNotFoundException($"Evolution mapping for {digimonName} was not found in {nameof(FromFreshEvolutionMapper)}");
        }
    }
}