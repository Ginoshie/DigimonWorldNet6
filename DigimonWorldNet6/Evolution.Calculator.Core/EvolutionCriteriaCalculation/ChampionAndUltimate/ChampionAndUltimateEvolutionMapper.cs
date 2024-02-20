using System.Collections.Generic;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Champion;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.ChampionAndUltimate;

public sealed class ChampionAndUltimateEvolutionMapper
{
    private readonly Dictionary<DigimonType, IEnumerable<IEvolutionCriteria>> _championAndUltimateEvolutionMappings = new();

    public ChampionAndUltimateEvolutionMapper()
    {
        _championAndUltimateEvolutionMappings[DigimonType.Agumon] = AgumonEvolutions;
    }

    public IEnumerable<IEvolutionCriteria> this[DigimonType evolutionResult] =>
        _championAndUltimateEvolutionMappings[evolutionResult] ??
        throw new KeyNotFoundException($"Evolution mapping for {evolutionResult} was not found in {nameof(ChampionAndUltimateEvolutionMapper)}");

    private IEnumerable<IEvolutionCriteria> AgumonEvolutions { get; } = new IEvolutionCriteria[]
    {
        new GreymonEvolutionCriteria(), new MeramonEvolutionCriteria(), new BirdramonEvolutionCriteria(), new CentarumonEvolutionCriteria(),
        new MonochromonEvolutionCriteria(), new TyrannomonEvolutionCriteria()
    };
}