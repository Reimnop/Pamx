using System.Text.Json.Serialization;
using Pamx.Converters;

namespace Pamx.Editor;

/// <summary>
/// Represents the settings for quick spawning prefabs in the editor.
/// </summary>
public sealed class EditorPrefabSpawn
{
    /// <summary>
    /// Whether the current prefab spawn is expanded.
    /// </summary>
    [JsonPropertyName("expanded")]
    public bool IsExpanded { get; set; } = false;

    /// <summary>
    /// Whether the current prefab spawn is active.
    /// </summary>
    [JsonPropertyName("active")]
    public bool IsActive { get; set; } = false;

    /// <summary>
    /// The ID of the prefab to spawn.
    /// </summary>
    [JsonPropertyName("prefab")]
    public string PrefabId { get; set; } = string.Empty;

    /// <summary>
    /// The keycodes to be used to spawn the prefab
    /// </summary>
    [JsonConverter(typeof(EPSKeycodeListConverter))]
    public List<string> Keycodes { get; set; } = [];
}