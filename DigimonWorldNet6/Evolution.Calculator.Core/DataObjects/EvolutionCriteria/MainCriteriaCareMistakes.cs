namespace DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

public sealed class MainCriteriaCareMistakes
{
    public MainCriteriaCareMistakes(int careMistakes = -1, bool isCareMistakesCriteriaMaximum = false)
    {
        CareMistakes = careMistakes;
        IsCareMistakesCriteriaAMaximum = isCareMistakesCriteriaMaximum;
    }

    public bool IsCareMistakesCriteriaAMaximum { get; }

    public int CareMistakes { get; }
}