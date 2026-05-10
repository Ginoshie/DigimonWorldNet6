using Velopack;

namespace Shared.Services;

public record UpdateCheckResult(bool HasUpdate, UpdateInfo? UpdateInfo, string NewVersion);