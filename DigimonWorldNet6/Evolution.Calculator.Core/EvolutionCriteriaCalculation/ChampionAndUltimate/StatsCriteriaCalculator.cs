using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.ChampionAndUltimate;

public sealed class StatsCriteriaCalculator : ICriteriaCalculator<MainCriteriaStats>
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