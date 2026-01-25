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
    [TestCase(DigimonName.Botamon, EvolutionResult.Koromon)]
    [TestCase(DigimonName.Poyomon, EvolutionResult.Tokomon)]
    [TestCase( DigimonName.Punimon, EvolutionResult.Tsunomon)]
    [TestCase(DigimonName.Yuramon, EvolutionResult.Tanemon)]
    public void FromFreshEvolutionMapperIndexer_ShouldReturnExpectedResult(DigimonName digimonName, EvolutionResult expectedEvolutionResult)
    {
        // Arrange
        FromFreshEvolutionMapper sut = new SetupBuilder()
            .Build();

        // Act
        EvolutionResult result = sut[digimonName];

        // Assert
        result.ShouldBe(expectedEvolutionResult);
    }
    
    [Test]
    public void FromFreshEvolutionMapperIndexer_ShouldThrowException_WhenMappingDoesNotExist([Values(DigimonName.Tsunomon, DigimonName.Penguinmon, DigimonName.Bakemon, DigimonName.Vademon)] DigimonName digimonName)
    {
        // Arrange
        FromFreshEvolutionMapper sut = new SetupBuilder()
            .Build();

        // Act
        Func<object?> mappingThrowingException = () => sut[digimonName];

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