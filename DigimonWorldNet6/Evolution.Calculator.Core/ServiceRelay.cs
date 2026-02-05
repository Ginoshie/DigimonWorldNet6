using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCalculation;
using MemoryAccess;
using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core;

public static class ServiceRelay
{
    private static readonly EvolutionCalculator _evolutionCalculator;

    static ServiceRelay()
    {
        _evolutionCalculator = EvolutionCalculator.Instance;

        LiveMemoryReader = LiveMemoryReader.Instance;
    }

    public static EvolutionResult CalculateEvolutionResult(UserDigimon userDigimon) => _evolutionCalculator.CalculateEvolutionResult(userDigimon);

    public static LiveMemoryReader LiveMemoryReader { get; }
}