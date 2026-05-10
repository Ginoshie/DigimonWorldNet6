using DigimonWorld.Evolution.Calculator.Core;
using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromInTraining;
using Evolution.Calculator.Tests.Builder;
using NUnit.Framework;
using Shared.Enums;
using Shouldly;

namespace Evolution.Calculator.Tests.EvolutionCriteriaCalculation.FromInTraining;

[TestFixture]
public sealed class FromInTrainingEvolutionScoreCalculatorTests
{
    private FromInTrainingEvolutionScoreCalculator _sut = null!;

    [SetUp]
    public void SetUp() => _sut = new FromInTrainingEvolutionScoreCalculator();

    // ── Core rule: enabled only when highest stat is in criteria ─────────────

    [Test]
    public void CalculateEvolutionScore_ReturnsHighestStatValue_WhenHighestStatIsRequiredByCriteria()
    {
        // Off=300 is the highest stat; criteria requires Off → evolution enabled, score=300
        EvolutionCalculationInput input = BuildInput(hp: 100, mp: 100, off: 300, def: 50, speed: 50, brains: 50);
        MainCriteriaStats criteria = new(off: 1);

        _sut.CalculateEvolutionScore(input, criteria).ShouldBe(300);
    }

    [Test]
    public void CalculateEvolutionScore_ReturnsZero_WhenHighestStatIsNotRequiredByCriteria()
    {
        // HP/10=200 is highest; criteria only requires Off → evolution NOT enabled
        EvolutionCalculationInput input = BuildInput(hp: 2000, mp: 100, off: 50, def: 50, speed: 50, brains: 50);
        MainCriteriaStats criteria = new(off: 1);

        _sut.CalculateEvolutionScore(input, criteria).ShouldBe(0);
    }

    [Test]
    public void CalculateEvolutionScore_ReturnsZero_WhenNoCriteriaIsSet()
    {
        EvolutionCalculationInput input = BuildInput(hp: 2000, off: 300);
        MainCriteriaStats criteria = new();

        _sut.CalculateEvolutionScore(input, criteria).ShouldBe(0);
    }

    // ── HP and MP are divided by 10 before comparison ────────────────────────

    [Test]
    public void CalculateEvolutionScore_DividesHpByTen_ForHighestStatComparison()
    {
        // HP/10=300; Off=50; HP/10 is highest and HP is in criteria
        EvolutionCalculationInput input = BuildInput(hp: 3000, off: 50);
        MainCriteriaStats criteria = new(hp: 1);

        _sut.CalculateEvolutionScore(input, criteria).ShouldBe(300);
    }

    [Test]
    public void CalculateEvolutionScore_DividesMpByTen_ForHighestStatComparison()
    {
        // MP/10=200; Off=50; MP/10 is highest and MP is in criteria
        EvolutionCalculationInput input = BuildInput(mp: 2000, off: 50);
        MainCriteriaStats criteria = new(mp: 1);

        _sut.CalculateEvolutionScore(input, criteria).ShouldBe(200);
    }

    // ── Tanemon bug scenario ──────────────────────────────────────────────────

    /// <summary>
    /// Documents the "Tanemon bug" from the game guide:
    /// When Offense is the highest stat but no Tanemon evolution requires Offense,
    /// no evolution is enabled so no evolution can occur (score=0 for all evolutions).
    /// Tanemon evolutions: Palmon (MP, Speed, Brains) and Betamon (HP, MP, Def).
    /// </summary>
    [Test]
    public void CalculateEvolutionScore_ReturnsZero_WhenOffenseIsHighest_AndNoCriteriaRequiresOffense()
    {
        // Off=200 is highest; Palmon requires MP, Speed, Brains — not Off
        EvolutionCalculationInput input = BuildInput(hp: 100, mp: 100, off: 200, def: 50, speed: 50, brains: 50);
        MainCriteriaStats palmonCriteria = new(mp: 1, speed: 1, brains: 1);

        _sut.CalculateEvolutionScore(input, palmonCriteria).ShouldBe(0);
    }

    [Test]
    public void CalculateEvolutionScore_ReturnsZero_WhenOffenseIsHighest_AndBetamonCriteria()
    {
        // Off=200 is highest; Betamon requires HP, MP, Def — not Off
        EvolutionCalculationInput input = BuildInput(hp: 100, mp: 100, off: 200, def: 50, speed: 50, brains: 50);
        MainCriteriaStats betamonCriteria = new(hp: 1, mp: 1, def: 1);

        _sut.CalculateEvolutionScore(input, betamonCriteria).ShouldBe(0);
    }

    // ── Tie-breaking: correct evolution wins when highest stat is present in multiple evolutions ──

    [Test]
    public void CalculateEvolutionScore_ReturnsSameScore_WhenTwoEvolutionsShareHighestStat()
    {
        // Both Agumon (HP, MP, Off) and Gabumon (Def, Speed, Brains) could require HP if it's highest.
        // If the evolution requires HP and HP is highest, it returns HP/10.
        EvolutionCalculationInput input = BuildInput(hp: 1000, mp: 800, off: 50, def: 50, speed: 50, brains: 50);
        MainCriteriaStats criteriaWithHp = new(hp: 1, off: 1);
        MainCriteriaStats criteriaWithoutHp = new(def: 1, speed: 1);

        int scoreWithHp = _sut.CalculateEvolutionScore(input, criteriaWithHp);
        int scoreWithoutHp = _sut.CalculateEvolutionScore(input, criteriaWithoutHp);

        scoreWithHp.ShouldBe(100);    // HP/10 = 100, HP is highest → enabled
        scoreWithoutHp.ShouldBe(0);   // HP is highest but not in criteria → disabled
    }

    // ── All six stats ─────────────────────────────────────────────────────────

    [Test]
    [TestCase(500, 0, 0, 0, 0, 0, 50)]   // HP/10=50 is highest, criteria has HP → 50
    [TestCase(0, 500, 0, 0, 0, 0, 50)]   // MP/10=50 is highest, criteria has MP → 50
    [TestCase(0, 0, 75, 0, 0, 0, 75)]   // Off=75 is highest, criteria has Off → 75
    [TestCase(0, 0, 0, 80, 0, 0, 80)]   // Def=80 is highest, criteria has Def → 80
    [TestCase(0, 0, 0, 0, 90, 0, 90)]   // Speed=90 is highest, criteria has Speed → 90
    [TestCase(0, 0, 0, 0, 0, 60, 60)]   // Brains=60 is highest, criteria has Brains → 60
    public void CalculateEvolutionScore_ReturnsCorrectScore_ForEachStat(
        int hp, int mp, int off, int def, int speed, int brains, int expectedScore)
    {
        EvolutionCalculationInput input = BuildInput(hp, mp, off, def, speed, brains);
        // All-inclusive criteria so the test stat is always in criteria
        MainCriteriaStats criteria = new(hp: 1, mp: 1, off: 1, def: 1, speed: 1, brains: 1);

        _sut.CalculateEvolutionScore(input, criteria).ShouldBe(expectedScore);
    }

    private static EvolutionCalculationInput BuildInput(
        int hp = 0, int mp = 0, int off = 0, int def = 0, int speed = 0, int brains = 0)
    {
        return new DigimonBuilder()
            .WithDigimonType(DigimonName.Koromon)
            .WithHP(hp).WithMP(mp).WithOff(off).WithDef(def).WithSpeed(speed).WithBrains(brains)
            .WithWeight(10).WithCareMistakes(0).WithHappiness(0).WithDiscipline(0).WithBattles(0).WithTechniqueCount(0)
            .Build();
    }
}
