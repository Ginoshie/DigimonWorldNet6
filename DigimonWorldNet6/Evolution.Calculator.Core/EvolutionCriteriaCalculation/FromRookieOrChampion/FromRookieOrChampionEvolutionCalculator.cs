using System;
using System.Collections.Generic;
using System.Linq;
using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;
using Generics.Extensions;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;

public sealed class FromRookieOrChampionEvolutionCalculator : IEvolutionCalculator
{
    private readonly FromRookieOrChampionEvolutionMapper _fromRookieOrChampionEvolutionMapper = new();
    private readonly FromRookieOrChampionEvolutionScoreCalculator _fromRookieOrChampionEvolutionScoreCalculator = new();
    private readonly StatsCriteriaCalculator _statsMainCriteriaCalculator = new();
    private readonly CareMistakeCriteriaCalculator _careMistakesMainCriteriaCalculator = new();
    private readonly WeightCriteriaCalculator _weightMainCriteriaCalculator = new();
    private readonly BonusCriteriaCalculator _bonusCriteriaCalculator = new();

    public EvolutionResult DetermineEvolutionResult(UserDigimon userDigimon)
    {
        if (userDigimon.EvolutionStage is not (EvolutionStage.Rookie or EvolutionStage.Champion))
        {
            throw new ArgumentException($"{userDigimon.DigimonName} is not a {nameof(EvolutionStage.Rookie)} or {nameof(EvolutionStage.Champion)} stage digimon.");
        }

        List<IEvolutionCriteria> evolutionCriteriaOfPossibleEvolutions = _fromRookieOrChampionEvolutionMapper.GetEvolutionCriteria(userDigimon.DigimonName);

        GuardAgainstCorruptEvolutionCriteria(evolutionCriteriaOfPossibleEvolutions);

        int highestEvolutionScore = 0;
        int carriedOverStatTotal = 0;
        int carriedOverStatCount = 0;
        EvolutionResult evolutionResult = EvolutionResult.None;

        foreach (IEvolutionCriteria evolutionCriteria in evolutionCriteriaOfPossibleEvolutions)
        {
            if (!EvolutionEnabled(userDigimon, evolutionCriteria))
            {
                continue;
            }

            EvolutionScoreCalculationResult evolutionScoreCalculationResult = _fromRookieOrChampionEvolutionScoreCalculator.CalculateEvolutionScore(userDigimon, evolutionCriteria.Stats, carriedOverStatTotal, carriedOverStatCount);

            if ((ValidEvolutionResult(evolutionResult) &&
                 CurrentBestEnabledEvolutionIsNewEvolution(evolutionResult.ToDigimonType()) &&
                 NextEvolutionIsHistoricEvolution(evolutionCriteria.EvolutionResult.ToDigimonType())) ||
                evolutionScoreCalculationResult.EvolutionScore <= highestEvolutionScore)
            {
                break;
            }

            highestEvolutionScore = evolutionScoreCalculationResult.EvolutionScore;
            carriedOverStatTotal = evolutionScoreCalculationResult.CarriedOverStatTotal;
            carriedOverStatCount = evolutionScoreCalculationResult.CarriedOverCount;
            evolutionResult = evolutionCriteria.EvolutionResult;
        }

        if (evolutionCriteriaOfPossibleEvolutions.FirstOrDefault()?.EvolutionStage == EvolutionStage.Champion && evolutionResult == EvolutionResult.None)
        {
            evolutionResult = EvolutionResult.Numemon;
        }

        return evolutionResult;
    }

    private void GuardAgainstCorruptEvolutionCriteria(List<IEvolutionCriteria> evolutionCriteriaOfPossibleEvolutions)
    {
        if (evolutionCriteriaOfPossibleEvolutions.All(evolutionCriteria => evolutionCriteria.EvolutionStage == EvolutionStage.Champion) ||
            evolutionCriteriaOfPossibleEvolutions.All(evolutionCriteria => evolutionCriteria.EvolutionStage == EvolutionStage.Ultimate))
        {
            return;
        }

        IEnumerable<IEvolutionCriteria> corruptEvolutionOptions = evolutionCriteriaOfPossibleEvolutions.Where(evolutionCriteria => evolutionCriteria.EvolutionStage is not (EvolutionStage.Champion or EvolutionStage.Ultimate));
        string formattedCorruptEvolutionOptions = string.Join(", ", corruptEvolutionOptions);

        throw new ArgumentException($"Evolution options are corrupt, all options should be either {EvolutionStage.Champion} or {EvolutionStage.Ultimate}; Corrupt evolution options: {formattedCorruptEvolutionOptions}");
    }

    private bool EvolutionEnabled(UserDigimon userDigimon, IEvolutionCriteria evolutionCriteria)
    {
        int criteriaMetCount = 0;

        if (_statsMainCriteriaCalculator.CriteriaIsMet(userDigimon, evolutionCriteria.Stats))
        {
            criteriaMetCount++;
        }

        if (_careMistakesMainCriteriaCalculator.CriteriaIsMet(userDigimon, evolutionCriteria.CareMistakes))
        {
            criteriaMetCount++;
        }

        if (_weightMainCriteriaCalculator.CriteriaIsMet(userDigimon, evolutionCriteria.Weight))
        {
            criteriaMetCount++;
        }

        if (_bonusCriteriaCalculator.CriteriaIsMet(userDigimon, evolutionCriteria.BonusCriteria))
        {
            criteriaMetCount++;
        }

        return criteriaMetCount >= 3;
    }

    private bool ValidEvolutionResult(EvolutionResult evolutionResult) => evolutionResult != EvolutionResult.None;

    private bool CurrentBestEnabledEvolutionIsNewEvolution(DigimonName digimonName) => !Session.HistoricEvolutions.Contains(digimonName);
    private bool NextEvolutionIsHistoricEvolution(DigimonName digimonName) => Session.HistoricEvolutions.Contains(digimonName);
}