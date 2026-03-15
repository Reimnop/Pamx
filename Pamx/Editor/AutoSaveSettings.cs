using System.Text.Json.Serialization;

namespace Pamx.Editor;

/// <summary>
/// Represents the beatmap's auto-save settings.
/// </summary>
public sealed class AutoSaveSettings
{
    /// <summary>
    /// The maximum number of auto-saves to keep
    /// </summary>
    [JsonPropertyName("as_max")]
    public int Max { get; set; } = 3;

    /// <summary>
    /// The interval in minutes between auto-saves
    /// </summary>
    [JsonPropertyName("as_interval")]
    public int Interval { get; set; } = 10;
}