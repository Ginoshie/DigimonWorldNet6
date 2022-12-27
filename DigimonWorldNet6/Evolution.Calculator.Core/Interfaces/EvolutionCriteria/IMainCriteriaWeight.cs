namespace DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;

public interface IMainCriteriaWeight
{
    public int LowerWeightLimit { get; }

    public int WeightTarget { get; }

    public int UpperWeightLimit { get; }
}