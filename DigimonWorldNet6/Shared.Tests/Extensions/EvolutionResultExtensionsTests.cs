using NUnit.Framework;
using Shared.Enums;
using Shared.Extensions;
using Shouldly;

namespace Shared.Tests.Extensions;

[TestFixture]
public sealed class EvolutionResultExtensionsTests
{
    // Happy path: every EvolutionResult that has a matching DigimonName should map correctly
    private static readonly EvolutionResult[] _validDigimonResults = Enum.GetValues<EvolutionResult>()
        .Where(r => r != EvolutionResult.Unknown && r != EvolutionResult.None && r != EvolutionResult.NotApplicable)
        .ToArray();

    [Test]
    [TestCaseSource(nameof(_validDigimonResults))]
    public void ToDigimonType_ShouldReturnMatchingDigimonName_WhenEvolutionResultIsAValidDigimon(EvolutionResult evolutionResult)
    {
        // Act
        DigimonName result = evolutionResult.ToDigimonType();

        // Assert
        result.ToString().ShouldBe(evolutionResult.ToString());
    }

    [Test]
    [TestCaseSource(nameof(_validDigimonResults))]
    public void ToDigimonType_ShouldNotThrow_WhenEvolutionResultIsAValidDigimon(EvolutionResult evolutionResult)
    {
        // Act
        Action act = () => evolutionResult.ToDigimonType();

        // Assert
        act.ShouldNotThrow();
    }

    // Spot checks
    [Test]
    public void ToDigimonType_ShouldReturnAgumon_WhenEvolutionResultIsAgumon()
    {
        EvolutionResult.Agumon.ToDigimonType().ShouldBe(DigimonName.Agumon);
    }

    [Test]
    public void ToDigimonType_ShouldReturnMetalGreymon_WhenEvolutionResultIsMetalGreymon()
    {
        EvolutionResult.MetalGreymon.ToDigimonType().ShouldBe(DigimonName.MetalGreymon);
    }

    [Test]
    public void ToDigimonType_ShouldReturnPanjyamon_WhenEvolutionResultIsPanjyamon()
    {
        EvolutionResult.Panjyamon.ToDigimonType().ShouldBe(DigimonName.Panjyamon);
    }

    // Unhappy path: non-Digimon results should throw
    [Test]
    public void ToDigimonType_ShouldThrowArgumentException_WhenEvolutionResultIsUnknown()
    {
        Action act = () => EvolutionResult.Unknown.ToDigimonType();

        act.ShouldThrow<ArgumentException>();
    }

    [Test]
    public void ToDigimonType_ShouldReturnDigimonTypeNone_WhenEvolutionResultIsNone()
    {
        EvolutionResult.None.ToDigimonType().ShouldBe(DigimonName.None);
    }

    [Test]
    public void ToDigimonType_ShouldThrowArgumentException_WhenEvolutionResultIsNotApplicable()
    {
        Action act = () => EvolutionResult.NotApplicable.ToDigimonType();

        act.ShouldThrow<ArgumentException>();
    }

    // Edge case: undefined enum value — Enum.TryParse successfully parses the numeric
    // string representation (e.g., "9999") back to an enum value, so ToDigimonType does NOT throw.
    // This means ToDigimonType can return a DigimonName with an undefined numeric value.
    [Test]
    public void ToDigimonType_ShouldNotThrow_WhenEvolutionResultIsUndefinedNumericValue()
    {
        const EvolutionResult undefinedResult = (EvolutionResult)9999;

        Action act = () => undefinedResult.ToDigimonType();

        act.ShouldNotThrow();
    }

    [Test]
    public void ToDigimonType_ShouldReturnMatchingNumericValue_WhenEvolutionResultIsUndefined()
    {
        const EvolutionResult undefinedResult = (EvolutionResult)9999;

        DigimonName result = undefinedResult.ToDigimonType();

        ((int)result).ShouldBe(9999);
    }
}

