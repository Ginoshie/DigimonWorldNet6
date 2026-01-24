using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.Ultimate;

public sealed class MegaSeadramonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Ultimate;

    public EvolutionResult DigimonType => EvolutionResult.MegaSeadramon;

    public MainCriteriaStats Stats => new(mp: 4000, off: 500, def: 400, brains: 400);

    public MainCriteriaCareMistakes CareMistakes => new(5, true);

    public MainCriteriaWeight Weight => new(30);

    public BonusCriteria BonusCriteria => new(battles: 0, isBattlesCriteriaAMaximum: true, techniqueCount: 40);
}