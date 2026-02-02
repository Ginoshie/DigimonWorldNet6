using Shared;
using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.DataObjects;

public sealed class UserDigimon
{
    public UserDigimon(DigimonName digimonName, int hp, int mp, int off, int def, int speed, int brains, int careMistakes, int weight, int happiness, int discipline, int battles, int techniqueCount)
    {
        DigimonName = digimonName;
        HP = hp;
        MP = mp;
        Off = off;
        Def = def;
        Speed = speed;
        Brains = brains;
        CareMistakes = careMistakes;
        Weight = weight;
        Happiness = happiness;
        Discipline = discipline;
        Battles = battles;
        TechniqueCount = techniqueCount;

        EvolutionStage = EvolutionStageMapper.Get(digimonName);
    }

    public DigimonName DigimonName
    {
        get;
        private set
        {
            EvolutionStage = EvolutionStageMapper.Get(value);
            field = value;
        }
    }

    public EvolutionStage EvolutionStage { get; private set; }

    public int HP { get; }

    public int MP { get; }

    public int Off { get; }

    public int Def { get; }

    public int Speed { get; }

    public int Brains { get; }

    public int CareMistakes { get; }

    public int Weight { get; }

    public int Happiness { get; }

    public int Discipline { get; }

    public int Battles { get; }

    public int TechniqueCount { get; }
}