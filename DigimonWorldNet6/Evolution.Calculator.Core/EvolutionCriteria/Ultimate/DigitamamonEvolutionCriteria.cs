using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Ultimate;

public sealed class DigitamamonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Ultimate;

    public EvolutionResult DigimonType => EvolutionResult.Digitamamon;

    public MainCriteriaStats Stats => new(hp: 3000, mp: 3000, off: 400, def: 400, speed: 400, brains: 300);

    public MainCriteriaCareMistakes CareMistakes => new(0, true);

    public MainCriteriaWeight Weight => new(10);

    public BonusCriteria BonusCriteria => new(battles: 100, isBattlesCriteriaAMaximum: false, techniqueCount: 49);
}