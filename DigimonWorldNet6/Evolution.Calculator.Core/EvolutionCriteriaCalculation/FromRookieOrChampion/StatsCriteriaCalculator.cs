using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;

public sealed class StatsCriteriaCalculator
{
    public bool CriteriaIsMet(Digimon digimon, MainCriteriaStats statsCriteria)
    {
        return digimon.HP >= statsCriteria.HP &&
               digimon.MP >= statsCriteria.MP &&
               digimon.Off >= statsCriteria.Off &&
               digimon.Speed >= statsCriteria.Speed &&
               digimon.Def >= statsCriteria.Def &&
               digimon.Brains >= statsCriteria.Brains;
    }
}