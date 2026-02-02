using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.Champion;

public sealed class KokatorimonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;

    public EvolutionResult EvolutionResult => EvolutionResult.Kokatorimon;

    public MainCriteriaStats Stats => new(hp: 1000);

    public MainCriteriaCareMistakes CareMistakes => new(3);

    public MainCriteriaWeight Weight => new(30);

    public BonusCriteria BonusCriteria => new(techniqueCount: 28, precursorDigimon: DigimonName.Biyomon);
}