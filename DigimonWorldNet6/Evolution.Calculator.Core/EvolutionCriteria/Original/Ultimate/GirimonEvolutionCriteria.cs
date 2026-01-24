using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.Ultimate;

public sealed class GiromonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Ultimate;

    public EvolutionResult DigimonType => EvolutionResult.Giromon;

    public MainCriteriaStats Stats => new(off: 400, speed: 3200, brains: 400);

    public MainCriteriaCareMistakes CareMistakes => new(15);

    public MainCriteriaWeight Weight => new(5);

    public BonusCriteria BonusCriteria => new(happiness: 95, battles: 100, isBattlesCriteriaAMaximum: false, techniqueCount: 35);
}