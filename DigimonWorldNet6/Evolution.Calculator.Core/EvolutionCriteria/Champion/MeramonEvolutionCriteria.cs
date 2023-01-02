using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Champion;

public sealed class MeramonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public DigimonType DigimonType => DigimonType.Meramon;

    public MainCriteriaStats Stats => new(off: 100);

    public MainCriteriaCareMistakes CareMistakes => new(5, false);

    public MainCriteriaWeight Weight => new(20);

    public BonusCriteria BonusCriteria => new(battles: 10, isBattlesCriteriaAMaximum: true, techniqueCount: 28);
}