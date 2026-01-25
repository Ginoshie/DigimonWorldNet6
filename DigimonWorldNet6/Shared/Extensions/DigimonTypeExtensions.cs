using Generics.Enums;

namespace Generics.Extensions;

public static class DigimonTypeExtensions
{
    public static EvolutionStage EvolutionStage(this DigimonName digimonType) => EvolutionStageMapper.Get(digimonType);
}