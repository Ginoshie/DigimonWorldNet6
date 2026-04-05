using System;
using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using NUnit.Framework;
using Shared.Enums;
using Shouldly;

namespace Evolution.Calculator.Tests.DataObjects;

[TestFixture]
public sealed class UserDigimonTests
{
    // --- Constructor tests ---

    [Test]
    public void Constructor_ShouldSetAllProperties_WhenCalledWithValidArguments()
    {
        // Arrange & Act
        UserDigimon sut = new(
            DigimonName.Agumon,
            hp: 1000, mp: 2000, off: 100, def: 200,
            speed: 300, brains: 400, careMistakes: 5,
            weight: 30, happiness: 80, discipline: 90,
            battles: 10, techniqueCount: 28);

        // Assert
        sut.DigimonName.ShouldBe(DigimonName.Agumon);
        sut.HP.ShouldBe(1000);
        sut.MP.ShouldBe(2000);
        sut.Off.ShouldBe(100);
        sut.Def.ShouldBe(200);
        sut.Speed.ShouldBe(300);
        sut.Brains.ShouldBe(400);
        sut.CareMistakes.ShouldBe(5);
        sut.Weight.ShouldBe(30);
        sut.Happiness.ShouldBe(80);
        sut.Discipline.ShouldBe(90);
        sut.Battles.ShouldBe(10);
        sut.TechniqueCount.ShouldBe(28);
    }

    // --- EvolutionStage derived from DigimonName ---

    [TestCase(DigimonName.Botamon, EvolutionStage.Fresh)]
    [TestCase(DigimonName.Koromon, EvolutionStage.InTraining)]
    [TestCase(DigimonName.Agumon, EvolutionStage.Rookie)]
    [TestCase(DigimonName.Greymon, EvolutionStage.Champion)]
    [TestCase(DigimonName.MetalGreymon, EvolutionStage.Ultimate)]
    public void Constructor_ShouldDeriveCorrectEvolutionStage_FromDigimonName(DigimonName digimonName, EvolutionStage expectedStage)
    {
        // Act
        UserDigimon sut = new(digimonName, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        // Assert
        sut.EvolutionStage.ShouldBe(expectedStage);
    }

    // --- Default constructor ---

    [Test]
    public void DefaultConstructor_ShouldCreateValidInstance()
    {
        // Act
        UserDigimon sut = new();

        // Assert
        sut.ShouldNotBeNull();
    }

    [Test]
    public void DefaultConstructor_ShouldSetIntPropertiesToZero()
    {
        // Act
        UserDigimon sut = new();

        // Assert
        sut.HP.ShouldBe(0);
        sut.MP.ShouldBe(0);
        sut.Off.ShouldBe(0);
        sut.Def.ShouldBe(0);
        sut.Speed.ShouldBe(0);
        sut.Brains.ShouldBe(0);
        sut.CareMistakes.ShouldBe(0);
        sut.Weight.ShouldBe(0);
        sut.Happiness.ShouldBe(0);
        sut.Discipline.ShouldBe(0);
        sut.Battles.ShouldBe(0);
        sut.TechniqueCount.ShouldBe(0);
    }

    [Test]
    public void DefaultConstructor_ShouldSetDigimonNameToDefaultEnumValue()
    {
        // The default value of DigimonName enum is Agumon (value 0)
        UserDigimon sut = new();

        sut.DigimonName.ShouldBe(DigimonName.Agumon);
    }

    // --- Constructor with all evolution stages ---

    [Test]
    public void Constructor_ShouldNotThrow_ForAnyDigimonName([Values] DigimonName digimonName)
    {
        // Act
        Action act = () =>
        {
            UserDigimon _ = new(digimonName, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        };

        // Assert
        act.ShouldNotThrow();
    }

    // --- Edge case: extreme stat values ---

    [Test]
    public void Constructor_ShouldAcceptMaxStats()
    {
        // Act
        UserDigimon sut = new(
            DigimonName.MetalGreymon,
            hp: 9999, mp: 9999, off: 999, def: 999,
            speed: 999, brains: 999, careMistakes: 99,
            weight: 99, happiness: 100, discipline: 100,
            battles: 999, techniqueCount: 49);

        // Assert
        sut.HP.ShouldBe(9999);
        sut.MP.ShouldBe(9999);
        sut.Off.ShouldBe(999);
        sut.TechniqueCount.ShouldBe(49);
    }

    [Test]
    public void Constructor_ShouldAcceptZeroForAllStats()
    {
        // Act
        UserDigimon sut = new(DigimonName.Agumon, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        // Assert
        sut.HP.ShouldBe(0);
        sut.MP.ShouldBe(0);
        sut.Off.ShouldBe(0);
        sut.Def.ShouldBe(0);
        sut.Speed.ShouldBe(0);
        sut.Brains.ShouldBe(0);
    }

    // --- Edge case: negative stats (no validation in UserDigimon, so they're accepted) ---

    [Test]
    public void Constructor_ShouldAcceptNegativeStats()
    {
        // UserDigimon has no validation — negative values are accepted
        UserDigimon sut = new(DigimonName.Agumon, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1);

        sut.HP.ShouldBe(-1);
        sut.CareMistakes.ShouldBe(-1);
    }

    // --- Properties are independently settable ---

    [Test]
    public void Properties_ShouldBeIndependentlySettable()
    {
        // Arrange
        UserDigimon sut = new();

        // Act
        sut.HP = 500;
        sut.MP = 600;
        sut.Off = 70;
        sut.Def = 80;
        sut.Speed = 90;
        sut.Brains = 100;

        // Assert
        sut.HP.ShouldBe(500);
        sut.MP.ShouldBe(600);
        sut.Off.ShouldBe(70);
        sut.Def.ShouldBe(80);
        sut.Speed.ShouldBe(90);
        sut.Brains.ShouldBe(100);
    }
}

