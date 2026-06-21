# Release Instructions

## Prerequisites

- Install the Velopack CLI: `dotnet tool install -g vpk`
- Ensure `Version.props` contains the correct version number

## Steps

### 1. Update the version

Edit `Version.props` and set the new version:

```xml
<AppVersion>1.4.0</AppVersion>
```

### 2. Publish the application

Publish via Rider as usual to `C:\DW1 Tool` (framework-dependent), or via command line:

```powershell
dotnet publish Frontend.WPF/Frontend.WPF.csproj -c Release --self-contained false -o "C:\DW1 Tool"
```

### 3. Pack with Velopack

```powershell
vpk pack --packId DW1Tool --packTitle "DW 1 Tool" --packVersion 1.4.0 --packDir "C:\DW1 Tool" --mainExe Frontend.WPF.exe --icon "C:\git\DigimonWorldNet6\DigimonWorldNet6\Frontend.WPF\Images\Misc\app-icon.ico" --framework net10.0-x64-desktop --outputDir "C:\DW1Tool-Releases"
```

> **The releases output dir must be OUTSIDE `--packDir`.** `vpk pack` bundles everything inside `--packDir` into the package, so if the releases folder lives under `C:\DW1 Tool` it nests every previous package into the new one — the package balloons to hundreds of MB / GB and compounds each release. Keep the app in `C:\DW1 Tool` and the releases in a separate `C:\DW1Tool-Releases`. Also always pass `--outputDir` explicitly: without it `vpk pack` writes to a `Releases` folder relative to the current directory, which both lands in the wrong place and misses the previous `.nupkg` (no delta). The `--outputDir` here must match the one in the upload step and hold the previous version's `.nupkg`.

This generates files in the `Releases` folder:
- `DW1Tool-Setup.exe` — Installer for new users
- `DW1Tool-1.4.0-full.nupkg` — Full update package
- `DW1Tool-1.4.0-delta.nupkg` — Delta update package (only if a previous version exists locally)
- `RELEASES` — Manifest file

### 4. Upload to GitHub

**Option A: Create a new release and upload automatically**

```powershell
vpk upload github --repoUrl https://github.com/Ginoshie/DigimonWorldNet6 --tag 1.4.0 --token YOUR_GITHUB_TOKEN --outputDir "C:\DW1Tool-Releases"
```

**Option B: Upload to an existing release**

Create your release manually on GitHub first (with your description and tag), then:

```powershell
vpk upload github --repoUrl https://github.com/Ginoshie/DigimonWorldNet6 --tag 1.4.0 --token YOUR_GITHUB_TOKEN --merge --outputDir "C:\DW1Tool-Releases"
```

> Generate a personal access token at https://github.com/settings/tokens with `repo` scope.

## Notes

- Delta packages are generated automatically when previous `.nupkg` files exist in the releases output folder (`C:\DW1Tool-Releases`). Keep previous release files there for delta generation. This folder must stay **outside** `C:\DW1 Tool` (the pack dir), or previous packages get bundled into the new one and bloat it.
- The app checks for updates on startup via the GitHub Releases API. Users with an older version will see the update button automatically.
- First-time users download and run `DW1Tool-Setup.exe`. Existing users receive delta updates in-app.
