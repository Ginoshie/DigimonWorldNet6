using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCalculation;

public sealed class EvolutionCalculator
{
    private readonly EvolutionCalculationMapper _evolutionCalculationMapper = new();
    
    public EvolutionResult CalculateEvolutionResult(Digimon digimon)
    {
        IEvolutionCalculator evolutionCalculator = _evolutionCalculationMapper[digimon.DigimonType];
        EvolutionResult evolutionResult = evolutionCalculator.DetermineEvolutionResult(digimon);

        return evolutionResult;
    }
}