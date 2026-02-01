using Generics.Enums;

namespace Generics.Constants;

public readonly record struct DigimonType(DigimonName Digimon, GameVariant IncludeGameVariantFlags, GameVariant ExcludeGameVariantFlags = 0);