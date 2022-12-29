using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Champion;

public class CentarumonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public DigimonType DigimonType => DigimonType.Centarumon;

    public MainCriteriaStats Stats => new(brains: 100);

    public MainCriteriaCareMistakes CareMistakes => new(3, true);

    public MainCriteriaWeight Weight => new(40);

    public BonusCriteria BonusCriteria => new(discipline: 60, techniqueCount: 28);
}