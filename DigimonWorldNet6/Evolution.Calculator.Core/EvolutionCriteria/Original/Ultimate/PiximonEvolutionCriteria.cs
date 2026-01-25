using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.Ultimate;

public sealed class PiximonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Ultimate;

    public EvolutionResult EvolutionResult => EvolutionResult.Piximon;

    public MainCriteriaStats Stats => new(off: 300, def: 300, speed: 400, brains: 400);

    public MainCriteriaCareMistakes CareMistakes => new(15);

    public MainCriteriaWeight Weight => new(5);

    public BonusCriteria BonusCriteria => new(happiness: 95, techniqueCount: 25);
}