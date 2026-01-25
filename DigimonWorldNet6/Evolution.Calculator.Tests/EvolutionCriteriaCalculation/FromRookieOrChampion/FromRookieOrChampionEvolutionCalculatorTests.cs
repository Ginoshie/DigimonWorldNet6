using System;
using DigimonWorld.Evolution.Calculator.Core;
using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;
using Evolution.Calculator.Tests.Builder;
using Generics.Enums;
using NUnit.Framework;
using Shouldly;

namespace Evolution.Calculator.Tests.EvolutionCriteriaCalculation.FromRookieOrChampion;

[TestFixture]
public sealed class FromRookieOrChampionEvolutionCalculatorTests
{
    [Test]
    [TestCase(DigimonName.Agumon, 1000, 1000, 250, 200, 500, 150, 3, 20, 80, 80, 0, 10, EvolutionResult.Birdramon)]
    [TestCase(DigimonName.Agumon, 1500, 1000, 100, 100, 100, 150, 0, 20, 80, 80, 0, 10, EvolutionResult.Centarumon)]
    [TestCase(DigimonName.Agumon, 1100, 1000, 100, 100, 150, 100, 0, 20, 80, 80, 0, 10, EvolutionResult.Tyrannomon)]
    [TestCase(DigimonName.Agumon, 1000, 1000, 100, 100, 100, 100, 0, 20, 80, 80, 0, 10, EvolutionResult.Centarumon)]
    [TestCase(DigimonName.Greymon, 4000, 3000, 500, 500, 300, 300, 0, 65, 80, 80, 0, 10, EvolutionResult.MetalGreymon)]
    [TestCase(DigimonName.Greymon, 4000, 3000, 500, 500, 300, 300, 0, 30, 80, 80, 0, 10, EvolutionResult.None)]
    [TestCase(DigimonName.Greymon, 4000, 6000, 500, 500, 300, 600, 30, 25, 80, 80, 0, 10, EvolutionResult.SkullGreymon)]
    public void DetermineEvolutionResult_ShouldReturnExpectedDigimon_WhenThereAreNoHistoricEvolutions(DigimonName digimonName, int hp, int mp, int off, int def, int speed, int brains, int careMistakes, int weight, int happiness,
        int discipline, int battles, int techniqueCount, EvolutionResult evolutionResult)
    {
        // Arrange
        FromRookieOrChampionEvolutionCalculator sut = new SetupBuilder()
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
    [TestCase(DigimonName.Agumon, 900, 1500, 90, 90, 150, 130, 0, 30, 0, 100, 0, 1, EvolutionResult.Centarumon)]
    public void DetermineEvolutionResult_ShouldReturnCentarumon_WhenCentarumonAndGarurumonAreEnabled(DigimonName digimonName, int hp, int mp, int off, int def, int speed, int brains, int careMistakes, int weight, int happiness,
        int discipline, int battles, int techniqueCount, EvolutionResult evolutionResult)
    {
        // Arrange
        FromRookieOrChampionEvolutionCalculator sut = new SetupBuilder()
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
    [TestCase(DigimonName.Agumon, 1000, 1000, 90, 100, 100, 100, 0, 25, 100, 100, 0, 40, EvolutionResult.Greymon)]
    public void DetermineEvolutionResult_ShouldReturnGreymon_WhenBirdramonHasHigherPrioScoreThanGreymonAndGreymonIsNotAHistoricEvolutionAndBirdramonIsAHistoricEvolution(DigimonName digimonName, int hp, int mp, int off, int def, int speed, int brains,
        int careMistakes, int weight, int happiness,
        int discipline, int battles, int techniqueCount, EvolutionResult evolutionResult)
    {
        // Arrange
        FromRookieOrChampionEvolutionCalculator sut = new SetupBuilder()
            .WithHistoricEvolution(DigimonName.Birdramon)
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
    [TestCase(DigimonName.Agumon, 1000, 1000, 90, 100, 100, 100, 0, 25, 100, 100, 0, 40, EvolutionResult.Birdramon)]
    public void DetermineEvolutionResult_ShouldReturnBirdramon_WhenBirdramonHasHigherPrioScoreThanGreymonAndAreAHistoricEvolution(DigimonName digimonName, int hp, int mp, int off, int def, int speed, int brains, int careMistakes, int weight,
        int happiness,
        int discipline, int battles, int techniqueCount, EvolutionResult evolutionResult)
    {
        // Arrange
        FromRookieOrChampionEvolutionCalculator sut = new SetupBuilder()
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
    [TestCase(DigimonName.Agumon, 1000, 1000, 90, 100, 100, 100, 0, 25, 100, 100, 0, 40, EvolutionResult.Birdramon)]
    public void DetermineEvolutionResult_ShouldReturnBirdramon_WhenBirdramonHasHigherPrioScoreThanGreymonAndBothGreymonAndBirdramonAreHistoricEvolutions(DigimonName digimonName, int hp, int mp, int off, int def, int speed, int brains,
        int careMistakes, int weight, int happiness,
        int discipline, int battles, int techniqueCount, EvolutionResult evolutionResult)
    {
        // Arrange
        FromRookieOrChampionEvolutionCalculator sut = new SetupBuilder()
            .WithHistoricEvolution(DigimonName.Greymon)
            .WithHistoricEvolution(DigimonName.Birdramon)
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
    [TestCase(DigimonName.Yuramon, 9999, 9999, 999, 999, 999, 999, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonName.Tsunomon, 9999, 9999, 999, 999, 999, 999, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonName.Andromon, 9999, 9999, 999, 999, 999, 999, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonName.Yuramon, 100, 100, 10, 10, 10, 10, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonName.Tsunomon, 100, 100, 10, 10, 10, 10, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonName.Andromon, 100, 100, 10, 10, 10, 10, 0, 30, 100, 100, 100, 58)]
    public void DetermineEvolutionResult_ShouldThrowException_WhenDigimonIsNotARookieOrChampion(DigimonName digimonName, int hp, int mp, int off, int def, int speed, int brains, int careMistakes, int weight, int happiness,
        int discipline, int battles, int techniqueCount)
    {
        // Arrange
        FromRookieOrChampionEvolutionCalculator sut = new SetupBuilder()
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
        public SetupBuilder()
        {
            Session.HistoricEvolutions.Clear();
        }

        public SetupBuilder WithHistoricEvolution(DigimonName digimonName)
        {
            Session.HistoricEvolutions.Add(digimonName);

            return this;
        }

        public FromRookieOrChampionEvolutionCalculator Build()
        {
            FromRookieOrChampionEvolutionCalculator sut = new();

            return sut;
        }
    }
}