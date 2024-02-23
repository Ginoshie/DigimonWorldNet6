using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Champion;

public sealed class AngemonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public EvolutionResult DigimonType => EvolutionResult.Angemon;

    public MainCriteriaStats Stats => new(mp: 1000, brains: 100);

    public MainCriteriaCareMistakes CareMistakes => new(0, true);

    public MainCriteriaWeight Weight => new(20);

    public BonusCriteria BonusCriteria => new(techniqueCount: 35, precursorDigimon: EvolutionResult.Patamon);
}