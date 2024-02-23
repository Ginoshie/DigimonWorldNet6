using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Rookie;

public sealed class BetamonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Rookie;

    public EvolutionResult DigimonType => EvolutionResult.Betamon;

    public MainCriteriaStats Stats => new(hp: 10, mp: 10, def: 1);

    public MainCriteriaCareMistakes CareMistakes => new();

    public MainCriteriaWeight Weight => new(15);

    public BonusCriteria BonusCriteria => new(battles: -2, precursorDigimon: EvolutionResult.Tanemon);
}