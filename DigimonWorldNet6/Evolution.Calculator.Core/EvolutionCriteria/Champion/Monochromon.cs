using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Champion;

public class Monochromon : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public DigimonType DigimonType => DigimonType.Monochromon;

    public IMainCriteriaStats Stats => new MainCriteriaStats(1000, brains: 100);

    public IMainCriteriaCareMistakes CareMistakes => new MainCriteriaCareMistakes(3, true);

    public IMainCriteriaWeight Weight => new MainCriteriaWeight(40);

    public IBonusCriteria BonusCriteria =>
        new BonusCriteria(battles: 5, isBattlesCriteriaAMaximum: true, techniqueCount: 35);
}