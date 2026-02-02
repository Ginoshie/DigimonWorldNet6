using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;

public interface IEvolutionCriteria
{
    EvolutionStage EvolutionStage { get; }

    EvolutionResult EvolutionResult { get; }

    MainCriteriaStats Stats { get; }

    MainCriteriaCareMistakes CareMistakes { get; }

    MainCriteriaWeight Weight { get; }

    BonusCriteria BonusCriteria { get; }
}