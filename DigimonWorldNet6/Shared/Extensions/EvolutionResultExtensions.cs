using Shared.Enums;

namespace Shared.Extensions;

public static class EvolutionResultExtensions
{
    public static DigimonName ToDigimonType(this EvolutionResult evolutionResult)
    {
        if (Enum.TryParse(evolutionResult.ToString(), out DigimonName digimonType))
        {
            return digimonType;
        }

        throw new ArgumentException($"No matching DigimonType found for EvolutionResult: {evolutionResult}", nameof(evolutionResult));
    }
}