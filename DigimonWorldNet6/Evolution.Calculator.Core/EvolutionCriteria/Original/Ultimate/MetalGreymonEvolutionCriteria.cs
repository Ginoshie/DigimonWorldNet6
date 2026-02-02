using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.Ultimate;

public sealed class MetalGreymonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Ultimate;

    public EvolutionResult EvolutionResult => EvolutionResult.MetalGreymon;

    public MainCriteriaStats Stats => new(hp: 4000, mp: 3000, off: 500, def: 500, speed: 300, brains: 300);

    public MainCriteriaCareMistakes CareMistakes => new(10, true);

    public MainCriteriaWeight Weight => new(65);

    public BonusCriteria BonusCriteria => new(discipline: 95, battles: 30, isBattlesCriteriaAMaximum: false, techniqueCount: 30);
}