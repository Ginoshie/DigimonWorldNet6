using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.Rookie;

public sealed class AgumonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Rookie;

    public EvolutionResult EvolutionResult => EvolutionResult.Agumon;

    public MainCriteriaStats Stats => new(hp: 10, mp: 10, off: 1);

    public MainCriteriaCareMistakes CareMistakes => new();

    public MainCriteriaWeight Weight => new(15);

    public BonusCriteria BonusCriteria => new(isBattlesCriteriaAMaximum: false, precursorDigimon: DigimonName.Koromon);
}