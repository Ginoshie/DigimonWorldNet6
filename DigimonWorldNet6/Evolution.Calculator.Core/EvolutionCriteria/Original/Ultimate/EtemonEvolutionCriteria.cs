using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.Ultimate;

public sealed class EtemonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Ultimate;

    public EvolutionResult EvolutionResult => EvolutionResult.Etemon;

    public MainCriteriaStats Stats => new(hp: 2000, mp: 3000, off: 400, def: 200, speed: 400, brains: 300);

    public MainCriteriaCareMistakes CareMistakes => new(0, true);

    public MainCriteriaWeight Weight => new(15);

    public BonusCriteria BonusCriteria => new(battles: 50, isBattlesCriteriaAMaximum: false, techniqueCount: 49);
}