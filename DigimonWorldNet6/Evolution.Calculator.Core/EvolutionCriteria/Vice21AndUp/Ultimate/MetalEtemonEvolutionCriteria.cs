using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Vice21AndUp.Ultimate;

public sealed class MetalEtemonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Ultimate;

    public EvolutionResult DigimonType => EvolutionResult.MetalEtemon;

    public MainCriteriaStats Stats => new(mp: 4000, off: 600, speed: 500, brains: 300);

    public MainCriteriaCareMistakes CareMistakes => new(0, true);

    public MainCriteriaWeight Weight => new(35);

    public BonusCriteria BonusCriteria => new(battles: 50, isBattlesCriteriaAMaximum: false, techniqueCount: 49);
}