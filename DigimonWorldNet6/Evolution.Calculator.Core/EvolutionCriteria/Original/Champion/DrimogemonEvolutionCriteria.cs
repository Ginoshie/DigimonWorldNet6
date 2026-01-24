using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.Champion;

public sealed class DrimogemonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public EvolutionResult DigimonType => EvolutionResult.Drimogemon;

    public MainCriteriaStats Stats => new(off: 100);

    public MainCriteriaCareMistakes CareMistakes => new(3);

    public MainCriteriaWeight Weight => new(40);

    public BonusCriteria BonusCriteria => new(happiness: 50, techniqueCount: 28);
}