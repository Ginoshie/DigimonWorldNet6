using System;
using DigimonWorld.Evolution.Calculator.Core.DataObjects;
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

    public EvolutionScoreCalculationResult CalculateEvolutionScore(UserDigimon userDigimon, MainCriteriaStats statsCriteria, int carriedOverStatTotal, int carriedOverStatCount)
    {
        if (_dontUseCarriedOverStats)
        {
            carriedOverStatTotal = 0;
            carriedOverStatCount = 0;
        }
        
        int evolutionStatsTotal = 0;
        int evolutionStatCountTotal = 0;

        if (statsCriteria.HP > 0)
        {
            evolutionStatsTotal += userDigimon.HP / 10;
            evolutionStatCountTotal++;
        }

        if (statsCriteria.MP > 0)
        {
            evolutionStatsTotal += userDigimon.MP / 10;
            evolutionStatCountTotal++;
        }

        if (statsCriteria.Off > 0)
        {
            evolutionStatsTotal += userDigimon.Off;
            evolutionStatCountTotal++;
        }

        if (statsCriteria.Def > 0)
        {
            evolutionStatsTotal += userDigimon.Def;
            evolutionStatCountTotal++;
        }

        if (statsCriteria.Speed > 0)
        {
            evolutionStatsTotal += userDigimon.Speed;
            evolutionStatCountTotal++;
        }

        if (statsCriteria.Brains > 0)
        {
            evolutionStatsTotal += userDigimon.Brains;
            evolutionStatCountTotal++;
        }

        return new EvolutionScoreCalculationResult
        {
            EvolutionScore = (evolutionStatsTotal + carriedOverStatTotal) / (evolutionStatCountTotal + carriedOverStatCount),
            CarriedOverStatTotal = carriedOverStatTotal + evolutionStatsTotal,
            CarriedOverCount = carriedOverStatCount + evolutionStatCountTotal
        };
    }
}