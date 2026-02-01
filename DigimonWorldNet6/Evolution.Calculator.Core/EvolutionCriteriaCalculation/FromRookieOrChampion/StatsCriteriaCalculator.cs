using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;

public sealed class StatsCriteriaCalculator
{
    public bool CriteriaIsMet(UserDigimon userDigimon, MainCriteriaStats statsCriteria)
    {
        return userDigimon.HP >= statsCriteria.HP &&
               userDigimon.MP >= statsCriteria.MP &&
               userDigimon.Off >= statsCriteria.Off &&
               userDigimon.Speed >= statsCriteria.Speed &&
               userDigimon.Def >= statsCriteria.Def &&
               userDigimon.Brains >= statsCriteria.Brains;
    }
}