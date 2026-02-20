namespace DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

public sealed class MainCriteriaWeight(int weight)
{
    private const int WEIGHT_MARGIN = 5;

    public int LowerWeightLimit => WeightTarget - WEIGHT_MARGIN;

    public int WeightTarget { get; } = weight;

    public int UpperWeightLimit => WeightTarget + WEIGHT_MARGIN;
}