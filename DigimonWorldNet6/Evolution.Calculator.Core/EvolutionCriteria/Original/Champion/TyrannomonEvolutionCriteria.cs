using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.Champion;

public sealed class TyrannomonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public EvolutionResult DigimonType => EvolutionResult.Tyrannomon;

    public MainCriteriaStats Stats => new(hp: 1000, def: 100);

    public MainCriteriaCareMistakes CareMistakes => new(5, true);

    public MainCriteriaWeight Weight => new(30);

    public BonusCriteria BonusCriteria => new(battles: 5, isBattlesCriteriaAMaximum: true, techniqueCount: 28);
}