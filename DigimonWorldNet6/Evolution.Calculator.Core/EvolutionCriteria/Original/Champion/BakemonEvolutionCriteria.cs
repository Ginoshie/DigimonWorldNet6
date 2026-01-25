using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.Champion;

public sealed class BakemonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public EvolutionResult EvolutionResult => EvolutionResult.Bakemon;

    public MainCriteriaStats Stats => new(mp: 1000);

    public MainCriteriaCareMistakes CareMistakes => new(3);

    public MainCriteriaWeight Weight => new(20);

    public BonusCriteria BonusCriteria => new(happiness: 50, techniqueCount: 28);
}