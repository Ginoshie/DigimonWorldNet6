using DigimonWorld.Evolution.Calculator.Core;
using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;
using Evolution.Calculator.Tests.Builder;
using NUnit.Framework;
using Shared.Enums;
using Shouldly;

namespace Evolution.Calculator.Tests.EvolutionCriteriaCalculation.FromRookieOrChampion.SubCalculators;

[TestFixture]
public sealed class StatsCriteriaCalculatorTests
{
    private StatsCriteriaCalculator _sut = null!;

    [SetUp]
    public void SetUp() => _sut = new StatsCriteriaCalculator();

    [Test]
    public void CriteriaIsMet_ReturnsTrue_WhenAllStatsExceedCriteria()
    {
        EvolutionCalculationInput input = BuildInput(hp: 1000, mp: 1000, off: 100, def: 100, speed: 100, brains: 100);
        MainCriteriaStats criteria = new(hp: 500, mp: 500, off: 50, def: 50, speed: 50, brains: 50);

        _sut.CriteriaIsMet(input, criteria).ShouldBeTrue();
    }

    [Test]
    public void CriteriaIsMet_ReturnsTrue_WhenAllStatsExactlyMeetCriteria()
    {
        EvolutionCalculationInput input = BuildInput(hp: 500, mp: 500, off: 50, def: 50, speed: 50, brains: 50);
        MainCriteriaStats criteria = new(hp: 500, mp: 500, off: 50, def: 50, speed: 50, brains: 50);

        _sut.CriteriaIsMet(input, criteria).ShouldBeTrue();
    }

    [Test]
    public void CriteriaIsMet_ReturnsFalse_WhenHpBelowCriteria()
    {
        EvolutionCalculationInput input = BuildInput(hp: 499, mp: 500, off: 50, def: 50, speed: 50, brains: 50);
        MainCriteriaStats criteria = new(hp: 500);

        _sut.CriteriaIsMet(input, criteria).ShouldBeFalse();
    }

    [Test]
    public void CriteriaIsMet_ReturnsFalse_WhenMpBelowCriteria()
    {
        EvolutionCalculationInput input = BuildInput(mp: 499);
        MainCriteriaStats criteria = new(mp: 500);

        _sut.CriteriaIsMet(input, criteria).ShouldBeFalse();
    }

    [Test]
    public void CriteriaIsMet_ReturnsFalse_WhenOffBelowCriteria()
    {
        EvolutionCalculationInput input = BuildInput(off: 49);
        MainCriteriaStats criteria = new(off: 50);

        _sut.CriteriaIsMet(input, criteria).ShouldBeFalse();
    }

    [Test]
    public void CriteriaIsMet_ReturnsFalse_WhenDefBelowCriteria()
    {
        EvolutionCalculationInput input = BuildInput(def: 49);
        MainCriteriaStats criteria = new(def: 50);

        _sut.CriteriaIsMet(input, criteria).ShouldBeFalse();
    }

    [Test]
    public void CriteriaIsMet_ReturnsFalse_WhenSpeedBelowCriteria()
    {
        EvolutionCalculationInput input = BuildInput(speed: 49);
        MainCriteriaStats criteria = new(speed: 50);

        _sut.CriteriaIsMet(input, criteria).ShouldBeFalse();
    }

    [Test]
    public void CriteriaIsMet_ReturnsFalse_WhenBrainsBelowCriteria()
    {
        EvolutionCalculationInput input = BuildInput(brains: 49);
        MainCriteriaStats criteria = new(brains: 50);

        _sut.CriteriaIsMet(input, criteria).ShouldBeFalse();
    }

    [Test]
    public void CriteriaIsMet_ReturnsTrue_WhenAllCriteriaAreZero()
    {
        EvolutionCalculationInput input = BuildInput();
        MainCriteriaStats criteria = new();

        _sut.CriteriaIsMet(input, criteria).ShouldBeTrue();
    }

    private static EvolutionCalculationInput BuildInput(
        int hp = 0, int mp = 0, int off = 0, int def = 0, int speed = 0, int brains = 0)
    {
        return new DigimonBuilder()
            .WithDigimonType(DigimonName.Agumon)
            .WithHp(hp).WithMp(mp).WithOff(off).WithDef(def).WithSpeed(speed).WithBrains(brains)
            .WithWeight(20).WithCareMistakes(0).WithHappiness(0).WithDiscipline(0).WithBattles(0).WithTechniqueCount(0)
            .Build();
    }
}
