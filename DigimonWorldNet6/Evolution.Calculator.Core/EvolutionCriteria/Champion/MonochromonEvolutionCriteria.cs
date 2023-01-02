using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Champion;

public sealed class MonochromonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public DigimonType DigimonType => DigimonType.Monochromon;

    public MainCriteriaStats Stats => new(1000, brains: 100);

    public MainCriteriaCareMistakes CareMistakes => new(3, true);

    public MainCriteriaWeight Weight => new(40);

    public BonusCriteria BonusCriteria => new(battles: 5, isBattlesCriteriaAMaximum: true, techniqueCount: 35);
}