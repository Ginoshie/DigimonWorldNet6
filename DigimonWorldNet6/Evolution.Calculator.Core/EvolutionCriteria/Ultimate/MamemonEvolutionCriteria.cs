using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Ultimate;

public sealed class MamemonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Ultimate;

    public EvolutionResult DigimonType => EvolutionResult.Mamemon;

    public MainCriteriaStats Stats => new(off: 400, def: 300, speed: 300, brains: 400);

    public MainCriteriaCareMistakes CareMistakes => new(15);

    public MainCriteriaWeight Weight => new(5);

    public BonusCriteria BonusCriteria => new(happiness: 90, isBattlesCriteriaAMaximum: false, techniqueCount: 25);
}