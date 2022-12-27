using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Champion;

public class Birdramon : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public DigimonType DigimonType => DigimonType.Birdramon;

    public IMainCriteriaStats Stats => new MainCriteriaStats(speed: 100);

    public IMainCriteriaCareMistakes CareMistakes => new MainCriteriaCareMistakes(3, false);

    public IMainCriteriaWeight Weight => new MainCriteriaWeight(20);

    public IBonusCriteria BonusCriteria => new BonusCriteria(techniqueCount: 35, precursorDigimon: DigimonType.Biyomon);
}