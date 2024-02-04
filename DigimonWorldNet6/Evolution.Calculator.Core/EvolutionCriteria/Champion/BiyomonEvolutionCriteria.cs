using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Champion;

public sealed class BiyomonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Rookie;

    public DigimonType DigimonType => DigimonType.Biyomon;

    public MainCriteriaStats Stats => new(mp: 10, def: 1, speed: 1);

    public MainCriteriaCareMistakes CareMistakes => new(0, false);

    public MainCriteriaWeight Weight => new(15);

    public BonusCriteria BonusCriteria => new(precursorDigimon: DigimonType.Tokomon);
}