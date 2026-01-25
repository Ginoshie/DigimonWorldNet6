using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.InTraining;

public sealed class TanemonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.InTraining;

    public EvolutionResult EvolutionResult => EvolutionResult.Tanemon;

    public MainCriteriaStats Stats => new();

    public MainCriteriaCareMistakes CareMistakes => new();

    public MainCriteriaWeight Weight => new(0);

    public BonusCriteria BonusCriteria => new();
}