using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCalculation;

public sealed class EvolutionCalculator
{
    private readonly EvolutionCalculationMapper _evolutionCalculationMapper = new();
    
    public DigimonType CalculateEvolutionResult(Digimon digimon)
    {
        var evolutionCalculator = _evolutionCalculationMapper[digimon.DigimonType];
        var evolutionResult = evolutionCalculator.DetermineEvolutionResult(digimon);

        return evolutionResult;
    }
}