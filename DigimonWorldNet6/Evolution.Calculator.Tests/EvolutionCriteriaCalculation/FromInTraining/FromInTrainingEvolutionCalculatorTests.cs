using System;
using DigimonWorld.Evolution.Calculator.Core.DataObjects;
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
    [TestCase(DigimonName.Tokomon, 1000, 200, 20, 20, 20, 20, 50, 15, 80, 80, 0, 10, EvolutionResult.Patamon)]
    [TestCase(DigimonName.Tokomon, 200, 200, 100, 20, 20, 20, 50, 15, 80, 80, 0, 10, EvolutionResult.Patamon)]
    [TestCase(DigimonName.Tokomon, 200, 200, 20, 20, 20, 100, 50, 15, 80, 80, 0, 10, EvolutionResult.Patamon)]
    [TestCase(DigimonName.Tokomon, 200, 1000, 20, 20, 20, 20, 50, 15, 80, 80, 0, 10, EvolutionResult.Biyomon)]
    [TestCase(DigimonName.Tokomon, 200, 200, 20, 100, 20, 20, 50, 15, 80, 80, 0, 10, EvolutionResult.Biyomon)]
    [TestCase(DigimonName.Tokomon, 200, 200, 20, 20, 100, 20, 50, 15, 80, 80, 0, 10, EvolutionResult.Biyomon)]
    public void DetermineEvolutionResult_ShouldReturnExpectedDigimon(DigimonName digimonName, int hp, int mp, int off, int def, int speed, int brains, int careMistakes, int weight, int happiness,
        int discipline, int battles, int techniqueCount, EvolutionResult evolutionResult)
    {
        // Arrange
        FromInTrainingEvolutionCalculator sut = new SetupBuilder()
            .Build();
        Digimon digimon = new DigimonBuilder()
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
        EvolutionResult result = sut.DetermineEvolutionResult(digimon);

        // Assert
        result.ShouldBe(evolutionResult);
    }
    
    [Test]
    [TestCase(DigimonName.Yuramon, 1000, 200, 20, 20, 20, 20, 50, 15, 80, 80, 0, 10)]
    [TestCase(DigimonName.Gabumon, 1000, 200, 20, 20, 20, 20, 50, 15, 80, 80, 0, 10)]
    [TestCase(DigimonName.Garurumon, 1000, 200, 20, 20, 20, 20, 50, 15, 80, 80, 0, 10)]
    [TestCase(DigimonName.Monzaemon, 1000, 200, 20, 20, 20, 20, 50, 15, 80, 80, 0, 10)]
    public void DetermineEvolutionResult__ShouldThrowException_WhenDigimonIsNotAnInTraining(DigimonName digimonName, int hp, int mp, int off, int def, int speed, int brains, int careMistakes, int weight, int happiness,
        int discipline, int battles, int techniqueCount)
    {
        // Arrange
        FromInTrainingEvolutionCalculator sut = new SetupBuilder()
            .Build();
        Digimon digimon = new DigimonBuilder()
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
        Action determineEvolutionResultThrowingException = () => sut.DetermineEvolutionResult(digimon);

        // Assert
        determineEvolutionResultThrowingException.ShouldThrow<Exception>();
    }

    private sealed class SetupBuilder
    {
        public FromInTrainingEvolutionCalculator Build()
        {
            FromInTrainingEvolutionCalculator sut = new();

            return sut;
        }
    }
}