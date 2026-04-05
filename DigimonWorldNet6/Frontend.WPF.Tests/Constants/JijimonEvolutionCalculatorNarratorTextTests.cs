using System;
using DigimonWorld.Frontend.WPF.Constants;
using NUnit.Framework;
using Shared.Enums;
using Shouldly;

namespace Frontend.WPF.Tests.Constants;

[TestFixture]
public sealed class JijimonEvolutionCalculatorNarratorTextTests
{
    // --- Special non-Digimon results ---

    [Test]
    public void EvolutionResultCalculated_ShouldReturnNonEmptyString_WhenResultIsUnknown()
    {
        string result = JijimonEvolutionCalculatorNarratorText.EvolutionResultCalculated(EvolutionResult.Unknown);

        result.ShouldNotBeNullOrEmpty();
    }

    [Test]
    public void EvolutionResultCalculated_ShouldReturnNonEmptyString_WhenResultIsNone()
    {
        string result = JijimonEvolutionCalculatorNarratorText.EvolutionResultCalculated(EvolutionResult.None);

        result.ShouldNotBeNullOrEmpty();
    }

    [Test]
    public void EvolutionResultCalculated_ShouldReturnNonEmptyString_WhenResultIsNotApplicable()
    {
        string result = JijimonEvolutionCalculatorNarratorText.EvolutionResultCalculated(EvolutionResult.NotApplicable);

        result.ShouldNotBeNullOrEmpty();
    }

    [Test]
    public void EvolutionResultCalculated_ShouldMentionUltimate_WhenResultIsNotApplicable()
    {
        string result = JijimonEvolutionCalculatorNarratorText.EvolutionResultCalculated(EvolutionResult.NotApplicable);

        result.ShouldContain("ultimate");
    }

    // --- InTraining results ---

    [TestCase(EvolutionResult.Koromon)]
    [TestCase(EvolutionResult.Tanemon)]
    [TestCase(EvolutionResult.Tokomon)]
    [TestCase(EvolutionResult.Tsunomon)]
    public void EvolutionResultCalculated_ShouldReturnTextContainingDigimonName_WhenResultIsInTraining(EvolutionResult evolutionResult)
    {
        string result = JijimonEvolutionCalculatorNarratorText.EvolutionResultCalculated(evolutionResult);

        result.ShouldContain(evolutionResult.ToString());
    }

    [Test]
    public void EvolutionResultCalculated_ShouldContainCuteFellow_WhenResultIsInTraining()
    {
        string result = JijimonEvolutionCalculatorNarratorText.EvolutionResultCalculated(EvolutionResult.Koromon);

        result.ShouldContain("cute little fellow");
    }

    // Edge case: InTraining results should use correct article (a/an)
    [Test]
    public void EvolutionResultCalculated_ShouldUseArticleA_WhenInTrainingNameStartsWithConsonant()
    {
        // Koromon starts with 'K' — should use "a"
        string result = JijimonEvolutionCalculatorNarratorText.EvolutionResultCalculated(EvolutionResult.Koromon);

        result.ShouldContain("a Koromon");
    }

    // --- Rookie results ---

    [TestCase(EvolutionResult.Agumon)]
    [TestCase(EvolutionResult.Betamon)]
    [TestCase(EvolutionResult.Biyomon)]
    [TestCase(EvolutionResult.Elecmon)]
    [TestCase(EvolutionResult.Gabumon)]
    [TestCase(EvolutionResult.Patamon)]
    [TestCase(EvolutionResult.Palmon)]
    [TestCase(EvolutionResult.Penguinmon)]
    public void EvolutionResultCalculated_ShouldReturnTextContainingDigimonName_WhenResultIsRookie(EvolutionResult evolutionResult)
    {
        string result = JijimonEvolutionCalculatorNarratorText.EvolutionResultCalculated(evolutionResult);

        result.ShouldContain(evolutionResult.ToString());
    }

    // Rookie-specific named responses
    [Test]
    public void EvolutionResultCalculated_ShouldReturnPenguinmonSpecificText_WhenResultIsPenguinmon()
    {
        string result = JijimonEvolutionCalculatorNarratorText.EvolutionResultCalculated(EvolutionResult.Penguinmon);

        result.ShouldContain("fishy");
    }

    [Test]
    public void EvolutionResultCalculated_ShouldReturnAgumonSpecificText_WhenResultIsAgumon()
    {
        string result = JijimonEvolutionCalculatorNarratorText.EvolutionResultCalculated(EvolutionResult.Agumon);

        result.ShouldContain("chuffed");
    }

    [Test]
    public void EvolutionResultCalculated_ShouldReturnGabumonSpecificText_WhenResultIsGabumon()
    {
        string result = JijimonEvolutionCalculatorNarratorText.EvolutionResultCalculated(EvolutionResult.Gabumon);

        result.ShouldContain("Gabumon");
        result.ShouldNotContain("{evolutionDigimonType}");
    }

    // Rookie default path
    [TestCase(EvolutionResult.Betamon)]
    [TestCase(EvolutionResult.Biyomon)]
    [TestCase(EvolutionResult.Elecmon)]
    [TestCase(EvolutionResult.Patamon)]
    [TestCase(EvolutionResult.Palmon)]
    public void EvolutionResultCalculated_ShouldReturnDefaultRookieText_WhenResultIsUnnamedRookie(EvolutionResult evolutionResult)
    {
        string result = JijimonEvolutionCalculatorNarratorText.EvolutionResultCalculated(evolutionResult);

        result.ShouldContain("grow up so fast");
    }

    // --- Champion results ---

    [TestCase(EvolutionResult.Vegiemon)]
    [TestCase(EvolutionResult.Sukamon)]
    [TestCase(EvolutionResult.Shellmon)]
    [TestCase(EvolutionResult.Ogremon)]
    [TestCase(EvolutionResult.Meramon)]
    [TestCase(EvolutionResult.Leomon)]
    [TestCase(EvolutionResult.Kokatorimon)]
    [TestCase(EvolutionResult.Greymon)]
    [TestCase(EvolutionResult.Garurumon)]
    [TestCase(EvolutionResult.Frigimon)]
    [TestCase(EvolutionResult.Drimogemon)]
    [TestCase(EvolutionResult.Devimon)]
    [TestCase(EvolutionResult.Coelamon)]
    [TestCase(EvolutionResult.Centarumon)]
    [TestCase(EvolutionResult.Birdramon)]
    [TestCase(EvolutionResult.Bakemon)]
    [TestCase(EvolutionResult.Numemon)]
    [TestCase(EvolutionResult.Tyrannomon)]
    public void EvolutionResultCalculated_ShouldReturnTextContainingDigimonName_WhenResultIsChampion(EvolutionResult evolutionResult)
    {
        string result = JijimonEvolutionCalculatorNarratorText.EvolutionResultCalculated(evolutionResult);

        result.ShouldContain(evolutionResult.ToString());
    }

    [TestCase(EvolutionResult.Vegiemon)]
    [TestCase(EvolutionResult.Sukamon)]
    [TestCase(EvolutionResult.Shellmon)]
    [TestCase(EvolutionResult.Ogremon)]
    [TestCase(EvolutionResult.Meramon)]
    [TestCase(EvolutionResult.Leomon)]
    [TestCase(EvolutionResult.Kokatorimon)]
    [TestCase(EvolutionResult.Greymon)]
    [TestCase(EvolutionResult.Garurumon)]
    [TestCase(EvolutionResult.Frigimon)]
    [TestCase(EvolutionResult.Drimogemon)]
    [TestCase(EvolutionResult.Devimon)]
    [TestCase(EvolutionResult.Coelamon)]
    [TestCase(EvolutionResult.Centarumon)]
    [TestCase(EvolutionResult.Birdramon)]
    [TestCase(EvolutionResult.Bakemon)]
    [TestCase(EvolutionResult.Numemon)]
    [TestCase(EvolutionResult.Tyrannomon)]
    public void EvolutionResultCalculated_ShouldContainShowEvolutionResultKeyWord_WhenResultIsChampion(EvolutionResult evolutionResult)
    {
        string result = JijimonEvolutionCalculatorNarratorText.EvolutionResultCalculated(evolutionResult);

        result.ShouldContain(JijimonEvolutionCalculatorNarratorText.ShowEvolutionResultKeyWord);
    }

    // Champion default path (Digimon not in the specific switch cases)
    [TestCase(EvolutionResult.Airdramon)]
    [TestCase(EvolutionResult.Angemon)]
    [TestCase(EvolutionResult.Kabuterimon)]
    [TestCase(EvolutionResult.Kuwagamon)]
    [TestCase(EvolutionResult.Mojyamon)]
    [TestCase(EvolutionResult.Monochromon)]
    [TestCase(EvolutionResult.Nanimon)]
    [TestCase(EvolutionResult.Ninjamon)]
    [TestCase(EvolutionResult.Seadramon)]
    [TestCase(EvolutionResult.Unimon)]
    [TestCase(EvolutionResult.Whamon)]
    [TestCase(EvolutionResult.Panjyamon)]
    public void EvolutionResultCalculated_ShouldReturnDefaultChampionText_WhenResultIsUnnamedChampion(EvolutionResult evolutionResult)
    {
        string result = JijimonEvolutionCalculatorNarratorText.EvolutionResultCalculated(evolutionResult);

        result.ShouldContain("mighty");
        result.ShouldContain(evolutionResult.ToString());
    }

    // --- Ultimate results ---

    [TestCase(EvolutionResult.SkullGreymon)]
    [TestCase(EvolutionResult.Vademon)]
    [TestCase(EvolutionResult.Phoenixmon)]
    [TestCase(EvolutionResult.Monzaemon)]
    [TestCase(EvolutionResult.MetalGreymon)]
    [TestCase(EvolutionResult.Andromon)]
    [TestCase(EvolutionResult.Digitamamon)]
    [TestCase(EvolutionResult.Etemon)]
    [TestCase(EvolutionResult.Giromon)]
    public void EvolutionResultCalculated_ShouldReturnTextContainingDigimonName_WhenResultIsNamedUltimate(EvolutionResult evolutionResult)
    {
        string result = JijimonEvolutionCalculatorNarratorText.EvolutionResultCalculated(evolutionResult);

        result.ShouldContain(evolutionResult.ToString());
    }

    // Ultimate default path
    [TestCase(EvolutionResult.HerculesKabuterimon)]
    [TestCase(EvolutionResult.Mamemon)]
    [TestCase(EvolutionResult.Megadramon)]
    [TestCase(EvolutionResult.MegaSeadramon)]
    [TestCase(EvolutionResult.MetalMamemon)]
    [TestCase(EvolutionResult.Piximon)]
    [TestCase(EvolutionResult.Weregarurumon)]
    [TestCase(EvolutionResult.Gigadramon)]
    [TestCase(EvolutionResult.MetalEtemon)]
    [TestCase(EvolutionResult.Machinedramon)]
    [TestCase(EvolutionResult.Myotismon)]
    public void EvolutionResultCalculated_ShouldReturnDefaultUltimateText_WhenResultIsUnnamedUltimate(EvolutionResult evolutionResult)
    {
        string result = JijimonEvolutionCalculatorNarratorText.EvolutionResultCalculated(evolutionResult);

        result.ShouldContain("ultimate");
        result.ShouldContain(evolutionResult.ToString());
    }

    [TestCase(EvolutionResult.SkullGreymon)]
    [TestCase(EvolutionResult.Vademon)]
    [TestCase(EvolutionResult.Phoenixmon)]
    [TestCase(EvolutionResult.Monzaemon)]
    [TestCase(EvolutionResult.MetalGreymon)]
    [TestCase(EvolutionResult.Andromon)]
    [TestCase(EvolutionResult.Digitamamon)]
    [TestCase(EvolutionResult.Giromon)]
    public void EvolutionResultCalculated_ShouldContainShowEvolutionResultKeyWord_WhenResultIsUltimateWithKeyWord(EvolutionResult evolutionResult)
    {
        string result = JijimonEvolutionCalculatorNarratorText.EvolutionResultCalculated(evolutionResult);

        result.ShouldContain(JijimonEvolutionCalculatorNarratorText.ShowEvolutionResultKeyWord);
    }

    // Etemon ultimate response now contains the ShowEvolutionResultKeyWord (was previously missing)
    [Test]
    public void EvolutionResultCalculated_ShouldContainShowEvolutionResultKeyWord_WhenResultIsEtemon()
    {
        string result = JijimonEvolutionCalculatorNarratorText.EvolutionResultCalculated(EvolutionResult.Etemon);

        result.ShouldContain(JijimonEvolutionCalculatorNarratorText.ShowEvolutionResultKeyWord);
    }

    // --- Monzaemon mentions Numemon ---

    [Test]
    public void EvolutionResultCalculated_ShouldMentionNumemon_WhenResultIsMonzaemon()
    {
        string result = JijimonEvolutionCalculatorNarratorText.EvolutionResultCalculated(EvolutionResult.Monzaemon);

        result.ShouldContain(nameof(DigimonName.Numemon));
    }

    // --- Vegiemon mentions Tyrannomon ---

    [Test]
    public void EvolutionResultCalculated_ShouldMentionTyrannomon_WhenResultIsVegiemon()
    {
        string result = JijimonEvolutionCalculatorNarratorText.EvolutionResultCalculated(EvolutionResult.Vegiemon);

        result.ShouldContain(nameof(DigimonName.Tyrannomon));
    }

    // --- ShowEvolutionResultKeyWord property ---

    [Test]
    public void ShowEvolutionResultKeyWord_ShouldReturnExpectedValue()
    {
        JijimonEvolutionCalculatorNarratorText.ShowEvolutionResultKeyWord.ShouldBe("ShowEvolutionResult");
    }

    // --- IntroText ---

    [Test]
    public void IntroText_ShouldNotBeNullOrEmpty()
    {
        JijimonEvolutionCalculatorNarratorText.IntroText.ShouldNotBeNullOrEmpty();
    }

    [Test]
    public void IntroText_ShouldContainCalculateButtonText()
    {
        JijimonEvolutionCalculatorNarratorText.IntroText.ShouldContain(UiText.CalculateButtonText);
    }

    // --- Edge case: undefined EvolutionResult enum value ---

    [Test]
    public void EvolutionResultCalculated_ShouldThrow_WhenEvolutionResultIsUndefined()
    {
        const EvolutionResult undefinedResult = (EvolutionResult)9999;

        Action act = () => JijimonEvolutionCalculatorNarratorText.EvolutionResultCalculated(undefinedResult);

        act.ShouldThrow<Exception>();
    }
}


