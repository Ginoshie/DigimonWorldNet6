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
    public void FromFreshEvolutionMapperIndexer_ShouldNotThrow_WhenMappingExists([Values(DigimonName.Agumon, DigimonName.Airdramon, DigimonName.Angemon, DigimonName.Bakemon, DigimonName.Betamon,
            DigimonName.Birdramon, DigimonName.Biyomon, DigimonName.Centarumon, DigimonName.Coelamon, DigimonName.Devimon, DigimonName.Drimogemon, DigimonName.Elecmon, DigimonName.Frigimon,
            DigimonName.Gabumon, DigimonName.Garurumon, DigimonName.Greymon, DigimonName.Kabuterimon, DigimonName.Kokatorimon, DigimonName.Kuwagamon, DigimonName.Leomon, DigimonName.Meramon,
            DigimonName.Mojyamon, DigimonName.Monochromon, DigimonName.Nanimon, DigimonName.Ninjamon, DigimonName.Numemon, DigimonName.Ogremon, DigimonName.Palmon, DigimonName.Patamon,
            DigimonName.Penguinmon, DigimonName.Seadramon, DigimonName.Shellmon, DigimonName.Sukamon, DigimonName.Tyrannomon, DigimonName.Unimon, DigimonName.Vegiemon, DigimonName.Whamon)]
        DigimonName digimonName)
    {
        // Arrange
        FromRookieOrChampionEvolutionMapper sut = new SetupBuilder()
            .Build();

        // Act
        Func<IEnumerable<IEvolutionCriteria>> mappingNotThrowingException = () => sut.GetEvolutionCriteria(digimonName);

        // Assert
        mappingNotThrowingException.ShouldNotThrow();
    }

    [Test]
    public void FromFreshEvolutionMapperIndexer_ShouldThrowException_WhenMappingDoesNotExist([Values(DigimonName.Yuramon, DigimonName.Tsunomon, DigimonName.Vademon)] DigimonName digimonName)
    {
        // Arrange
        FromRookieOrChampionEvolutionMapper sut = new SetupBuilder()
            .Build();

        // Act
        Func<object?> mappingThrowingException = () => sut.GetEvolutionCriteria(digimonName);

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