using System;
using System.Collections.Generic;
using System.Linq;
using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromInTraining;

public sealed class FromInTrainingEvolutionCalculator : IEvolutionCalculator
{
    private readonly FromInTrainingEvolutionMapper _fromInTrainingEvolutionMapper = new();
    private readonly FromInTrainingEvolutionScoreCalculator _fromInTrainingEvolutionScoreCalculator = new();

    public EvolutionResult DetermineEvolutionResult(Digimon digimon)
    {
        if (digimon.EvolutionStage != EvolutionStage.InTraining) throw new ArgumentException($"{digimon.DigimonName} is not a {nameof(EvolutionStage.InTraining)} stage digimon.");
        
        List<IEvolutionCriteria> evolutionCriteriaOfPossibleEvolutions = _fromInTrainingEvolutionMapper[digimon.DigimonName].ToList();

        GuardAgainstCorruptEvolutionCriteria(evolutionCriteriaOfPossibleEvolutions);

        int highestEvolutionScore = 0;
        EvolutionResult evolutionResult = EvolutionResult.None;

        foreach (IEvolutionCriteria evolutionCriteria in evolutionCriteriaOfPossibleEvolutions)
        {
            int evolutionScore = _fromInTrainingEvolutionScoreCalculator.CalculateEvolutionScore(digimon, evolutionCriteria.Stats);

            if (evolutionScore <= highestEvolutionScore) continue;
            
            highestEvolutionScore = evolutionScore;
            evolutionResult = evolutionCriteria.EvolutionResult;
        }

        return evolutionResult;
    }

    private static void GuardAgainstCorruptEvolutionCriteria(IReadOnlyCollection<IEvolutionCriteria> evolutionCriteriaOfPossibleEvolutions)
    {
        IEnumerable<IEvolutionCriteria> corruptEvolutionOptions =
            evolutionCriteriaOfPossibleEvolutions.Where(evolutionCriteria => evolutionCriteria.EvolutionStage != EvolutionStage.Rookie);
        string formattedCorruptEvolutionOptions = string.Join(", ", corruptEvolutionOptions);
        if (evolutionCriteriaOfPossibleEvolutions.Any(evolutionCriteria => evolutionCriteria.EvolutionStage != EvolutionStage.Rookie))
            throw new ArgumentException(
                $"Evolution options are corrupt, all options should be {EvolutionStage.Rookie}; Corrupt evolution options: {formattedCorruptEvolutionOptions}");
    }
}