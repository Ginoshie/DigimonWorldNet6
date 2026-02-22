using Shared.Enums;

namespace Shared.Constants;

public readonly record struct Digimon(int ByteValue, DigimonName DigimonName, GameVariant IncludeGameVariantFlags, GameVariant ExcludeGameVariantFlags = 0);