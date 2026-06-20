using System;
using System.Collections.Generic;
using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using Shared.Configuration;
using Shared.Enums;
using Shared.Services;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;

public sealed class FromRookieOrChampionEvolutionScoreCalculator
{
    private bool _dontUseCarriedOverStats;

    public FromRookieOrChampionEvolutionScoreCalculator()
    {
        UserConfigurationManager.CurrentEvolutionCalculatorConfig.Subscribe(OnEvolutionCalculatorConfigChanged);
    }

    private void OnEvolutionCalculatorConfigChanged(EvolutionCalculatorConfig evolutionCalculatorConfig) => _dontUseCarriedOverStats = evolutionCalculatorConfig.GameVariant != GameVariant.Original;

    public EvolutionScoreCalculationResult CalculateEvolutionScore(EvolutionCalculationInput evolutionCalculationInput, MainCriteriaStats statsCriteria, int carriedOverStatTotal, int carriedOverStatCount)
    {
        return CalculateEvolutionScore(
            evolutionCalculationInput.Hp, evolutionCalculationInput.Mp, evolutionCalculationInput.Off, evolutionCalculationInput.Def, evolutionCalculationInput.Speed, evolutionCalculationInput.Brains,
            statsCriteria, carriedOverStatTotal, carriedOverStatCount, !_dontUseCarriedOverStats);
    }

    /// <summary>
    /// Calculates the evolution score for a single evolution given raw user stats and criteria.
    /// </summary>
    public static EvolutionScoreCalculationResult CalculateEvolutionScore(
        int userHp, int userMp, int userOff, int userDef, int userSpeed, int userBrains,
        MainCriteriaStats statsCriteria, int carriedOverStatTotal, int carriedOverStatCount, bool useCarriedOverStats)
    {
        if (!useCarriedOverStats)
        {
            carriedOverStatTotal = 0;
            carriedOverStatCount = 0;
        }

        int evolutionStatsTotal = 0;
        int evolutionStatCountTotal = 0;

        if (statsCriteria.HP > 0)
        {
            evolutionStatsTotal += userHp / 10;
            evolutionStatCountTotal++;
        }

        if (statsCriteria.MP > 0)
        {
            evolutionStatsTotal += userMp / 10;
            evolutionStatCountTotal++;
        }

        if (statsCriteria.Off > 0)
        {
            evolutionStatsTotal += userOff;
            evolutionStatCountTotal++;
        }

        if (statsCriteria.Def > 0)
        {
            evolutionStatsTotal += userDef;
            evolutionStatCountTotal++;
        }

        if (statsCriteria.Speed > 0)
        {
            evolutionStatsTotal += userSpeed;
            evolutionStatCountTotal++;
        }

        if (statsCriteria.Brains > 0)
        {
            evolutionStatsTotal += userBrains;
            evolutionStatCountTotal++;
        }

        int totalCount = evolutionStatCountTotal + carriedOverStatCount;
        return new EvolutionScoreCalculationResult
        {
            EvolutionScore = totalCount > 0 ? (evolutionStatsTotal + carriedOverStatTotal) / totalCount : 0,
            StatTotal = carriedOverStatTotal + evolutionStatsTotal,
            StatCount = carriedOverStatCount + evolutionStatCountTotal
        };
    }

    /// <summary>
    /// Determines the index of the winning evolution from an ordered list of evolutions.
    /// Each evolution is represented by (isEnabled, scoreTotal, statCount).
    /// Returns -1 if no evolution wins.
    /// </summary>
    public static int DetermineWinningEvolutionIndex(IReadOnlyList<(bool IsEnabled, int ScoreTotal, int StatCount)> evolutions, bool useCarriedOverStats)
    {
        int highestScore = 0;
        int winnerIndex = -1;
        int carriedOverTotal = 0;
        int carriedOverCount = 0;

        for (int i = 0; i < evolutions.Count; i++)
        {
            (bool isEnabled, int scoreTotal, int statCount) = evolutions[i];

            if (!isEnabled)
            {
                continue;
            }

            int effectiveTotal = useCarriedOverStats ? carriedOverTotal + scoreTotal : scoreTotal;
            int effectiveCount = useCarriedOverStats ? carriedOverCount + statCount : statCount;
            int cumulativeScore = effectiveCount > 0 ? effectiveTotal / effectiveCount : 0;

            if (cumulativeScore <= highestScore && winnerIndex != -1)
            {
                break;
            }

            highestScore = cumulativeScore;
            carriedOverTotal += scoreTotal;
            carriedOverCount += statCount;
            winnerIndex = i;
        }

        return winnerIndex;
    }
}