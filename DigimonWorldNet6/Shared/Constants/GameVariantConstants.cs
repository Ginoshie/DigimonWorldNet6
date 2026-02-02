using Shared.Enums;

namespace Shared.Constants;

public static class GameVariantConstants
{
    public const GameVariant ViceAll = GameVariant.Vice | GameVariant.MyotismonPatch | GameVariant.PanjyamonPatch;
    public const GameVariant All = GameVariant.Original | ViceAll;
}