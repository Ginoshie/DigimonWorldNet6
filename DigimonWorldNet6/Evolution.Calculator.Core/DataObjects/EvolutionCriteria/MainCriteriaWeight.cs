namespace DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

public sealed class MainCriteriaWeight
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