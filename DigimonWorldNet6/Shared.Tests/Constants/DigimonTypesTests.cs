using NUnit.Framework;
using Shared.Constants;
using Shared.Enums;

namespace Shared.Tests.Constants;

public class DigimonTypesTests
{
    #region DigimonNameCollections

    private static readonly DigimonName[] AllOriginalDigimonNames =
    [
        DigimonName.Agumon,
        DigimonName.Airdramon,
        DigimonName.Andromon,
        DigimonName.Angemon,
        DigimonName.Bakemon,
        DigimonName.Betamon,
        DigimonName.Birdramon,
        DigimonName.Biyomon,
        DigimonName.Botamon,
        DigimonName.Centarumon,
        DigimonName.Coelamon,
        DigimonName.Devimon,
        DigimonName.Digitamamon,
        DigimonName.Drimogemon,
        DigimonName.Elecmon,
        DigimonName.Etemon,
        DigimonName.Frigimon,
        DigimonName.Gabumon,
        DigimonName.Garurumon,
        DigimonName.Giromon,
        DigimonName.Greymon,
        DigimonName.HerculesKabuterimon,
        DigimonName.Kabuterimon,
        DigimonName.Kokatorimon,
        DigimonName.Koromon,
        DigimonName.Kunemon,
        DigimonName.Kuwagamon,
        DigimonName.Leomon,
        DigimonName.Mamemon,
        DigimonName.Megadramon,
        DigimonName.MegaSeadramon,
        DigimonName.Meramon,
        DigimonName.MetalGreymon,
        DigimonName.MetalMamemon,
        DigimonName.Mojyamon,
        DigimonName.Monochromon,
        DigimonName.Monzaemon,
        DigimonName.Nanimon,
        DigimonName.Ninjamon,
        DigimonName.Numemon,
        DigimonName.Ogremon,
        DigimonName.Palmon,
        DigimonName.Patamon,
        DigimonName.Penguinmon,
        DigimonName.Phoenixmon,
        DigimonName.Piximon,
        DigimonName.Poyomon,
        DigimonName.Punimon,
        DigimonName.Seadramon,
        DigimonName.Shellmon,
        DigimonName.SkullGreymon,
        DigimonName.Sukamon,
        DigimonName.Tanemon,
        DigimonName.Tokomon,
        DigimonName.Tsunomon,
        DigimonName.Tyrannomon,
        DigimonName.Unimon,
        DigimonName.Vademon,
        DigimonName.Vegiemon,
        DigimonName.Whamon,
        DigimonName.Yuramon
    ];

    private static readonly DigimonName[] AllViceDigimonNames = AllOriginalDigimonNames
        .Concat([
            DigimonName.Weregarurumon,
            DigimonName.Gigadramon,
            DigimonName.MetalEtemon,
            DigimonName.Machinedramon
        ])
        .ToArray();

    private static readonly DigimonName[] AllMyotismonPatchDigimonNames = AllOriginalDigimonNames
        .Concat([
            DigimonName.Weregarurumon,
            DigimonName.Gigadramon,
            DigimonName.MetalEtemon,
            DigimonName.Myotismon
        ])
        .ToArray();

    private static readonly DigimonName[] AllPanjyamonPatchDigimonNames = AllOriginalDigimonNames
        .Concat([
            DigimonName.Gigadramon,
            DigimonName.MetalEtemon,
            DigimonName.Machinedramon,
            DigimonName.Panjyamon
        ])
        .ToArray();

    private static readonly DigimonName[] AllMyotismonAndPanjyamonPatchDigimonNames = AllOriginalDigimonNames
        .Concat([
            DigimonName.Gigadramon,
            DigimonName.MetalEtemon,
            DigimonName.Myotismon,
            DigimonName.Panjyamon
        ])
        .ToArray();

    #endregion

    [Test]
    public void Get_ShouldReturnValidCollectionOfDigimonTypes_WhenGameVariantIsOriginal()
    {
        // Act
        IEnumerable<DigimonName> result = DigimonTypes.Get(GameVariant.Original);

        // Assert
        Assert.That(result, Is.EquivalentTo(AllOriginalDigimonNames));
    }

    [Test]
    public void Get_ShouldReturnValidCollectionOfDigimonTypes_WhenGameVariantIsVice()
    {
        // Act
        IEnumerable<DigimonName> result = DigimonTypes.Get(GameVariant.Vice);

        // Assert
        Assert.That(result, Is.EquivalentTo(AllViceDigimonNames));
    }

    [Test]
    public void Get_ShouldReturnValidCollectionOfDigimonTypes_WhenGameVariantIsViceMyotismon()
    {
        // Act
        IEnumerable<DigimonName> result = DigimonTypes.Get(GameVariant.Vice | GameVariant.MyotismonPatch);

        // Assert
        Assert.That(result, Is.EquivalentTo(AllMyotismonPatchDigimonNames));
    }

    [Test]
    public void Get_ShouldReturnValidCollectionOfDigimonTypes_WhenGameVariantIsVicePanjyamon()
    {
        // Act
        IEnumerable<DigimonName> result = DigimonTypes.Get(GameVariant.Vice | GameVariant.PanjyamonPatch);

        // Assert
        Assert.That(result, Is.EquivalentTo(AllPanjyamonPatchDigimonNames));
    }

    [Test]
    public void Get_ShouldReturnValidCollectionOfDigimonTypes_WhenGameVariantIsViceMyotismonPanjyamon()
    {
        // Act
        IEnumerable<DigimonName> result = DigimonTypes.Get(GameVariant.Vice | GameVariant.MyotismonPatch | GameVariant.PanjyamonPatch);

        // Assert
        Assert.That(result, Is.EquivalentTo(AllMyotismonAndPanjyamonPatchDigimonNames));
    }
}