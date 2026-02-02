using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.Champion;

public sealed class WhamonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public EvolutionResult EvolutionResult => EvolutionResult.Whamon;

    public MainCriteriaStats Stats => new(hp: 1000, brains: 100);

    public MainCriteriaCareMistakes CareMistakes => new(5, true);

    public MainCriteriaWeight Weight => new(40);

    public BonusCriteria BonusCriteria => new(discipline: 60, techniqueCount: 28);
}