namespace DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;

public interface IMainCriteriaCareMistakes
{
    public bool IsCareMistakesCriteriaAMaximum { get; }

    public int CareMistakes { get; }
}