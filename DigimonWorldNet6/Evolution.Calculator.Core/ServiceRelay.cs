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

    public static EvolutionResult CalculateEvolutionResult(EvolutionCalculationInput evolutionCalculationInput) => _evolutionCalculator.CalculateEvolutionResult(evolutionCalculationInput);

    public static LiveMemoryReader LiveMemoryReader { get; }
}