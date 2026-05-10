using DigimonWorld.Evolution.Calculator.Core;
using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;
using Evolution.Calculator.Tests.Builder;
using NUnit.Framework;
using Shared.Enums;
using Shouldly;

namespace Evolution.Calculator.Tests.EvolutionCriteriaCalculation.FromRookieOrChampion.SubCalculators;

[TestFixture]
public sealed class BonusCriteriaCalculatorTests
{
    private BonusCriteriaCalculator _sut = null!;

    [SetUp]
    public void SetUp() => _sut = new BonusCriteriaCalculator();

    // ── Happiness ────────────────────────────────────────────────────────────

    [Test]
    public void CriteriaIsMet_ReturnsTrue_WhenHappinessMeetsThreshold()
    {
        EvolutionCalculationInput input = BuildInput(happiness: 80);
        BonusCriteria criteria = new(happiness: 80, discipline: -1, battles: -1);

        _sut.CriteriaIsMet(input, criteria).ShouldBeTrue();
    }

    [Test]
    public void CriteriaIsMet_ReturnsTrue_WhenHappinessExceedsThreshold()
    {
        EvolutionCalculationInput input = BuildInput(happiness: 90);
        BonusCriteria criteria = new(happiness: 80, discipline: -1, battles: -1);

        _sut.CriteriaIsMet(input, criteria).ShouldBeTrue();
    }

    [Test]
    public void CriteriaIsMet_ReturnsFalse_ForHappiness_WhenBelowThresholdAndNothingElseMet()
    {
        EvolutionCalculationInput input = BuildInput(happiness: 79, techniqueCount: 0);
        // discipline=-1 and battles=-1 disabled, techniqueCount=1 so technique won't pass with 0 techniques
        BonusCriteria criteria = new(happiness: 80, discipline: -1, battles: -1, techniqueCount: 1);

        _sut.CriteriaIsMet(input, criteria).ShouldBeFalse();
    }

    // ── Discipline ───────────────────────────────────────────────────────────

    [Test]
    public void CriteriaIsMet_ReturnsTrue_WhenDisciplineMeetsThreshold()
    {
        EvolutionCalculationInput input = BuildInput(discipline: 50);
        BonusCriteria criteria = new(happiness: -1, discipline: 50, battles: -1);

        _sut.CriteriaIsMet(input, criteria).ShouldBeTrue();
    }

    [Test]
    public void CriteriaIsMet_ReturnsFalse_ForDiscipline_WhenBelowThresholdAndNothingElseMet()
    {
        EvolutionCalculationInput input = BuildInput(discipline: 49, techniqueCount: 0);
        BonusCriteria criteria = new(happiness: -1, discipline: 50, battles: -1, techniqueCount: 1);

        _sut.CriteriaIsMet(input, criteria).ShouldBeFalse();
    }

    // ── Battles (maximum) ────────────────────────────────────────────────────

    [Test]
    public void CriteriaIsMet_ReturnsTrue_WhenBattlesAtOrBelowMaximum()
    {
        EvolutionCalculationInput input = BuildInput(battles: 5);
        BonusCriteria criteria = new(happiness: -1, discipline: -1, battles: 10, isBattlesCriteriaAMaximum: true);

        _sut.CriteriaIsMet(input, criteria).ShouldBeTrue();
    }

    [Test]
    public void CriteriaIsMet_ReturnsFalse_WhenBattlesExceedMaximum_AndNothingElseMet()
    {
        EvolutionCalculationInput input = BuildInput(battles: 11, techniqueCount: 0);
        BonusCriteria criteria = new(happiness: -1, discipline: -1, battles: 10, isBattlesCriteriaAMaximum: true, techniqueCount: 1);

        _sut.CriteriaIsMet(input, criteria).ShouldBeFalse();
    }

    // ── Battles (minimum) ────────────────────────────────────────────────────

    [Test]
    public void CriteriaIsMet_ReturnsTrue_WhenBattlesAtOrAboveMinimum()
    {
        EvolutionCalculationInput input = BuildInput(battles: 10);
        BonusCriteria criteria = new(happiness: -1, discipline: -1, battles: 10, isBattlesCriteriaAMaximum: false);

        _sut.CriteriaIsMet(input, criteria).ShouldBeTrue();
    }

    [Test]
    public void CriteriaIsMet_ReturnsFalse_WhenBattlesBelowMinimum_AndNothingElseMet()
    {
        EvolutionCalculationInput input = BuildInput(battles: 9, techniqueCount: 0);
        BonusCriteria criteria = new(happiness: -1, discipline: -1, battles: 10, isBattlesCriteriaAMaximum: false, techniqueCount: 1);

        _sut.CriteriaIsMet(input, criteria).ShouldBeFalse();
    }

    // ── TechniqueCount ───────────────────────────────────────────────────────

    [Test]
    public void CriteriaIsMet_ReturnsTrue_WhenTechniqueCountMeetsThreshold()
    {
        EvolutionCalculationInput input = BuildInput(techniqueCount: 3);
        BonusCriteria criteria = new(happiness: -1, discipline: -1, battles: -1, techniqueCount: 3);

        _sut.CriteriaIsMet(input, criteria).ShouldBeTrue();
    }

    [Test]
    public void CriteriaIsMet_ReturnsTrue_WhenTechniqueCountExceedsThreshold()
    {
        EvolutionCalculationInput input = BuildInput(techniqueCount: 5);
        BonusCriteria criteria = new(happiness: -1, discipline: -1, battles: -1, techniqueCount: 3);

        _sut.CriteriaIsMet(input, criteria).ShouldBeTrue();
    }

    /// <summary>
    /// When techniqueCount criterion is 0, the technique check is always met (any TechniqueCount >= 0).
    /// This is intentional: techniqueCount=0 signals "no technique requirement". It is used for:
    ///  - Trash/fallback evolutions (Numemon, Sukamon, Devimon, Vademon, ...) which have all-zero criteria.
    ///  - Fresh→InTraining and InTraining→Rookie evolutions (Agumon, Betamon, ...) where the Digimon
    ///    has not yet learned techniques and there is no technique requirement at that stage.
    /// </summary>
    [Test]
    public void CriteriaIsMet_ReturnsTrue_WhenTechniqueCountCriteriaIsZero_BecauseZeroMeansNoRequirement()
    {
        EvolutionCalculationInput inputWithZeroTechniques = BuildInput(techniqueCount: 0);
        EvolutionCalculationInput inputWithManyTechniques = BuildInput(techniqueCount: 99);
        BonusCriteria criteria = new(happiness: -1, discipline: -1, battles: -1, techniqueCount: 0);

        _sut.CriteriaIsMet(inputWithZeroTechniques, criteria).ShouldBeTrue();
        _sut.CriteriaIsMet(inputWithManyTechniques, criteria).ShouldBeTrue();
    }

    // ── PrecursorDigimon ─────────────────────────────────────────────────────

    [Test]
    public void CriteriaIsMet_ReturnsTrue_WhenPrecursorDigimonMatches()
    {
        EvolutionCalculationInput input = BuildInput(digimonName: DigimonName.Kunemon, techniqueCount: 0);
        BonusCriteria criteria = new(happiness: -1, discipline: -1, battles: -1, techniqueCount: 1, precursorDigimon: DigimonName.Kunemon);

        _sut.CriteriaIsMet(input, criteria).ShouldBeTrue();
    }

    [Test]
    public void CriteriaIsMet_ReturnsFalse_WhenPrecursorDigimonDoesNotMatch_AndNothingElseMet()
    {
        EvolutionCalculationInput input = BuildInput(digimonName: DigimonName.Palmon, techniqueCount: 0);
        BonusCriteria criteria = new(happiness: -1, discipline: -1, battles: -1, techniqueCount: 1, precursorDigimon: DigimonName.Kunemon);

        _sut.CriteriaIsMet(input, criteria).ShouldBeFalse();
    }

    [Test]
    public void CriteriaIsMet_ReturnsFalse_WhenNoPrecursorSet_AndNothingElseMet()
    {
        EvolutionCalculationInput input = BuildInput(techniqueCount: 0);
        BonusCriteria criteria = new(happiness: -1, discipline: -1, battles: -1, techniqueCount: 1, precursorDigimon: null);

        _sut.CriteriaIsMet(input, criteria).ShouldBeFalse();
    }

    // ── First matching criterion wins (short-circuit) ────────────────────────

    [Test]
    public void CriteriaIsMet_ReturnsTrue_WhenHappinessMet_EvenIfDisciplineNotMet()
    {
        EvolutionCalculationInput input = BuildInput(happiness: 80, discipline: 0);
        BonusCriteria criteria = new(happiness: 80, discipline: 50, battles: -1);

        _sut.CriteriaIsMet(input, criteria).ShouldBeTrue();
    }

    [Test]
    public void CriteriaIsMet_ReturnsTrue_WhenDisciplineMet_EvenIfHappinessNotMet()
    {
        EvolutionCalculationInput input = BuildInput(happiness: 0, discipline: 50, techniqueCount: 0);
        BonusCriteria criteria = new(happiness: 80, discipline: 50, battles: -1, techniqueCount: 1);

        _sut.CriteriaIsMet(input, criteria).ShouldBeTrue();
    }

    private static EvolutionCalculationInput BuildInput(
        DigimonName digimonName = DigimonName.Agumon,
        int happiness = 0,
        int discipline = 0,
        int battles = 0,
        int techniqueCount = 0)
    {
        return new DigimonBuilder()
            .WithDigimonType(digimonName)
            .WithHP(0).WithMP(0).WithOff(0).WithDef(0).WithSpeed(0).WithBrains(0)
            .WithWeight(20).WithCareMistakes(0)
            .WithHappiness(happiness).WithDiscipline(discipline).WithBattles(battles).WithTechniqueCount(techniqueCount)
            .Build();
    }
}

