using DigimonWorld.Evolution.Calculator.Core;
using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;
using Evolution.Calculator.Tests.Builder;
using NUnit.Framework;
using Shared.Enums;
using Shouldly;

namespace Evolution.Calculator.Tests.EvolutionCriteriaCalculation.FromRookieOrChampion.SubCalculators;

[TestFixture]
public sealed class WeightCriteriaCalculatorTests
{
    private WeightCriteriaCalculator _sut = null!;

    [SetUp]
    public void SetUp() => _sut = new WeightCriteriaCalculator();

    [Test]
    public void CriteriaIsMet_ReturnsTrue_WhenWeightIsExactlyTarget()
    {
        // Weight target 20 → range [15, 25]
        EvolutionCalculationInput input = BuildInput(weight: 20);
        MainCriteriaWeight criteria = new(20);

        _sut.CriteriaIsMet(input, criteria).ShouldBeTrue();
    }

    [Test]
    public void CriteriaIsMet_ReturnsTrue_WhenWeightIsAtLowerBound()
    {
        EvolutionCalculationInput input = BuildInput(weight: 15);
        MainCriteriaWeight criteria = new(20);

        _sut.CriteriaIsMet(input, criteria).ShouldBeTrue();
    }

    [Test]
    public void CriteriaIsMet_ReturnsTrue_WhenWeightIsAtUpperBound()
    {
        EvolutionCalculationInput input = BuildInput(weight: 25);
        MainCriteriaWeight criteria = new(20);

        _sut.CriteriaIsMet(input, criteria).ShouldBeTrue();
    }

    [Test]
    public void CriteriaIsMet_ReturnsFalse_WhenWeightIsBelowLowerBound()
    {
        EvolutionCalculationInput input = BuildInput(weight: 14);
        MainCriteriaWeight criteria = new(20);

        _sut.CriteriaIsMet(input, criteria).ShouldBeFalse();
    }

    [Test]
    public void CriteriaIsMet_ReturnsFalse_WhenWeightIsAboveUpperBound()
    {
        EvolutionCalculationInput input = BuildInput(weight: 26);
        MainCriteriaWeight criteria = new(20);

        _sut.CriteriaIsMet(input, criteria).ShouldBeFalse();
    }

    [Test]
    public void WeightCriteriaHasFivePointMarginOnEachSide()
    {
        MainCriteriaWeight criteria = new(30);

        criteria.LowerWeightLimit.ShouldBe(25);
        criteria.UpperWeightLimit.ShouldBe(35);
    }

    private static EvolutionCalculationInput BuildInput(int weight = 20)
    {
        return new DigimonBuilder()
            .WithDigimonType(DigimonName.Agumon)
            .WithHp(0).WithMp(0).WithOff(0).WithDef(0).WithSpeed(0).WithBrains(0)
            .WithWeight(weight).WithCareMistakes(0).WithHappiness(0).WithDiscipline(0).WithBattles(0).WithTechniqueCount(0)
            .Build();
    }
}
