namespace DigimonWorld.Frontend.WPF.Constants;

public static class CheatSheetInputInfo
{
    // Stats
    public const long HP_MIN = 0;
    public const long HP_MAX = 9999;
    public const long MP_MIN = 0;
    public const long MP_MAX = 9999;
    public const long CURRENT_HP_MIN = 0;
    public const long CURRENT_HP_MAX = 9999;
    public const long CURRENT_MP_MIN = 0;
    public const long CURRENT_MP_MAX = 9999;
    public const long OFFENSE_MIN = 0;
    public const long OFFENSE_MAX = 999;
    public const long DEFENSE_MIN = 0;
    public const long DEFENSE_MAX = 999;
    public const long SPEED_MIN = 0;
    public const long SPEED_MAX = 999;
    public const long BRAINS_MIN = 0;
    public const long BRAINS_MAX = 999;

    // Condition
    public const long CONDITION_TIREDNESS_MIN = 0;
    public const long CONDITION_TIREDNESS_MAX = 100;
    public const long HAPPINESS_MIN = -100;
    public const long HAPPINESS_MAX = 100;
    public const long DISCIPLINE_MIN = 0;
    public const long DISCIPLINE_MAX = 100;
    public const long CARE_MISTAKES_MIN = 0;
    public const long CARE_MISTAKES_MAX = 99;
    public const long BATTLES_MIN = 0;
    public const long BATTLES_MAX = 9999;

    // Care
    public const long POOP_LEVEL_MIN = 0;
    public const long POOP_LEVEL_MAX = 32767;
    public const long VIRUS_BAR_MIN = 0;
    public const long VIRUS_BAR_MAX = 16;
    public const long POOPING_TIMER_MIN = -1;
    public const long POOPING_TIMER_MAX = 32767;
    public const long ENERGY_LEVEL_MIN = 0;
    public const long ENERGY_LEVEL_MAX = 80;
    public const long HUNGRY_TIMER_MIN = 0;
    public const long HUNGRY_TIMER_MAX = 32767;
    public const long STARVATION_TIMER_MIN = 0;
    public const long STARVATION_TIMER_MAX = 32767;
    public const long LIFESPAN_MIN = 0;
    public const long LIFESPAN_MAX = 32767;

    // Profile
    public const long WEIGHT_MIN = 0;
    public const long WEIGHT_MAX = 99;
    public const long LIVES_MIN = 0;
    public const long LIVES_MAX = 255;

    // Combat
    public const long COMBAT_TIMER_MIN = 0;
    public const long COMBAT_TIMER_MAX = 32767;
    public const long COOLDOWN_MIN = 0;
    public const long COOLDOWN_MAX = 255;
    public const long STATUS_EFFECTS_MIN = 0;
    public const long STATUS_EFFECTS_MAX = 255;

    // Inventory
    public const long ITEM_AMOUNT_MIN = 0;
    public const long ITEM_AMOUNT_MAX = 99;
    public const long INVENTORY_SIZE_MIN = 0;
    public const long INVENTORY_SIZE_MAX = 30;

    // Tamer
    public const long TAMER_LEVEL_MIN = 0;
    public const long TAMER_LEVEL_MAX = 10;
    public const long BITS_MIN = 0;
    public const long BITS_MAX = 9998;
    public const long MERIT_POINTS_MIN = 0;
    public const long MERIT_POINTS_MAX = 9998;

    // Technical
    public const long RNG_MIN = 0;
    public const long RNG_MAX = 4294967294;
    public const long YEAR_MIN = 0;
    public const long YEAR_MAX = 255;
    public const long DAY_MIN = 98;
    public const long DAY_MAX = 9999;
    public const long HOUR_MIN = 0;
    public const long HOUR_MAX = 23;
    public const long MINUTE_MIN = 0;
    public const long MINUTE_MAX = 59;
    public const long AGE_IN_DAYS_MIN = 0;
    public const long AGE_IN_DAYS_MAX = 99;
    public const long EVOLUTION_AGE_MIN = 0;
    public const long EVOLUTION_AGE_MAX = 999;

    public static readonly string HpTooltip = $"Maximum HP of your Digimon.\n\nAllowed range {HP_MIN}–{HP_MAX}.";
    public static readonly string MpTooltip = $"Maximum MP of your Digimon.\n\nAllowed range {MP_MIN}–{MP_MAX}.";
    public static readonly string CurrentHpTooltip = $"Current HP of your Digimon.\n\nAllowed range {CURRENT_HP_MIN}–{CURRENT_HP_MAX}.";
    public static readonly string CurrentMpTooltip = $"Current MP of your Digimon.\n\nAllowed range {CURRENT_MP_MIN}–{CURRENT_MP_MAX}.";
    public static readonly string OffenseTooltip = $"Offense stat of your Digimon.\n\nAllowed range {OFFENSE_MIN}–{OFFENSE_MAX}.";
    public static readonly string DefenseTooltip = $"Defense stat of your Digimon.\n\nAllowed range {DEFENSE_MIN}–{DEFENSE_MAX}.";
    public static readonly string SpeedTooltip = $"Speed stat of your Digimon.\n\nAllowed range {SPEED_MIN}–{SPEED_MAX}.";
    public static readonly string BrainsTooltip = $"Brains stat of your Digimon.\n\nAllowed range {BRAINS_MIN}–{BRAINS_MAX}.";

    public static readonly string ConditionTirednessTooltip = $"How tired your Digimon's condition is.\n\nAllowed range {CONDITION_TIREDNESS_MIN}–{CONDITION_TIREDNESS_MAX}.";
    public static readonly string HappinessTooltip = $"Happiness of your Digimon.\n\nAllowed range {HAPPINESS_MIN}–{HAPPINESS_MAX}.";
    public static readonly string DisciplineTooltip = $"Discipline of your Digimon.\n\nAllowed range {DISCIPLINE_MIN}–{DISCIPLINE_MAX}.";
    public static readonly string CareMistakesTooltip = $"Number of care mistakes made.\n\nAllowed range {CARE_MISTAKES_MIN}–{CARE_MISTAKES_MAX}.";
    public static readonly string BattlesTooltip = $"Number of battles fought.\n\nAllowed range {BATTLES_MIN}–{BATTLES_MAX}.";

    public static readonly string PoopLevelTooltip = $"Current poop level.\n\nAllowed range {POOP_LEVEL_MIN}–{POOP_LEVEL_MAX}.";
    public static readonly string VirusBarTooltip = $"Virus bar value.\n\nAllowed range {VIRUS_BAR_MIN}–{VIRUS_BAR_MAX}.";
    public static readonly string PoopingTimerTooltip = $"Timer until the next poop.\n\nAllowed range {POOPING_TIMER_MIN}–{POOPING_TIMER_MAX}.";
    public static readonly string EnergyLevelTooltip = $"Current energy level.\n\nAllowed range {ENERGY_LEVEL_MIN}–{ENERGY_LEVEL_MAX}.";
    public static readonly string HungryTimerTooltip = $"Timer until your Digimon gets hungry.\n\nAllowed range {HUNGRY_TIMER_MIN}–{HUNGRY_TIMER_MAX}.";
    public static readonly string StarvationTimerTooltip = $"Timer until your Digimon starts starving.\n\nAllowed range {STARVATION_TIMER_MIN}–{STARVATION_TIMER_MAX}.";
    public static readonly string LifespanTooltip = $"Remaining lifespan of your Digimon in hours.\n\nAllowed range {LIFESPAN_MIN}–{LIFESPAN_MAX}.";

    public static readonly string TamerLevelTooltip = $"Your Tamer level.\n\nAllowed range {TAMER_LEVEL_MIN}–{TAMER_LEVEL_MAX}.";
    public static readonly string BitsTooltip = $"Your Bits (currency).\n\nAllowed range {BITS_MIN}–{BITS_MAX}.";
    public static readonly string MeritPointsTooltip = $"Your Merit Points.\n\nAllowed range {MERIT_POINTS_MIN}–{MERIT_POINTS_MAX}.";

    public static readonly string RngTooltip = $"Current random number generator value.\n\nAllowed range {RNG_MIN}–{RNG_MAX}.";
    public static readonly string YearTooltip = $"In-game year.\n\nAllowed range {YEAR_MIN}–{YEAR_MAX}.";
    public static readonly string DayTooltip = $"In-game day.\n\nAllowed range {DAY_MIN}–{DAY_MAX}.";
    public static readonly string HourTooltip = $"In-game hour.\n\nAllowed range {HOUR_MIN}–{HOUR_MAX}.";
    public static readonly string MinuteTooltip = $"In-game minute.\n\nAllowed range {MINUTE_MIN}–{MINUTE_MAX}.";

    public static readonly string AgeInDaysTooltip = $"Age of your Digimon in days.\n\nAllowed range {AGE_IN_DAYS_MIN}–{AGE_IN_DAYS_MAX}.";
    public static readonly string EvolutionAgeTooltip = $"Hours spent in the current form, used for evolution.\n\nAllowed range {EVOLUTION_AGE_MIN}–{EVOLUTION_AGE_MAX}.";

    private const string UPGRADE_COUNTER_EXPLANATION = "Training at least 10 times with a Fresh or In-Training Digimon upgrades the station, boosting that gym's gains by 20%.";

    public static readonly string UpgradeCounterHpTooltip = $"Whether the HP training station is upgraded.\n\n{UPGRADE_COUNTER_EXPLANATION}";
    public static readonly string UpgradeCounterMpTooltip = $"Whether the MP training station is upgraded (also affects Brains).\n\n{UPGRADE_COUNTER_EXPLANATION}";
    public static readonly string UpgradeCounterOffenseTooltip = $"Whether the Offense training station is upgraded.\n\n{UPGRADE_COUNTER_EXPLANATION}";
    public static readonly string UpgradeCounterDefenseTooltip = $"Whether the Defense training station is upgraded.\n\n{UPGRADE_COUNTER_EXPLANATION}";
    public static readonly string UpgradeCounterSpeedTooltip = $"Whether the Speed training station is upgraded.\n\n{UPGRADE_COUNTER_EXPLANATION}";

    public static readonly string GoldenPoopEnabledTooltip = "Whether the Golden Poop can be won in the Bonus Try slot-machine minigame.\n\nNormally the game rigs each training so the Golden Poop is rarely achievable; enabling this makes it winnable for a 10x training multiplier.\n\nNote: This value is recalculated at the start of training.";

    public static readonly string FinisherGoalTooltip = $"Charge required before the finisher fires.\n\nAllowed range {COMBAT_TIMER_MIN}–{COMBAT_TIMER_MAX}.";
    public static readonly string FinisherProgressTooltip = $"Current charge towards the finisher.\n\nAllowed range {COMBAT_TIMER_MIN}–{COMBAT_TIMER_MAX}.";
    public static readonly string PoisonTimerTooltip = $"Time remaining poisoned.\n\nAllowed range {COMBAT_TIMER_MIN}–{COMBAT_TIMER_MAX}.";
    public static readonly string ConfusedTimerTooltip = $"Time remaining confused.\n\nAllowed range {COMBAT_TIMER_MIN}–{COMBAT_TIMER_MAX}.";
    public static readonly string StunTimerTooltip = $"Time remaining stunned.\n\nAllowed range {COMBAT_TIMER_MIN}–{COMBAT_TIMER_MAX}.";
    public static readonly string FlattenTimerTooltip = $"Time remaining flattened.\n\nAllowed range {COMBAT_TIMER_MIN}–{COMBAT_TIMER_MAX}.";
    public static readonly string FlattenAttackTimerTooltip = $"Time before the flatten attack lands.\n\nAllowed range {COMBAT_TIMER_MIN}–{COMBAT_TIMER_MAX}.";
    public static readonly string CooldownTooltip = $"Attack cooldown.\n\nAllowed range {COOLDOWN_MIN}–{COOLDOWN_MAX}.";
    public static readonly string DumbTimerTooltip = $"Time remaining stupefied.\n\nAllowed range {COMBAT_TIMER_MIN}–{COMBAT_TIMER_MAX}.";
    public static readonly string StatusEffectsTooltip = $"Status-effect bit flags (poisoned, confused, stunned, etc.).\n\nAllowed range {STATUS_EFFECTS_MIN}–{STATUS_EFFECTS_MAX}.";

    public static readonly string ItemTypeTooltip = "Item held in this slot.\n\nReopen the inventory to have the name update after changing this value.";
    public static readonly string ItemAmountTooltip = $"Quantity held in this slot.\n\nAllowed range {ITEM_AMOUNT_MIN}–{ITEM_AMOUNT_MAX}.";
    public static readonly string InventorySizeTooltip = $"Number of inventory slots in use.\n\n- No upgrade: 10\n\n-First upgrade: 20\n\n- Second Upgrade: 30\n\nValues lower than 10 causes the name of the items to become glitched.\n\nAllowed range {INVENTORY_SIZE_MIN}–{INVENTORY_SIZE_MAX}.";

    public static readonly string WeightTooltip = $"Weight of your Digimon.\n\nAllowed range {WEIGHT_MIN}–{WEIGHT_MAX}.";
    public static readonly string LivesTooltip = $"Number of lives your Digimon has had.\n\nAllowed range {LIVES_MIN}–{LIVES_MAX}.";
}
