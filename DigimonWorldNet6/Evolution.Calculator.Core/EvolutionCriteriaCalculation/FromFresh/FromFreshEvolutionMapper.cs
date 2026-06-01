using System.Collections.Generic;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.InTraining;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromFresh;

public sealed class FromFreshEvolutionMapper
{
    private readonly Dictionary<DigimonName, IEvolutionCriteria> _fromFreshEvolutionMappings = new();

    public FromFreshEvolutionMapper()
    {
        _fromFreshEvolutionMappings[DigimonName.Botamon] = new KoromonEvolutionCriteria();
        _fromFreshEvolutionMappings[DigimonName.Poyomon] = new TokomonEvolutionCriteria();
        _fromFreshEvolutionMappings[DigimonName.Punimon] = new TsunomonEvolutionCriteria();
        _fromFreshEvolutionMappings[DigimonName.Yuramon] = new TanemonEvolutionCriteria();
    }

    public IEvolutionCriteria this[DigimonName digimonName]
    {
        get
        {
            if (_fromFreshEvolutionMappings.TryGetValue(digimonName, out IEvolutionCriteria? evolutionResult))
            {
                return evolutionResult;
            }

            throw new KeyNotFoundException($"Evolution mapping for {digimonName} was not found in {nameof(FromFreshEvolutionMapper)}");
        }
    }
}