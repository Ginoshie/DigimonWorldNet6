using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;

namespace Shared.Services;

public static class UpdateService
{
    private const string GITHUB_API_URL = "https://api.github.com/repos/Ginoshie/DigimonWorldNet6/releases/latest";

    private static readonly HttpClient _httpClient = CreateHttpClient();

    private static HttpClient CreateHttpClient()
    {
        HttpClient client = new();
        client.DefaultRequestHeaders.Add("User-Agent", "DigimonWorldTool");
        return client;
    }

    public static async Task<UpdateCheckResult> CheckForUpdateAsync()
    {
        try
        {
            JsonElement release = await _httpClient.GetFromJsonAsync<JsonElement>(GITHUB_API_URL);

            string tagName = release.GetProperty("tag_name").GetString() ?? string.Empty;
            string latestVersion = ExtractVersion(tagName);

            if (!IsNewer(latestVersion, AppVersion.Current))
            {
                return new UpdateCheckResult(false, string.Empty, string.Empty);
            }

            string downloadUrl = FindExeDownloadUrl(release);

            return new UpdateCheckResult(true, downloadUrl, latestVersion);
        } catch
        {
            return new UpdateCheckResult(false, string.Empty, string.Empty);
        }
    }

    public static async Task DownloadAndApplyUpdateAsync(string downloadUrl, string newVersion)
    {
        string currentExePath = Environment.ProcessPath!;
        string directory = Path.GetDirectoryName(currentExePath)!;
        string newExeName = $"DW.1.Tool.{newVersion}.exe";
        string newExePath = Path.Combine(directory, newExeName);
        string updateExePath = currentExePath + ".update";

        await using (Stream stream = await _httpClient.GetStreamAsync(downloadUrl))
        await using (FileStream fileStream = File.Create(updateExePath))
        {
            await stream.CopyToAsync(fileStream);
        }

        string script = $"""
            @echo off
            timeout /t 2 /nobreak >nul
            del "{currentExePath}"
            move /Y "{updateExePath}" "{newExePath}"
            start "" "{newExePath}"
            """;

        string scriptPath = Path.Combine(Path.GetTempPath(), "dw_tool_update.cmd");
        await File.WriteAllTextAsync(scriptPath, script);

        Process.Start(new ProcessStartInfo
        {
            FileName = scriptPath,
            UseShellExecute = true,
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden
        });

        Environment.Exit(0);
    }

    private static string ExtractVersion(string tagName)
    {
        string cleaned = tagName.TrimStart('v', 'V');
        return cleaned;
    }

    private static bool IsNewer(string latest, string current)
    {
        if (Version.TryParse(NormalizeVersion(latest), out Version? latestVer) &&
            Version.TryParse(NormalizeVersion(current), out Version? currentVer))
        {
            return latestVer > currentVer;
        }
        return false;
    }

    private static string NormalizeVersion(string version)
    {
        int dotCount = version.Count(c => c == '.');
        return dotCount switch
        {
            1 => version + ".0.0",
            2 => version + ".0",
            _ => version
        };
    }

    private static string FindExeDownloadUrl(JsonElement release)
    {
        if (!release.TryGetProperty("assets", out JsonElement assets))
        {
            return string.Empty;
        }

        foreach (JsonElement asset in assets.EnumerateArray())
        {
            string name = asset.GetProperty("name").GetString() ?? string.Empty;
            if (name.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
            {
                return asset.GetProperty("browser_download_url").GetString() ?? string.Empty;
            }
        }
        return string.Empty;
    }
}

public record UpdateCheckResult(bool HasUpdate, string DownloadUrl, string NewVersion);
