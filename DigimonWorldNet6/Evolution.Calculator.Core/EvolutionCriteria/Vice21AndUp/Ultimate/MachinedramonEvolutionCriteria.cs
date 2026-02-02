using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Vice21AndUp.Ultimate;

public sealed class MachinedramonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Ultimate;

    public EvolutionResult EvolutionResult => EvolutionResult.Machinedramon;

    public MainCriteriaStats Stats => new(hp: 5000, mp: 5000, off: 500, def: 500, speed: 500, brains: 500);

    public MainCriteriaCareMistakes CareMistakes => new(10);

    public MainCriteriaWeight Weight => new(55);

    public BonusCriteria BonusCriteria => new(battles: 100, isBattlesCriteriaAMaximum: false, techniqueCount: 49);
}