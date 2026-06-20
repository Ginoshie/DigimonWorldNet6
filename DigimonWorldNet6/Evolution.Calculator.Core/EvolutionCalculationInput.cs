using Domain;
using Shared;
using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core;

public sealed class EvolutionCalculationInput(DigimonName digimonName, int hp, int mp, int off, int def, int speed, int brains, int careMistakes, int weight, int happiness, int discipline, int battles, int techniqueCount)
{
    public EvolutionCalculationInput(UserDigimon userDigimon)
        : this(userDigimon.DigimonName, userDigimon.Hp, userDigimon.Mp, userDigimon.Off, userDigimon.Def, userDigimon.Speed, userDigimon.Brains, userDigimon.CareMistakes, userDigimon.Weight, userDigimon.Happiness, userDigimon.Discipline, userDigimon.Battles, userDigimon.TechniqueCount)
    {
    }

    public DigimonName DigimonName { get; } = digimonName;
    public EvolutionStage EvolutionStage { get; } = EvolutionStageMapper.Get(digimonName);
    public int Hp { get; } = hp;
    public int Mp { get; } = mp;
    public int Off { get; } = off;
    public int Def { get; } = def;
    public int Speed { get; } = speed;
    public int Brains { get; } = brains;
    public int CareMistakes { get; } = careMistakes;
    public int Weight { get; } = weight;
    public int Happiness { get; } = happiness;
    public int Discipline { get; } = discipline;
    public int Battles { get; } = battles;
    public int TechniqueCount { get; } = techniqueCount;
}


