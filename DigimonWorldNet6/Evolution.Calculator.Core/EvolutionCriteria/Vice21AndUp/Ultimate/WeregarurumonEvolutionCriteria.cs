using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Vice21AndUp.Ultimate;

public sealed class WeregarurumonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Ultimate;

    public EvolutionResult DigimonType => EvolutionResult.Weregarurumon;

    public MainCriteriaStats Stats => new(hp: 4000, def: 400, speed: 400, brains: 400);

    public MainCriteriaCareMistakes CareMistakes => new(3, true);

    public MainCriteriaWeight Weight => new(30);

    public BonusCriteria BonusCriteria => new(battles: 10, isBattlesCriteriaAMaximum: false, techniqueCount: 30, precursorDigimon: EvolutionResult.Garurumon);
}