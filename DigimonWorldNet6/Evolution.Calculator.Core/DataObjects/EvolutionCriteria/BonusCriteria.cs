using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

public sealed class BonusCriteria(int happiness = -1, int discipline = -1, int battles = -1, bool isBattlesCriteriaAMaximum = true, int techniqueCount = 0, DigimonName? precursorDigimon = null)
{
    public int Happiness { get; } = happiness;

    public int Discipline { get; } = discipline;

    public bool IsBattlesCriteriaAMaximum { get; } = isBattlesCriteriaAMaximum;

    public int Battles { get; } = battles;

    public int TechniqueCount { get; } = techniqueCount;

    public DigimonName? PrecursorDigimon { get; } = precursorDigimon;
}