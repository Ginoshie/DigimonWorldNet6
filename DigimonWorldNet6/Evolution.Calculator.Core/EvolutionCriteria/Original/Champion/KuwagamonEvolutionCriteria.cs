using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.Champion;

public sealed class KuwagamonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public EvolutionResult EvolutionResult => EvolutionResult.Kuwagamon;

    public MainCriteriaStats Stats => new(hp: 1000, mp: 1000, off: 100, speed: 100);

    public MainCriteriaCareMistakes CareMistakes => new(5);

    public MainCriteriaWeight Weight => new(30);

    public BonusCriteria BonusCriteria => new(techniqueCount: 28, precursorDigimon: DigimonName.Kuwagamon);
}