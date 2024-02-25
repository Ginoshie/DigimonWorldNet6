using System.Collections.Generic;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Rookie;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromInTraining;

public sealed class FromInTrainingEvolutionMapper
{
    private readonly Dictionary<DigimonType, IEnumerable<IEvolutionCriteria>> _fromInTrainingEvolutionMappings = new();

    public FromInTrainingEvolutionMapper()
    {
        _fromInTrainingEvolutionMappings[DigimonType.Koromon] = KoromonEvolutions;
        _fromInTrainingEvolutionMappings[DigimonType.Tanemon] = TanemonEvolutions;
        _fromInTrainingEvolutionMappings[DigimonType.Tokomon] = TokomonEvolutions;
        _fromInTrainingEvolutionMappings[DigimonType.Tsunomon] = TsunomonEvolutions;
    }

    public IEnumerable<IEvolutionCriteria> this[DigimonType digimonType]
    {
        get
        {
            if (_fromInTrainingEvolutionMappings.TryGetValue(digimonType, out var evolutionResult))
            {
                return evolutionResult;
            }
            else
            {
                throw new KeyNotFoundException($"Evolution mapping for {digimonType} was not found in {nameof(FromInTrainingEvolutionMapper)}");
            }
        }
    }

    private IEnumerable<IEvolutionCriteria> KoromonEvolutions { get; } = new IEvolutionCriteria[]
    {
        new AgumonEvolutionCriteria(), new GabumonEvolutionCriteria()
    };

    private IEnumerable<IEvolutionCriteria> TanemonEvolutions { get; } = new IEvolutionCriteria[]
    {
        new PalmonEvolutionCriteria(), new BetamonEvolutionCriteria()
    };

    private IEnumerable<IEvolutionCriteria> TokomonEvolutions { get; } = new IEvolutionCriteria[]
    {
        new PatamonEvolutionCriteria(), new BiyomonEvolutionCriteria()
    };

    private IEnumerable<IEvolutionCriteria> TsunomonEvolutions { get; } = new IEvolutionCriteria[]
    {
        new ElecmonEvolutionCriteria(), new PenguinmonEvolutionCriteria()
    };
}