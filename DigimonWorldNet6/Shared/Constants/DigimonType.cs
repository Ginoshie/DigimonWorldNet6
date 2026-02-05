using Shared.Enums;

namespace Shared.Constants;

public readonly record struct DigimonType(int ByteValue, DigimonName Digimon, GameVariant IncludeGameVariantFlags, GameVariant ExcludeGameVariantFlags = 0);