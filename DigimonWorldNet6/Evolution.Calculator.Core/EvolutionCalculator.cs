using System;
using System.Collections.Generic;
using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core;

public class EvolutionCalculator : IEvolutionCalculator
{
    private readonly ICriteriaCalculator<MainCriteriaStats> _statsMainCriteriaCalculator;
    private readonly ICriteriaCalculator<MainCriteriaCareMistakes> _careMistakesMainCriteriaCalculator;
    private readonly ICriteriaCalculator<MainCriteriaWeight> _weightMainCriteriaCalculator;
    private readonly ICriteriaCalculator<BonusCriteria> _bonusCriteriaCalculator;
    private readonly Dictionary<DigimonType, IEnumerable<IEvolutionCriteria>> _allEvolutionMappings;

    public EvolutionCalculator(ICriteriaCalculator<MainCriteriaStats> statsMainCriteriaCalculator,
        ICriteriaCalculator<MainCriteriaCareMistakes> careMistakesMainCriteriaCalculator, ICriteriaCalculator<MainCriteriaWeight> weightMainCriteriaCalculator,
        ICriteriaCalculator<BonusCriteria> bonusCriteriaCalculator, IEvolutionMapper evolutionMapper)
    {
        _statsMainCriteriaCalculator = statsMainCriteriaCalculator;
        _careMistakesMainCriteriaCalculator = careMistakesMainCriteriaCalculator;
        _weightMainCriteriaCalculator = weightMainCriteriaCalculator;
        _bonusCriteriaCalculator = bonusCriteriaCalculator;

        _allEvolutionMappings = evolutionMapper.GetAllEvolutionMappings();
    }

    public DigimonType DetermineEvolutionResult(Digimon digimon)
    {
        var evolutionCriteriaOfPossibleEvolutions = _allEvolutionMappings[digimon.DigimonType];

        foreach (var evolutionCriteria in evolutionCriteriaOfPossibleEvolutions)
        {
            var highestEvolutionScore = 0;

            if (EvolutionEnabled(digimon, evolutionCriteria))
            {
                var currentEvolutionScore = calculateEvolutionScore(digimon, evolutionCriteria.Stats);

                // TODO: Continue here.
                if (currentEvolutionScore > highestEvolutionScore)
                {
                    highestEvolutionScore = currentEvolutionScore;
                }
            }
        }
    }

    private bool EvolutionEnabled(Digimon digimon, IEvolutionCriteria evolutionCriteria)
    {
        var criteriaMetCount = 0;

        if (_statsMainCriteriaCalculator.CriteriaIsMet(digimon, evolutionCriteria.Stats)) criteriaMetCount++;
        if (_careMistakesMainCriteriaCalculator.CriteriaIsMet(digimon, evolutionCriteria.CareMistakes)) criteriaMetCount++;
        if (_weightMainCriteriaCalculator.CriteriaIsMet(digimon, evolutionCriteria.Weight)) criteriaMetCount++;
        if (_bonusCriteriaCalculator.CriteriaIsMet(digimon, evolutionCriteria.BonusCriteria)) criteriaMetCount++;

        return criteriaMetCount >= 3;
    }

    private int calculateEvolutionScore(Digimon digimon, MainCriteriaStats statsCriteria)
    {
        var evolutionStatsTotal = 0;
        var evolutionStatCountTotal = 0;

        if (statsCriteria.HP > 0)
        {
            evolutionStatsTotal += digimon.HP;
            evolutionStatCountTotal++;
        }

        if (statsCriteria.MP > 0)
        {
            evolutionStatsTotal += digimon.MP;
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
        
        return evolutionStatsTotal / evolutionStatCountTotal;
    }
}