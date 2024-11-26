using System;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromFresh;
using Generics.Enums;
using NUnit.Framework;
using Shouldly;

namespace Evolution.Calculator.Tests.EvolutionCriteriaCalculation.FromFresh;

[TestFixture]
public class FromFreshEvolutionMapperTests
{
    [Test]
    [TestCase(DigimonType.Botamon, EvolutionResult.Koromon)]
    [TestCase(DigimonType.Poyomon, EvolutionResult.Tokomon)]
    [TestCase( DigimonType.Punimon, EvolutionResult.Tsunomon)]
    [TestCase(DigimonType.Yuramon, EvolutionResult.Tanemon)]
    public void FromFreshEvolutionMapperIndexer_ShouldReturnExpectedResult(DigimonType digimonType, EvolutionResult expectedEvolutionResult)
    {
        // Arrange
        FromFreshEvolutionMapper sut = new SetupBuilder()
            .Build();

        // Act
        EvolutionResult result = sut[digimonType];

        // Assert
        result.ShouldBe(expectedEvolutionResult);
    }
    
    [Test]
    public void FromFreshEvolutionMapperIndexer_ShouldThrowException_WhenMappingDoesNotExist([Values(DigimonType.Tsunomon, DigimonType.Penguinmon, DigimonType.Bakemon, DigimonType.Vademon)] DigimonType digimonType)
    {
        // Arrange
        FromFreshEvolutionMapper sut = new SetupBuilder()
            .Build();

        // Act
        Func<object?> mappingThrowingException = () => sut[digimonType];

        // Assert
        mappingThrowingException.ShouldThrow<Exception>();
    }

    private sealed class SetupBuilder
    {
        public FromFreshEvolutionMapper Build()
        {
            FromFreshEvolutionMapper sut = new();

            return sut;
        }
    }
}