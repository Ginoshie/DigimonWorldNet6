using System;
using System.Collections.Generic;
using System.Linq;
using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.ChampionAndUltimate;

public sealed class ChampionAndUltimateEvolutionCalculator : IEvolutionCalculator
{
    private readonly ChampionAndUltimateEvolutionMapper _championAndUltimateEvolutionMapper = new();
    private readonly ChampionAndUltimateEvolutionScoreCalculator _championAndUltimateEvolutionScoreCalculator = new();
    private readonly ICriteriaCalculator<MainCriteriaStats> _statsMainCriteriaCalculator = new StatsCriteriaCalculator();
    private readonly ICriteriaCalculator<MainCriteriaCareMistakes> _careMistakesMainCriteriaCalculator = new CareMistakeCriteriaCalculator();
    private readonly ICriteriaCalculator<MainCriteriaWeight> _weightMainCriteriaCalculator = new WeightCriteriaCalculator();
    private readonly ICriteriaCalculator<BonusCriteria> _bonusCriteriaCalculator = new BonusCriteriaCalculator();

    public DigimonType DetermineEvolutionResult(Digimon digimon)
    {
        var evolutionCriteriaOfPossibleEvolutions = _championAndUltimateEvolutionMapper[digimon.DigimonType].ToList();

        GuardAgainstCorruptEvolutionCriteria(evolutionCriteriaOfPossibleEvolutions);

        var highestEvolutionScore = 0;
        var evolutionResult = DigimonType.None;

        foreach (var evolutionCriteria in evolutionCriteriaOfPossibleEvolutions)
        {
            if (!EvolutionEnabled(digimon, evolutionCriteria)) continue;

            var currentEvolutionScore = _championAndUltimateEvolutionScoreCalculator.CalculateEvolutionScore(digimon, evolutionCriteria.Stats);
            if (currentEvolutionScore <= highestEvolutionScore) break;

            highestEvolutionScore = currentEvolutionScore;
            evolutionResult = evolutionCriteria.DigimonType;
        }

        if (evolutionCriteriaOfPossibleEvolutions.FirstOrDefault()?.EvolutionStage == EvolutionStage.Champion && evolutionResult == DigimonType.None)
        {
            evolutionResult = DigimonType.Numemon;
        }

        return evolutionResult;
    }

    private static void GuardAgainstCorruptEvolutionCriteria(List<IEvolutionCriteria> evolutionCriteriaOfPossibleEvolutions)
    {
        if (!(evolutionCriteriaOfPossibleEvolutions.All(evolutionCriteria => evolutionCriteria.EvolutionStage == EvolutionStage.Champion) ||
              evolutionCriteriaOfPossibleEvolutions.All(evolutionCriteria => evolutionCriteria.EvolutionStage == EvolutionStage.Ultimate))
           )
        {
            var corruptEvolutionOptions =
                evolutionCriteriaOfPossibleEvolutions.Where(evolutionCriteria => evolutionCriteria.EvolutionStage is not (EvolutionStage.Champion or EvolutionStage.Ultimate));
            var formattedCorruptEvolutionOptions = string.Join(", ", corruptEvolutionOptions);
            throw new ArgumentException(
                $"Evolution options are corrupt, all options should be either {EvolutionStage.Champion} or {EvolutionStage.Ultimate}; Corrupt evolution options: {formattedCorruptEvolutionOptions}");
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
}