using System;
using MemoryAccess;
using MemoryAccess.MemoryValues.Digimon;
using Shared;
using Shared.Constants;
using Shared.Enums;
using Shared.Services;
using Shared.Services.Events;

namespace Domain;

public sealed class UserDigimon
{
    internal static UserDigimon Instance { get; } = new();

    private UserDigimon()
    {
        EmulatorLinkEventHub.DigimonParameterStatsSynchronizedObservable.Subscribe(_ => SyncParameterStats());
        EmulatorLinkEventHub.DigimonConditionStatsSynchronizedObservable.Subscribe(_ => SyncConditionStats());
        EmulatorLinkEventHub.DigimonProfileStatsSynchronizedObservable.Subscribe(_ => SyncProfileStats());
        UserConfigurationManager.CurrentEvolutionCalculatorConfig.Subscribe(config => _gameVariant = config.GameVariant);
    }

    internal void Set(DigimonName digimonName, int hp, int mp, int off, int def, int speed, int brains, int careMistakes, int weight, int happiness, int discipline, int battles, int techniqueCount)
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
    }

    private GameVariant _gameVariant = GameVariant.Original;

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

    public int HP { get; private set; }

    public int MP { get; private set; }

    public int Off { get; private set; }

    public int Def { get; private set; }

    public int Speed { get; private set; }

    public int Brains { get; private set; }

    public int CareMistakes { get; private set; }

    public int Weight { get; private set; }

    public int Happiness { get; private set; }

    public int Discipline { get; private set; }

    public int Battles { get; private set; }

    public int TechniqueCount { get; private set; }

    private void SyncParameterStats()
    {
        try
        {
            LiveMemoryReader reader = LiveMemoryReader.Instance;
            if (!reader.Connected)
            {
                return;
            }

            ParameterStats p = reader.ParameterStats;
            HP = p.HP;
            MP = p.MP;
            Off = p.Offense;
            Def = p.Defense;
            Speed = p.Speed;
            Brains = p.Brains;
        } catch
        {
            // Memory data may not be available yet
        }
    }

    private void SyncConditionStats()
    {
        try
        {
            LiveMemoryReader reader = LiveMemoryReader.Instance;
            if (!reader.Connected)
            {
                return;
            }

            ConditionStats c = reader.ConditionStats;
            CareMistakes = c.CareMistakes;
            Happiness = c.Happiness;
            Discipline = c.Discipline;
            Battles = c.Battles;
        } catch
        {
            // Memory data may not be available yet
        }
    }

    private void SyncProfileStats()
    {
        try
        {
            LiveMemoryReader reader = LiveMemoryReader.Instance;
            if (!reader.Connected)
            {
                return;
            }

            ProfileStats pr = reader.ProfileStats;
            byte digimonType = pr.DigimonType;
            if (digimonType == 0)
            {
                return;
            }

            DigimonName = DigimonTypes.Get(digimonType, _gameVariant).DigimonName;
            Weight = pr.Weight;
        } catch
        {
            // Memory data may not be available yet
        }
    }
}
