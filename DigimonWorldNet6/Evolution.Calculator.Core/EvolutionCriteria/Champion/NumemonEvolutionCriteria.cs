using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Champion;

public sealed class NumemonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public EvolutionResult DigimonType => EvolutionResult.Numemon;

    public MainCriteriaStats Stats => new();

    public MainCriteriaCareMistakes CareMistakes => new();

    public MainCriteriaWeight Weight => new(0);

    public BonusCriteria BonusCriteria => new();
}