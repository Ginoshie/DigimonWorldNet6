using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;

public interface IBonusCriteria
{
    public int Happiness { get; }

    public int Discipline { get; }

    public bool IsBattlesCriteriaAMaximum { get; }

    public int Battles { get; }

    public int TechniqueCount { get; }

    public DigimonType PrecursorDigimon { get; }
}