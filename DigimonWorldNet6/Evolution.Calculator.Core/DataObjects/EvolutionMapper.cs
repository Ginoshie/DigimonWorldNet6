using System.Collections.Generic;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Champion;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.DataObjects;

public sealed class EvolutionMapper : IEvolutionMapper
{
    public Dictionary<DigimonType, IEnumerable<IEvolutionCriteria>> GetAllEvolutionMappings()
    {
        var allEvolutionMappingsDictionary = new Dictionary<DigimonType, IEnumerable<IEvolutionCriteria>>
        {
            { DigimonType.Agumon, GetAgumonEvolutionCriteria() }
        };

        return allEvolutionMappingsDictionary;
    }

    private IEnumerable<IEvolutionCriteria> GetAgumonEvolutionCriteria()
    {
        return new IEvolutionCriteria[]
        {
            new GreymonEvolutionCriteria(), new MeramonEvolutionCriteria(), new BirdramonEvolutionCriteria(), new CentarumonEvolutionCriteria(),
            new MonochromonEvolutionCriteria(), new TyrannomonEvolutionCriteria()
        };
    }
}