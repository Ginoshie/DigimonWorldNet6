namespace DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

public class MainCriteriaCareMistakes
{
    public MainCriteriaCareMistakes(int careMistakes, bool isCareMistakesCriteriaMaximum)
    {
        CareMistakes = careMistakes;
        IsCareMistakesCriteriaAMaximum = isCareMistakesCriteriaMaximum;
    }

    public bool IsCareMistakesCriteriaAMaximum { get; }

    public int CareMistakes { get; }
}