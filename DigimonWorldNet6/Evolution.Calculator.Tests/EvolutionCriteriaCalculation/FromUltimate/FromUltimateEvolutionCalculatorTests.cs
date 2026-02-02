using System;
using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromUltimate;
using Evolution.Calculator.Tests.Builder;
using Shared.Enums;
using NUnit.Framework;
using Shouldly;

namespace Evolution.Calculator.Tests.EvolutionCriteriaCalculation.FromUltimate;

[TestFixture]
public sealed class FromUltimateEvolutionCalculatorTests
{
    [Test]
    [TestCase(DigimonName.Andromon, 9999, 9999, 999, 999, 999, 999, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonName.Digitamamon, 9999, 9999, 999, 999, 999, 999, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonName.Etemon, 9999, 9999, 999, 999, 999, 999, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonName.Andromon, 100, 100, 10, 10, 10, 10, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonName.Digitamamon, 100, 100, 10, 10, 10, 10, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonName.Etemon, 100, 100, 10, 10, 10, 10, 0, 30, 100, 100, 100, 58)]
    public void DetermineEvolutionResult_ShouldReturnExpectedDigimon(DigimonName digimonName, int hp, int mp, int off, int def, int speed, int brains, int careMistakes, int weight, int happiness,
        int discipline, int battles, int techniqueCount)
    {
        // Arrange
        FromUltimateEvolutionCalculator sut = new SetupBuilder()
            .Build();
        UserDigimon userDigimon = new DigimonBuilder()
            .WithDigimonType(digimonName)
            .WithHP(hp)
            .WithMP(mp)
            .WithOff(off)
            .WithDef(def)
            .WithSpeed(speed)
            .WithBrains(brains)
            .WithCareMistakes(careMistakes)
            .WithWeight(weight)
            .WithHappiness(happiness)
            .WithDiscipline(discipline)
            .WithBattles(battles)
            .WithTechniqueCount(techniqueCount)
            .Build();

        // Act
        EvolutionResult result = sut.DetermineEvolutionResult(userDigimon);

        // Assert
        result.ShouldBe(EvolutionResult.None);
    }
    
    [Test]
    [TestCase(DigimonName.Yuramon, 9999, 9999, 999, 999, 999, 999, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonName.Tsunomon, 9999, 9999, 999, 999, 999, 999, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonName.Agumon, 9999, 9999, 999, 999, 999, 999, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonName.Kabuterimon, 9999, 9999, 999, 999, 999, 999, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonName.Yuramon, 100, 100, 10, 10, 10, 10, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonName.Tsunomon, 100, 100, 10, 10, 10, 10, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonName.Agumon, 100, 100, 10, 10, 10, 10, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonName.Kabuterimon, 100, 100, 10, 10, 10, 10, 0, 30, 100, 100, 100, 58)]
    public void DetermineEvolutionResult_ShouldThrowException_WhenDigimonIsNotAnUltimate(DigimonName digimonName, int hp, int mp, int off, int def, int speed, int brains, int careMistakes, int weight, int happiness,
        int discipline, int battles, int techniqueCount)
    {
        // Arrange
        FromUltimateEvolutionCalculator sut = new SetupBuilder()
            .Build();
        UserDigimon userDigimon = new DigimonBuilder()
            .WithDigimonType(digimonName)
            .WithHP(hp)
            .WithMP(mp)
            .WithOff(off)
            .WithDef(def)
            .WithSpeed(speed)
            .WithBrains(brains)
            .WithCareMistakes(careMistakes)
            .WithWeight(weight)
            .WithHappiness(happiness)
            .WithDiscipline(discipline)
            .WithBattles(battles)
            .WithTechniqueCount(techniqueCount)
            .Build();

        // Act
        Action determineEvolutionResultThrowingException = () => sut.DetermineEvolutionResult(userDigimon);

        // Assert
        determineEvolutionResultThrowingException.ShouldThrow<Exception>();
    }

    private sealed class SetupBuilder
    {
        public FromUltimateEvolutionCalculator Build()
        {
            FromUltimateEvolutionCalculator sut = new();

            return sut;
        }
    }
}