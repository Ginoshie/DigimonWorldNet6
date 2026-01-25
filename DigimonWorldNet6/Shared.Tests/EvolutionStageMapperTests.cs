using Generics.Enums;
using NUnit.Framework;
using Shouldly;

namespace Generics.Tests;

[TestFixture]
public sealed class EvolutionStageMapperTests
{
    [Test]
    public void EvolutionStageMapper_ShouldNotThrowException_WhenValidIndexIsUsed([Values] DigimonName digimonName)
    {
        // Act
        Func<EvolutionStage> applyValidIndex = () => EvolutionStageMapper.Get(digimonName);

        // Assert
        applyValidIndex.ShouldNotThrow();
    }
    
    [Test]
    public void EvolutionStageMapper_ShouldNotThrowException_WhenValidIndexIsUsed()
    {
        // Arrange
        const DigimonName invalidDigimonTypeIndex = (DigimonName)999;

        // Act
        Func<object?> applyValidIndex = () => EvolutionStageMapper.Get(invalidDigimonTypeIndex);

        // Assert
        applyValidIndex.ShouldThrow<Exception>();
    }
}