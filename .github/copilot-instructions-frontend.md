# Copilot Instructions — WPF Frontend

> This file supplements `.github/copilot-instructions.md` with deep details on the Frontend.WPF project.

---

## Window System

### Window Hierarchy

```
System.Windows.Window
  ├── BaseWindow (auto-positions relative to MainWindow)
  │     └── (Feature windows that dock beside the main window)
  └── BaseDialogWindow (centered dialog)
        └── (Modal dialogs like GeneralConfig, AboutAndCredits)
```

### BaseWindow

Automatically positions itself to the left of the MainWindow:
```csharp
public abstract class BaseWindow : Window
{
    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        Left = Application.Current.MainWindow!.Left - ActualWidth - 10;
        Top = Application.Current.MainWindow.Top;
    }
}
```

### BaseWindowViewModel

Provides standard window commands:
```csharp
public abstract class BaseWindowViewModel : BaseViewModel
{
    protected readonly Window Window;

    protected BaseWindowViewModel(Window window)
    {
        MinimizeCommand = new CommandHandler(() => Window.WindowState = WindowState.Minimized);
        CloseCommand = new CommandHandler(CloseApplication);
        DragCommand = new CommandHandler(() => DragWindow(window));
    }

    public ICommand MinimizeCommand { get; }
    public ICommand CloseCommand { get; }
    public ICommand DragCommand { get; }

    protected virtual void CloseApplication() => Window.Close(); // Override for custom close logic
}
```

---

## ViewModel Components

### BaseViewModel

The MVVM foundation — implements `INotifyPropertyChanged` with `SetField<T>`:

```csharp
public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) { ... }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
```

**Usage with `field` keyword (C# 14):**
```csharp
public bool PaneIsOpen
{
    get;
    private set => SetField(ref field, value);
}
```

### PaneBaseViewModel

Extends `BaseViewModel` with animation support for sliding panes:

```csharp
public class PaneBaseViewModel : BaseViewModel
{
    public const int AnimationDurationInMs = 600;

    protected IObservable<double> AnimateOffset(double start, double target)
    {
        // Generates 60fps interpolated values over 600ms with ease-out curve
        // t = 1 - Math.Pow(1 - t, 2)  ← quadratic ease-out
    }
}
```

### CommandHandler

Parameterless `ICommand` using primary constructor:
```csharp
public sealed class CommandHandler(Action action) : ICommand
{
    public bool CanExecute(object? parameter) => true;
    public void Execute(object? parameter) => action();
}
```

### RelayCommand\<T\>

Typed generic `ICommand`:
```csharp
public class RelayCommand<T>(Action<T> execute) : ICommand
{
    public void Execute(object? parameter) => _execute((T)parameter!);
}
```

---

## Main Window Architecture

### MainWindowViewModel

The central ViewModel that orchestrates all panes and features:

```csharp
public class MainWindowViewModel : BaseWindowViewModel, IDisposable
{
    // Three sliding panes
    public NavigationLeftPaneViewModelComponent LeftPaneViewModelComponent { get; }
    public HistoricEvolutionsBottomPaneViewModelComponent BottomPaneViewModelComponent { get; }
    public EmulatorLinkRightPaneViewModelComponent RightPaneViewModelComponent { get; }

    // Currently displayed main content (swapped via navigation)
    public UserControl CurrentSelectedMainWindowContent { get; private set; }

    // Window management commands
    public ICommand OpenConfigurationWindowCommand { get; }
    public ICommand OpenAboutAndCreditsWindowCommand { get; }
    public CommandHandler OpenMusicPlayerWindowCommand { get; }
}
```

### Pane System

Three sliding panes surround the main content:

| Pane | Class | Position | Purpose |
|------|-------|----------|---------|
| Navigation | `NavigationLeftPaneViewModelComponent` | Left | Switch between EvolutionCalculator, DigiWiki, TamerVision |
| Historic Evolutions | `HistoricEvolutionsBottomPaneViewModelComponent` | Bottom | Show/manage historic evolution picks |
| Emulator Link | `EmulatorLinkRightPaneViewModelComponent` | Right | Emulator connection status and settings |

**Pane animation pattern:**
```csharp
public class NavigationLeftPaneViewModelComponent : PaneBaseViewModel, IDisposable
{
    private const double PANEL_OPENED_X_OFFSET = 10;
    private const double PANEL_CLOSED_X_OFFSET = 120;

    public NavigationLeftPaneViewModelComponent(Action<UserControl> setCurrentSelectedMainWindowContent)
    {
        _disposable = new CompositeDisposable(
            this.WhenAnyValue(x => x.PaneIsOpen)
                .Select(open => open ? PANEL_OPENED_X_OFFSET : PANEL_CLOSED_X_OFFSET)
                .DistinctUntilChanged()
                .SelectMany(targetOffset => AnimateOffset(PaneOffset, targetOffset))
                .ObserveOn(SynchronizationContext.Current!)
                .Subscribe(v => PaneOffset = v));
    }

    public bool PaneIsOpen { get; private set => SetField(ref field, value); }
    public double PaneOffset { get; private set => SetField(ref field, value); }

    private void ToggleLeftPane() => PaneIsOpen = !PaneIsOpen;
}
```

---

## Main Content Areas

### UserControl Organization

```
Windows/Main/UserControls/
├── EvolutionCalculator/     # Main evolution calculation UI
│   ├── EvolutionCalculatorUserControl.xaml(.cs)
│   ├── EvolutionCalculatorViewModel.cs
│   ├── Styles/              # Feature-specific styles
│   └── UserControls/        # Sub-components
├── TamerVision/             # Live stat overlay from emulator
│   └── UserControls/        # Sub-components (EvolutionStatsUserControl, etc.)
├── DigiWiki/                # Digimon encyclopedia
│   └── UserControls/        # Per-Digimon pages with .jpg images
├── EmulatorLink/            # Emulator connection management
├── HistoricEvolutionPicker/ # Historic evolution selection
├── Navigation/              # Navigation buttons
└── Panes/                   # The three sliding panes
```

### Content Switching

Navigation replaces the main content by creating new UserControl instances:
```csharp
private void ShowEvolutionCalculator() => _setCurrentSelectedMainWindowContent(new EvolutionCalculatorUserControl());
private void ShowDigiWiki() => _setCurrentSelectedMainWindowContent(new DigiWikiUserControl());
private void ShowTamerVision() => _setCurrentSelectedMainWindowContent(new TamerVisionUserControl());
```

---

## Window Opening Pattern

All secondary windows follow this exact pattern:
```csharp
private void OpenFeatureWindow()
{
    FeatureWindow window = new()
    {
        Owner = Application.Current.MainWindow  // Set owner for positioning
    };

    FeatureWindowViewModel viewModel = new(window);  // Pass window to ViewModel
    window.DataContext = viewModel;                    // Wire DataContext

    // For modal:
    window.ShowDialog();

    // For non-modal (with lifecycle tracking):
    window.Closed += (_, _) =>
    {
        viewModel.Dispose();
        _featureIsOpen = false;
    };
    window.Show();
}
```

**Important:** ViewModels receive the `Window` instance in their constructor. DataContext is set in the opener, NOT in XAML or code-behind of the window itself. ViewModels are **always created directly** with `new` — they are never resolved from a DI container.

---

## Value Converters

Located in `Frontend.WPF/Conversion/`:

| Converter | Purpose |
|-----------|---------|
| `EnumToImageConverter` | Maps enum values to corresponding image resources |
| `TrueToVisibilityConverter` | `true` → `Visibility.Visible`, `false` → `Visibility.Collapsed` |
| `TrueToHiddenConverter` | `true` → `Visibility.Visible`, `false` → `Visibility.Hidden` |
| `DigimonTypeContainsMultiConverter` | Multi-binding converter for checking Digimon type membership |
| `ToolTipTextConverter` | Formats tooltip text |

### Creating a New Converter

```csharp
// Frontend.WPF/Conversion/MyNewConverter.cs
using System;
using System.Globalization;
using System.Windows.Data;

namespace DigimonWorld.Frontend.WPF.Conversion;

public sealed class MyNewConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // Transform value for display
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
```

---

## Validation Rules

Located in `Frontend.WPF/Validation/`:

### Base Rules

```csharp
// Bases/ZeroToNineNineNineNineStringValidationRule.cs
public class ZeroToNineNineNineNineStringValidationRule : ValidationRule
{
    public override ValidationResult Validate(object? value, CultureInfo cultureInfo)
    {
        // Validates binding expression source value is 0-9999
        // Uses reflection to get source property value from BindingExpression
    }
}
```

### Derived Rules

Per-stat rules are simple derivations:
```
CombatStats/
  HPValidationRule.cs        (inherits 0-9999 base)
  MPValidationRule.cs
  OffValidationRule.cs
  DefValidationRule.cs
  SpeedValidationRule.cs
  BrainsValidationRule.cs
NonCombatStats/
  WeightValidationRule.cs    (inherits 0-999 base)
  HappinessValidationRule.cs
  DisciplineValidationRule.cs
  ...
```

### Adding a New Validation Rule

1. Choose the appropriate base (`ZeroToNineNineNineNineStringValidationRule` or `ZeroToNineNineNineValidationRule`)
2. Create a derived class in the appropriate subfolder:
   ```csharp
   namespace DigimonWorld.Frontend.WPF.Validation.CombatStats;

   public sealed class MyStatValidationRule : ZeroToNineNineNineNineStringValidationRule { }
   ```
3. Reference in XAML binding validation rules

---

## Services

### MusicPlayer (`Frontend.WPF/Services/MusicPlayer.cs`)

Static class using **CSCore** library with **WasapiOut** for audio playback:
- Plays `.mp3` and `.wav` files from `Music/` directory
- Supports shuffle, repeat (single/all), volume, mute
- Subscribes to `MusicPlayerEventHub` for control signals
- **Important:** Must be disposed on application exit (`Services.MusicPlayer.Dispose()` in `CloseApplication()`)

### SpeakingSimulator (`Frontend.WPF/Services/SpeakingSimulator.cs`)

Typewriter text animation effect:
- Reveals text character by character with configurable speed
- Supports cancellation via `CancellationTokenSource`
- Used for narrative UI text displays

### Icon Factories

Static factory classes that create `BitmapImage` instances from Digimon data:

| Factory | Purpose |
|---------|---------|
| `DigimonIconFactory` | Creates Digimon portrait images from `DigimonName` enum |
| `TypeIconFactory` | Creates elemental type icons from `DigimonType` enum |
| `SpecialIconFactory` | Creates special ability icons from `Special` enum |

---

## Resources & Assets

### Custom Font

```xml
<!-- In XAML -->
<TextBlock FontFamily="pack://application:,,,/Fonts/#DW1_US_Regular" />
```

The font `DW1_US_Regular.ttf` is the authentic Digimon World 1 US font, included as a WPF Resource.

### Images

```xml
<!-- Digimon images as resources -->
<Image Source="/Images/Digimon/Agumon.png" />
```

All images under `Images/` are included as WPF Resources via `<Resource Include="Images\**\*"/>`.

### Music Files

```xml
<!-- Copied to output directory -->
<None Include="Music\**\*.mp3">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
</None>
```

### ResourceDictionaries

WPF style dictionaries are in `Frontend.WPF/ResourceDictionaries/` and referenced in `App.xaml`.

---

## Behaviors

Located in `Frontend.WPF/Behaviors/` — uses `Microsoft.Xaml.Behaviors.Wpf` for attached behaviors.

---

## Adding a New UI Feature

### Complete Checklist

1. **Create UserControl:**
   ```
   Frontend.WPF/Windows/Main/UserControls/MyFeature/
     MyFeatureUserControl.xaml
     MyFeatureUserControl.xaml.cs
     MyFeatureViewModel.cs
   ```

2. **Create ViewModel:**
   ```csharp
   namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.MyFeature;

   public sealed class MyFeatureViewModel : BaseViewModel, IDisposable
   {
       private readonly CompositeDisposable _disposable;

       public MyFeatureViewModel()
       {
           _disposable = new CompositeDisposable(
               // Subscribe to EventHub observables
               EmulatorLinkEventHub.EmulatorConnectedObservable.Subscribe(OnEmulatorConnected)
           );
       }

       // Properties using field keyword:
       public string MyProperty
       {
           get;
           private set => SetField(ref field, value);
       }

       // Commands:
       public ICommand MyCommand { get; } = new CommandHandler(() => { /* action */ });

       private void OnEmulatorConnected(bool connected) { /* react */ }

       public void Dispose() => _disposable.Dispose();
   }
   ```

3. **Wire DataContext in code-behind:**
   ```csharp
   public partial class MyFeatureUserControl : UserControl
   {
       public MyFeatureUserControl()
       {
           InitializeComponent();
           DataContext = new MyFeatureViewModel();
       }
   }
   ```

4. **Add navigation:**
   In `NavigationLeftPaneViewModelComponent`:
   ```csharp
   public ICommand ShowMyFeatureCommand { get; }
   // In constructor: ShowMyFeatureCommand = new CommandHandler(ShowMyFeature);
   private void ShowMyFeature() => _setCurrentSelectedMainWindowContent(new MyFeatureUserControl());
   ```

5. **Add navigation button** in `NavigationLeftPane.xaml`

---

## Reactive Patterns in ViewModels

### Standard subscription pattern

```csharp
// In constructor:
_compositeDisposable = new CompositeDisposable(
    EventHub1.Observable1.Subscribe(handler1),
    EventHub2.Observable2.Subscribe(handler2),
    someRxQuery
        .ObserveOn(SynchronizationContext.Current!)  // ALWAYS for UI updates
        .Subscribe(uiHandler)
);
```

### ReactiveUI integration

The project uses `ReactiveUI` selectively (e.g., `WhenAnyValue` for property change observables):
```csharp
this.WhenAnyValue(x => x.PaneIsOpen)
    .DistinctUntilChanged()
    .SelectMany(...)
    .ObserveOn(SynchronizationContext.Current!)
    .Subscribe(v => PaneOffset = v);
```

### Threading rules

- **Always** use `.ObserveOn(SynchronizationContext.Current!)` before subscribing to update UI properties
- Memory value readers run on background threads (via `Observable.Interval`)
- EventHub signals fire on whatever thread the signal originated from

