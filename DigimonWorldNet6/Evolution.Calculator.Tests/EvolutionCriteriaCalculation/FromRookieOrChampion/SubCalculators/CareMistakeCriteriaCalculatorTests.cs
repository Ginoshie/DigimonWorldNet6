using DigimonWorld.Evolution.Calculator.Core;
using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;
using Evolution.Calculator.Tests.Builder;
using NUnit.Framework;
using Shared.Enums;
using Shouldly;

namespace Evolution.Calculator.Tests.EvolutionCriteriaCalculation.FromRookieOrChampion.SubCalculators;

[TestFixture]
public sealed class CareMistakeCriteriaCalculatorTests
{
    private CareMistakeCriteriaCalculator _sut = null!;

    [SetUp]
    public void SetUp() => _sut = new CareMistakeCriteriaCalculator();

    // ── IsCareMistakesCriteriaAMaximum = true (must be at or below the limit) ──

    [Test]
    public void CriteriaIsMet_ReturnsTrue_WhenCareMistakesAtMaximumLimit()
    {
        EvolutionCalculationInput input = BuildInput(careMistakes: 3);
        MainCriteriaCareMistakes criteria = new(careMistakes: 3, isCareMistakesCriteriaMaximum: true);

        _sut.CriteriaIsMet(input, criteria).ShouldBeTrue();
    }

    [Test]
    public void CriteriaIsMet_ReturnsTrue_WhenCareMistakesBelowMaximumLimit()
    {
        EvolutionCalculationInput input = BuildInput(careMistakes: 2);
        MainCriteriaCareMistakes criteria = new(careMistakes: 3, isCareMistakesCriteriaMaximum: true);

        _sut.CriteriaIsMet(input, criteria).ShouldBeTrue();
    }

    [Test]
    public void CriteriaIsMet_ReturnsFalse_WhenCareMistakesExceedMaximumLimit()
    {
        EvolutionCalculationInput input = BuildInput(careMistakes: 4);
        MainCriteriaCareMistakes criteria = new(careMistakes: 3, isCareMistakesCriteriaMaximum: true);

        _sut.CriteriaIsMet(input, criteria).ShouldBeFalse();
    }

    // ── IsCareMistakesCriteriaAMaximum = false (must be at or above the minimum) ──

    [Test]
    public void CriteriaIsMet_ReturnsTrue_WhenCareMistakesAtMinimumThreshold()
    {
        EvolutionCalculationInput input = BuildInput(careMistakes: 5);
        MainCriteriaCareMistakes criteria = new(careMistakes: 5, isCareMistakesCriteriaMaximum: false);

        _sut.CriteriaIsMet(input, criteria).ShouldBeTrue();
    }

    [Test]
    public void CriteriaIsMet_ReturnsTrue_WhenCareMistakesAboveMinimumThreshold()
    {
        EvolutionCalculationInput input = BuildInput(careMistakes: 6);
        MainCriteriaCareMistakes criteria = new(careMistakes: 5, isCareMistakesCriteriaMaximum: false);

        _sut.CriteriaIsMet(input, criteria).ShouldBeTrue();
    }

    [Test]
    public void CriteriaIsMet_ReturnsFalse_WhenCareMistakesBelowMinimumThreshold()
    {
        EvolutionCalculationInput input = BuildInput(careMistakes: 4);
        MainCriteriaCareMistakes criteria = new(careMistakes: 5, isCareMistakesCriteriaMaximum: false);

        _sut.CriteriaIsMet(input, criteria).ShouldBeFalse();
    }

    private static EvolutionCalculationInput BuildInput(int careMistakes = 0)
    {
        return new DigimonBuilder()
            .WithDigimonType(DigimonName.Agumon)
            .WithHp(0).WithMp(0).WithOff(0).WithDef(0).WithSpeed(0).WithBrains(0)
            .WithWeight(20).WithCareMistakes(careMistakes).WithHappiness(0).WithDiscipline(0).WithBattles(0).WithTechniqueCount(0)
            .Build();
    }
}
