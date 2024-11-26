using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromInTraining;

public sealed class FromInTrainingEvolutionScoreCalculator
{
    public int CalculateEvolutionScore(Digimon digimon, MainCriteriaStats statsCriteria)
    {
        int highestEvolutionStat = 0;
        int hpScore = digimon.HP / 10;
        int mpScore = digimon.MP / 10;

        if (statsCriteria.HP > 0 && hpScore > highestEvolutionStat)
        {
            highestEvolutionStat = hpScore;
        }

        if (statsCriteria.MP > 0 && mpScore > highestEvolutionStat)
        {
            highestEvolutionStat = mpScore;
        }

        if (statsCriteria.Off > 0 && digimon.Off > highestEvolutionStat)
        {
            highestEvolutionStat = digimon.Off;
        }

        if (statsCriteria.Def > 0 && digimon.Def > highestEvolutionStat)
        {
            highestEvolutionStat = digimon.Def;
        }

        if (statsCriteria.Speed > 0 && digimon.Speed > highestEvolutionStat)
        {
            highestEvolutionStat = digimon.Speed;
        }

        if (statsCriteria.Brains > 0 && digimon.Brains > highestEvolutionStat)
        {
            highestEvolutionStat = digimon.Brains;
        }

        return highestEvolutionStat;
    }
}