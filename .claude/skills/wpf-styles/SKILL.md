---
name: wpf-styles
description: Conventions for adding or editing WPF Styles, DataTemplates, and ResourceDictionaries in the Frontend.WPF project of this Digimon World app. Use whenever creating or changing any .xaml style, control template, data template, or resource dictionary, or when a control isn't picking up expected theming.
---

# WPF styles & templates

## One style per file
- Every `Style` lives in **its own file** under `ResourceDictionaries/Styles/` — never inline, never lumped with an unrelated style.
- The first/primary style for a control type is **the default**, keyed `DefaultXxxStyle` (`DefaultButtonStyle`, `DefaultComboboxStyle`, `DefaultCheckboxStyle`, …). **At most one default per element + style-type.** Closely-related variants of the *same* concept (e.g. `DefaultButtonActiveStyle`, `DisplayOnlyButtonStyle`) may share that file.
- Styles are merged into each consumer's `MergedDictionaries` and applied explicitly: `Style="{StaticResource DefaultXxxStyle}"`.
- UserControl-specific styles and `DataTemplate`s each live in their own file (same one-per-file rule) in a `Styles/` or `DataTemplates/` subfolder **beside that UserControl**, merged into its `MergedDictionaries`.

## App.xaml implicit-style gotcha
`App.xaml` defines *implicit* (keyless) styles for `TextBlock` (DW1 font, bold), `Control` (DW1 font), and **`Grid` (background `#2E3841`)** — so **every `Grid` paints that dark background**. When laying content over a different surface, use a `StackPanel`/`Border` or set `Background="Transparent"`, or the `Grid` shows a dark box.

## Shared dimensions
When a size is reused (especially square button/icon/label dimensions) or likely to be tweaked, declare it once as a resource and reference it:
```xml
xmlns:system="clr-namespace:System;assembly=System.Runtime"
<system:Double x:Key="ButtonDimension">41</system:Double>
... Width="{StaticResource ButtonDimension}" Height="{StaticResource ButtonDimension}"
```

## No comments in XAML
Keep `.xaml` files free of `<!-- … -->` comments — element/attribute names should be self-explanatory.

## Theming reaches templated borders, not the control's BorderBrush
Custom control templates here (`DefaultTextboxStyle`, `DefaultComboboxStyle`, `DefaultCheckboxStyle`) use **nested fixed-color `Border`s**, so setting `BorderBrush`/`Background` on the control itself often won't show. To recolor on a state, add a `Trigger` in the `ControlTemplate.Triggers` targeting the named inner border (`TargetName="OuterBorder"`, `"BevelBorder"`, `"Box"`, etc.). Prefer this over adorners — adorners break under `LayoutTransform` (double-scaling) and on `Visibility`-collapsed sections (stale overlays bleed onto the visible UI).
