using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.Champion;

public sealed class KabuterimonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public EvolutionResult DigimonType => EvolutionResult.Kabuterimon;

    public MainCriteriaStats Stats => new(hp: 1000, mp: 1000, off: 100, speed: 100);

    public MainCriteriaCareMistakes CareMistakes => new(1, true);

    public MainCriteriaWeight Weight => new(30);

    public BonusCriteria BonusCriteria => new(techniqueCount: 35, precursorDigimon: EvolutionResult.Kuwagamon);
}