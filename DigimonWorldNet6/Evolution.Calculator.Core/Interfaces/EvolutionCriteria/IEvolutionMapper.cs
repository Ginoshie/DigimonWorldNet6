using System.Collections.Generic;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;

public interface IEvolutionMapper
{
    public Dictionary<DigimonType, IEnumerable<IEvolutionCriteria>> GetAllEvolutionMappings();
}