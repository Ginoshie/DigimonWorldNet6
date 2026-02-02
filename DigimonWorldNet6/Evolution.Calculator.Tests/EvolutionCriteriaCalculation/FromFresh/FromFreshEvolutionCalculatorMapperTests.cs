using System;
using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromFresh;
using Evolution.Calculator.Tests.Builder;
using Shared.Enums;
using NUnit.Framework;
using Shouldly;

namespace Evolution.Calculator.Tests.EvolutionCriteriaCalculation.FromFresh;

public sealed class FromFreshEvolutionCalculatorMapperTests
{
    [Test]
    public void DetermineEvolutionResult_ShouldReturnExpectedDigimon([Values(DigimonName.Poyomon)] DigimonName digimonName, [Values(0, 9999)] int hp, [Values(0, 9999)] int mp,
        [Values(0, 999)] int off, [Values(0, 999)] int def, [Values(0, 999)] int speed, [Values(0, 999)] int brains, [Values(0, 10)] int careMistakes, [Values(1, 15)] int weight,
        [Values(0)] int happiness, [Values(0)] int discipline, [Values(0)] int battles, [Values(40)] int techniqueCount, [Values(EvolutionResult.Tokomon)] EvolutionResult evolutionResult)
    {
        // Arrange
        FromFreshEvolutionCalculator sut = new SetupBuilder()
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
        result.ShouldBe(evolutionResult);
    }
    [Test]
    [TestCase(DigimonName.Tsunomon, 100, 100, 10, 10, 10, 10, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonName.Palmon, 100, 100, 10, 10, 10, 10, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonName.Kuwagamon, 100, 100, 10, 10, 10, 10, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonName.Giromon, 100, 100, 10, 10, 10, 10, 0, 30, 100, 100, 100, 58)]
    public void DetermineEvolutionResult_ShouldThrowException_WhenDigimonIsNotAFresh(DigimonName digimonName, int hp, int mp, int off, int def, int speed, int brains, int careMistakes, int weight, int happiness,
        int discipline, int battles, int techniqueCount)
    {
        // Arrange
        FromFreshEvolutionCalculator sut = new SetupBuilder()
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
        public FromFreshEvolutionCalculator Build()
        {
            FromFreshEvolutionCalculator sut = new();

            return sut;
        }
    }
}