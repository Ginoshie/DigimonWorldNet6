using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.Champion;

public sealed class CoelamonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public EvolutionResult EvolutionResult => EvolutionResult.Coelamon;

    public MainCriteriaStats Stats => new(def: 100);

    public MainCriteriaCareMistakes CareMistakes => new(3);

    public MainCriteriaWeight Weight => new(30);

    public BonusCriteria BonusCriteria => new(battles: 5, techniqueCount: 35);
}