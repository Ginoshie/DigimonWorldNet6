using System;
using System.Collections.Generic;
using System.Linq;
using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.Rookie;

public sealed class RookieEvolutionCalculator : IEvolutionCalculator
{
    private readonly RookieEvolutionMapper _rookieEvolutionMapper = new();
    private readonly RookieEvolutionScoreCalculator _rookieEvolutionScoreCalculator = new();

    public EvolutionResult DetermineEvolutionResult(Digimon digimon)
    {
        var evolutionCriteriaOfPossibleEvolutions = _rookieEvolutionMapper[digimon.DigimonType].ToList();

        GuardAgainstCorruptEvolutionCriteria(evolutionCriteriaOfPossibleEvolutions);

        var highestEvolutionScore = 0;
        var evolutionResult = EvolutionResult.None;

        foreach (var evolutionCriteria in evolutionCriteriaOfPossibleEvolutions)
        {
            var evolutionScore = _rookieEvolutionScoreCalculator.CalculateEvolutionScore(digimon, evolutionCriteria.Stats);

            if (evolutionScore <= highestEvolutionScore) continue;
            
            highestEvolutionScore = evolutionScore;
            evolutionResult = (EvolutionResult)evolutionCriteria.DigimonType;
        }

        return evolutionResult;
    }

    private static void GuardAgainstCorruptEvolutionCriteria(IReadOnlyCollection<IEvolutionCriteria> evolutionCriteriaOfPossibleEvolutions)
    {
        var corruptEvolutionOptions =
            evolutionCriteriaOfPossibleEvolutions.Where(evolutionCriteria => evolutionCriteria.EvolutionStage != EvolutionStage.Rookie);
        var formattedCorruptEvolutionOptions = string.Join(", ", corruptEvolutionOptions);
        if (evolutionCriteriaOfPossibleEvolutions.Any(evolutionCriteria => evolutionCriteria.EvolutionStage != EvolutionStage.Rookie))
            throw new ArgumentException(
                $"Evolution options are corrupt, all options should be {EvolutionStage.Rookie}; Corrupt evolution options: {formattedCorruptEvolutionOptions}");
    }
}