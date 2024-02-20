using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;

public interface IEvolutionCriteria
{
    EvolutionStage EvolutionStage { get; }

    EvolutionResult DigimonType { get; }

    MainCriteriaStats Stats { get; }

    MainCriteriaCareMistakes CareMistakes { get; }

    MainCriteriaWeight Weight { get; }

    BonusCriteria BonusCriteria { get; }
}