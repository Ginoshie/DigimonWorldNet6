using Shared.Enums;

namespace Shared.Extensions;

public static class GameVariantExtensions
{
    private const GameVariant PATCH_FLAGS = GameVariant.MyotismonPatch | GameVariant.PanjyamonPatch;

    public static bool IsAvailableIn(this GameVariant includeGameVariantFlags, GameVariant excludeGameVariantFlags, GameVariant currentVariant)
    {
        // Exclude always wins (any excluded flag present => not available)
        if ((currentVariant & excludeGameVariantFlags) != 0)
        {
            return false;
        }

        // ORIGINAL mode
        if (currentVariant.HasFlag(GameVariant.Original))
        {
            return includeGameVariantFlags.HasFlag(GameVariant.Original);
        }

        // VICE mode (with or without patches)
        if (!currentVariant.HasFlag(GameVariant.Vice) || 
            !includeGameVariantFlags.HasFlag(GameVariant.Vice))
        {
            return false;
        }

        // Patch requirements: only enforce patches that the digimon explicitly requires
        GameVariant requiredPatches = includeGameVariantFlags & PATCH_FLAGS;
        GameVariant activePatches = currentVariant & PATCH_FLAGS;

        // Must have all required patches; extra active patches are allowed.
        return (activePatches & requiredPatches) == requiredPatches;
    }
}