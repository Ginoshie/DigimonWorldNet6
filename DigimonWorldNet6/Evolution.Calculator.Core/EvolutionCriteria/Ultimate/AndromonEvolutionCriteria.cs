using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Ultimate;

public sealed class AndromonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Ultimate;

    public EvolutionResult DigimonType => EvolutionResult.Andromon;

    public MainCriteriaStats Stats => new(hp: 2000, mp: 4000, off: 200, def: 400, speed: 200, brains: 400);

    public MainCriteriaCareMistakes CareMistakes => new(5, true);

    public MainCriteriaWeight Weight => new(40);

    public BonusCriteria BonusCriteria => new(discipline: 95, battles: 30, isBattlesCriteriaAMaximum: false, techniqueCount: 30);
}