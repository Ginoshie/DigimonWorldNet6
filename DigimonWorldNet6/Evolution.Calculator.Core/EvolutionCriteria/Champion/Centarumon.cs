using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Champion;

public class Centarumon : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public DigimonType DigimonType => DigimonType.Centarumon;

    public IMainCriteriaStats Stats => new MainCriteriaStats(brains: 100);

    public IMainCriteriaCareMistakes CareMistakes => new MainCriteriaCareMistakes(3, true);

    public IMainCriteriaWeight Weight => new MainCriteriaWeight(40);

    public IBonusCriteria BonusCriteria => new BonusCriteria(discipline: 60, techniqueCount: 28);
}