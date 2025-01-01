using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCalculation;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core;

public static class ServiceRelay
{
    private static readonly EvolutionCalculator EvolutionCalculator;

    static ServiceRelay()
    {
        EvolutionCalculator = new EvolutionCalculator();
    }

    public static EvolutionResult CalculateEvolutionResult(Digimon digimon) => EvolutionCalculator.CalculateEvolutionResult(digimon);
}