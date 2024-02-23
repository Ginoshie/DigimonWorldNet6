using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Champion;

public sealed class NinjamonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public EvolutionResult DigimonType => EvolutionResult.Ninjamon;

    public MainCriteriaStats Stats => new(mp: 1000, off: 100, speed: 100);

    public MainCriteriaCareMistakes CareMistakes => new(1, true);

    public MainCriteriaWeight Weight => new(10);

    public BonusCriteria BonusCriteria => new(battles: 15, isBattlesCriteriaAMaximum: true, techniqueCount: 35);
}