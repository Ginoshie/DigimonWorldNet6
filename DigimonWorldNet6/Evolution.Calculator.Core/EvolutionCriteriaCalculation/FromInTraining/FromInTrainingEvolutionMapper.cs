using System.Collections.Generic;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Rookie;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.Rookie;

public sealed class FromInTrainingEvolutionMapper
{
    private readonly Dictionary<DigimonType, IEnumerable<IEvolutionCriteria>> _rookieEvolutionMappings = new();

    public FromInTrainingEvolutionMapper()
    {
        _rookieEvolutionMappings[DigimonType.Tokomon] = TokomonEvolutions;
    }

    public IEnumerable<IEvolutionCriteria> this[DigimonType digimonType] =>
        _rookieEvolutionMappings[digimonType] ??
        throw new KeyNotFoundException($"Evolution mapping for {digimonType} was not found in {nameof(FromInTrainingEvolutionMapper)}");

    private IEnumerable<IEvolutionCriteria> TokomonEvolutions { get; } = new IEvolutionCriteria[]
    {
        new PatamonEvolutionCriteria(), new BiyomonEvolutionCriteria()
    };
}