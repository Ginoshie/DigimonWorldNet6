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
vpk pack --packId DW1Tool --packTitle "DW 1 Tool" --packVersion 1.4.0 --packDir "C:\DW1 Tool" --mainExe Frontend.WPF.exe --icon "C:\git\DigimonWorldNet6\DigimonWorldNet6\Frontend.WPF\Images\Misc\app-icon.ico" --framework net10.0-x64-desktop
```

This generates files in the `Releases` folder:
- `DW1Tool-Setup.exe` — Installer for new users
- `DW1Tool-1.4.0-full.nupkg` — Full update package
- `DW1Tool-1.4.0-delta.nupkg` — Delta update package (only if a previous version exists locally)
- `RELEASES` — Manifest file

### 4. Upload to GitHub

**Option A: Create a new release and upload automatically**

```powershell
vpk upload github --repoUrl https://github.com/Ginoshie/DigimonWorldNet6 --tag v1.4.0 --token YOUR_GITHUB_TOKEN
```

**Option B: Upload to an existing release**

Create your release manually on GitHub first (with your description and tag), then:

```powershell
vpk upload github --repoUrl https://github.com/Ginoshie/DigimonWorldNet6 --tag v1.4.0 --token YOUR_GITHUB_TOKEN --merge
```

> Generate a personal access token at https://github.com/settings/tokens with `repo` scope.

## Notes

- Delta packages are generated automatically when previous `.nupkg` files exist in the `Releases` folder. Keep previous release files there for delta generation.
- The app checks for updates on startup via the GitHub Releases API. Users with an older version will see the update button automatically.
- First-time users download and run `DW1Tool-Setup.exe`. Existing users receive delta updates in-app.

