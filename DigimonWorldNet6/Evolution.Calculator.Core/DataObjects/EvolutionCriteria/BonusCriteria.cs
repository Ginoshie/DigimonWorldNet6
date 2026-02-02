using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

public sealed class BonusCriteria
{
    public BonusCriteria(int happiness = -1, int discipline = -1, int battles = -1, bool isBattlesCriteriaAMaximum = true, int techniqueCount = 0, DigimonName? precursorDigimon = null)
    {
        Happiness = happiness;
        Discipline = discipline;
        IsBattlesCriteriaAMaximum = isBattlesCriteriaAMaximum;
        Battles = battles;
        TechniqueCount = techniqueCount;
        PrecursorDigimon = precursorDigimon;
    }

    public int Happiness { get; }

    public int Discipline { get; }

    public bool IsBattlesCriteriaAMaximum { get; }

    public int Battles { get; }

    public int TechniqueCount { get; }

    public DigimonName? PrecursorDigimon { get; }
}