using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.InTraining;

public sealed class TokomonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.InTraining;

    public EvolutionResult DigimonType => EvolutionResult.Tokomon;

    public MainCriteriaStats Stats => new();

    public MainCriteriaCareMistakes CareMistakes => new();

    public MainCriteriaWeight Weight => new(0);

    public BonusCriteria BonusCriteria => new();
}