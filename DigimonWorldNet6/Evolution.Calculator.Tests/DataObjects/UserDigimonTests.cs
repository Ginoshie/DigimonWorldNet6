using System;
using Domain;
using NUnit.Framework;
using Shared.Enums;
using Shouldly;

namespace Evolution.Calculator.Tests.DataObjects;

[TestFixture]
public sealed class UserDigimonTests
{
    [Test]
    public void Instance_ShouldReturnSameReference()
    {
        UserDigimon first = UserDigimon.Instance;
        UserDigimon second = UserDigimon.Instance;

        first.ShouldBeSameAs(second);
    }

    [Test]
    public void Set_ShouldSetAllProperties()
    {
        // Act
        UserDigimon.Instance.Set(
            DigimonName.Agumon,
            hp: 1000, mp: 2000, off: 100, def: 200,
            speed: 300, brains: 400, careMistakes: 5,
            weight: 30, happiness: 80, discipline: 90,
            battles: 10, techniqueCount: 28);

        // Assert
        UserDigimon sut = UserDigimon.Instance;
        sut.DigimonName.ShouldBe(DigimonName.Agumon);
        sut.Hp.ShouldBe(1000);
        sut.Mp.ShouldBe(2000);
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
    public void Set_ShouldDeriveCorrectEvolutionStage_FromDigimonName(DigimonName digimonName, EvolutionStage expectedStage)
    {
        // Act
        UserDigimon.Instance.Set(digimonName, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        // Assert
        UserDigimon.Instance.EvolutionStage.ShouldBe(expectedStage);
    }

    // --- Set with all evolution stages ---

    [Test]
    public void Set_ShouldNotThrow_ForAnyDigimonName([Values] DigimonName digimonName)
    {
        // Arrange
        if (digimonName == DigimonName.None) return;
        
        // Act
        Action act = () => UserDigimon.Instance.Set(digimonName, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        // Assert
        act.ShouldNotThrow();
    }

    // --- Edge case: extreme stat values ---

    [Test]
    public void Set_ShouldAcceptMaxStats()
    {
        // Act
        UserDigimon.Instance.Set(
            DigimonName.MetalGreymon,
            hp: 9999, mp: 9999, off: 999, def: 999,
            speed: 999, brains: 999, careMistakes: 99,
            weight: 99, happiness: 100, discipline: 100,
            battles: 999, techniqueCount: 49);

        // Assert
        UserDigimon sut = UserDigimon.Instance;
        sut.Hp.ShouldBe(9999);
        sut.Mp.ShouldBe(9999);
        sut.Off.ShouldBe(999);
        sut.TechniqueCount.ShouldBe(49);
    }

    [Test]
    public void Set_ShouldAcceptZeroForAllStats()
    {
        // Act
        UserDigimon.Instance.Set(DigimonName.Agumon, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        // Assert
        UserDigimon sut = UserDigimon.Instance;
        sut.Hp.ShouldBe(0);
        sut.Mp.ShouldBe(0);
        sut.Off.ShouldBe(0);
        sut.Def.ShouldBe(0);
        sut.Speed.ShouldBe(0);
        sut.Brains.ShouldBe(0);
    }

    // --- Edge case: negative stats ---

    [Test]
    public void Set_ShouldAcceptNegativeStats()
    {
        UserDigimon.Instance.Set(DigimonName.Agumon, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1);

        UserDigimon.Instance.Hp.ShouldBe(-1);
        UserDigimon.Instance.CareMistakes.ShouldBe(-1);
    }
}
