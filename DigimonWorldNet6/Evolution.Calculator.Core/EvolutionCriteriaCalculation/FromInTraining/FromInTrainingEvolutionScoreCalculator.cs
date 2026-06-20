using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromInTraining;

public sealed class FromInTrainingEvolutionScoreCalculator
{
    /// <summary>
    /// Calculates the evolution score for an InTraining → Rookie evolution.
    /// Per the game guide, the stats requirement for InTraining evolutions only counts as fulfilled
    /// if the Digimon's currently highest stat is one the target evolution requires.
    /// Returns the value of the highest stat if it is required by this evolution (enabling it),
    /// or 0 if the highest stat is not among this evolution's required stats (disabling it).
    /// HP and MP are divided by 10 for comparison purposes.
    /// </summary>
    public int CalculateEvolutionScore(EvolutionCalculationInput evolutionCalculationInput, MainCriteriaStats statsCriteria)
    {
        int hpScore = evolutionCalculationInput.Hp / 10;
        int mpScore = evolutionCalculationInput.Mp / 10;
        int offScore = evolutionCalculationInput.Off;
        int defScore = evolutionCalculationInput.Def;
        int speedScore = evolutionCalculationInput.Speed;
        int brainsScore = evolutionCalculationInput.Brains;

        int overallHighestStat = Max6(hpScore, mpScore, offScore, defScore, speedScore, brainsScore);

        // The stats requirement only counts if the current overall highest stat is one this evolution requires.
        // If so, the score is that stat's value (for priority ordering among enabled evolutions).
        if (statsCriteria.HP > 0 && hpScore == overallHighestStat)
        {
            return hpScore;
        }

        if (statsCriteria.MP > 0 && mpScore == overallHighestStat)
        {
            return mpScore;
        }

        if (statsCriteria.Off > 0 && offScore == overallHighestStat)
        {
            return offScore;
        }

        if (statsCriteria.Def > 0 && defScore == overallHighestStat)
        {
            return defScore;
        }

        if (statsCriteria.Speed > 0 && speedScore == overallHighestStat)
        {
            return speedScore;
        }

        if (statsCriteria.Brains > 0 && brainsScore == overallHighestStat)
        {
            return brainsScore;
        }

        return 0;
    }

    private static int Max6(int a, int b, int c, int d, int e, int f)
    {
        int max = a;
        if (b > max)
        {
            max = b;
        }

        if (c > max)
        {
            max = c;
        }

        if (d > max)
        {
            max = d;
        }

        if (e > max)
        {
            max = e;
        }

        if (f > max)
        {
            max = f;
        }

        return max;
    }
}