using System;
using System.Collections.Generic;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;
using NUnit.Framework;
using Shouldly;

namespace Evolution.Calculator.Tests.EvolutionCriteriaCalculation.FromRookieOrChampion;

[TestFixture]
public class FromRookieOrChampionEvolutionMapperTests
{
    [Test]
    public void FromFreshEvolutionMapperIndexer_ShouldNotThrow_WhenMappingExists([Values(DigimonType.Agumon, DigimonType.Airdramon, DigimonType.Angemon, DigimonType.Bakemon, DigimonType.Betamon,
            DigimonType.Birdramon, DigimonType.Biyomon, DigimonType.Centarumon, DigimonType.Coelamon, DigimonType.Devimon, DigimonType.Drimogemon, DigimonType.Elecmon, DigimonType.Frigimon,
            DigimonType.Gabumon, DigimonType.Garurumon, DigimonType.Greymon, DigimonType.Kabuterimon, DigimonType.Kokatorimon, DigimonType.Kuwagamon, DigimonType.Leomon, DigimonType.Meramon,
            DigimonType.Mojyamon, DigimonType.Monochromon, DigimonType.Nanimon, DigimonType.Ninjamon, DigimonType.Numemon, DigimonType.Ogremon, DigimonType.Palmon, DigimonType.Patamon,
            DigimonType.Penguinmon, DigimonType.Seadramon, DigimonType.Shellmon, DigimonType.Sukamon, DigimonType.Tyrannomon, DigimonType.Unimon, DigimonType.Vegiemon, DigimonType.Whamon)]
        DigimonType digimonType)
    {
        // Arrange
        FromRookieOrChampionEvolutionMapper sut = new SetupBuilder()
            .Build();

        // Act
        Func<IEnumerable<IEvolutionCriteria>> mappingNotThrowingException = () => sut[digimonType];

        // Assert
        mappingNotThrowingException.ShouldNotThrow();
    }

    [Test]
    public void FromFreshEvolutionMapperIndexer_ShouldThrowException_WhenMappingDoesNotExist([Values(DigimonType.Yuramon, DigimonType.Tsunomon, DigimonType.Vademon)] DigimonType digimonType)
    {
        // Arrange
        FromRookieOrChampionEvolutionMapper sut = new SetupBuilder()
            .Build();

        // Act
        Func<object?> mappingThrowingException = () => sut[digimonType];

        // Assert
        mappingThrowingException.ShouldThrow<Exception>();
    }

    private sealed class SetupBuilder
    {
        public FromRookieOrChampionEvolutionMapper Build()
        {
            FromRookieOrChampionEvolutionMapper sut = new();

            return sut;
        }
    }
}