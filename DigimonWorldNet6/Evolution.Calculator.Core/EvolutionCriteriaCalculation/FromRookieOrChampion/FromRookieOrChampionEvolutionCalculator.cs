using System;
using System.Collections.Generic;
using System.Linq;
using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;

public sealed class FromRookieOrChampionEvolutionCalculator : IEvolutionCalculator
{
    private readonly FromRookieOrChampionEvolutionMapper _fromRookieOrChampionEvolutionMapper = new();
    private readonly FromRookieOrChampionEvolutionScoreCalculator _fromRookieOrChampionEvolutionScoreCalculator = new();
    private readonly StatsCriteriaCalculator _statsMainCriteriaCalculator = new ();
    private readonly CareMistakeCriteriaCalculator _careMistakesMainCriteriaCalculator = new ();
    private readonly WeightCriteriaCalculator _weightMainCriteriaCalculator = new ();
    private readonly BonusCriteriaCalculator _bonusCriteriaCalculator = new ();

    public EvolutionResult DetermineEvolutionResult(Digimon digimon)
    {
        var evolutionCriteriaOfPossibleEvolutions = _fromRookieOrChampionEvolutionMapper[digimon.DigimonType].ToList();

        GuardAgainstCorruptEvolutionCriteria(evolutionCriteriaOfPossibleEvolutions);

        var highestEvolutionScore = 0;
        var evolutionResult = EvolutionResult.None;

        foreach (var evolutionCriteria in evolutionCriteriaOfPossibleEvolutions)
        {
            if (!EvolutionEnabled(digimon, evolutionCriteria)) continue;

            var currentEvolutionScore = _fromRookieOrChampionEvolutionScoreCalculator.CalculateEvolutionScore(digimon, evolutionCriteria.Stats);
            if (currentEvolutionScore <= highestEvolutionScore) break;

            highestEvolutionScore = currentEvolutionScore;
            evolutionResult = (EvolutionResult)evolutionCriteria.DigimonType;
        }

        if (evolutionCriteriaOfPossibleEvolutions.FirstOrDefault()?.EvolutionStage == EvolutionStage.Champion && evolutionResult == EvolutionResult.None)
        {
            evolutionResult = EvolutionResult.Numemon;
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