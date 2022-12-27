using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

public class BonusCriteria : IBonusCriteria
{
    public BonusCriteria(int happiness = 0, int discipline = 0, int battles = 0, bool isBattlesCriteriaAMaximum = true,
        int techniqueCount = 0, DigimonType precursorDigimon = DigimonType.None)
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

    public DigimonType PrecursorDigimon { get; }
}