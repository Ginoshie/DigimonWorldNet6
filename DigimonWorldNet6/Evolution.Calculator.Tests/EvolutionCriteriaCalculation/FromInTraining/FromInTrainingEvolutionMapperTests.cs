using System;
using System.Collections.Generic;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromInTraining;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;
using NUnit.Framework;
using Shouldly;

namespace Evolution.Calculator.Tests.EvolutionCriteriaCalculation.FromInTraining;

[TestFixture]
public class FromInTrainingEvolutionMapperTests
{
    [Test]
    public void FromFreshEvolutionMapperIndexer_ShouldNotThrow_WhenMappingExists([Values(DigimonName.Koromon, DigimonName.Tokomon, DigimonName.Tsunomon, DigimonName.Tanemon)] DigimonName digimonName)
    {
        // Arrange
        FromInTrainingEvolutionMapper sut = new SetupBuilder()
            .Build();

        // Act
        Func<IEnumerable<IEvolutionCriteria>> mappingNotThrowingException = () => sut[digimonName];

        // Assert
        mappingNotThrowingException.ShouldNotThrow();
    }

    [Test]
    public void FromFreshEvolutionMapperIndexer_ShouldThrowException_WhenMappingDoesNotExist(
        [Values(DigimonName.Yuramon, DigimonName.Penguinmon, DigimonName.Bakemon, DigimonName.Vademon)]
        DigimonName digimonName)
    {
        // Arrange
        FromInTrainingEvolutionMapper sut = new SetupBuilder()
            .Build();

        // Act
        Func<object?> mappingThrowingException = () => sut[digimonName];

        // Assert
        mappingThrowingException.ShouldThrow<Exception>();
    }

    private sealed class SetupBuilder
    {
        public FromInTrainingEvolutionMapper Build()
        {
            FromInTrainingEvolutionMapper sut = new();

            return sut;
        }
    }
}