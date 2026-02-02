using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.Champion;

public sealed class CentarumonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public EvolutionResult EvolutionResult => EvolutionResult.Centarumon;

    public MainCriteriaStats Stats => new(brains: 100);

    public MainCriteriaCareMistakes CareMistakes => new(3, true);

    public MainCriteriaWeight Weight => new(30);

    public BonusCriteria BonusCriteria => new(discipline: 60, techniqueCount: 28);
}