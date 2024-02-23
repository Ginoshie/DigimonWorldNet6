using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Champion;

public sealed class ShellmonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public EvolutionResult DigimonType => EvolutionResult.Shellmon;

    public MainCriteriaStats Stats => new(hp: 1000, def: 100);

    public MainCriteriaCareMistakes CareMistakes => new(5, false);

    public MainCriteriaWeight Weight => new(40);

    public BonusCriteria BonusCriteria => new(techniqueCount: 35, precursorDigimon: EvolutionResult.Betamon);
}