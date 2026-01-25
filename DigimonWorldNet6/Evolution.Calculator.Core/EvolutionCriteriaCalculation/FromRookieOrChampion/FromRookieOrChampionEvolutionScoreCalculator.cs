using System;
using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using Generics.Configuration;
using Generics.Enums;
using Generics.Services;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;

public sealed class FromRookieOrChampionEvolutionScoreCalculator
{
    private bool _dontUseCarriedOverStats;

    public FromRookieOrChampionEvolutionScoreCalculator()
    {
        UserConfigurationManager.CurrentEvolutionCalculatorConfig.Subscribe(OnEvolutionCalculatorConfigChanged);
    }

    private void OnEvolutionCalculatorConfigChanged(EvolutionCalculatorConfig evolutionCalculatorConfig) => _dontUseCarriedOverStats = evolutionCalculatorConfig.EvolutionCalculatorMode != EvolutionCalculatorMode.Original;

    public EvolutionScoreCalculationResult CalculateEvolutionScore(Digimon digimon, MainCriteriaStats statsCriteria, int carriedOverStatTotal, int carriedOverStatCount)
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
            evolutionStatsTotal += digimon.HP / 10;
            evolutionStatCountTotal++;
        }

        if (statsCriteria.MP > 0)
        {
            evolutionStatsTotal += digimon.MP / 10;
            evolutionStatCountTotal++;
        }

        if (statsCriteria.Off > 0)
        {
            evolutionStatsTotal += digimon.Off;
            evolutionStatCountTotal++;
        }

        if (statsCriteria.Def > 0)
        {
            evolutionStatsTotal += digimon.Def;
            evolutionStatCountTotal++;
        }

        if (statsCriteria.Speed > 0)
        {
            evolutionStatsTotal += digimon.Speed;
            evolutionStatCountTotal++;
        }

        if (statsCriteria.Brains > 0)
        {
            evolutionStatsTotal += digimon.Brains;
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