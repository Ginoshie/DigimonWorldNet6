using Shared.Enums;

namespace Shared.Constants;

public readonly record struct DigimonType(DigimonName Digimon, GameVariant IncludeGameVariantFlags, GameVariant ExcludeGameVariantFlags = 0);