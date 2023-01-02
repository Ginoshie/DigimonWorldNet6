namespace DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;

public sealed class MainCriteriaStats
{
    public MainCriteriaStats(int hp = 0, int mp = 0, int off = 0, int def = 0, int speed = 0, int brains = 0)
    {
        HP = hp;
        MP = mp;
        Off = off;
        Def = def;
        Speed = speed;
        Brains = brains;
    }

    public int HP { get; }

    public int MP { get; }

    public int Off { get; }

    public int Def { get; }

    public int Speed { get; }

    public int Brains { get; }
}