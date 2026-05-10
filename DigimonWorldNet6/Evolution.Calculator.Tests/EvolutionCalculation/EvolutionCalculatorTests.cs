using DigimonWorld.Evolution.Calculator.Core;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCalculation;
using Evolution.Calculator.Tests.Builder;
using NUnit.Framework;
using Shared.Enums;
using Shouldly;

namespace Evolution.Calculator.Tests.EvolutionCalculation;

[TestFixture]
public sealed class EvolutionCalculatorTests
{
    [Test]
    [TestCase(DigimonName.Botamon, EvolutionResult.Koromon)]
    [TestCase(DigimonName.Poyomon, EvolutionResult.Tokomon)]
    [TestCase(DigimonName.Punimon, EvolutionResult.Tsunomon)]
    [TestCase(DigimonName.Yuramon, EvolutionResult.Tanemon)]
    public void CalculateEvolutionResult_ShouldReturnCorrectResult_ForFreshDigimon(DigimonName digimonName, EvolutionResult expectedResult)
    {
        // Arrange
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
        EvolutionResult result = EvolutionCalculator.Instance.CalculateEvolutionResult(input);

        // Assert
        result.ShouldBe(expectedResult);
    }

    [Test]
    [TestCase(DigimonName.Tokomon, 1000, 200, 20, 20, 20, 20, EvolutionResult.Patamon)]
    [TestCase(DigimonName.Tokomon, 200, 1000, 20, 20, 20, 20, EvolutionResult.Biyomon)]
    public void CalculateEvolutionResult_ShouldReturnCorrectResult_ForInTrainingDigimon(DigimonName digimonName, int hp, int mp, int off, int def, int speed, int brains, EvolutionResult expectedResult)
    {
        // Arrange
        EvolutionCalculationInput input = new DigimonBuilder()
            .WithDigimonType(digimonName)
            .WithHP(hp)
            .WithMP(mp)
            .WithOff(off)
            .WithDef(def)
            .WithSpeed(speed)
            .WithBrains(brains)
            .WithCareMistakes(0)
            .WithWeight(15)
            .WithHappiness(80)
            .WithDiscipline(80)
            .WithBattles(0)
            .WithTechniqueCount(10)
            .Build();

        // Act
        EvolutionResult result = EvolutionCalculator.Instance.CalculateEvolutionResult(input);

        // Assert
        result.ShouldBe(expectedResult);
    }

    [Test]
    [TestCase(DigimonName.MetalGreymon)]
    [TestCase(DigimonName.Andromon)]
    public void CalculateEvolutionResult_ShouldReturnNone_ForUltimateDigimon(DigimonName digimonName)
    {
        // Arrange
        EvolutionCalculationInput input = new DigimonBuilder()
            .WithDigimonType(digimonName)
            .WithHP(9999)
            .WithMP(9999)
            .WithOff(999)
            .WithDef(999)
            .WithSpeed(999)
            .WithBrains(999)
            .WithCareMistakes(0)
            .WithWeight(30)
            .WithHappiness(100)
            .WithDiscipline(100)
            .WithBattles(100)
            .WithTechniqueCount(58)
            .Build();

        // Act
        EvolutionResult result = EvolutionCalculator.Instance.CalculateEvolutionResult(input);

        // Assert
        result.ShouldBe(EvolutionResult.None);
    }

    [Test]
    public void CalculateEvolutionResult_ShouldDelegateToRookieOrChampionCalculator_ForRookieDigimon()
    {
        // Arrange - Agumon with stats that should produce a valid evolution
        EvolutionCalculationInput input = new DigimonBuilder()
            .WithDigimonType(DigimonName.Agumon)
            .WithHP(1000)
            .WithMP(1000)
            .WithOff(100)
            .WithDef(100)
            .WithSpeed(100)
            .WithBrains(100)
            .WithCareMistakes(0)
            .WithWeight(20)
            .WithHappiness(80)
            .WithDiscipline(80)
            .WithBattles(0)
            .WithTechniqueCount(10)
            .Build();

        // Act
        EvolutionResult result = EvolutionCalculator.Instance.CalculateEvolutionResult(input);

        // Assert - should return a valid champion evolution, not None or throw
        result.ShouldNotBe(EvolutionResult.None);
    }

    [Test]
    public void Instance_ShouldReturnSameInstance()
    {
        // Act
        EvolutionCalculator instance1 = EvolutionCalculator.Instance;
        EvolutionCalculator instance2 = EvolutionCalculator.Instance;

        // Assert
        instance1.ShouldBeSameAs(instance2);
    }
}
