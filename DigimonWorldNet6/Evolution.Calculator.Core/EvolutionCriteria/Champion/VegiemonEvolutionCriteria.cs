using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Champion;

public sealed class VegiemonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public EvolutionResult DigimonType => EvolutionResult.Vegiemon;

    public MainCriteriaStats Stats => new(mp: 1000);

    public MainCriteriaCareMistakes CareMistakes => new(5, false);

    public MainCriteriaWeight Weight => new(10);

    public BonusCriteria BonusCriteria => new(happiness: 50, techniqueCount: 21);
}