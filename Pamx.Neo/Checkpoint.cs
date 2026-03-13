using System.Numerics;
using System.Text.Json.Serialization;

namespace Pamx.Neo;

/// <summary>
/// Represents a checkpoint in the beatmap
/// </summary>
public sealed class Checkpoint : IIdentifiable<string>
{
    /// <summary>
    /// The checkpoint's ID.
    /// </summary>
    [JsonPropertyName("ID")]
    public required string Id { get; set; }

    /// <summary>
    /// The checkpoint's name.
    /// </summary>
    [JsonPropertyName("n")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The checkpoint's time.
    /// </summary>
    [JsonPropertyName("t")]
    public float Time { get; set; }

    /// <summary>
    /// The position of the player upon respawning at the checkpoint.
    /// </summary>
    [JsonPropertyName("p")]
    public Vector2 Position { get; set; } = Vector2.Zero;
}