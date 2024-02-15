using System.Collections.Generic;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Rookie;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.ChampionAndUltimate;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.Rookie;

public sealed class RookieEvolutionMapper
{
    private readonly Dictionary<DigimonType, IEnumerable<IEvolutionCriteria>> _championAndUltimateEvolutionMappings = new();

    public RookieEvolutionMapper()
    {
        _championAndUltimateEvolutionMappings[DigimonType.Tokomon] = TokomonEvolutions;
    }

    public IEnumerable<IEvolutionCriteria> this[DigimonType digimonType] =>
        _championAndUltimateEvolutionMappings[digimonType] ??
        throw new KeyNotFoundException($"Evolution mapping for {digimonType} was not found in {nameof(ChampionAndUltimateEvolutionMapper)}");

    private IEnumerable<IEvolutionCriteria> TokomonEvolutions { get; } = new IEvolutionCriteria[]
    {
        new PatamonEvolutionCriteria(), new BiyomonEvolutionCriteria()
    };
}