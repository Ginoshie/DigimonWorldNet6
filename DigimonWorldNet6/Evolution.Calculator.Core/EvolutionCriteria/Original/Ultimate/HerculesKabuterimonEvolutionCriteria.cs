using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.Ultimate;

public sealed class HerculesKabuterimonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Ultimate;

    public EvolutionResult DigimonType => EvolutionResult.HerculesKabuterimon;

    public MainCriteriaStats Stats => new(hp: 7000, off: 400, def: 600, speed: 400);

    public MainCriteriaCareMistakes CareMistakes => new(5, true);

    public MainCriteriaWeight Weight => new(55);

    public BonusCriteria BonusCriteria => new(battles: 0, isBattlesCriteriaAMaximum: true, techniqueCount: 40);
}