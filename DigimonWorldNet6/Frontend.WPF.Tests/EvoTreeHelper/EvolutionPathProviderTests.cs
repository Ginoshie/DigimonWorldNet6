using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvoTreeHelper;
using NUnit.Framework;
using Shared.Enums;
using Shouldly;

namespace Frontend.WPF.Tests.EvoTreeHelper;

[TestFixture]
public sealed class EvolutionPathProviderTests
{
    [Test]
    [TestCase(DigimonName.Botamon, new[] { DigimonName.Koromon })]
    [TestCase(DigimonName.Poyomon, new[] { DigimonName.Tokomon })]
    [TestCase(DigimonName.Punimon, new[] { DigimonName.Tsunomon })]
    [TestCase(DigimonName.Yuramon, new[] { DigimonName.Tanemon })]
    public void GetEvolutions_ShouldReturnCorrectInTraining_ForFreshDigimon(DigimonName fresh, DigimonName[] expected)
    {
        // Act
        DigimonName[] result = EvolutionPathProvider.GetEvolutions(fresh);

        // Assert
        result.ShouldBe(expected);
    }

    [Test]
    [TestCase(DigimonName.Koromon)]
    [TestCase(DigimonName.Tokomon)]
    [TestCase(DigimonName.Tsunomon)]
    [TestCase(DigimonName.Tanemon)]
    public void GetEvolutions_ShouldReturnTwoRookies_ForInTrainingDigimon(DigimonName inTraining)
    {
        // Act
        DigimonName[] result = EvolutionPathProvider.GetEvolutions(inTraining);

        // Assert
        result.Length.ShouldBe(2);
    }

    [Test]
    public void GetEvolutions_ShouldReturnCorrectChampions_ForKunemon()
    {
        // Act
        DigimonName[] result = EvolutionPathProvider.GetEvolutions(DigimonName.Kunemon);

        // Assert
        result.ShouldBe(new[] { DigimonName.Bakemon, DigimonName.Kabuterimon, DigimonName.Kuwagamon, DigimonName.Vegiemon });
    }

    [Test]
    public void GetEvolutions_ShouldReturnEmptyArray_ForDigimonWithNoEvolutions()
    {
        // Act - Ultimate digimon have no forward evolutions in the tree
        DigimonName[] result = EvolutionPathProvider.GetEvolutions(DigimonName.MetalGreymon);

        // Assert
        result.ShouldBeEmpty();
    }

    [Test]
    public void GetPrecursors_ShouldReturnEmptyArray_ForFreshDigimon()
    {
        // Act
        DigimonName[] result = EvolutionPathProvider.GetPrecursors(DigimonName.Botamon);

        // Assert
        result.ShouldBeEmpty();
    }

    [Test]
    public void GetPrecursors_ShouldReturnCorrectFresh_ForInTrainingDigimon()
    {
        // Act
        DigimonName[] result = EvolutionPathProvider.GetPrecursors(DigimonName.Koromon);

        // Assert
        result.ShouldContain(DigimonName.Botamon);
    }

    [Test]
    public void GetPrecursors_ShouldReturnMultipleRookies_ForChampionDigimon()
    {
        // Act - Birdramon can come from Agumon and Biyomon
        DigimonName[] result = EvolutionPathProvider.GetPrecursors(DigimonName.Birdramon);

        // Assert
        result.ShouldContain(DigimonName.Agumon);
        result.ShouldContain(DigimonName.Biyomon);
    }

    [Test]
    public void GetEvolutions_ShouldReturnNonEmpty_ForAllRookies()
    {
        // All rookies should have champion evolutions
        DigimonName[] rookies = [DigimonName.Agumon, DigimonName.Betamon, DigimonName.Biyomon, DigimonName.Elecmon, DigimonName.Gabumon, DigimonName.Kunemon, DigimonName.Palmon, DigimonName.Patamon, DigimonName.Penguinmon];

        foreach (DigimonName rookie in rookies)
        {
            DigimonName[] evolutions = EvolutionPathProvider.GetEvolutions(rookie);
            evolutions.ShouldNotBeEmpty($"{rookie} should have champion evolutions");
        }
    }

    [Test]
    public void GetPrecursors_KunemonShouldHaveNoPrecursors()
    {
        // Kunemon is a special evolution - no InTraining evolves into it
        DigimonName[] result = EvolutionPathProvider.GetPrecursors(DigimonName.Kunemon);

        // Assert - Kunemon has no precursors in the tree since no InTraining maps to it
        result.ShouldBeEmpty();
    }
}
