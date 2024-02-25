using System.Collections.Generic;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Champion;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Ultimate;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;

public sealed class FromRookieOrChampionEvolutionMapper
{
    private readonly Dictionary<DigimonType, IEnumerable<IEvolutionCriteria>> _championAndUltimateEvolutionMappings = new();

    public FromRookieOrChampionEvolutionMapper()
    {
        _championAndUltimateEvolutionMappings[DigimonType.Agumon] = AgumonEvolutions;
        _championAndUltimateEvolutionMappings[DigimonType.Greymon] = GreymonEvolutions;
    }

    public IEnumerable<IEvolutionCriteria> this[DigimonType digimonType]
    {
        get
        {
            if (!_championAndUltimateEvolutionMappings.TryGetValue(digimonType, out var evolutionOptions))
                throw new KeyNotFoundException($"Evolution mapping for {digimonType} was not found in {nameof(FromRookieOrChampionEvolutionMapper)}");
            
            return evolutionOptions;
        }
    }

    private IEnumerable<IEvolutionCriteria> AgumonEvolutions { get; } = new IEvolutionCriteria[]
    {
        new GreymonEvolutionCriteria(), new MeramonEvolutionCriteria(), new BirdramonEvolutionCriteria(), new CentarumonEvolutionCriteria(),
        new MonochromonEvolutionCriteria(), new TyrannomonEvolutionCriteria()
    };

    private IEnumerable<IEvolutionCriteria> GreymonEvolutions { get; } = new IEvolutionCriteria[]
    {
        new MetalGreymonEvolutionCriteria(), new SkullGreymonEvolutionCriteria()
    };
}