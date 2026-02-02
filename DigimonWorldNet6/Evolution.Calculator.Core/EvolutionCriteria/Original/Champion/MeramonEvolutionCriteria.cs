using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.Champion;

public sealed class MeramonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public EvolutionResult EvolutionResult => EvolutionResult.Meramon;

    public MainCriteriaStats Stats => new(off: 100);

    public MainCriteriaCareMistakes CareMistakes => new(5);

    public MainCriteriaWeight Weight => new(20);

    public BonusCriteria BonusCriteria => new(battles: 10, isBattlesCriteriaAMaximum: false, techniqueCount: 28);
}