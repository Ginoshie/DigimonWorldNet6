using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.InTraining;

public sealed class KoromonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.InTraining;

    public EvolutionResult EvolutionResult => EvolutionResult.Koromon;

    public MainCriteriaStats Stats => new();

    public MainCriteriaCareMistakes CareMistakes => new();

    public MainCriteriaWeight Weight => new(0);

    public BonusCriteria BonusCriteria => new();
}