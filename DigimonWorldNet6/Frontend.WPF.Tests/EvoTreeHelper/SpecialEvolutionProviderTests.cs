using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvoTreeHelper.Models;
using NUnit.Framework;
using Shared.Enums;
using Shouldly;

namespace Frontend.WPF.Tests.EvoTreeHelper;

[TestFixture]
public sealed class SpecialEvolutionProviderTests
{
    [Test]
    [TestCase(DigimonName.Koromon)]
    [TestCase(DigimonName.Tokomon)]
    [TestCase(DigimonName.Tsunomon)]
    [TestCase(DigimonName.Tanemon)]
    public void GetAvailableSpecialEvolutions_ShouldIncludeKunemonAndSukamon_ForInTrainingDigimon(DigimonName digimon)
    {
        // Act
        List<SpecialEvolutionInfo> result = SpecialEvolutionProvider.GetAvailableSpecialEvolutions(digimon);

        // Assert
        result.ShouldContain(s => s.Target == DigimonName.Kunemon);
        result.ShouldContain(s => s.Target == DigimonName.Sukamon);
    }

    [Test]
    [TestCase(DigimonName.Agumon)]
    [TestCase(DigimonName.Gabumon)]
    [TestCase(DigimonName.Betamon)]
    public void GetAvailableSpecialEvolutions_ShouldIncludeNumemonNanimonBakemonSukamon_ForRookieDigimon(DigimonName digimon)
    {
        // Act
        List<SpecialEvolutionInfo> result = SpecialEvolutionProvider.GetAvailableSpecialEvolutions(digimon);

        // Assert
        result.ShouldContain(s => s.Target == DigimonName.Sukamon);
        result.ShouldContain(s => s.Target == DigimonName.Numemon);
        result.ShouldContain(s => s.Target == DigimonName.Nanimon);
        result.ShouldContain(s => s.Target == DigimonName.Bakemon);
    }

    [Test]
    public void GetAvailableSpecialEvolutions_ShouldNotIncludeBakemon_ForPenguinmon()
    {
        // Act
        List<SpecialEvolutionInfo> result = SpecialEvolutionProvider.GetAvailableSpecialEvolutions(DigimonName.Penguinmon);

        // Assert
        result.ShouldNotContain(s => s.Target == DigimonName.Bakemon);
        result.ShouldContain(s => s.Target == DigimonName.Numemon);
    }

    [Test]
    [TestCase(DigimonName.Greymon)]
    [TestCase(DigimonName.Garurumon)]
    public void GetAvailableSpecialEvolutions_ShouldIncludeSukamonAndVademon_ForChampionDigimon(DigimonName digimon)
    {
        // Act
        List<SpecialEvolutionInfo> result = SpecialEvolutionProvider.GetAvailableSpecialEvolutions(digimon);

        // Assert
        result.ShouldContain(s => s.Target == DigimonName.Sukamon);
        result.ShouldContain(s => s.Target == DigimonName.Vademon);
    }

    [Test]
    public void GetAvailableSpecialEvolutions_ShouldIncludeDevimon_ForAngemon()
    {
        // Act
        List<SpecialEvolutionInfo> result = SpecialEvolutionProvider.GetAvailableSpecialEvolutions(DigimonName.Angemon);

        // Assert
        result.ShouldContain(s => s.Target == DigimonName.Devimon);
    }

    [Test]
    public void GetAvailableSpecialEvolutions_ShouldIncludeAirdramon_ForSeadramon()
    {
        // Act
        List<SpecialEvolutionInfo> result = SpecialEvolutionProvider.GetAvailableSpecialEvolutions(DigimonName.Seadramon);

        // Assert
        result.ShouldContain(s => s.Target == DigimonName.Airdramon);
    }

    [Test]
    public void GetAvailableSpecialEvolutions_ShouldIncludeAirdramon_ForBirdramon()
    {
        // Act
        List<SpecialEvolutionInfo> result = SpecialEvolutionProvider.GetAvailableSpecialEvolutions(DigimonName.Birdramon);

        // Assert
        result.ShouldContain(s => s.Target == DigimonName.Airdramon);
    }

    [Test]
    public void GetAvailableSpecialEvolutions_ShouldIncludeCoelamon_ForWhamon()
    {
        // Act
        List<SpecialEvolutionInfo> result = SpecialEvolutionProvider.GetAvailableSpecialEvolutions(DigimonName.Whamon);

        // Assert
        result.ShouldContain(s => s.Target == DigimonName.Coelamon);
    }

    [Test]
    public void GetAvailableSpecialEvolutions_ShouldIncludeCoelamon_ForShellmon()
    {
        // Act
        List<SpecialEvolutionInfo> result = SpecialEvolutionProvider.GetAvailableSpecialEvolutions(DigimonName.Shellmon);

        // Assert
        result.ShouldContain(s => s.Target == DigimonName.Coelamon);
    }

    [Test]
    public void GetAvailableSpecialEvolutions_ShouldIncludeNinjamon_ForVegiemon()
    {
        // Act
        List<SpecialEvolutionInfo> result = SpecialEvolutionProvider.GetAvailableSpecialEvolutions(DigimonName.Vegiemon);

        // Assert
        result.ShouldContain(s => s.Target == DigimonName.Ninjamon);
    }

    [Test]
    public void GetAvailableSpecialEvolutions_ShouldIncludeMonochromon_ForDrimogemon()
    {
        // Act
        List<SpecialEvolutionInfo> result = SpecialEvolutionProvider.GetAvailableSpecialEvolutions(DigimonName.Drimogemon);

        // Assert
        result.ShouldContain(s => s.Target == DigimonName.Monochromon);
    }

    [Test]
    public void GetAvailableSpecialEvolutions_ShouldIncludePhoenixmon_ForKokatorimon()
    {
        // Act
        List<SpecialEvolutionInfo> result = SpecialEvolutionProvider.GetAvailableSpecialEvolutions(DigimonName.Kokatorimon);

        // Assert
        result.ShouldContain(s => s.Target == DigimonName.Phoenixmon);
    }

    [Test]
    public void GetAvailableSpecialEvolutions_ShouldIncludeSkullGreymon_ForMetalGreymon()
    {
        // Act
        List<SpecialEvolutionInfo> result = SpecialEvolutionProvider.GetAvailableSpecialEvolutions(DigimonName.MetalGreymon);

        // Assert
        result.ShouldContain(s => s.Target == DigimonName.SkullGreymon);
    }

    [Test]
    public void GetAvailableSpecialEvolutions_ShouldIncludeSkullGreymon_ForMegadramon()
    {
        // Act
        List<SpecialEvolutionInfo> result = SpecialEvolutionProvider.GetAvailableSpecialEvolutions(DigimonName.Megadramon);

        // Assert
        result.ShouldContain(s => s.Target == DigimonName.SkullGreymon);
    }

    [Test]
    public void GetAvailableSpecialEvolutions_ShouldIncludeMetalMamemonAndGiromon_ForMamemon()
    {
        // Act
        List<SpecialEvolutionInfo> result = SpecialEvolutionProvider.GetAvailableSpecialEvolutions(DigimonName.Mamemon);

        // Assert
        result.ShouldContain(s => s.Target == DigimonName.MetalMamemon);
        result.ShouldContain(s => s.Target == DigimonName.Giromon);
    }

    [Test]
    public void GetAvailableSpecialEvolutions_ShouldReturnOnlySukamon_ForUltimateWithNoSpecific()
    {
        // Andromon has no specific special evolution, only Sukamon (any digimon)
        List<SpecialEvolutionInfo> result = SpecialEvolutionProvider.GetAvailableSpecialEvolutions(DigimonName.Andromon);

        // Assert
        result.ShouldContain(s => s.Target == DigimonName.Sukamon);
        result.Count.ShouldBe(1);
    }

    [Test]
    public void GetAvailableSpecialEvolutions_ShouldHaveDescriptions_ForAllEntries()
    {
        // Act
        List<SpecialEvolutionInfo> result = SpecialEvolutionProvider.GetAvailableSpecialEvolutions(DigimonName.Koromon);

        // Assert
        result.ShouldAllBe(s => !string.IsNullOrWhiteSpace(s.Description));
    }
}
