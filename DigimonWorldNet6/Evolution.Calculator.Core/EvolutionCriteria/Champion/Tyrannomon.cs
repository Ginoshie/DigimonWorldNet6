using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Champion;

public class Tyrannomon : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public DigimonType DigimonType => DigimonType.Tyrannomon;

    public IMainCriteriaStats Stats => new MainCriteriaStats(1000, def: 100);

    public IMainCriteriaCareMistakes CareMistakes => new MainCriteriaCareMistakes(5, true);

    public IMainCriteriaWeight Weight => new MainCriteriaWeight(30);

    public IBonusCriteria BonusCriteria =>
        new BonusCriteria(battles: 5, isBattlesCriteriaAMaximum: true, techniqueCount: 28);
}