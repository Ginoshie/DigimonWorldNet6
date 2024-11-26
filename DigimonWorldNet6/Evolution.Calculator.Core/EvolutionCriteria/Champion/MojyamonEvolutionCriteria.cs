using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Champion;

public sealed class MojyamonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public EvolutionResult DigimonType => EvolutionResult.Mojyamon;

    public MainCriteriaStats Stats => new(hp: 1000);

    public MainCriteriaCareMistakes CareMistakes => new(5);

    public MainCriteriaWeight Weight => new(20);

    public BonusCriteria BonusCriteria => new(battles: 5, isBattlesCriteriaAMaximum: true,techniqueCount: 28);
}