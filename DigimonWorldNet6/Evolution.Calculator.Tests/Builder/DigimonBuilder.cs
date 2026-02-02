using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using Shared.Enums;

namespace Evolution.Calculator.Tests.Builder;

public sealed class DigimonBuilder
{
    private DigimonName _digimonName;
    private int _hp;
    private int _mp;
    private int _off;
    private int _def;
    private int _speed;
    private int _brains;
    private int _careMistakes;
    private int _weight;
    private int _happiness;
    private int _discipline;
    private int _battles;
    private int _techniqueCount;

    public DigimonBuilder WithDigimonType(DigimonName digimonName)
    {
        _digimonName = digimonName;

        return this;
    }

    public DigimonBuilder WithHP(int hp)
    {
        _hp = hp;
        return this;
    }

    public DigimonBuilder WithMP(int mp)
    {
        _mp = mp;
        return this;
    }

    public DigimonBuilder WithOff(int off)
    {
        _off = off;
        return this;
    }

    public DigimonBuilder WithDef(int def)
    {
        _def = def;
        return this;
    }

    public DigimonBuilder WithSpeed(int speed)
    {
        _speed = speed;
        return this;
    }

    public DigimonBuilder WithBrains(int brains)
    {
        _brains = brains;
        return this;
    }

    public DigimonBuilder WithCareMistakes(int careMistakes)
    {
        _careMistakes = careMistakes;
        return this;
    }

    public DigimonBuilder WithWeight(int weight)
    {
        _weight = weight;
        return this;
    }

    public DigimonBuilder WithHappiness(int happiness)
    {
        _happiness = happiness;
        return this;
    }

    public DigimonBuilder WithDiscipline(int discipline)
    {
        _discipline = discipline;
        return this;
    }

    public DigimonBuilder WithBattles(int battles)
    {
        _battles = battles;
        return this;
    }

    public DigimonBuilder WithTechniqueCount(int techniqueCount)
    {
        _techniqueCount = techniqueCount;
        return this;
    }


    public UserDigimon Build()
    {
        return new UserDigimon(_digimonName, _hp, _mp, _off, _def, _speed, _brains, _careMistakes, _weight, _happiness, _discipline, _battles, _techniqueCount);
    }
}