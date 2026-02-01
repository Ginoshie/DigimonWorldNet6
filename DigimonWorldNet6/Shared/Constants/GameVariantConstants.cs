using Generics.Enums;

namespace Generics.Constants;

public static class GameVariantConstants
{
    public const GameVariant ViceAll = GameVariant.Vice | GameVariant.MyotismonPatch | GameVariant.PanjyamonPatch;
    public const GameVariant All = GameVariant.Original | ViceAll;
}