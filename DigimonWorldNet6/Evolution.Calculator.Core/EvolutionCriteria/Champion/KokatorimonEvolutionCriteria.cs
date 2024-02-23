using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Champion;

public sealed class KokatorimonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public EvolutionResult DigimonType => EvolutionResult.Kokatorimon;

    public MainCriteriaStats Stats => new(hp: 1000);

    public MainCriteriaCareMistakes CareMistakes => new(3, false);

    public MainCriteriaWeight Weight => new(30);

    public BonusCriteria BonusCriteria => new(techniqueCount: 28, precursorDigimon: EvolutionResult.Biyomon);
}