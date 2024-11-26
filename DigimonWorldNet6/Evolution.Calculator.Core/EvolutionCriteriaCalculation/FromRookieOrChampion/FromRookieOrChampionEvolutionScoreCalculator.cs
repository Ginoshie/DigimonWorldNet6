using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;

public sealed class FromRookieOrChampionEvolutionScoreCalculator
{
    public int CalculateEvolutionScore(Digimon digimon, MainCriteriaStats statsCriteria)
    {
        int evolutionStatsTotal = 0;
        int evolutionStatCountTotal = 0;

        if (statsCriteria.HP > 0)
        {
            evolutionStatsTotal += digimon.HP / 10;
            evolutionStatCountTotal++;
        }

        if (statsCriteria.MP > 0)
        {
            evolutionStatsTotal += digimon.MP / 10;
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