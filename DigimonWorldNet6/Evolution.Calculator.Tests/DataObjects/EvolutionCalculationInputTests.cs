using DigimonWorld.Evolution.Calculator.Core;
using Domain;
using NUnit.Framework;
using Shared.Enums;
using Shouldly;

namespace Evolution.Calculator.Tests.DataObjects;

[TestFixture]
public sealed class EvolutionCalculationInputTests
{
    // ── Raw constructor ───────────────────────────────────────────────────────

    [Test]
    public void Constructor_SetsAllPropertiesCorrectly()
    {
        EvolutionCalculationInput input = new(
            DigimonName.Agumon, hp: 1000, mp: 2000, off: 100, def: 200,
            speed: 300, brains: 400, careMistakes: 5, weight: 30,
            happiness: 80, discipline: 90, battles: 10, techniqueCount: 28);

        input.DigimonName.ShouldBe(DigimonName.Agumon);
        input.HP.ShouldBe(1000);
        input.MP.ShouldBe(2000);
        input.Off.ShouldBe(100);
        input.Def.ShouldBe(200);
        input.Speed.ShouldBe(300);
        input.Brains.ShouldBe(400);
        input.CareMistakes.ShouldBe(5);
        input.Weight.ShouldBe(30);
        input.Happiness.ShouldBe(80);
        input.Discipline.ShouldBe(90);
        input.Battles.ShouldBe(10);
        input.TechniqueCount.ShouldBe(28);
    }

    [TestCase(DigimonName.Botamon, EvolutionStage.Fresh)]
    [TestCase(DigimonName.Koromon, EvolutionStage.InTraining)]
    [TestCase(DigimonName.Agumon, EvolutionStage.Rookie)]
    [TestCase(DigimonName.Greymon, EvolutionStage.Champion)]
    [TestCase(DigimonName.MetalGreymon, EvolutionStage.Ultimate)]
    public void Constructor_DerivedEvolutionStage_MatchesDigimonName(DigimonName digimonName, EvolutionStage expected)
    {
        EvolutionCalculationInput input = new(digimonName, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        input.EvolutionStage.ShouldBe(expected);
    }

    // ── UserDigimon constructor ───────────────────────────────────────────────

    [Test]
    public void UserDigimonConstructor_MapsAllFieldsFromSingleton()
    {
        UserDigimon.Instance.Set(
            DigimonName.Gabumon, hp: 500, mp: 600, off: 70, def: 80,
            speed: 90, brains: 110, careMistakes: 3, weight: 25,
            happiness: 75, discipline: 60, battles: 15, techniqueCount: 7);

        EvolutionCalculationInput input = new(UserDigimon.Instance);

        input.DigimonName.ShouldBe(DigimonName.Gabumon);
        input.HP.ShouldBe(500);
        input.MP.ShouldBe(600);
        input.Off.ShouldBe(70);
        input.Def.ShouldBe(80);
        input.Speed.ShouldBe(90);
        input.Brains.ShouldBe(110);
        input.CareMistakes.ShouldBe(3);
        input.Weight.ShouldBe(25);
        input.Happiness.ShouldBe(75);
        input.Discipline.ShouldBe(60);
        input.Battles.ShouldBe(15);
        input.TechniqueCount.ShouldBe(7);
    }

    [Test]
    public void UserDigimonConstructor_DerivedEvolutionStage_MatchesDigimonName()
    {
        UserDigimon.Instance.Set(DigimonName.Greymon, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        EvolutionCalculationInput input = new(UserDigimon.Instance);

        input.EvolutionStage.ShouldBe(EvolutionStage.Champion);
    }

    [Test]
    public void UserDigimonConstructor_IsIndependentFromSingleton_AfterConstruction()
    {
        UserDigimon.Instance.Set(DigimonName.Agumon, 1000, 0, 0, 0, 0, 0, 0, 20, 0, 0, 0, 0);
        EvolutionCalculationInput input = new(UserDigimon.Instance);

        // Mutate the singleton after capturing
        UserDigimon.Instance.Set(DigimonName.Greymon, 9999, 0, 0, 0, 0, 0, 0, 20, 0, 0, 0, 0);

        // input should still hold the original snapshot
        input.HP.ShouldBe(1000);
        input.DigimonName.ShouldBe(DigimonName.Agumon);
    }
}
