using NUnit.Framework;
using Shared.Enums;
using Shouldly;

namespace Shared.Tests;

[TestFixture]
public sealed class EvolutionStageMapperTests
{
    [Test]
    public void Get_ByEnum_ShouldNotThrowException_WhenValidIndexIsUsed([Values] DigimonName digimonName)
    {
        // Act
        Func<EvolutionStage> applyValidIndex = () => EvolutionStageMapper.Get(digimonName);

        // Assert
        applyValidIndex.ShouldNotThrow();
    }
    
    [Test]
    public void Get_ByEnum_ShouldThrowException_WhenInvalidIndexIsUsed()
    {
        // Arrange
        const DigimonName invalidDigimonTypeIndex = (DigimonName)999;

        // Act
        Func<object?> applyValidIndex = () => EvolutionStageMapper.Get(invalidDigimonTypeIndex);

        // Assert
        applyValidIndex.ShouldThrow<Exception>();
    }

    // --- String overload tests ---

    [TestCase("Agumon", EvolutionStage.Rookie)]
    [TestCase("Greymon", EvolutionStage.Champion)]
    [TestCase("MetalGreymon", EvolutionStage.Ultimate)]
    [TestCase("Botamon", EvolutionStage.Fresh)]
    [TestCase("Koromon", EvolutionStage.InTraining)]
    public void Get_ByString_ShouldReturnCorrectStage_WhenValidNameIsProvided(string digimonName, EvolutionStage expectedStage)
    {
        // Act
        EvolutionStage result = EvolutionStageMapper.Get(digimonName);

        // Assert
        result.ShouldBe(expectedStage);
    }

    [Test]
    public void Get_ByString_ShouldThrowKeyNotFoundException_WhenNameIsInvalid()
    {
        // Act
        Action act = () => EvolutionStageMapper.Get("NonExistentDigimon");

        // Assert
        act.ShouldThrow<KeyNotFoundException>();
    }

    [Test]
    public void Get_ByString_ShouldThrowKeyNotFoundException_WhenNameIsEmpty()
    {
        // Act
        Action act = () => EvolutionStageMapper.Get(string.Empty);

        // Assert
        act.ShouldThrow<KeyNotFoundException>();
    }

    // Edge case: Enum.TryParse is case-insensitive by default in some overloads,
    // but the overload without ignoreCase parameter is case-sensitive
    [Test]
    public void Get_ByString_ShouldThrowKeyNotFoundException_WhenNameHasWrongCase()
    {
        // "agumon" (lowercase) should fail since Enum.TryParse without ignoreCase is case-sensitive
        Action act = () => EvolutionStageMapper.Get("agumon");

        act.ShouldThrow<KeyNotFoundException>();
    }

    // --- Spot-check stage correctness ---

    [TestCase(DigimonName.Botamon, EvolutionStage.Fresh)]
    [TestCase(DigimonName.Poyomon, EvolutionStage.Fresh)]
    [TestCase(DigimonName.Punimon, EvolutionStage.Fresh)]
    [TestCase(DigimonName.Yuramon, EvolutionStage.Fresh)]
    public void Get_ShouldReturnFresh_ForFreshDigimon(DigimonName digimonName, EvolutionStage expectedStage)
    {
        EvolutionStageMapper.Get(digimonName).ShouldBe(expectedStage);
    }

    [TestCase(DigimonName.Koromon, EvolutionStage.InTraining)]
    [TestCase(DigimonName.Tanemon, EvolutionStage.InTraining)]
    [TestCase(DigimonName.Tokomon, EvolutionStage.InTraining)]
    [TestCase(DigimonName.Tsunomon, EvolutionStage.InTraining)]
    public void Get_ShouldReturnInTraining_ForInTrainingDigimon(DigimonName digimonName, EvolutionStage expectedStage)
    {
        EvolutionStageMapper.Get(digimonName).ShouldBe(expectedStage);
    }

    [TestCase(DigimonName.Agumon, EvolutionStage.Rookie)]
    [TestCase(DigimonName.Betamon, EvolutionStage.Rookie)]
    [TestCase(DigimonName.Biyomon, EvolutionStage.Rookie)]
    [TestCase(DigimonName.Gabumon, EvolutionStage.Rookie)]
    [TestCase(DigimonName.Penguinmon, EvolutionStage.Rookie)]
    public void Get_ShouldReturnRookie_ForRookieDigimon(DigimonName digimonName, EvolutionStage expectedStage)
    {
        EvolutionStageMapper.Get(digimonName).ShouldBe(expectedStage);
    }

    [TestCase(DigimonName.Greymon, EvolutionStage.Champion)]
    [TestCase(DigimonName.Garurumon, EvolutionStage.Champion)]
    [TestCase(DigimonName.Devimon, EvolutionStage.Champion)]
    [TestCase(DigimonName.Numemon, EvolutionStage.Champion)]
    [TestCase(DigimonName.Panjyamon, EvolutionStage.Champion)]
    public void Get_ShouldReturnChampion_ForChampionDigimon(DigimonName digimonName, EvolutionStage expectedStage)
    {
        EvolutionStageMapper.Get(digimonName).ShouldBe(expectedStage);
    }

    [TestCase(DigimonName.MetalGreymon, EvolutionStage.Ultimate)]
    [TestCase(DigimonName.Phoenixmon, EvolutionStage.Ultimate)]
    [TestCase(DigimonName.Monzaemon, EvolutionStage.Ultimate)]
    [TestCase(DigimonName.Myotismon, EvolutionStage.Ultimate)]
    [TestCase(DigimonName.Weregarurumon, EvolutionStage.Ultimate)]
    public void Get_ShouldReturnUltimate_ForUltimateDigimon(DigimonName digimonName, EvolutionStage expectedStage)
    {
        EvolutionStageMapper.Get(digimonName).ShouldBe(expectedStage);
    }

    // --- Verify every DigimonName enum value returns a valid EvolutionStage ---

    [Test]
    public void Get_ShouldReturnValidEvolutionStage_ForEveryDigimonName([Values] DigimonName digimonName)
    {
        // Act
        EvolutionStage result = EvolutionStageMapper.Get(digimonName);

        // Assert
        Enum.IsDefined(result).ShouldBeTrue();
    }
}