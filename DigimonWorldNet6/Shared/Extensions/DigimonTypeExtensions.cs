using Shared.Enums;

namespace Shared.Extensions;

public static class DigimonTypeExtensions
{
    public static EvolutionStage EvolutionStage(this DigimonName digimonType) => EvolutionStageMapper.Get(digimonType);
}