namespace DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

public sealed class MainCriteriaStats(int hp = 0, int mp = 0, int off = 0, int def = 0, int speed = 0, int brains = 0)
{
    public int HP { get; } = hp;

    public int MP { get; } = mp;

    public int Off { get; } = off;

    public int Def { get; } = def;

    public int Speed { get; } = speed;

    public int Brains { get; } = brains;
}