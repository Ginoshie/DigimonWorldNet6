using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Champion;

public sealed class OgremonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public EvolutionResult DigimonType => EvolutionResult.Ogremon;

    public MainCriteriaStats Stats => new(hp: 1000, off: 100);

    public MainCriteriaCareMistakes CareMistakes => new(5, false);

    public MainCriteriaWeight Weight => new(30);

    public BonusCriteria BonusCriteria => new(battles: 15, isBattlesCriteriaAMaximum: false, techniqueCount: 35);
}