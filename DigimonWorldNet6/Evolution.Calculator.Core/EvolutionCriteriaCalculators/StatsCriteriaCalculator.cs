using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculators;

public class StatsCriteriaCalculator : ICriteriaCalculator
{
    public bool CriteriaIsMet(Digimon digimon, IEvolutionCriteria evolutionCriteria)
    {
        var statsCriteria = evolutionCriteria.Stats;

        return digimon.HP >= statsCriteria.HP &&
               digimon.MP >= statsCriteria.MP &&
               digimon.Off >= statsCriteria.Off &&
               digimon.Speed >= statsCriteria.Speed &&
               digimon.Def >= statsCriteria.Def &&
               digimon.Brains >= statsCriteria.Brains;
    }
}