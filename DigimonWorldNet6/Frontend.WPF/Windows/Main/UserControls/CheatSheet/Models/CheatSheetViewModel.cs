using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.Enums;
using DigimonWorld.Frontend.WPF.Services;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using MemoryAccess;
using MemoryAccess.MemoryValues.Digimon;
using MemoryAccess.MemoryValues.Tamer;
using MemoryAccess.MemoryValues.Technical;
using MemoryAccess.MemoryValues.World;
using Shared.Enums;
using Shared.Services;
using Shared.Services.Events;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.CheatSheet.Models;

public class CheatSheetViewModel : BaseViewModel, IDisposable
{
    private const int NO_EVOLUTION_VALUE = 65535;
    private const int UPGRADE_TRAINED_THRESHOLD = 10;
    private const int FREEZE_INTERVAL_MS = 100;

    private readonly SpeakingSimulator _speakingSimulator;
    private readonly List<IRefreshable> _refreshables = [];
    private readonly List<ILockableValue> _lockables;
    private readonly CompositeDisposable _subscriptions;
    private int _visibleInventorySlotCount = -1;

    public CheatSheetViewModel()
    {
        Func<Recruitment> recruitment = () => LiveMemoryReader.Instance.Recruitment;

        RecruitmentZoneViewModel[] zones =
        [
            new("Native Forest",
            [
                new MemoryValueViewModel("Agumon", () => recruitment().Agumon, v => recruitment().Agumon = v),
                new MemoryValueViewModel("Kunemon", () => recruitment().Kunemon, v => recruitment().Kunemon = v),
                new MemoryValueViewModel("Palmon", () => recruitment().Palmon, v => recruitment().Palmon = v),
                new MemoryValueViewModel("Coelamon", () => recruitment().Coelamon, v => recruitment().Coelamon = v),
                new MemoryValueViewModel("Seadramon", () => recruitment().Seadramon, v => recruitment().Seadramon = v),
                new MemoryValueViewModel("Etemon", () => recruitment().Etemon, v => recruitment().Etemon = v),
                new MemoryValueViewModel("Ninjamon", () => recruitment().Ninjamon, v => recruitment().Ninjamon = v)
            ]),
            new("Beetle Land",
            [
                new MemoryValueViewModel("Kuwagamon", () => recruitment().Kuwagamon, v => recruitment().Kuwagamon = v),
                new MemoryValueViewModel("Kabuterimon", () => recruitment().Kabuterimon, v => recruitment().Kabuterimon = v)
            ]),
            new("File City",
            [
                new MemoryValueViewModel("Greymon", () => recruitment().Greymon, v => recruitment().Greymon = v),
                new MemoryValueViewModel("Airdramon", () => recruitment().Airdramon, v => recruitment().Airdramon = v)
            ]),
            new("Mt. Infinity",
            [
                new MemoryValueViewModel("Devimon", () => recruitment().Devimon, v => recruitment().Devimon = v),
                new MemoryValueViewModel("Megadramon", () => recruitment().Megadramon, v => recruitment().Megadramon = v),
                new MemoryValueViewModel("MetalGreymon", () => recruitment().MetalGreymon, v => recruitment().MetalGreymon = v),
                new MemoryValueViewModel("Digitamamon", () => recruitment().Digitamamon, v => recruitment().Digitamamon = v)
            ]),
            new("Random Encounters",
            [
                new MemoryValueViewModel("Piximon", () => recruitment().Piximon, v => recruitment().Piximon = v),
                new MemoryValueViewModel("Mamemon", () => recruitment().Mamemon, v => recruitment().Mamemon = v),
                new MemoryValueViewModel("MetalMamemon", () => recruitment().MetalMamemon, v => recruitment().MetalMamemon = v)
            ]),
            new("Drill Tunnel",
            [
                new MemoryValueViewModel("Drimogemon", () => recruitment().Drimogemon, v => recruitment().Drimogemon = v),
                new MemoryValueViewModel("Meramon", () => recruitment().Meramon, v => recruitment().Meramon = v)
            ]),
            new("Gear Savanna",
            [
                new MemoryValueViewModel("Elecmon", () => recruitment().Elecmon, v => recruitment().Elecmon = v),
                new MemoryValueViewModel("Patamon", () => recruitment().Patamon, v => recruitment().Patamon = v),
                new MemoryValueViewModel("Biyomon", () => recruitment().Biyomon, v => recruitment().Biyomon = v),
                new MemoryValueViewModel("Sukamon", () => recruitment().Sukamon, v => recruitment().Sukamon = v),
                new MemoryValueViewModel("Leomon", () => recruitment().Leomon, v => recruitment().Leomon = v)
            ]),
            new("Tropical Jungle",
            [
                new MemoryValueViewModel("Vegiemon", () => recruitment().Vegiemon, v => recruitment().Vegiemon = v),
                new MemoryValueViewModel("Centarumon", () => recruitment().Centarumon, v => recruitment().Centarumon = v),
                new MemoryValueViewModel("Tyrannomon", () => recruitment().Tyrannomon, v => recruitment().Tyrannomon = v),
                new MemoryValueViewModel("Betamon", () => recruitment().Betamon, v => recruitment().Betamon = v),
                new MemoryValueViewModel("Bakemon", () => recruitment().Bakemon, v => recruitment().Bakemon = v)
            ]),
            new("Mt. Panorama",
            [
                new MemoryValueViewModel("Unimon", () => recruitment().Unimon, v => recruitment().Unimon = v),
                new MemoryValueViewModel("Vademon", () => recruitment().Vademon, v => recruitment().Vademon = v)
            ]),
            new("Misty Trees",
            [
                new MemoryValueViewModel("Gabumon", () => recruitment().Gabumon, v => recruitment().Gabumon = v),
                new MemoryValueViewModel("Kokatorimon", () => recruitment().Kokatorimon, v => recruitment().Kokatorimon = v)
            ]),
            new("Toy Town",
            [
                new MemoryValueViewModel("Monzaemon", () => recruitment().Monzaemon, v => recruitment().Monzaemon = v)
            ]),
            new("Factorial Town",
            [
                new MemoryValueViewModel("Andromon", () => recruitment().Andromon, v => recruitment().Andromon = v),
                new MemoryValueViewModel("Giromon", () => recruitment().Giromon, v => recruitment().Giromon = v),
                new MemoryValueViewModel("Numemon", () => recruitment().Numemon, v => recruitment().Numemon = v)
            ]),
            new("Greylord Mansion",
            [
                new MemoryValueViewModel("SkullGreymon", () => recruitment().SkullGreymon, v => recruitment().SkullGreymon = v)
            ]),
            new("Great Canyon",
            [
                new MemoryValueViewModel("Monochromon", () => recruitment().Monochromon, v => recruitment().Monochromon = v),
                new MemoryValueViewModel("Birdramon", () => recruitment().Birdramon, v => recruitment().Birdramon = v),
                new MemoryValueViewModel("Shellmon", () => recruitment().Shellmon, v => recruitment().Shellmon = v)
            ]),
            new("Freezeland",
            [
                new MemoryValueViewModel("Penguinmon", () => recruitment().Penguinmon, v => recruitment().Penguinmon = v),
                new MemoryValueViewModel("Mojyamon", () => recruitment().Mojyamon, v => recruitment().Mojyamon = v),
                new MemoryValueViewModel("Whamon", () => recruitment().Whamon, v => recruitment().Whamon = v),
                new MemoryValueViewModel("Angemon", () => recruitment().Angemon, v => recruitment().Angemon = v),
                new MemoryValueViewModel("Frigimon", () => recruitment().Frigimon, v => recruitment().Frigimon = v),
                new MemoryValueViewModel("Garurumon", () => recruitment().Garurumon, v => recruitment().Garurumon = v)
            ]),
            new("Moving Recruitments",
            [
                new MemoryValueViewModel("Nanimon", () => recruitment().Nanimon, v => recruitment().Nanimon = v),
                new MemoryValueViewModel("Ogremon", () => recruitment().Ogremon, v => recruitment().Ogremon = v)
            ])
        ];

        List<RecruitmentZoneViewModel> leftZones = [];
        List<RecruitmentZoneViewModel> rightZones = [];
        int leftCount = 0;
        int rightCount = 0;
        foreach (RecruitmentZoneViewModel zone in zones.OrderByDescending(z => z.Recruitments.Count))
        {
            if (leftCount <= rightCount)
            {
                leftZones.Add(zone);
                leftCount += zone.Recruitments.Count;
            }
            else
            {
                rightZones.Add(zone);
                rightCount += zone.Recruitments.Count;
            }
        }

        RecruitmentZonesLeft = leftZones;
        RecruitmentZonesRight = rightZones;

        Func<TechniqueStats> technique = () => LiveMemoryReader.Instance.TechniqueStats;

        TechniqueGroups =
        [
            new TechniqueCategoryViewModel("Fire",
            [
                new MemoryValueViewModel("1. Fire Tower", () => technique().FireTower, v => technique().FireTower = v),
                new MemoryValueViewModel("2. Prominence Beam", () => technique().ProminenceBeam, v => technique().ProminenceBeam = v),
                new MemoryValueViewModel("3. Spit Fire", () => technique().SpitFire, v => technique().SpitFire = v),
                new MemoryValueViewModel("4. Red Inferno", () => technique().RedInferno, v => technique().RedInferno = v),
                new MemoryValueViewModel("5. Magma Bomb", () => technique().MagmaBomb, v => technique().MagmaBomb = v),
                new MemoryValueViewModel("6. Heat Laser", () => technique().HeatLaser, v => technique().HeatLaser = v),
                new MemoryValueViewModel("7. Infinity Burn", () => technique().InfinityBurn, v => technique().InfinityBurn = v),
                new MemoryValueViewModel("8. Meltdown", () => technique().Meltdown, v => technique().Meltdown = v)
            ]),
            new TechniqueCategoryViewModel("Battle",
            [
                new MemoryValueViewModel("1. Tremar", () => technique().Tremar, v => technique().Tremar = v),
                new MemoryValueViewModel("2. Muscle Charge", () => technique().MuscleCharge, v => technique().MuscleCharge = v),
                new MemoryValueViewModel("3. War Cry", () => technique().WarCry, v => technique().WarCry = v),
                new MemoryValueViewModel("4. Sonic Jab", () => technique().SonicJab, v => technique().SonicJab = v),
                new MemoryValueViewModel("5. Dynamite Kick", () => technique().DynamiteKick, v => technique().DynamiteKick = v),
                new MemoryValueViewModel("6. Counter", () => technique().Counter, v => technique().Counter = v),
                new MemoryValueViewModel("7. Megaton Punch", () => technique().MegatonPunch, v => technique().MegatonPunch = v),
                new MemoryValueViewModel("8. Buster Dive", () => technique().BusterDive, v => technique().BusterDive = v)
            ]),
            new TechniqueCategoryViewModel("Air",
            [
                new MemoryValueViewModel("1. Thunder Justice", () => technique().ThunderJustice, v => technique().ThunderJustice = v),
                new MemoryValueViewModel("2. Spinning Shot", () => technique().SpinningShot, v => technique().SpinningShot = v),
                new MemoryValueViewModel("3. Electric Cloud", () => technique().ElectricCloud, v => technique().ElectricCloud = v),
                new MemoryValueViewModel("4. Megalo Spark", () => technique().MegaloSpark, v => technique().MegaloSpark = v),
                new MemoryValueViewModel("5. Static Elect", () => technique().StaticElect, v => technique().StaticElect = v),
                new MemoryValueViewModel("6. Wind Cutter", () => technique().WindCutter, v => technique().WindCutter = v),
                new MemoryValueViewModel("7. Confused Storm", () => technique().ConfusedStorm, v => technique().ConfusedStorm = v),
                new MemoryValueViewModel("8. Hurricane", () => technique().Hurricane, v => technique().Hurricane = v)
            ]),
            new TechniqueCategoryViewModel("Earth",
            [
                new MemoryValueViewModel("1. Poison Powder", () => technique().PoisonPowder, v => technique().PoisonPowder = v),
                new MemoryValueViewModel("2. Bug", () => technique().Bug, v => technique().Bug = v),
                new MemoryValueViewModel("3. Mass Morph", () => technique().MassMorph, v => technique().MassMorph = v),
                new MemoryValueViewModel("4. Insect Plague", () => technique().InsectPlague, v => technique().InsectPlague = v),
                new MemoryValueViewModel("5. Charm Perfume", () => technique().CharmPerfume, v => technique().CharmPerfume = v),
                new MemoryValueViewModel("6. Poison Claw", () => technique().PoisonClaw, v => technique().PoisonClaw = v),
                new MemoryValueViewModel("7. Danger Sting", () => technique().DangerSting, v => technique().DangerSting = v),
                new MemoryValueViewModel("8. Green Trap", () => technique().GreenTrap, v => technique().GreenTrap = v)
            ]),
            new TechniqueCategoryViewModel("Ice",
            [
                new MemoryValueViewModel("1. Giga Freeze", () => technique().GigaFreeze, v => technique().GigaFreeze = v),
                new MemoryValueViewModel("2. Ice Statue", () => technique().IceStatue, v => technique().IceStatue = v),
                new MemoryValueViewModel("3. Winter Blast", () => technique().WinterBlast, v => technique().WinterBlast = v),
                new MemoryValueViewModel("4. Ice Needle", () => technique().IceNeedle, v => technique().IceNeedle = v),
                new MemoryValueViewModel("5. Water Blit", () => technique().WaterBlit, v => technique().WaterBlit = v),
                new MemoryValueViewModel("6. Aqua Magic", () => technique().AquaMagic, v => technique().AquaMagic = v),
                new MemoryValueViewModel("7. Aurora Freeze", () => technique().AuroraFreeze, v => technique().AuroraFreeze = v),
                new MemoryValueViewModel("8. Tear Drop", () => technique().TearDrop, v => technique().TearDrop = v)
            ]),
            new TechniqueCategoryViewModel("Mech",
            [
                new MemoryValueViewModel("1. Power Crane", () => technique().PowerCrane, v => technique().PowerCrane = v),
                new MemoryValueViewModel("2. All Range Beam", () => technique().AllRangeBeam, v => technique().AllRangeBeam = v),
                new MemoryValueViewModel("3. Metal Sprinter", () => technique().MetalSprinter, v => technique().MetalSprinter = v),
                new MemoryValueViewModel("4. Pulse Laser", () => technique().PulseLaser, v => technique().PulseLaser = v),
                new MemoryValueViewModel("5. Delete Program", () => technique().DeleteProgram, v => technique().DeleteProgram = v),
                new MemoryValueViewModel("6. DG Dimension", () => technique().DgDimension, v => technique().DgDimension = v),
                new MemoryValueViewModel("7. Full Potential", () => technique().FullPotential, v => technique().FullPotential = v),
                new MemoryValueViewModel("8. Reverse Program", () => technique().ReverseProgram, v => technique().ReverseProgram = v)
            ]),
            new TechniqueCategoryViewModel("Filth",
            [
                new MemoryValueViewModel("1. Odor Spray", () => technique().OdorSpray, v => technique().OdorSpray = v),
                new MemoryValueViewModel("2. Poop Spd Toss", () => technique().PoopSpdToss, v => technique().PoopSpdToss = v),
                new MemoryValueViewModel("3. Big Poop Toss", () => technique().BigPoopToss, v => technique().BigPoopToss = v),
                new MemoryValueViewModel("4. Big Rnd Toss", () => technique().BigRndToss, v => technique().BigRndToss = v),
                new MemoryValueViewModel("5. Poop Rnd Toss", () => technique().PoopRndToss, v => technique().PoopRndToss = v),
                new MemoryValueViewModel("6. Rnd Spd Toss", () => technique().RndSpdToss, v => technique().RndSpdToss = v),
                new MemoryValueViewModel("7. Horizontal Kick", () => technique().HorizontalKick, v => technique().HorizontalKick = v),
                new MemoryValueViewModel("8. Ultimate Poop Hell", () => technique().UltimatePoopHell, v => technique().UltimatePoopHell = v)
            ])
        ];

        Func<Tamer> tamer = () => LiveMemoryReader.Instance.Tamer;

        TamerLevel = new NumericMemoryValueViewModel("Level", () => tamer().TamerLevel, v => tamer().TamerLevel = v);
        TamerBits = new NumericMemoryValueViewModel("Bits", () => tamer().Bits, v => tamer().Bits = v);
        TamerMeritPoints = new NumericMemoryValueViewModel("Merit Points", () => tamer().MeritPoints, v => tamer().MeritPoints = v);

        Func<Technical> technical = () => LiveMemoryReader.Instance.Technical;

        TechnicalRng = new LongMemoryValueViewModel("RNG", () => technical().CurrentRng, v => technical().CurrentRng = (uint)v);
        TechnicalAgeInDays = new NumericMemoryValueViewModel("Age (days)", () => technical().AgeInDays, v => technical().AgeInDays = v);
        TechnicalEvolutionAgeInHours = new NumericMemoryValueViewModel("Evolution Age (h)", () => technical().EvolutionAgeInHours, v => technical().EvolutionAgeInHours = v);
        TechnicalUpgradeCounterHp = new MemoryValueViewModel("Upgrade HP", () => technical().UpgradeCounterHp >= UPGRADE_TRAINED_THRESHOLD, v => technical().UpgradeCounterHp = v ? UPGRADE_TRAINED_THRESHOLD : 0);
        TechnicalUpgradeCounterMp = new MemoryValueViewModel("Upgrade MP", () => technical().UpgradeCounterMp >= UPGRADE_TRAINED_THRESHOLD, v => technical().UpgradeCounterMp = v ? UPGRADE_TRAINED_THRESHOLD : 0);
        TechnicalUpgradeCounterOffense = new MemoryValueViewModel("Upgrade Offense", () => technical().UpgradeCounterOffense >= UPGRADE_TRAINED_THRESHOLD, v => technical().UpgradeCounterOffense = v ? UPGRADE_TRAINED_THRESHOLD : 0);
        TechnicalUpgradeCounterDefense = new MemoryValueViewModel("Upgrade Defense", () => technical().UpgradeCounterDefense >= UPGRADE_TRAINED_THRESHOLD, v => technical().UpgradeCounterDefense = v ? UPGRADE_TRAINED_THRESHOLD : 0);
        TechnicalUpgradeCounterSpeed = new MemoryValueViewModel("Upgrade Speed", () => technical().UpgradeCounterSpeed >= UPGRADE_TRAINED_THRESHOLD, v => technical().UpgradeCounterSpeed = v ? UPGRADE_TRAINED_THRESHOLD : 0);
        TechnicalGoldenPoopEnabled = new MemoryValueViewModel("Golden Poop Enabled", () => technical().GoldenPoopEnabled, v => technical().GoldenPoopEnabled = v);

        Func<WorldTime> worldTime = () => LiveMemoryReader.Instance.WorldTime;

        WorldYear = new NumericMemoryValueViewModel("Year", () => worldTime().Year, v => worldTime().Year = v);
        WorldDay = new NumericMemoryValueViewModel("Day", () => worldTime().Day, v => worldTime().Day = v);
        WorldHour = new NumericMemoryValueViewModel("Hour", () => worldTime().Hour, v => worldTime().Hour = v);
        WorldMinute = new NumericMemoryValueViewModel("Minute", () => worldTime().Minute, v => worldTime().Minute = v);

        EvolveTargetOption[] evolveTargets =
        [
            new(NO_EVOLUTION_VALUE, "No Evolution"),
            new(0, "Tamer"),
            new(1, "Botamon"),
            new(2, "Koromon"),
            new(3, "Agumon"),
            new(4, "Betamon"),
            new(5, "Greymon"),
            new(6, "Devimon"),
            new(7, "Airdramon"),
            new(8, "Tyrannomon"),
            new(9, "Meramon"),
            new(10, "Seadramon"),
            new(11, "Numemon"),
            new(12, "MetalGreymon"),
            new(13, "Mamemon"),
            new(14, "Monzaemon"),
            new(15, "Punimon"),
            new(16, "Tsunomon"),
            new(17, "Gabumon"),
            new(18, "Elecmon"),
            new(19, "Kabuterimon"),
            new(20, "Angemon"),
            new(21, "Birdramon"),
            new(22, "Garurumon"),
            new(23, "Frigimon"),
            new(24, "Whamon"),
            new(25, "Vegiemon"),
            new(26, "SkullGreymon"),
            new(27, "MetalMamemon"),
            new(28, "Vademon"),
            new(29, "Poyomon"),
            new(30, "Tokomon"),
            new(31, "Patamon"),
            new(32, "Kunemon"),
            new(33, "Unimon"),
            new(34, "Ogremon"),
            new(35, "Shellmon"),
            new(36, "Centarumon"),
            new(37, "Bakemon"),
            new(38, "Drimogemon"),
            new(39, "Sukamon"),
            new(40, "Andromon"),
            new(41, "Giromon"),
            new(42, "Etemon"),
            new(43, "Yuramon"),
            new(44, "Tanemon"),
            new(45, "Biyomon"),
            new(46, "Palmon"),
            new(47, "Monochromon"),
            new(48, "Leomon"),
            new(49, "Coelamon"),
            new(50, "Kokatorimon"),
            new(51, "Kuwagamon"),
            new(52, "Mojamon"),
            new(53, "Nanimon"),
            new(54, "Megadramon"),
            new(55, "Piximon"),
            new(56, "Digitamamon"),
            new(57, "Penguinmon"),
            new(58, "Ninjamon"),
            new(59, "Phoenixmon"),
            new(60, "HerculesKabuterimon"),
            new(61, "MegaSeadramon"),
            new(63, "Panjamon"),
            new(64, "Gigadramon"),
            new(65, "MetalEtemon"),
            new(65288, "Brachiomon"),
            new(65304, "Tekkamon"),
            new(65391, "RedVegiemon"),
            new(65396, "Gotsumon"),
            new(65420, "IceDevimon")
        ];

        EvolveTargets =
        [
            .. evolveTargets
                .OrderBy(target => target.Value == NO_EVOLUTION_VALUE ? 0 : 1)
                .ThenBy(target => target.Name)
        ];

        SelectedEvolveTarget = EvolveTargets.FirstOrDefault(target => target.Value == technical().EvolveTrigger) ?? EvolveTargets[0];

        Func<ParameterStats> parameter = () => LiveMemoryReader.Instance.ParameterStats;

        StatOffense = new NumericMemoryValueViewModel("Offense", () => parameter().Offense, v => parameter().Offense = (short)v);
        StatDefense = new NumericMemoryValueViewModel("Defense", () => parameter().Defense, v => parameter().Defense = (short)v);
        StatSpeed = new NumericMemoryValueViewModel("Speed", () => parameter().Speed, v => parameter().Speed = (short)v);
        StatBrains = new NumericMemoryValueViewModel("Brains", () => parameter().Brains, v => parameter().Brains = (short)v);
        StatHp = new NumericMemoryValueViewModel("HP", () => parameter().Hp, v => parameter().Hp = (short)v);
        StatMp = new NumericMemoryValueViewModel("MP", () => parameter().Mp, v => parameter().Mp = (short)v);
        StatCurrentHp = new NumericMemoryValueViewModel("Current HP", () => parameter().CurrentHp, v => parameter().CurrentHp = (short)v);
        StatCurrentMp = new NumericMemoryValueViewModel("Current MP", () => parameter().CurrentMp, v => parameter().CurrentMp = (short)v);

        Func<ProfileStats> profile = () => LiveMemoryReader.Instance.ProfileStats;

        ProfileWeight = new NumericMemoryValueViewModel("Weight", () => profile().Weight, v => profile().Weight = (short)v);
        ProfileLives = new NumericMemoryValueViewModel("Lives", () => profile().Lives, v => profile().Lives = v);

        Func<CombatStats> combat = () => LiveMemoryReader.Instance.CombatStats;

        CombatFinisherGoal = new NumericMemoryValueViewModel("Finisher Goal", () => combat().FinisherGoal, v => combat().FinisherGoal = (short)v);
        CombatFinisherProgress = new NumericMemoryValueViewModel("Finisher Progress", () => combat().FinisherProgress, v => combat().FinisherProgress = (short)v);
        CombatPoisonTimer = new NumericMemoryValueViewModel("Poison Timer", () => combat().PoisonTimer, v => combat().PoisonTimer = (short)v);
        CombatConfusedTimer = new NumericMemoryValueViewModel("Confused Timer", () => combat().ConfusedTimer, v => combat().ConfusedTimer = (short)v);
        CombatStunTimer = new NumericMemoryValueViewModel("Stun Timer", () => combat().StunTimer, v => combat().StunTimer = (short)v);
        CombatFlattenTimer = new NumericMemoryValueViewModel("Flatten Timer", () => combat().FlattenTimer, v => combat().FlattenTimer = (short)v);
        CombatFlattenAttackTimer = new NumericMemoryValueViewModel("Flatten Attack Timer", () => combat().FlattenAttackTimer, v => combat().FlattenAttackTimer = (short)v);
        CombatCooldown = new NumericMemoryValueViewModel("Cooldown", () => combat().Cooldown, v => combat().Cooldown = v);
        CombatDumbTimer = new NumericMemoryValueViewModel("Dumb Timer", () => combat().DumbTimer, v => combat().DumbTimer = (short)v);
        CombatStatusEffects = new NumericMemoryValueViewModel("Status Effects", () => combat().StatusEffects, v => combat().StatusEffects = v);

        Func<InventoryStats> inventory = () => LiveMemoryReader.Instance.InventoryStats;

        InventorySize = new NumericMemoryValueViewModel("Inventory Size", () => inventory().InventorySize, v => inventory().InventorySize = v);
        InventorySize.PropertyChanged += (_, _) => UpdateVisibleInventorySlots();

        List<InventorySlotViewModel> inventorySlots = [];
        for (int i = 0; i < InventoryStats.SLOT_COUNT; i++)
        {
            int slot = i;
            inventorySlots.Add(new InventorySlotViewModel(
                slot + 1,
                () => inventory().GetItemType(slot), v => inventory().SetItemType(slot, v),
                () => inventory().GetItemAmount(slot), v => inventory().SetItemAmount(slot, v)));
        }

        InventorySlots = inventorySlots;

        Func<CareStats> careStats = () => LiveMemoryReader.Instance.CareStats;

        CarePoopLevel = new NumericMemoryValueViewModel("Poop Level", () => careStats().PoopLevel, v => careStats().PoopLevel = v);
        CareVirusBar = new NumericMemoryValueViewModel("Virus Bar", () => careStats().VirusBar, v => careStats().VirusBar = v);
        CarePoopingTimer = new NumericMemoryValueViewModel("Pooping Timer", () => careStats().PoopingTimer, v => careStats().PoopingTimer = v);
        CareEnergyLevel = new NumericMemoryValueViewModel("Energy Level", () => careStats().EnergyLevel, v => careStats().EnergyLevel = v);
        CareHungryTimer = new NumericMemoryValueViewModel("Hungry Timer", () => careStats().HungryTimer, v => careStats().HungryTimer = v);
        CareStarvationTimer = new NumericMemoryValueViewModel("Starvation Timer", () => careStats().StarvationTimer, v => careStats().StarvationTimer = v);
        CareLifespan = new NumericMemoryValueViewModel("Lifespan", () => careStats().Lifespan, v => careStats().Lifespan = v);

        Func<ConditionStats> condition = () => LiveMemoryReader.Instance.ConditionStats;

        ConditionTiredness = new NumericMemoryValueViewModel("Tiredness", () => condition().Tiredness, v => condition().Tiredness = (short)v);
        ConditionHappiness = new NumericMemoryValueViewModel("Happiness", () => condition().Happiness, v => condition().Happiness = (short)v);
        ConditionDiscipline = new NumericMemoryValueViewModel("Discipline", () => condition().Discipline, v => condition().Discipline = (short)v);
        ConditionCareMistakes = new NumericMemoryValueViewModel("Care Mistakes", () => condition().CareMistakes, v => condition().CareMistakes = (short)v);
        ConditionBattles = new NumericMemoryValueViewModel("Battles", () => condition().Battles, v => condition().Battles = (short)v);

        ConditionFlags =
        [
            new MemoryValueViewModel("Sleepy", () => condition().Sleepy, v => condition().Sleepy = v),
            new MemoryValueViewModel("Tired", () => condition().Tired, v => condition().Tired = v),
            new MemoryValueViewModel("Hungry", () => condition().Hungry, v => condition().Hungry = v),
            new MemoryValueViewModel("Poopy", () => condition().Poop, v => condition().Poop = v),
            new MemoryValueViewModel("Unhappy", () => condition().Unhappy, v => condition().Unhappy = v),
            new MemoryValueViewModel("Injured", () => condition().Injured, v => condition().Injured = v),
            new MemoryValueViewModel("Sick", () => condition().Sick, v => condition().Sick = v)
        ];

        _speakingSimulator = new SpeakingSimulator();
        InstantDisplayCommand = new CommandHandler(InstantDisplay);

        SpeechDelay initialDelay = UserConfigurationManager.SpeakingSimulatorConfig.NarratorMode == NarratorMode.Instant
            ? SpeechDelay.None
            : SpeechDelay.Long;

        _ = _speakingSimulator.SpeakAsync(PiximonCheatSheetNarratorText.INTRO_TEXT, text => PiximonText = text, initialDelay);

        SelectSectionCommand = new RelayCommand<string>(section => SelectedSection = section);

        _refreshables.AddRange(RecruitmentZonesLeft.Concat(RecruitmentZonesRight).SelectMany(zone => zone.Recruitments));
        _refreshables.AddRange(TechniqueGroups.SelectMany(group => group.Techniques));
        _refreshables.AddRange(ConditionFlags);
        _refreshables.AddRange(InventorySlots.SelectMany(s => new[] { s.ItemType, s.ItemAmount }));
        _refreshables.AddRange(
        [
            CombatFinisherGoal, CombatFinisherProgress, CombatPoisonTimer, CombatConfusedTimer, CombatStunTimer,
            CombatFlattenTimer, CombatFlattenAttackTimer, CombatCooldown, CombatDumbTimer, CombatStatusEffects,
            InventorySize
        ]);
        _refreshables.AddRange(
        [
            TamerLevel, TamerBits, TamerMeritPoints, TechnicalRng, TechnicalAgeInDays, TechnicalEvolutionAgeInHours,
            TechnicalUpgradeCounterHp, TechnicalUpgradeCounterMp, TechnicalUpgradeCounterOffense, TechnicalUpgradeCounterDefense, TechnicalUpgradeCounterSpeed, TechnicalGoldenPoopEnabled,
            ProfileWeight, ProfileLives,
            WorldYear, WorldDay, WorldHour, WorldMinute,
            CarePoopLevel, CareVirusBar, CarePoopingTimer, CareEnergyLevel, CareHungryTimer, CareStarvationTimer, CareLifespan,
            ConditionTiredness, ConditionHappiness, ConditionDiscipline, ConditionCareMistakes, ConditionBattles,
            StatOffense, StatDefense, StatSpeed, StatBrains, StatHp, StatMp, StatCurrentHp, StatCurrentMp
        ]);

        _lockables = _refreshables.OfType<ILockableValue>().ToList();

        _subscriptions = new CompositeDisposable(
            Observable.Merge(
                    EmulatorLinkEventHub.RecruitmentSynchronizedObservable,
                    EmulatorLinkEventHub.DigimonTechniqueStatsSynchronizedObservable,
                    EmulatorLinkEventHub.DigimonConditionStatsSynchronizedObservable,
                    EmulatorLinkEventHub.DigimonCareStatsSynchronizedObservable,
                    EmulatorLinkEventHub.DigimonParameterStatsSynchronizedObservable,
                    EmulatorLinkEventHub.DigimonProfileStatsSynchronizedObservable,
                    EmulatorLinkEventHub.DigimonCombatStatsSynchronizedObservable,
                    EmulatorLinkEventHub.InventorySynchronizedObservable)
                .ObserveOn(SynchronizationContext.Current!)
                .Subscribe(_ => RefreshValues()),
            Observable.Interval(TimeSpan.FromMilliseconds(FREEZE_INTERVAL_MS))
                .ObserveOn(SynchronizationContext.Current!)
                .Subscribe(_ => PushLockedValuesToMemory()));

        UpdateVisibleInventorySlots();
    }

    public IReadOnlyList<RecruitmentZoneViewModel> RecruitmentZonesLeft { get; }

    public IReadOnlyList<RecruitmentZoneViewModel> RecruitmentZonesRight { get; }

    public NumericMemoryValueViewModel TamerLevel { get; }

    public NumericMemoryValueViewModel TamerBits { get; }

    public NumericMemoryValueViewModel TamerMeritPoints { get; }

    public LongMemoryValueViewModel TechnicalRng { get; }

    public NumericMemoryValueViewModel WorldYear { get; }

    public NumericMemoryValueViewModel WorldDay { get; }

    public NumericMemoryValueViewModel WorldHour { get; }

    public NumericMemoryValueViewModel WorldMinute { get; }

    public IReadOnlyList<EvolveTargetOption> EvolveTargets { get; }

    public EvolveTargetOption? SelectedEvolveTarget
    {
        get;
        set
        {
            if (!SetField(ref field, value))
            {
                return;
            }

            if (value is not null)
            {
                LiveMemoryReader.Instance.Technical.EvolveTrigger = value.Value;
            }
        }
    }

    public NumericMemoryValueViewModel TechnicalAgeInDays { get; }

    public NumericMemoryValueViewModel TechnicalEvolutionAgeInHours { get; }

    public MemoryValueViewModel TechnicalUpgradeCounterHp { get; }

    public MemoryValueViewModel TechnicalUpgradeCounterMp { get; }

    public MemoryValueViewModel TechnicalUpgradeCounterOffense { get; }

    public MemoryValueViewModel TechnicalUpgradeCounterDefense { get; }

    public MemoryValueViewModel TechnicalUpgradeCounterSpeed { get; }

    public MemoryValueViewModel TechnicalGoldenPoopEnabled { get; }

    public NumericMemoryValueViewModel ProfileWeight { get; }

    public NumericMemoryValueViewModel ProfileLives { get; }

    public NumericMemoryValueViewModel CombatFinisherGoal { get; }

    public NumericMemoryValueViewModel CombatFinisherProgress { get; }

    public NumericMemoryValueViewModel CombatPoisonTimer { get; }

    public NumericMemoryValueViewModel CombatConfusedTimer { get; }

    public NumericMemoryValueViewModel CombatStunTimer { get; }

    public NumericMemoryValueViewModel CombatFlattenTimer { get; }

    public NumericMemoryValueViewModel CombatFlattenAttackTimer { get; }

    public NumericMemoryValueViewModel CombatCooldown { get; }

    public NumericMemoryValueViewModel CombatDumbTimer { get; }

    public NumericMemoryValueViewModel CombatStatusEffects { get; }

    public NumericMemoryValueViewModel InventorySize { get; }

    public IReadOnlyList<InventorySlotViewModel> InventorySlots { get; }

    public ObservableCollection<InventorySlotViewModel> InventorySlotsColumn1 { get; } = [];

    public ObservableCollection<InventorySlotViewModel> InventorySlotsColumn2 { get; } = [];

    public ObservableCollection<InventorySlotViewModel> InventorySlotsColumn3 { get; } = [];

    public ICommand InstantDisplayCommand { get; }

    public ICommand SelectSectionCommand { get; }

    public string SelectedSection
    {
        get;
        private set
        {
            if (field == value)
            {
                return;
            }

            field = value;
            OnPropertyChanged(nameof(IsRecruitmentSelected));
            OnPropertyChanged(nameof(IsTechniquesSelected));
            OnPropertyChanged(nameof(IsDigimonSelected));
            OnPropertyChanged(nameof(IsTechnicalSelected));
            OnPropertyChanged(nameof(IsTamerSelected));
            OnPropertyChanged(nameof(IsCombatSelected));
        }
    } = "Recruitment";

    public bool IsRecruitmentSelected => SelectedSection == "Recruitment";

    public bool IsTechniquesSelected => SelectedSection == "Techniques";

    public bool IsDigimonSelected => SelectedSection == "Digimon";

    public bool IsTechnicalSelected => SelectedSection == "Technical";

    public bool IsTamerSelected => SelectedSection == "Tamer";

    public bool IsCombatSelected => SelectedSection == "Combat";

    public string PiximonText
    {
        get;
        private set => SetField(ref field, value);
    } = string.Empty;

    public NumericMemoryValueViewModel StatOffense { get; }

    public NumericMemoryValueViewModel StatDefense { get; }

    public NumericMemoryValueViewModel StatSpeed { get; }

    public NumericMemoryValueViewModel StatBrains { get; }

    public NumericMemoryValueViewModel StatHp { get; }

    public NumericMemoryValueViewModel StatMp { get; }

    public NumericMemoryValueViewModel StatCurrentHp { get; }

    public NumericMemoryValueViewModel StatCurrentMp { get; }

    public NumericMemoryValueViewModel CarePoopLevel { get; }

    public NumericMemoryValueViewModel CareVirusBar { get; }

    public NumericMemoryValueViewModel CarePoopingTimer { get; }

    public NumericMemoryValueViewModel CareEnergyLevel { get; }

    public NumericMemoryValueViewModel CareHungryTimer { get; }

    public NumericMemoryValueViewModel CareStarvationTimer { get; }

    public NumericMemoryValueViewModel CareLifespan { get; }

    public NumericMemoryValueViewModel ConditionTiredness { get; }

    public NumericMemoryValueViewModel ConditionHappiness { get; }

    public NumericMemoryValueViewModel ConditionDiscipline { get; }

    public NumericMemoryValueViewModel ConditionCareMistakes { get; }

    public NumericMemoryValueViewModel ConditionBattles { get; }

    public ObservableCollection<MemoryValueViewModel> ConditionFlags { get; }

    public IReadOnlyList<TechniqueCategoryViewModel> TechniqueGroups { get; }

    private void InstantDisplay() => _speakingSimulator.RequestInstantDisplay();

    private void RefreshValues()
    {
        foreach (IRefreshable refreshable in _refreshables)
        {
            refreshable.Refresh();
        }

        UpdateVisibleInventorySlots();
    }

    private void PushLockedValuesToMemory()
    {
        foreach (ILockableValue lockable in _lockables)
        {
            lockable.PushLockedValueToMemory();
        }
    }

    private void UpdateVisibleInventorySlots()
    {
        int size = Math.Clamp(LiveMemoryReader.Instance.InventoryStats.InventorySize, 0, InventorySlots.Count);
        if (size == _visibleInventorySlotCount)
        {
            return;
        }

        _visibleInventorySlotCount = size;

        int perColumn = (size + 2) / 3;
        SyncVisibleSlots(InventorySlotsColumn1, InventorySlots.Take(size).Take(perColumn));
        SyncVisibleSlots(InventorySlotsColumn2, InventorySlots.Take(size).Skip(perColumn).Take(perColumn));
        SyncVisibleSlots(InventorySlotsColumn3, InventorySlots.Take(size).Skip(perColumn * 2));
    }

    private static void SyncVisibleSlots(ObservableCollection<InventorySlotViewModel> target, IEnumerable<InventorySlotViewModel> slots)
    {
        target.Clear();
        foreach (InventorySlotViewModel slot in slots)
        {
            target.Add(slot);
        }
    }

    public void Dispose()
    {
        _subscriptions.Dispose();
        _speakingSimulator.Dispose();
    }
}