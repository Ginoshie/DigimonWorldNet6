using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Vice21AndUp.Ultimate;

public sealed class GigadramonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Ultimate;

    public EvolutionResult EvolutionResult => EvolutionResult.Gigadramon;

    public MainCriteriaStats Stats => new(hp: 4000, mp: 5000, off: 600, speed: 400);

    public MainCriteriaCareMistakes CareMistakes => new(10, true);

    public MainCriteriaWeight Weight => new(45);

    public BonusCriteria BonusCriteria => new(battles: 30, isBattlesCriteriaAMaximum: false, techniqueCount: 30);
}