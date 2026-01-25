using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.Ultimate;

public sealed class MonzaemonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Ultimate;

    public EvolutionResult EvolutionResult => EvolutionResult.Monzaemon;

    public MainCriteriaStats Stats => new(hp: 3000, mp: 3000, off: 300, def: 300, speed: 300, brains: 300);

    public MainCriteriaCareMistakes CareMistakes => new(0, true);

    public MainCriteriaWeight Weight => new(40);

    public BonusCriteria BonusCriteria => new(battles: 50, isBattlesCriteriaAMaximum: false, techniqueCount: 49);
}