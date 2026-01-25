using System.Collections.Generic;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.Rookie;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromInTraining;

public sealed class FromInTrainingEvolutionMapper
{
    private readonly Dictionary<DigimonName, IEnumerable<IEvolutionCriteria>> _fromInTrainingEvolutionMappings = new();

    public FromInTrainingEvolutionMapper()
    {
        _fromInTrainingEvolutionMappings[DigimonName.Koromon] = KoromonEvolutions;
        _fromInTrainingEvolutionMappings[DigimonName.Tanemon] = TanemonEvolutions;
        _fromInTrainingEvolutionMappings[DigimonName.Tokomon] = TokomonEvolutions;
        _fromInTrainingEvolutionMappings[DigimonName.Tsunomon] = TsunomonEvolutions;
    }

    public IEnumerable<IEvolutionCriteria> this[DigimonName digimonName]
    {
        get
        {
            if (_fromInTrainingEvolutionMappings.TryGetValue(digimonName, out IEnumerable<IEvolutionCriteria>? evolutionResult))
            {
                return evolutionResult;
            }

            throw new KeyNotFoundException($"Evolution mapping for {digimonName} was not found in {nameof(FromInTrainingEvolutionMapper)}");
        }
    }

    private IEnumerable<IEvolutionCriteria> KoromonEvolutions { get; } =
    [
        new AgumonEvolutionCriteria(), new GabumonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> TanemonEvolutions { get; } =
    [
        new PalmonEvolutionCriteria(), new BetamonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> TokomonEvolutions { get; } =
    [
        new PatamonEvolutionCriteria(), new BiyomonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> TsunomonEvolutions { get; } =
    [
        new ElecmonEvolutionCriteria(), new PenguinmonEvolutionCriteria()
    ];
}