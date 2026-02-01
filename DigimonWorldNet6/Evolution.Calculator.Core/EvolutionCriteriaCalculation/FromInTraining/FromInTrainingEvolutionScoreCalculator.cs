using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromInTraining;

public sealed class FromInTrainingEvolutionScoreCalculator
{
    public int CalculateEvolutionScore(UserDigimon userDigimon, MainCriteriaStats statsCriteria)
    {
        int highestEvolutionStat = 0;
        int hpScore = userDigimon.HP / 10;
        int mpScore = userDigimon.MP / 10;

        if (statsCriteria.HP > 0 && hpScore > highestEvolutionStat)
        {
            highestEvolutionStat = hpScore;
        }

        if (statsCriteria.MP > 0 && mpScore > highestEvolutionStat)
        {
            highestEvolutionStat = mpScore;
        }

        if (statsCriteria.Off > 0 && userDigimon.Off > highestEvolutionStat)
        {
            highestEvolutionStat = userDigimon.Off;
        }

        if (statsCriteria.Def > 0 && userDigimon.Def > highestEvolutionStat)
        {
            highestEvolutionStat = userDigimon.Def;
        }

        if (statsCriteria.Speed > 0 && userDigimon.Speed > highestEvolutionStat)
        {
            highestEvolutionStat = userDigimon.Speed;
        }

        if (statsCriteria.Brains > 0 && userDigimon.Brains > highestEvolutionStat)
        {
            highestEvolutionStat = userDigimon.Brains;
        }

        return highestEvolutionStat;
    }
}