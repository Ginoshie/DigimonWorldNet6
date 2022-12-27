using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;

namespace DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

public class MainCriteriaWeight : IMainCriteriaWeight
{
    private const int WEIGHT_MARGIN = 5;

    public MainCriteriaWeight(int weight)
    {
        WeightTarget = weight;
    }

    public int LowerWeightLimit => WeightTarget - WEIGHT_MARGIN;

    public int WeightTarget { get; }

    public int UpperWeightLimit => WeightTarget + WEIGHT_MARGIN;
}