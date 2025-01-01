using System;
using System.Collections.Generic;
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
    [TestCase(DigimonType.Agumon, 1000, 1000, 250, 200, 500, 150, 3, 20, 80, 80, 0, 10, EvolutionResult.Birdramon)]
    [TestCase(DigimonType.Agumon, 1500, 1000, 100, 100, 100, 150, 0, 20, 80, 80, 0, 10, EvolutionResult.Centarumon)]
    [TestCase(DigimonType.Agumon, 1100, 1000, 100, 100, 150, 100, 0, 20, 80, 80, 0, 10, EvolutionResult.Tyrannomon)]
    [TestCase(DigimonType.Agumon, 1000, 1000, 100, 100, 100, 100, 0, 20, 80, 80, 0, 10, EvolutionResult.Centarumon)]
    [TestCase(DigimonType.Greymon, 4000, 3000, 500, 500, 300, 300, 0, 65, 80, 80, 0, 10, EvolutionResult.MetalGreymon)]
    [TestCase(DigimonType.Greymon, 4000, 3000, 500, 500, 300, 300, 0, 30, 80, 80, 0, 10, EvolutionResult.None)]
    [TestCase(DigimonType.Greymon, 4000, 6000, 500, 500, 300, 600, 30, 25, 80, 80, 0, 10, EvolutionResult.SkullGreymon)]
    public void DetermineEvolutionResult_ShouldReturnExpectedDigimon_WhenThereAreNoHistoricEvolutions(DigimonType digimonType, int hp, int mp, int off, int def, int speed, int brains, int careMistakes, int weight, int happiness,
        int discipline, int battles, int techniqueCount, EvolutionResult evolutionResult)
    {
        // Arrange
        FromRookieOrChampionEvolutionCalculator sut = new SetupBuilder()
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
        result.ShouldBe(evolutionResult);
    }

    [Test]
    [TestCase(DigimonType.Agumon, 1000, 1000, 90, 100, 100, 100, 0, 25, 100, 100, 0, 40, EvolutionResult.Greymon)]
    public void DetermineEvolutionResult_ShouldReturnGreymon_WhenBirdramonHasHigherPrioScoreThanGreymonAndGreymonIsNotAHistoricEvolutionAndBirdramonIsAHistoricEvolution(DigimonType digimonType, int hp, int mp, int off, int def, int speed, int brains, int careMistakes, int weight, int happiness,
        int discipline, int battles, int techniqueCount, EvolutionResult evolutionResult)
    {
        // Arrange
        FromRookieOrChampionEvolutionCalculator sut = new SetupBuilder()
            .WithHistoricEvolution(DigimonType.Birdramon)
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
        result.ShouldBe(evolutionResult);
    }

    [Test]
    [TestCase(DigimonType.Agumon, 1000, 1000, 90, 100, 100, 100, 0, 25, 100, 100, 0, 40, EvolutionResult.Birdramon)]
    public void DetermineEvolutionResult_ShouldReturnBirdramon_WhenBirdramonHasHigherPrioScoreThanGreymonAndBothGreymonAndBirdramonAreHistoricEvolutions(DigimonType digimonType, int hp, int mp, int off, int def, int speed, int brains, int careMistakes, int weight, int happiness,
        int discipline, int battles, int techniqueCount, EvolutionResult evolutionResult)
    {
        // Arrange
        FromRookieOrChampionEvolutionCalculator sut = new SetupBuilder()
            .WithHistoricEvolution(DigimonType.Greymon)
            .WithHistoricEvolution(DigimonType.Birdramon)
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
        result.ShouldBe(evolutionResult);
    }
    
    [Test]
    [TestCase(DigimonType.Yuramon, 9999, 9999, 999, 999, 999, 999, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonType.Tsunomon, 9999, 9999, 999, 999, 999, 999, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonType.Andromon, 9999, 9999, 999, 999, 999, 999, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonType.Yuramon, 100, 100, 10, 10, 10, 10, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonType.Tsunomon, 100, 100, 10, 10, 10, 10, 0, 30, 100, 100, 100, 58)]
    [TestCase(DigimonType.Andromon, 100, 100, 10, 10, 10, 10, 0, 30, 100, 100, 100, 58)]
    public void DetermineEvolutionResult_ShouldThrowException_WhenDigimonIsNotARookieOrChampion(DigimonType digimonType, int hp, int mp, int off, int def, int speed, int brains, int careMistakes, int weight, int happiness,
        int discipline, int battles, int techniqueCount)
    {
        // Arrange
        FromRookieOrChampionEvolutionCalculator sut = new SetupBuilder()
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
        public SetupBuilder WithHistoricEvolution(DigimonType digimonType)
        {
            Session.HistoricEvolutions.Add(digimonType);
            
            return this;
        }
        
        public FromRookieOrChampionEvolutionCalculator Build()
        {
            FromRookieOrChampionEvolutionCalculator sut = new();

            return sut;
        }
    }
}