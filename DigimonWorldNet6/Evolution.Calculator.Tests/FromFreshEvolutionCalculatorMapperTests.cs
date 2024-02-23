using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromFresh;
using Evolution.Calculator.Tests.Builder;
using Generics.Enums;
using NUnit.Framework;
using Shouldly;

namespace Evolution.Calculator.Tests;

public sealed class FromFreshEvolutionCalculatorMapperTests
{
    [Test]
    public void DetermineEvolutionResult_ShouldReturnExpectedDigimon([Values(DigimonType.Poyomon)] DigimonType digimonType, [Values(0, 9999)] int hp, [Values(0, 9999)] int mp,
        [Values(0, 999)] int off, [Values(0, 999)] int def, [Values(0, 999)] int speed, [Values(0, 999)] int brains, [Values(0, 10)] int careMistakes, [Values(1, 15)] int weight,
        [Values(0)] int happiness, [Values(0)] int discipline, [Values(0)] int battles, [Values(40)] int techniqueCount, [Values(EvolutionResult.Tokomon)] EvolutionResult evolutionResult)
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

    private sealed class SetupBuilder
    {
        public FromFreshEvolutionCalculator Build()
        {
            var sut = new FromFreshEvolutionCalculator();

            return sut;
        }
    }
}