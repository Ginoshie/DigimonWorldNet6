using System.Collections.Generic;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Champion;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;

public sealed class FromRookieOrChampionEvolutionMapper
{
    private readonly Dictionary<DigimonType, IEnumerable<IEvolutionCriteria>> _championAndUltimateEvolutionMappings = new();

    public FromRookieOrChampionEvolutionMapper()
    {
        _championAndUltimateEvolutionMappings[DigimonType.Agumon] = AgumonEvolutions;
    }

    public IEnumerable<IEvolutionCriteria> this[DigimonType evolutionResult] =>
        _championAndUltimateEvolutionMappings[evolutionResult] ??
        throw new KeyNotFoundException($"Evolution mapping for {evolutionResult} was not found in {nameof(FromRookieOrChampionEvolutionMapper)}");

    private IEnumerable<IEvolutionCriteria> AgumonEvolutions { get; } = new IEvolutionCriteria[]
    {
        new GreymonEvolutionCriteria(), new MeramonEvolutionCriteria(), new BirdramonEvolutionCriteria(), new CentarumonEvolutionCriteria(),
        new MonochromonEvolutionCriteria(), new TyrannomonEvolutionCriteria()
    };
}