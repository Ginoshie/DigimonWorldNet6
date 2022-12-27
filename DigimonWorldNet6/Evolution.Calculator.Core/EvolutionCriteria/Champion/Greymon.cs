using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Champion;

public class Greymon : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public DigimonType DigimonType => DigimonType.Greymon;

    public IMainCriteriaStats Stats => new MainCriteriaStats(off: 100, def: 100, speed: 100, brains: 100);

    public IMainCriteriaCareMistakes CareMistakes => new MainCriteriaCareMistakes(0, true);

    public IMainCriteriaWeight Weight => new MainCriteriaWeight(30);

    public IBonusCriteria BonusCriteria => new BonusCriteria(discipline: 90, techniqueCount: 35);
}