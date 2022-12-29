using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.DataObjects;

public class Digimon
{
    public DigimonType DigimonType { get; set; }

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

    public int TechniqueCount { get; }
}