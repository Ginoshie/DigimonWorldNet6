using Generics.Enums;

namespace Generics.Extensions;

public static class EvolutionResultExtensions
{
    public static DigimonType ToDigimonType(this EvolutionResult evolutionResult)
    {
        if (Enum.TryParse(evolutionResult.ToString(), out DigimonType digimonType))
        {
            return digimonType;
        }

        throw new ArgumentException($"No matching DigimonType found for EvolutionResult: {evolutionResult}", nameof(evolutionResult));
    }
}