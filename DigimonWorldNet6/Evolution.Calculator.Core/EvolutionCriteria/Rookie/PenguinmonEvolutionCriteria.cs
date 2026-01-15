using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Rookie;

public sealed class PenguinmonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Rookie;

    public EvolutionResult DigimonType => EvolutionResult.Penguinmon;

    public MainCriteriaStats Stats => new(mp: 10, def: 1, brains: 1);

    public MainCriteriaCareMistakes CareMistakes => new();

    public MainCriteriaWeight Weight => new(15);

    public BonusCriteria BonusCriteria => new(isBattlesCriteriaAMaximum: false, precursorDigimon: EvolutionResult.Tsunomon);
}