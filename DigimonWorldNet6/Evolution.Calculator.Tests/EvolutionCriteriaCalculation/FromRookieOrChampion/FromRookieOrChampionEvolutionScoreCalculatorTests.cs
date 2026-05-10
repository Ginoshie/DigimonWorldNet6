using System.Collections.Generic;
using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;
using NUnit.Framework;
using Shouldly;

namespace Evolution.Calculator.Tests.EvolutionCriteriaCalculation.FromRookieOrChampion;

[TestFixture]
public sealed class FromRookieOrChampionEvolutionScoreCalculatorTests
{
    // ─── CalculateEvolutionScore ──────────────────────────────────────────────

    [Test]
    public void CalculateEvolutionScore_OnlyCountsStatsWherecriteriaIsGreaterThanZero()
    {
        // criteria only has HP and Off set
        MainCriteriaStats criteria = new(hp: 1000, off: 100);

        EvolutionScoreCalculationResult result = FromRookieOrChampionEvolutionScoreCalculator.CalculateEvolutionScore(
            userHp: 1000, userMp: 2000, userOff: 150, userDef: 200, userSpeed: 300, userBrains: 400,
            criteria, carriedOverStatTotal: 0, carriedOverStatCount: 0, useCarriedOverStats: false);

        // HP/10 = 100, Off = 150 → total 250, count 2 → score 125
        result.EvolutionScore.ShouldBe(125);
        result.CarriedOverStatTotal.ShouldBe(250);
        result.CarriedOverCount.ShouldBe(2);
    }

    [Test]
    public void CalculateEvolutionScore_DividesHpAndMpByTen()
    {
        MainCriteriaStats criteria = new(hp: 1, mp: 1);

        EvolutionScoreCalculationResult result = FromRookieOrChampionEvolutionScoreCalculator.CalculateEvolutionScore(
            userHp: 1000, userMp: 2000, userOff: 0, userDef: 0, userSpeed: 0, userBrains: 0,
            criteria, 0, 0, false);

        // HP/10=100, MP/10=200 → total 300, count 2 → score 150
        result.EvolutionScore.ShouldBe(150);
        result.CarriedOverStatTotal.ShouldBe(300);
        result.CarriedOverCount.ShouldBe(2);
    }

    [Test]
    public void CalculateEvolutionScore_AllSixStatsCounted_WhenAllCriteriaAreSet()
    {
        MainCriteriaStats criteria = new(hp: 1, mp: 1, off: 1, def: 1, speed: 1, brains: 1);

        EvolutionScoreCalculationResult result = FromRookieOrChampionEvolutionScoreCalculator.CalculateEvolutionScore(
            userHp: 1000, userMp: 1000, userOff: 100, userDef: 100, userSpeed: 100, userBrains: 100,
            criteria, 0, 0, false);

        // HP/10=100, MP/10=100, Off=100, Def=100, Speed=100, Brains=100 → total 600, count 6 → score 100
        result.EvolutionScore.ShouldBe(100);
        result.CarriedOverStatTotal.ShouldBe(600);
        result.CarriedOverCount.ShouldBe(6);
    }

    [Test]
    public void CalculateEvolutionScore_WithCarryOver_IncludesPreviousTotal()
    {
        MainCriteriaStats criteria = new(off: 1);

        EvolutionScoreCalculationResult result = FromRookieOrChampionEvolutionScoreCalculator.CalculateEvolutionScore(
            userHp: 0, userMp: 0, userOff: 200, userDef: 0, userSpeed: 0, userBrains: 0,
            criteria, carriedOverStatTotal: 100, carriedOverStatCount: 1, useCarriedOverStats: true);

        // Off=200, carried total=100, carried count=1 → (200+100)/(1+1) = 150
        result.EvolutionScore.ShouldBe(150);
        result.CarriedOverStatTotal.ShouldBe(300);
        result.CarriedOverCount.ShouldBe(2);
    }

    [Test]
    public void CalculateEvolutionScore_WithoutCarryOver_IgnoresPreviousTotal()
    {
        MainCriteriaStats criteria = new(off: 1);

        EvolutionScoreCalculationResult result = FromRookieOrChampionEvolutionScoreCalculator.CalculateEvolutionScore(
            userHp: 0, userMp: 0, userOff: 200, userDef: 0, userSpeed: 0, userBrains: 0,
            criteria, carriedOverStatTotal: 999, carriedOverStatCount: 10, useCarriedOverStats: false);

        // carriedOver ignored → Off=200/1 = 200
        result.EvolutionScore.ShouldBe(200);
        result.CarriedOverStatTotal.ShouldBe(200);
        result.CarriedOverCount.ShouldBe(1);
    }

    [Test]
    public void CalculateEvolutionScore_NoCriteriaSet_ReturnsZeroScore()
    {
        MainCriteriaStats criteria = new();

        EvolutionScoreCalculationResult result = FromRookieOrChampionEvolutionScoreCalculator.CalculateEvolutionScore(
            userHp: 1000, userMp: 1000, userOff: 100, userDef: 100, userSpeed: 100, userBrains: 100,
            criteria, 0, 0, false);

        result.EvolutionScore.ShouldBe(0);
        result.CarriedOverStatTotal.ShouldBe(0);
        result.CarriedOverCount.ShouldBe(0);
    }

    // ─── DetermineWinningEvolutionIndex ──────────────────────────────────────

    [Test]
    public void DetermineWinningEvolutionIndex_ReturnsNegativeOne_WhenNoEvolutionsEnabled()
    {
        List<(bool IsEnabled, int ScoreTotal, int StatCount)> evolutions =
        [
            (false, 300, 3),
            (false, 200, 2),
        ];

        int result = FromRookieOrChampionEvolutionScoreCalculator.DetermineWinningEvolutionIndex(evolutions, useCarriedOverStats: false);

        result.ShouldBe(-1);
    }

    [Test]
    public void DetermineWinningEvolutionIndex_ReturnsIndexOfOnlyEnabledEvolution()
    {
        List<(bool IsEnabled, int ScoreTotal, int StatCount)> evolutions =
        [
            (false, 500, 5),
            (true, 200, 2),
            (false, 300, 3),
        ];

        int result = FromRookieOrChampionEvolutionScoreCalculator.DetermineWinningEvolutionIndex(evolutions, useCarriedOverStats: false);

        result.ShouldBe(1);
    }

    [Test]
    public void DetermineWinningEvolutionIndex_WithoutCarryOver_ReturnsHighestScoreIndex()
    {
        // No carry-over: each evolution scored independently
        List<(bool IsEnabled, int ScoreTotal, int StatCount)> evolutions =
        [
            (true, 100, 2),  // score = 50
            (true, 300, 2),  // score = 150  ← winner
            (true, 200, 2),  // score = 100  (stops checking after score drops)
        ];

        int result = FromRookieOrChampionEvolutionScoreCalculator.DetermineWinningEvolutionIndex(evolutions, useCarriedOverStats: false);

        result.ShouldBe(1);
    }

    [Test]
    public void DetermineWinningEvolutionIndex_WithoutCarryOver_StopsAtFirstNonImprovingEvolution()
    {
        // Third evolution has a higher score than second but algorithm stops at second
        List<(bool IsEnabled, int ScoreTotal, int StatCount)> evolutions =
        [
            (true, 200, 2),  // score = 100  ← becomes winner
            (true, 100, 2),  // score = 50   → not better, stop
            (true, 600, 2),  // score = 300  → never reached
        ];

        int result = FromRookieOrChampionEvolutionScoreCalculator.DetermineWinningEvolutionIndex(evolutions, useCarriedOverStats: false);

        result.ShouldBe(0);
    }

    [Test]
    public void DetermineWinningEvolutionIndex_WithCarryOver_AccumulatesScoresAcrossEvolutions()
    {
        // With carry-over the cumulative average keeps growing as long as next evo's
        // individual score is above the running average.
        // evo0: total=200, count=2 → cumulative 200/2 = 100
        // evo1: total=200+300=500, count=2+3=5 → cumulative 500/5 = 100 (equal, but winnerIndex=-1 initially so it gets set)
        // evo2: total=500+100=600, count=5+1=6 → cumulative 600/6 = 100 → not strictly greater, stop
        List<(bool IsEnabled, int ScoreTotal, int StatCount)> evolutions =
        [
            (true, 200, 2),
            (true, 300, 3),
            (true, 100, 1),
        ];

        int result = FromRookieOrChampionEvolutionScoreCalculator.DetermineWinningEvolutionIndex(evolutions, useCarriedOverStats: true);

        // evo0 wins first, then evo1 sets a new winner (cumulative stays 100 but winnerIndex was -1 initially, then set to 0, then score equals so stops at evo1 check)
        // Let's trace more carefully:
        // i=0: enabled, effectiveTotal=0+200=200, effectiveCount=0+2=2, cumulativeScore=100, 100>0 → winner=0, highestScore=100, carriedTotal=200, carriedCount=2
        // i=1: enabled, effectiveTotal=200+300=500, effectiveCount=2+3=5, cumulativeScore=100, 100<=100 and winnerIndex!=-1 → break
        result.ShouldBe(0);
    }

    [Test]
    public void DetermineWinningEvolutionIndex_WithCarryOver_SecondEvolutionWins_WhenItRaisesTheCumulativeScore()
    {
        // evo0: scoreTotal=100, count=1 → cumulative 100/1=100 → winner=0
        // evo1: effectiveTotal=100+400=500, count=1+1=2 → cumulative 500/2=250 → 250>100 → winner=1
        // evo2: effectiveTotal=500+10=510, count=2+1=3 → cumulative 510/3=170 → 170<=250 → break
        List<(bool IsEnabled, int ScoreTotal, int StatCount)> evolutions =
        [
            (true, 100, 1),
            (true, 400, 1),
            (true, 10, 1),
        ];

        int result = FromRookieOrChampionEvolutionScoreCalculator.DetermineWinningEvolutionIndex(evolutions, useCarriedOverStats: true);

        result.ShouldBe(1);
    }

    [Test]
    public void DetermineWinningEvolutionIndex_SkipsDisabledEvolutions_AndContinuesChecking()
    {
        List<(bool IsEnabled, int ScoreTotal, int StatCount)> evolutions =
        [
            (false, 999, 1),  // disabled
            (true, 200, 2),   // score=100 → winner=1
            (false, 999, 1),  // disabled
            (true, 50, 2),    // score=25 → not better, stop
        ];

        int result = FromRookieOrChampionEvolutionScoreCalculator.DetermineWinningEvolutionIndex(evolutions, useCarriedOverStats: false);

        result.ShouldBe(1);
    }

    [Test]
    public void DetermineWinningEvolutionIndex_WithEmptyList_ReturnsNegativeOne()
    {
        int result = FromRookieOrChampionEvolutionScoreCalculator.DetermineWinningEvolutionIndex([], useCarriedOverStats: false);

        result.ShouldBe(-1);
    }

    [Test]
    public void DetermineWinningEvolutionIndex_AllScoresEqual_ReturnsFirstEnabled()
    {
        List<(bool IsEnabled, int ScoreTotal, int StatCount)> evolutions =
        [
            (true, 100, 1),  // score=100 → winner=0
            (true, 100, 1),  // score=100, equal not strictly greater → stop
        ];

        int result = FromRookieOrChampionEvolutionScoreCalculator.DetermineWinningEvolutionIndex(evolutions, useCarriedOverStats: false);

        result.ShouldBe(0);
    }
}
