using System;
using DigimonWorld.Evolution.Calculator.Core;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromFresh;
using Evolution.Calculator.Tests.Builder;
using NUnit.Framework;
using Shared.Enums;
using Shouldly;

namespace Evolution.Calculator.Tests.EvolutionCriteriaCalculation.FromFresh;

[TestFixture]
public sealed class FromFreshEvolutionCalculatorTests
{
    [Test]
    [TestCase(DigimonName.Botamon, EvolutionResult.Koromon)]
    [TestCase(DigimonName.Poyomon, EvolutionResult.Tokomon)]
    [TestCase(DigimonName.Punimon, EvolutionResult.Tsunomon)]
    [TestCase(DigimonName.Yuramon, EvolutionResult.Tanemon)]
    public void DetermineEvolutionResult_ShouldReturnExpectedDigimon(DigimonName digimonName, EvolutionResult expectedResult)
    {
        // Arrange
        FromFreshEvolutionCalculator sut = new();
        EvolutionCalculationInput input = new DigimonBuilder()
            .WithDigimonType(digimonName)
            .WithHP(100)
            .WithMP(100)
            .WithOff(10)
            .WithDef(10)
            .WithSpeed(10)
            .WithBrains(10)
            .WithCareMistakes(0)
            .WithWeight(15)
            .WithHappiness(50)
            .WithDiscipline(50)
            .WithBattles(0)
            .WithTechniqueCount(0)
            .Build();

        // Act
        EvolutionResult result = sut.DetermineEvolutionResult(input);

        // Assert
        result.ShouldBe(expectedResult);
    }

    [Test]
    [TestCase(DigimonName.Koromon)]
    [TestCase(DigimonName.Agumon)]
    [TestCase(DigimonName.Greymon)]
    [TestCase(DigimonName.MetalGreymon)]
    public void DetermineEvolutionResult_ShouldThrowException_WhenDigimonIsNotFresh(DigimonName digimonName)
    {
        // Arrange
        FromFreshEvolutionCalculator sut = new();
        EvolutionCalculationInput input = new DigimonBuilder()
            .WithDigimonType(digimonName)
            .WithHP(100)
            .WithMP(100)
            .WithOff(10)
            .WithDef(10)
            .WithSpeed(10)
            .WithBrains(10)
            .WithCareMistakes(0)
            .WithWeight(15)
            .WithHappiness(50)
            .WithDiscipline(50)
            .WithBattles(0)
            .WithTechniqueCount(0)
            .Build();

        // Act
        Action act = () => sut.DetermineEvolutionResult(input);

        // Assert
        act.ShouldThrow<ArgumentException>();
    }
}
