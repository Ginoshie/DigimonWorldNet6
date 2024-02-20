using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.ChampionAndUltimate;
using Evolution.Calculator.Tests.Builder;
using Generics.Enums;
using NUnit.Framework;
using Shouldly;

namespace Evolution.Calculator.Tests;

[TestFixture]
public sealed class ChampionAndUltimateEvolutionCalculatorTests
{
    [Test]
    [TestCase(DigimonType.Agumon, 1000, 1000, 250, 200, 500, 150, 3, 20, 80, 80, 0, 10, EvolutionResult.Birdramon)]
    [TestCase(DigimonType.Agumon, 1500, 1000, 100, 100, 100, 150, 0, 20, 80, 80, 0, 10, EvolutionResult.Centarumon)]
    [TestCase(DigimonType.Agumon, 1100, 1000, 100, 100, 150, 100, 0, 20, 80, 80, 0, 10, EvolutionResult.Tyrannomon)]
    [TestCase(DigimonType.Agumon, 1000, 1000, 100, 100, 100, 100, 0, 20, 80, 80, 0, 10, EvolutionResult.Centarumon)]
    public void DetermineEvolutionResult_ShouldReturnExpectedDigimon(DigimonType digimonType, int hp, int mp, int off, int def, int speed, int brains, int careMistakes, int weight, int happiness,
        int discipline, int battles, int techniqueCount, EvolutionResult evolutionResult)
    {
        // Arrange
        var championAndUltimateEvolutionCalculator = new SetupBuilder()
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
        var result = championAndUltimateEvolutionCalculator.DetermineEvolutionResult(digimon);

        // Assert
        result.ShouldBe(evolutionResult);
    }

    private sealed class SetupBuilder
    {
        public ChampionAndUltimateEvolutionCalculator Build()
        {
            var sut = new ChampionAndUltimateEvolutionCalculator();

            return sut;
        }
    }
}