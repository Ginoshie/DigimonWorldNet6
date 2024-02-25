using System;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromInTraining;
using Evolution.Calculator.Tests.Builder;
using Generics.Enums;
using NUnit.Framework;
using Shouldly;

namespace Evolution.Calculator.Tests.EvolutionCriteriaCalculation.FromInTraining;

[TestFixture]
public sealed class FromInTrainingEvolutionCalculatorTests
{
    [Test]
    [TestCase(DigimonType.Tokomon, 1000, 200, 20, 20, 20, 20, 50, 15, 80, 80, 0, 10, EvolutionResult.Patamon)]
    [TestCase(DigimonType.Tokomon, 200, 200, 100, 20, 20, 20, 50, 15, 80, 80, 0, 10, EvolutionResult.Patamon)]
    [TestCase(DigimonType.Tokomon, 200, 200, 20, 20, 20, 100, 50, 15, 80, 80, 0, 10, EvolutionResult.Patamon)]
    [TestCase(DigimonType.Tokomon, 200, 1000, 20, 20, 20, 20, 50, 15, 80, 80, 0, 10, EvolutionResult.Biyomon)]
    [TestCase(DigimonType.Tokomon, 200, 200, 20, 100, 20, 20, 50, 15, 80, 80, 0, 10, EvolutionResult.Biyomon)]
    [TestCase(DigimonType.Tokomon, 200, 200, 20, 20, 100, 20, 50, 15, 80, 80, 0, 10, EvolutionResult.Biyomon)]
    public void DetermineEvolutionResult_ShouldReturnExpectedDigimon(DigimonType digimonType, int hp, int mp, int off, int def, int speed, int brains, int careMistakes, int weight, int happiness,
        int discipline, int battles, int techniqueCount, EvolutionResult evolutionResult)
    {
        // Arrange
        var sut = new SetupBuilder()
            .Build();
        var digimon = new DigimonBuilder()
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
        var result = sut.DetermineEvolutionResult(digimon);

        // Assert
        result.ShouldBe(evolutionResult);
    }
    
    [Test]
    [TestCase(DigimonType.Yuramon, 1000, 200, 20, 20, 20, 20, 50, 15, 80, 80, 0, 10)]
    [TestCase(DigimonType.Gabumon, 1000, 200, 20, 20, 20, 20, 50, 15, 80, 80, 0, 10)]
    [TestCase(DigimonType.Garurumon, 1000, 200, 20, 20, 20, 20, 50, 15, 80, 80, 0, 10)]
    [TestCase(DigimonType.Monzaemon, 1000, 200, 20, 20, 20, 20, 50, 15, 80, 80, 0, 10)]
    public void DetermineEvolutionResult__ShouldThrowException_WhenDigimonIsNotAnInTraining(DigimonType digimonType, int hp, int mp, int off, int def, int speed, int brains, int careMistakes, int weight, int happiness,
        int discipline, int battles, int techniqueCount)
    {
        // Arrange
        var sut = new SetupBuilder()
            .Build();
        var digimon = new DigimonBuilder()
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
        public FromInTrainingEvolutionCalculator Build()
        {
            var sut = new FromInTrainingEvolutionCalculator();

            return sut;
        }
    }
}