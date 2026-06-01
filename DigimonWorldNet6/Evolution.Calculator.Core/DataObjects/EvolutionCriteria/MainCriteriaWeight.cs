namespace DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

public sealed class MainCriteriaWeight(int weight)
{
    public MainCriteriaWeight(int weight, int weightMargin) : this(weight)
    {
        _weightMargin = weightMargin;
    }
    
    private readonly int _weightMargin = 5;

    public int LowerWeightLimit => WeightTarget - _weightMargin;

    public int WeightTarget { get; } = weight;

    public int UpperWeightLimit => WeightTarget + _weightMargin;
}