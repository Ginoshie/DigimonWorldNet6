using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Champion;

public sealed class BirdramonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public DigimonType DigimonType => DigimonType.Birdramon;

    public MainCriteriaStats Stats => new(speed: 100);

    public MainCriteriaCareMistakes CareMistakes => new(3, false);

    public MainCriteriaWeight Weight => new(20);

    public BonusCriteria BonusCriteria => new(techniqueCount: 35, precursorDigimon: DigimonType.Biyomon);
}