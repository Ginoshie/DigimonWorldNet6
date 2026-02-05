using Shared;
using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.DataObjects;

public sealed class UserDigimon
{
    public UserDigimon() { }
    
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

    public int HP { get; set; }

    public int MP { get; set; }

    public int Off { get; set; }

    public int Def { get; set; }

    public int Speed { get; set; }

    public int Brains { get; set; }

    public int CareMistakes { get; set; }

    public int Weight { get; set; }

    public int Happiness { get; set; }

    public int Discipline { get; set; }

    public int Battles { get; set; }

    public int TechniqueCount { get; set; }
}