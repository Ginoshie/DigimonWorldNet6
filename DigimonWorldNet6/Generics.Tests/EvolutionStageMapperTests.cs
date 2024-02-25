using Generics.Enums;
using NUnit.Framework;
using Shouldly;

namespace Generics.Tests;

[TestFixture]
public sealed class EvolutionStageMapperTests
{
    [Test]
    public void EvolutionStageMapper_ShouldNotThrowException_WhenValidIndexIsUsed([Values] DigimonType digimonType)
    {
        // Arrange
        var sut = new SetupBuilder()
            .Build();

        // Act
        var applyValidIndex = () => sut[digimonType];

        // Assert
        applyValidIndex.ShouldNotThrow();
    }
    
    [Test]
    public void EvolutionStageMapper_ShouldNotThrowException_WhenValidIndexIsUsed()
    {
        // Arrange
        const DigimonType invalidDigimonTypeIndex = (DigimonType)999;
        
        var sut = new SetupBuilder()
            .Build();

        // Act
        Func<object?> applyValidIndex = () => sut[invalidDigimonTypeIndex];

        // Assert
        applyValidIndex.ShouldThrow<Exception>();
    }

    private sealed class SetupBuilder
    {
        public EvolutionStageMapper Build()
        {
            var sut = new EvolutionStageMapper();

            return sut;
        }
    }
}