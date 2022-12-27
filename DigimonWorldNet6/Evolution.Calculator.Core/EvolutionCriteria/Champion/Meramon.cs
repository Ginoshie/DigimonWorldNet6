using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Champion;

public class Meramon : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public DigimonType DigimonType => DigimonType.Meramon;

    public IMainCriteriaStats Stats => new MainCriteriaStats(off: 100);

    public IMainCriteriaCareMistakes CareMistakes => new MainCriteriaCareMistakes(5, false);

    public IMainCriteriaWeight Weight => new MainCriteriaWeight(20);

    public IBonusCriteria BonusCriteria =>
        new BonusCriteria(battles: 10, isBattlesCriteriaAMaximum: true, techniqueCount: 28);
}