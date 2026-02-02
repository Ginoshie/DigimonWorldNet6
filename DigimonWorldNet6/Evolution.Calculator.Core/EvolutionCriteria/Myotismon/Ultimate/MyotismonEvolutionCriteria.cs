using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Myotismon.Ultimate;

public sealed class MyotismonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Ultimate;

    public EvolutionResult EvolutionResult => EvolutionResult.Myotismon;

    public MainCriteriaStats Stats => new(mp: 5000, off: 400, speed: 400, brains: 600);

    public MainCriteriaCareMistakes CareMistakes => new(3, true);

    public MainCriteriaWeight Weight => new(30);

    public BonusCriteria BonusCriteria => new(discipline: 100, techniqueCount: 49, precursorDigimon: DigimonName.Devimon);
}