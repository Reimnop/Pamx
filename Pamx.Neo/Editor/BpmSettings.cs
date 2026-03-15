using System.Text.Json.Serialization;

namespace Pamx.Neo.Editor;

/// <summary>
/// Represents the beatmap's BPM snap settings.
/// </summary>
public sealed class BpmSettings
{
    /// <summary>
    /// The types of data that should be snapped to BPM. 
    /// </summary>
    [JsonPropertyName("snap")]
    public BpmSnapType Type { get; set; } = BpmSnapType.Objects;

    /// <summary>
    /// The BPM value.
    /// </summary>
    [JsonPropertyName("bpm_value")]
    public float Value { get; set; } = 140.0f;

    /// <summary>
    /// The BPM snap offset.
    /// </summary>
    [JsonPropertyName("bpm_offset")]
    public float Offset { get; set; }

    /// <summary>
    /// Which beat should the data be snapped to.
    /// </summary>
    [JsonPropertyName("bpm_snap")]
    public int Snap { get; set; } = 1;

    /// <summary>
    /// Whether the <see cref="Snap"/> value should be treated as a whole (value) or a fraction (1 / value).
    /// </summary>
    [JsonPropertyName("bpm_snap_fraction")]
    public bool IsSnapFraction { get; set; }
}

/// <summary>
/// The type of data that should be snapped to BPM.
/// </summary>
[Flags]
public enum BpmSnapType
{
    None = 0,
    Objects = 1 << 0,
    ObjectKeyframes = 1 << 1,
    Checkpoints = 1 << 2,
    Events = 1 << 3,
    All = Objects | ObjectKeyframes | Checkpoints | Events
}