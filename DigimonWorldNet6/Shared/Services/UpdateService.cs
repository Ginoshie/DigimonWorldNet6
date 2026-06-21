using Velopack;
using Velopack.Sources;

namespace Shared.Services;

public static class UpdateService
{
    private const string GITHUB_REPO_URL = "https://github.com/Ginoshie/DigimonWorldNet6";

    public static async Task<UpdateCheckResult> CheckForUpdateAsync()
    {
        UpdateManager manager = CreateUpdateManager();
        UpdateInfo? updateInfo = await manager.CheckForUpdatesAsync();

        if (updateInfo == null)
        {
            return new UpdateCheckResult(false, null, string.Empty);
        }

        return new UpdateCheckResult(true, updateInfo, updateInfo.TargetFullRelease.Version.ToString());
    }

    public static async Task DownloadUpdateAsync(UpdateInfo updateInfo)
    {
        UpdateManager manager = CreateUpdateManager();
        await manager.DownloadUpdatesAsync(updateInfo);
    }

    public static void ApplyUpdateAndRestart(UpdateInfo updateInfo)
    {
        UpdateManager manager = CreateUpdateManager();
        manager.ApplyUpdatesAndRestart(updateInfo);
    }

    private static UpdateManager CreateUpdateManager()
    {
        return new UpdateManager(new GithubSource(GITHUB_REPO_URL, null, false));
    }
}