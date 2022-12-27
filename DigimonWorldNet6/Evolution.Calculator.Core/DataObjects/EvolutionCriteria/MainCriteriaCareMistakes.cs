using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;

namespace DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

public class MainCriteriaCareMistakes : IMainCriteriaCareMistakes
{
    public MainCriteriaCareMistakes(int careMistakes, bool isCareMistakesCriteriaMaximum)
    {
        CareMistakes = careMistakes;
        IsCareMistakesCriteriaAMaximum = isCareMistakesCriteriaMaximum;
    }

    public bool IsCareMistakesCriteriaAMaximum { get; }

    public int CareMistakes { get; }
}