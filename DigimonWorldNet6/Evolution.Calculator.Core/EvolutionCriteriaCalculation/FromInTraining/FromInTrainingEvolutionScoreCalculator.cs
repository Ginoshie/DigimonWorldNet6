using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromInTraining;

public sealed class FromInTrainingEvolutionScoreCalculator
{
    public int CalculateEvolutionScore(EvolutionCalculationInput evolutionCalculationInput, MainCriteriaStats statsCriteria)
    {
        int highestEvolutionStat = 0;
        int hpScore = evolutionCalculationInput.HP / 10;
        int mpScore = evolutionCalculationInput.MP / 10;

        if (statsCriteria.HP > 0 && hpScore > highestEvolutionStat)
        {
            highestEvolutionStat = hpScore;
        }

        if (statsCriteria.MP > 0 && mpScore > highestEvolutionStat)
        {
            highestEvolutionStat = mpScore;
        }

        if (statsCriteria.Off > 0 && evolutionCalculationInput.Off > highestEvolutionStat)
        {
            highestEvolutionStat = evolutionCalculationInput.Off;
        }

        if (statsCriteria.Def > 0 && evolutionCalculationInput.Def > highestEvolutionStat)
        {
            highestEvolutionStat = evolutionCalculationInput.Def;
        }

        if (statsCriteria.Speed > 0 && evolutionCalculationInput.Speed > highestEvolutionStat)
        {
            highestEvolutionStat = evolutionCalculationInput.Speed;
        }

        if (statsCriteria.Brains > 0 && evolutionCalculationInput.Brains > highestEvolutionStat)
        {
            highestEvolutionStat = evolutionCalculationInput.Brains;
        }

        return highestEvolutionStat;
    }
}