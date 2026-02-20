namespace DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

public sealed class MainCriteriaCareMistakes(int careMistakes = -1, bool isCareMistakesCriteriaMaximum = false)
{
    public bool IsCareMistakesCriteriaAMaximum { get; } = isCareMistakesCriteriaMaximum;

    public int CareMistakes { get; } = careMistakes;
}