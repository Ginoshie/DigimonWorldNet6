using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Panjyamon.Champion;

public sealed class PanjyamonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public EvolutionResult DigimonType => EvolutionResult.Panjyamon;

    public MainCriteriaStats Stats => new(off: 100, speed: 100, brains: 100);

    public MainCriteriaCareMistakes CareMistakes => new(1, true);

    public MainCriteriaWeight Weight => new(20);

    public BonusCriteria BonusCriteria => new(battles: 10, isBattlesCriteriaAMaximum: true, techniqueCount: 35, precursorDigimon: EvolutionResult.Gabumon);
}