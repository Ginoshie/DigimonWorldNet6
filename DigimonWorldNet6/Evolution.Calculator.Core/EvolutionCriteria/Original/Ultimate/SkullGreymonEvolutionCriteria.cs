using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.Ultimate;

public sealed class SkullGreymonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Ultimate;

    public EvolutionResult EvolutionResult => EvolutionResult.SkullGreymon;

    public MainCriteriaStats Stats => new(hp: 4000, mp: 6000, off: 400, def: 400, speed: 200, brains: 500);

    public MainCriteriaCareMistakes CareMistakes => new(10);

    public MainCriteriaWeight Weight => new(30);

    public BonusCriteria BonusCriteria => new(battles: 40, isBattlesCriteriaAMaximum: false, techniqueCount: 30);
}