using System;
using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromUltimate;
using Evolution.Calculator.Tests.Builder;
using Generics.Enums;
using NUnit.Framework;
using Shouldly;

namespace Evolution.Calculator.Tests.EvolutionCriteriaCalculation.FromUltimate;

[TestFixture]
public sealed class FromUltimateEvolutionCalculatorTests
{
    [Test]
    [TestCase(DigimonType.Andromon, 9999, 9999, 999, 999, 999, 999, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonType.Digitamamon, 9999, 9999, 999, 999, 999, 999, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonType.Etemon, 9999, 9999, 999, 999, 999, 999, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonType.Andromon, 100, 100, 10, 10, 10, 10, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonType.Digitamamon, 100, 100, 10, 10, 10, 10, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonType.Etemon, 100, 100, 10, 10, 10, 10, 0, 30, 100, 100, 100, 58)]
    public void DetermineEvolutionResult_ShouldReturnExpectedDigimon(DigimonType digimonType, int hp, int mp, int off, int def, int speed, int brains, int careMistakes, int weight, int happiness,
        int discipline, int battles, int techniqueCount)
    {
        // Arrange
        FromUltimateEvolutionCalculator sut = new SetupBuilder()
            .Build();
        Digimon digimon = new DigimonBuilder()
            .WithDigimonType(digimonType)
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
        EvolutionResult result = sut.DetermineEvolutionResult(digimon);

        // Assert
        result.ShouldBe(EvolutionResult.None);
    }
    
    [Test]
    [TestCase(DigimonType.Yuramon, 9999, 9999, 999, 999, 999, 999, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonType.Tsunomon, 9999, 9999, 999, 999, 999, 999, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonType.Agumon, 9999, 9999, 999, 999, 999, 999, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonType.Kabuterimon, 9999, 9999, 999, 999, 999, 999, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonType.Yuramon, 100, 100, 10, 10, 10, 10, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonType.Tsunomon, 100, 100, 10, 10, 10, 10, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonType.Agumon, 100, 100, 10, 10, 10, 10, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonType.Kabuterimon, 100, 100, 10, 10, 10, 10, 0, 30, 100, 100, 100, 58)]
    public void DetermineEvolutionResult_ShouldThrowException_WhenDigimonIsNotAnUltimate(DigimonType digimonType, int hp, int mp, int off, int def, int speed, int brains, int careMistakes, int weight, int happiness,
        int discipline, int battles, int techniqueCount)
    {
        // Arrange
        FromUltimateEvolutionCalculator sut = new SetupBuilder()
            .Build();
        Digimon digimon = new DigimonBuilder()
            .WithDigimonType(digimonType)
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
        Action determineEvolutionResultThrowingException = () => sut.DetermineEvolutionResult(digimon);

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