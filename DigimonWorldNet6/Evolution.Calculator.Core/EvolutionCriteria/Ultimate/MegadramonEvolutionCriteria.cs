using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Ultimate;

public sealed class MegadramonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Ultimate;

    public EvolutionResult DigimonType => EvolutionResult.Megadramon;

    public MainCriteriaStats Stats => new(hp: 3000, mp: 5000, off: 500, def: 300, speed: 400, brains: 400);

    public MainCriteriaCareMistakes CareMistakes => new(10, true);

    public MainCriteriaWeight Weight => new(55);

    public BonusCriteria BonusCriteria => new(battles: 30, isBattlesCriteriaAMaximum: false, techniqueCount: 30);
}