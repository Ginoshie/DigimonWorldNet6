using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.Champion;

public sealed class NumemonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public EvolutionResult EvolutionResult => EvolutionResult.Numemon;

    public MainCriteriaStats Stats => new();

    public MainCriteriaCareMistakes CareMistakes => new();

    public MainCriteriaWeight Weight => new(0);

    public BonusCriteria BonusCriteria => new();
}