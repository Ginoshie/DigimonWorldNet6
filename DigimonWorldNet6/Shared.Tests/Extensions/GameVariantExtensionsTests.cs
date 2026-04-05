using NUnit.Framework;
using Shared.Enums;
using Shared.Extensions;
using Shouldly;

namespace Shared.Tests.Extensions;

[TestFixture]
public sealed class GameVariantExtensionsTests
{
    // --- Original mode ---

    [Test]
    public void IsAvailableIn_ShouldReturnTrue_WhenIncludeFlagsHasOriginalAndCurrentIsOriginal()
    {
        GameVariant includeFlags = GameVariant.Original | GameVariant.Vice;
        GameVariant excludeFlags = 0;
        GameVariant current = GameVariant.Original;

        includeFlags.IsAvailableIn(excludeFlags, current).ShouldBeTrue();
    }

    [Test]
    public void IsAvailableIn_ShouldReturnFalse_WhenIncludeFlagsDoesNotHaveOriginalAndCurrentIsOriginal()
    {
        GameVariant includeFlags = GameVariant.Vice;
        GameVariant excludeFlags = 0;
        GameVariant current = GameVariant.Original;

        includeFlags.IsAvailableIn(excludeFlags, current).ShouldBeFalse();
    }

    // --- Vice mode (no patches) ---

    [Test]
    public void IsAvailableIn_ShouldReturnTrue_WhenIncludeFlagsHasViceAndCurrentIsVice()
    {
        GameVariant includeFlags = GameVariant.Vice;
        GameVariant excludeFlags = 0;
        GameVariant current = GameVariant.Vice;

        includeFlags.IsAvailableIn(excludeFlags, current).ShouldBeTrue();
    }

    [Test]
    public void IsAvailableIn_ShouldReturnFalse_WhenIncludeFlagsHasOriginalOnlyAndCurrentIsVice()
    {
        GameVariant includeFlags = GameVariant.Original;
        GameVariant excludeFlags = 0;
        GameVariant current = GameVariant.Vice;

        includeFlags.IsAvailableIn(excludeFlags, current).ShouldBeFalse();
    }

    // --- Vice mode with patch requirements ---

    [Test]
    public void IsAvailableIn_ShouldReturnTrue_WhenMyotismonPatchRequiredAndActive()
    {
        GameVariant includeFlags = GameVariant.Vice | GameVariant.MyotismonPatch;
        GameVariant excludeFlags = 0;
        GameVariant current = GameVariant.Vice | GameVariant.MyotismonPatch;

        includeFlags.IsAvailableIn(excludeFlags, current).ShouldBeTrue();
    }

    [Test]
    public void IsAvailableIn_ShouldReturnFalse_WhenMyotismonPatchRequiredButNotActive()
    {
        GameVariant includeFlags = GameVariant.Vice | GameVariant.MyotismonPatch;
        GameVariant excludeFlags = 0;
        GameVariant current = GameVariant.Vice;

        includeFlags.IsAvailableIn(excludeFlags, current).ShouldBeFalse();
    }

    [Test]
    public void IsAvailableIn_ShouldReturnTrue_WhenPanjyamonPatchRequiredAndActive()
    {
        GameVariant includeFlags = GameVariant.Vice | GameVariant.PanjyamonPatch;
        GameVariant excludeFlags = 0;
        GameVariant current = GameVariant.Vice | GameVariant.PanjyamonPatch;

        includeFlags.IsAvailableIn(excludeFlags, current).ShouldBeTrue();
    }

    [Test]
    public void IsAvailableIn_ShouldReturnFalse_WhenPanjyamonPatchRequiredButNotActive()
    {
        GameVariant includeFlags = GameVariant.Vice | GameVariant.PanjyamonPatch;
        GameVariant excludeFlags = 0;
        GameVariant current = GameVariant.Vice;

        includeFlags.IsAvailableIn(excludeFlags, current).ShouldBeFalse();
    }

    [Test]
    public void IsAvailableIn_ShouldReturnTrue_WhenBothPatchesRequiredAndBothActive()
    {
        GameVariant includeFlags = GameVariant.Vice | GameVariant.MyotismonPatch | GameVariant.PanjyamonPatch;
        GameVariant excludeFlags = 0;
        GameVariant current = GameVariant.Vice | GameVariant.MyotismonPatch | GameVariant.PanjyamonPatch;

        includeFlags.IsAvailableIn(excludeFlags, current).ShouldBeTrue();
    }

    [Test]
    public void IsAvailableIn_ShouldReturnFalse_WhenBothPatchesRequiredButOnlyOneActive()
    {
        GameVariant includeFlags = GameVariant.Vice | GameVariant.MyotismonPatch | GameVariant.PanjyamonPatch;
        GameVariant excludeFlags = 0;
        GameVariant current = GameVariant.Vice | GameVariant.MyotismonPatch;

        includeFlags.IsAvailableIn(excludeFlags, current).ShouldBeFalse();
    }

    // Extra active patches are allowed when no specific patch is required
    [Test]
    public void IsAvailableIn_ShouldReturnTrue_WhenNoPatchRequiredButPatchIsActive()
    {
        GameVariant includeFlags = GameVariant.Vice;
        GameVariant excludeFlags = 0;
        GameVariant current = GameVariant.Vice | GameVariant.MyotismonPatch;

        includeFlags.IsAvailableIn(excludeFlags, current).ShouldBeTrue();
    }

    // --- Exclude flags ---

    [Test]
    public void IsAvailableIn_ShouldReturnFalse_WhenExcludeFlagMatchesCurrentVariant()
    {
        GameVariant includeFlags = GameVariant.Original | GameVariant.Vice;
        GameVariant excludeFlags = GameVariant.MyotismonPatch;
        GameVariant current = GameVariant.Vice | GameVariant.MyotismonPatch;

        includeFlags.IsAvailableIn(excludeFlags, current).ShouldBeFalse();
    }

    [Test]
    public void IsAvailableIn_ShouldReturnTrue_WhenExcludeFlagDoesNotMatchCurrentVariant()
    {
        GameVariant includeFlags = GameVariant.Original | GameVariant.Vice;
        GameVariant excludeFlags = GameVariant.MyotismonPatch;
        GameVariant current = GameVariant.Vice;

        includeFlags.IsAvailableIn(excludeFlags, current).ShouldBeTrue();
    }

    [Test]
    public void IsAvailableIn_ShouldReturnFalse_WhenExcludeFlagIsOriginalAndCurrentIsOriginal()
    {
        // Exclude always wins, even for Original
        GameVariant includeFlags = GameVariant.Original;
        GameVariant excludeFlags = GameVariant.Original;
        GameVariant current = GameVariant.Original;

        includeFlags.IsAvailableIn(excludeFlags, current).ShouldBeFalse();
    }

    // --- Edge cases ---

    [Test]
    public void IsAvailableIn_ShouldReturnFalse_WhenCurrentVariantHasNeitherOriginalNorVice()
    {
        // A currentVariant that is just a patch flag without Original or Vice
        GameVariant includeFlags = GameVariant.Vice;
        GameVariant excludeFlags = 0;
        GameVariant current = GameVariant.MyotismonPatch;

        includeFlags.IsAvailableIn(excludeFlags, current).ShouldBeFalse();
    }

    [Test]
    public void IsAvailableIn_ShouldReturnFalse_WhenCurrentVariantIsZero()
    {
        GameVariant includeFlags = GameVariant.Original;
        GameVariant excludeFlags = 0;
        GameVariant current = 0;

        includeFlags.IsAvailableIn(excludeFlags, current).ShouldBeFalse();
    }

    // Real-world scenario: Vegiemon is Original-only, excluded from Vice
    [Test]
    public void IsAvailableIn_ShouldReturnTrue_ForVegiemonInOriginal()
    {
        GameVariant includeFlags = GameVariant.Original;
        GameVariant excludeFlags = GameVariant.Vice;
        GameVariant current = GameVariant.Original;

        includeFlags.IsAvailableIn(excludeFlags, current).ShouldBeTrue();
    }

    [Test]
    public void IsAvailableIn_ShouldReturnFalse_ForVegiemonInVice()
    {
        GameVariant includeFlags = GameVariant.Original;
        GameVariant excludeFlags = GameVariant.Vice;
        GameVariant current = GameVariant.Vice;

        includeFlags.IsAvailableIn(excludeFlags, current).ShouldBeFalse();
    }

    // Real-world scenario: Weregarurumon is Vice-only, excluded from Original and PanjyamonPatch
    [Test]
    public void IsAvailableIn_ShouldReturnTrue_ForWeregarurumonInVice()
    {
        GameVariant includeFlags = GameVariant.Vice;
        GameVariant excludeFlags = GameVariant.Original | GameVariant.PanjyamonPatch;
        GameVariant current = GameVariant.Vice;

        includeFlags.IsAvailableIn(excludeFlags, current).ShouldBeTrue();
    }

    [Test]
    public void IsAvailableIn_ShouldReturnFalse_ForWeregarurumonInViceWithPanjyamonPatch()
    {
        GameVariant includeFlags = GameVariant.Vice;
        GameVariant excludeFlags = GameVariant.Original | GameVariant.PanjyamonPatch;
        GameVariant current = GameVariant.Vice | GameVariant.PanjyamonPatch;

        includeFlags.IsAvailableIn(excludeFlags, current).ShouldBeFalse();
    }
}
