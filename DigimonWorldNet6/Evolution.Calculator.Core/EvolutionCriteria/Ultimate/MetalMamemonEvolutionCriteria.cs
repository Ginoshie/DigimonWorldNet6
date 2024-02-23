using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Ultimate;

public sealed class MetalMamemonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Ultimate;

    public EvolutionResult DigimonType => EvolutionResult.MetalMamemon;

    public MainCriteriaStats Stats => new(off: 500, def: 400, speed: 400, brains: 400);

    public MainCriteriaCareMistakes CareMistakes => new(15, true);

    public MainCriteriaWeight Weight => new(10);

    public BonusCriteria BonusCriteria => new(happiness: 95, battles: 30, isBattlesCriteriaAMaximum: false, techniqueCount: 30);
}