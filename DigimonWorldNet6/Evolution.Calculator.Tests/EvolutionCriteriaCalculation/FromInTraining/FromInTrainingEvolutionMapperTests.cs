using System;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromInTraining;
using Generics.Enums;
using NUnit.Framework;
using Shouldly;

namespace Evolution.Calculator.Tests.EvolutionCriteriaCalculation.FromInTraining;

[TestFixture]
public class FromInTrainingEvolutionMapperTests
{
    [Test]
    public void FromFreshEvolutionMapperIndexer_ShouldNotThrow_WhenMappingExists([Values(DigimonType.Koromon, DigimonType.Tokomon, DigimonType.Tsunomon, DigimonType.Tanemon)] DigimonType digimonType)
    {
        // Arrange
        var sut = new SetupBuilder()
            .Build();

        // Act
        var mappingNotThrowingException = () => sut[digimonType];

        // Assert
        mappingNotThrowingException.ShouldNotThrow();
    }

    [Test]
    public void FromFreshEvolutionMapperIndexer_ShouldThrowException_WhenMappingDoesNotExist(
        [Values(DigimonType.Yuramon, DigimonType.Penguinmon, DigimonType.Bakemon, DigimonType.Vademon)]
        DigimonType digimonType)
    {
        // Arrange
        var sut = new SetupBuilder()
            .Build();

        // Act
        Func<object?> mappingThrowingException = () => sut[digimonType];

        // Assert
        mappingThrowingException.ShouldThrow<Exception>();
    }

    private sealed class SetupBuilder
    {
        public FromInTrainingEvolutionMapper Build()
        {
            var sut = new FromInTrainingEvolutionMapper();

            return sut;
        }
    }
}