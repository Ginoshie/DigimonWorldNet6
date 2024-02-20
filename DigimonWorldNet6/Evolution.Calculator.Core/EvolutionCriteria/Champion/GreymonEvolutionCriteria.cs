using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Champion;

public sealed class GreymonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public EvolutionResult DigimonType => EvolutionResult.Greymon;

    public MainCriteriaStats Stats => new(off: 100, def: 100, speed: 100, brains: 100);

    public MainCriteriaCareMistakes CareMistakes => new(1, true);

    public MainCriteriaWeight Weight => new(30);

    public BonusCriteria BonusCriteria => new(discipline: 90, techniqueCount: 35);
}