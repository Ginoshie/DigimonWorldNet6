using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;

public interface IEvolutionCriteria
{
    EvolutionStage EvolutionStage { get; }

    DigimonType DigimonType { get; }

    IMainCriteriaStats Stats { get; }

    IMainCriteriaCareMistakes CareMistakes { get; }

    IMainCriteriaWeight Weight { get; }

    IBonusCriteria BonusCriteria { get; }
}