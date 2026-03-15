using System.Text.Json.Serialization;
using Pamx.Objects;

namespace Pamx.Prefabs;

/// <summary>
/// Represents a prefab in the beatmap.
/// </summary>
public sealed class Prefab : IIdentifiable<string>
{
    /// <summary>
    /// The prefab's ID.
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// The prefab's name.
    /// </summary>
    [JsonPropertyName("n")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The prefab's description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The prefab's preview image. Must be a base64-encoded PNG or an empty string.
    /// </summary>
    public string Preview { get; set; } = string.Empty;

    /// <summary>
    /// The prefab's time offset.
    /// </summary>
    [JsonPropertyName("o")]
    public float Offset { get; set; } = 0.0f;

    /// <summary>
    /// The prefab's type
    /// </summary>
    public PrefabType Type { get; set; } = PrefabType.Character;

    /// <summary>
    /// The prefab's beatmap objects.
    /// </summary>
    [JsonPropertyName("objs")]
    public List<BeatmapObject> Objects { get; set; } = [];
}