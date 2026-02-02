using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCalculation;
using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core;

public static class ServiceRelay
{
    private static readonly EvolutionCalculator EvolutionCalculator;

    static ServiceRelay()
    {
        EvolutionCalculator = new EvolutionCalculator();
    }

    public static EvolutionResult CalculateEvolutionResult(UserDigimon userDigimon) => EvolutionCalculator.CalculateEvolutionResult(userDigimon);
}