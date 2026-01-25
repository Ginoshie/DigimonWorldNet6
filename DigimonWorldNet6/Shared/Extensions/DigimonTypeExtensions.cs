using Generics.Enums;

namespace Generics.Extensions;

public static class DigimonTypeExtensions
{
    private static readonly EvolutionStageMapper EvolutionStageMapper = new();
    
    public static EvolutionStage EvolutionStage(this DigimonName digimonType) => EvolutionStageMapper[digimonType];
}