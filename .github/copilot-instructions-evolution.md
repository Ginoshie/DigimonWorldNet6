# Copilot Instructions — Evolution Calculation Engine

> This file supplements `.github/copilot-instructions.md` with deep details on the evolution calculation domain.

---

## Evolution Calculation Pipeline

### High-Level Flow

```
UserDigimon (stats + DigimonName)
  → EvolutionCalculator.CalculateEvolutionResult()
    → Strategy dispatch by EvolutionStage:
        Fresh       → FromFreshEvolutionCalculator
        InTraining  → FromInTrainingEvolutionCalculator
        Rookie/Champion → FromRookieOrChampionEvolutionCalculator
        Ultimate    → FromUltimateEvolutionCalculator
    → Stage-specific calculator:
        1. Get evolution criteria list from Mapper
        2. For each candidate evolution:
           a. Check if evolution is "enabled" (3-of-4 criteria met)
           b. Calculate evolution score (stat average)
           c. Consider historic evolution priority
        3. Return highest-scoring enabled evolution
```

### Entry Point

```csharp
// Evolution.Calculator.Core/EvolutionCalculation/EvolutionCalculator.cs
public sealed class EvolutionCalculator
{
    // Singleton
    private static readonly Lazy<EvolutionCalculator> _instance = new(() => new EvolutionCalculator());
    public static EvolutionCalculator Instance { get; } = _instance.Value;

    public EvolutionResult CalculateEvolutionResult(UserDigimon userDigimon)
    {
        IEvolutionCalculator evolutionCalculator = userDigimon.EvolutionStage switch
        {
            EvolutionStage.Fresh => new FromFreshEvolutionCalculator(),
            EvolutionStage.InTraining => new FromInTrainingEvolutionCalculator(),
            EvolutionStage.Rookie or EvolutionStage.Champion => new FromRookieOrChampionEvolutionCalculator(),
            EvolutionStage.Ultimate => new FromUltimateEvolutionCalculator(),
            _ => throw new ArgumentOutOfRangeException(...)
        };
        return evolutionCalculator.DetermineEvolutionResult(userDigimon);
    }
}
```

---

## IEvolutionCriteria — Evolution Requirements

Each possible evolution target is represented by a class implementing `IEvolutionCriteria`:

```csharp
public interface IEvolutionCriteria
{
    EvolutionStage EvolutionStage { get; }    // The target stage
    EvolutionResult EvolutionResult { get; }   // The resulting Digimon
    MainCriteriaStats Stats { get; }           // Stat thresholds
    MainCriteriaCareMistakes CareMistakes { get; } // Care mistake criteria
    MainCriteriaWeight Weight { get; }         // Weight criteria (±5 margin)
    BonusCriteria BonusCriteria { get; }       // Bonus conditions
}
```

### Example — GreymonEvolutionCriteria

```csharp
public sealed class GreymonEvolutionCriteria : IEvolutionCriteria
{
    public EvolutionStage EvolutionStage => EvolutionStage.Champion;
    public EvolutionResult EvolutionResult => EvolutionResult.Greymon;
    public MainCriteriaStats Stats => new(off: 100, def: 100, speed: 100, brains: 100);
    public MainCriteriaCareMistakes CareMistakes => new(1, true);  // max 1 care mistake
    public MainCriteriaWeight Weight => new(30);  // weight 25-35 (30 ± 5)
    public BonusCriteria BonusCriteria => new(discipline: 90, techniqueCount: 35);
}
```

### Criteria Data Objects

| Class | Constructor Parameters | Notes |
|-------|----------------------|-------|
| `MainCriteriaStats` | `hp, mp, off, def, speed, brains` (all default 0) | 0 means "not required" |
| `MainCriteriaCareMistakes` | `careMistakes` (default -1), `isCareMistakesCriteriaMaximum` (default false) | -1 means no criteria; `true` = max, `false` = min |
| `MainCriteriaWeight` | `weight` (target) | Always has ±5 margin (`WEIGHT_MARGIN`) |
| `BonusCriteria` | `happiness` (-1), `discipline` (-1), `battles` (-1), `isBattlesCriteriaAMaximum` (true), `techniqueCount` (0), `precursorDigimon` (null) | -1 means "not required" |

---

## Evolution Enabled Check (3-of-4 Rule)

An evolution is **enabled** when at least 3 out of 4 criteria categories are met:

```csharp
private bool EvolutionEnabled(UserDigimon userDigimon, IEvolutionCriteria evolutionCriteria)
{
    int criteriaMetCount = 0;
    if (_statsMainCriteriaCalculator.CriteriaIsMet(userDigimon, evolutionCriteria.Stats)) criteriaMetCount++;
    if (_careMistakesMainCriteriaCalculator.CriteriaIsMet(userDigimon, evolutionCriteria.CareMistakes)) criteriaMetCount++;
    if (_weightMainCriteriaCalculator.CriteriaIsMet(userDigimon, evolutionCriteria.Weight)) criteriaMetCount++;
    if (_bonusCriteriaCalculator.CriteriaIsMet(userDigimon, evolutionCriteria.BonusCriteria)) criteriaMetCount++;
    return criteriaMetCount >= 3;
}
```

Criteria calculator classes:
- `StatsCriteriaCalculator` — checks if user stats meet thresholds
- `CareMistakeCriteriaCalculator` — checks care mistake count direction
- `WeightCriteriaCalculator` — checks weight within ±5 range
- `BonusCriteriaCalculator` — checks happiness, discipline, battles, techniques, precursor

---

## Evolution Score Calculation

When multiple evolutions are enabled, the **highest-scoring** one wins.

```csharp
// FromRookieOrChampionEvolutionScoreCalculator
public EvolutionScoreCalculationResult CalculateEvolutionScore(
    UserDigimon userDigimon,
    MainCriteriaStats statsCriteria,
    int carriedOverStatTotal,
    int carriedOverStatCount)
{
    // Only count stats that the evolution actually requires (> 0)
    // HP and MP are divided by 10 before averaging
    // Off, Def, Speed, Brains are used as-is
    // Score = (sum of applicable stats + carried over) / (count + carried over count)
}
```

### EvolutionScoreCalculationResult

```csharp
public class EvolutionScoreCalculationResult
{
    public int EvolutionScore { get; init; }       // The average stat score
    public int CarriedOverStatTotal { get; init; }  // Accumulated total for next iteration
    public int CarriedOverCount { get; init; }      // Accumulated count for next iteration
}
```

### Carried-Over Stats (Original vs Vice)

- **Original mode:** Stats from previous evolution score calculations carry over to the next (accumulating average)
- **Vice mode:** Carried-over stats are reset to 0 for each evolution check (independent scoring)

```csharp
private void OnEvolutionCalculatorConfigChanged(EvolutionCalculatorConfig config) =>
    _dontUseCarriedOverStats = config.GameVariant != GameVariant.Original;
```

---

## Historic Evolution Priority

New (never-before-evolved) evolutions are prioritized over historically-evolved ones:

```csharp
// If current best is NEW and next candidate is HISTORIC → stop (keep the new one)
if (ValidEvolutionResult(evolutionResult) &&
    CurrentBestEnabledEvolutionIsNewEvolution(evolutionResult.ToDigimonType()) &&
    NextEvolutionIsHistoricEvolution(evolutionCriteria.EvolutionResult.ToDigimonType()))
{
    break;
}
```

`Session.HistoricEvolutions` tracks previously-evolved Digimon across the session.

---

## Evolution Mapper (Game Variant Filtering)

`FromRookieOrChampionEvolutionMapper` maps each `DigimonName` to its list of possible evolutions, with **variant-specific alternatives**:

```csharp
// Same Digimon can have different evolution paths per GameVariant
_fromRookieOrChampionEvolutionMappings[DigimonTypes.Greymon] = GreymonEvolutions;       // Original
_fromRookieOrChampionEvolutionMappings[DigimonTypes.GreymonVice] = GreymonViceEvolutions; // Vice
```

Selection uses `GameVariantExtensions.IsAvailableIn()`:
```csharp
public List<IEvolutionCriteria> GetEvolutionCriteria(DigimonName digimonName)
{
    var candidates = _fromRookieOrChampionEvolutionMappings
        .Where(e => e.Key.IncludeGameVariantFlags.IsAvailableIn(e.Key.ExcludeGameVariantFlags, _gameVariant)
                  && e.Key.DigimonName == digimonName)
        .ToList();
    return candidates.Single().Value.ToList();
}
```

### GameVariant Filtering Logic

```csharp
public static bool IsAvailableIn(this GameVariant include, GameVariant exclude, GameVariant current)
{
    // 1. Exclude always wins (any excluded flag present → not available)
    // 2. Original mode → must have Original flag
    // 3. Vice mode → must have Vice flag, then check required patches
    //    Extra active patches are allowed; only required patches must be present
}
```

---

## Evolution Criteria File Organization

```
EvolutionCriteria/
├── Original/           # Vanilla game criteria
│   ├── Rookie/         # Rookie → Champion evolutions (AgumonEvolutionCriteria, etc.)
│   ├── Champion/       # Champion criteria (GreymonEvolutionCriteria, etc.)
│   ├── InTraining/     # InTraining → Rookie evolutions
│   └── Ultimate/       # Ultimate criteria
├── Vice21AndUp/        # Vice mod additions
│   └── Ultimate/       # New Ultimate evolutions (Weregarurumon, Gigadramon, etc.)
├── Myotismon/          # Myotismon patch additions
│   └── Ultimate/       # MyotismonEvolutionCriteria
└── Panjyamon/          # Panjyamon patch additions
    └── Champion/       # PanjyamonEvolutionCriteria
```

### Adding a New Evolution Criteria

1. Create a new `sealed class` implementing `IEvolutionCriteria`:
   ```csharp
   // File: EvolutionCriteria/{Variant}/{Stage}/{Name}EvolutionCriteria.cs
   public sealed class NewDigimonEvolutionCriteria : IEvolutionCriteria
   {
       public EvolutionStage EvolutionStage => EvolutionStage.Ultimate;
       public EvolutionResult EvolutionResult => EvolutionResult.NewDigimon;
       public MainCriteriaStats Stats => new(hp: 5000, mp: 5000, off: 500, def: 500, speed: 500, brains: 500);
       public MainCriteriaCareMistakes CareMistakes => new(3, true);
       public MainCriteriaWeight Weight => new(40);
       public BonusCriteria BonusCriteria => new(happiness: 95, discipline: 95, techniqueCount: 49);
   }
   ```

2. Register in the appropriate evolution mapper with the corresponding `Digimon` constant from `DigimonTypes`.

3. Add the `Digimon` constant to `DigimonTypes` if it doesn't exist (with correct `GameVariant` flags).

4. Write tests using `[TestCase]` parameterized tests covering:
   - Successful evolution with meeting stats
   - Failed evolution with insufficient stats
   - Historic evolution priority interactions
   - GameVariant filtering

---

## Default Evolution Fallbacks

- **Rookie → Champion:** If no Champion evolution criteria are met, result is `EvolutionResult.Numemon` (the fallback "failure" evolution)
- **Fresh → InTraining:** Deterministic based on Fresh type
- **InTraining → Rookie:** Based on stats and care
- **Champion → Ultimate:** Must meet strict criteria, otherwise `EvolutionResult.None`

---

## Key Extension Methods

```csharp
// Convert EvolutionResult to DigimonName (for historic evolution lookups)
EvolutionResult.MetalGreymon.ToDigimonType() // → DigimonName.MetalGreymon

// Check if a Digimon is available in a given GameVariant
digimon.IncludeGameVariantFlags.IsAvailableIn(digimon.ExcludeGameVariantFlags, currentVariant)
```

