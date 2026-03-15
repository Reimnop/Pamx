using System.Numerics;
using System.Text.Json.Serialization;

namespace Pamx.Parallax;

/// <summary>
/// Represents the parallax object's looping animation.
/// </summary>
public sealed class ParallaxObjectAnimation
{
    /// <summary>
    /// The end animation position.
    /// </summary>
    [JsonPropertyName("p")]
    public Vector2 Position { get; set; } = Vector2.Zero;

    /// <summary>
    /// The end animation scale.
    /// </summary>
    [JsonPropertyName("s")]
    public Vector2 Scale { get; set; } = Vector2.One;

    /// <summary>
    /// The end animation rotation.
    /// </summary>
    [JsonPropertyName("r")]
    public float Rotation { get; set; }

    /// <summary>
    /// Whether to animate the object's position or not.
    /// </summary>
    [JsonPropertyName("ap")]
    public bool AnimatePosition { get; set; }

    /// <summary>
    /// Whether to animate the object's scale or not.
    /// </summary>
    [JsonPropertyName("as")]
    public bool AnimateScale { get; set; }

    /// <summary>
    /// Whether to animate the object's rotation or not.
    /// </summary>
    [JsonPropertyName("ar")]
    public bool AnimateRotation { get; set; }

    /// <summary>
    /// The animation duration
    /// </summary>
    [JsonPropertyName("l")]
    public float LoopLength { get; set; } = 1.0f;

    /// <summary>
    /// The animation start delay
    /// </summary>
    [JsonPropertyName("ld")]
    public float LoopDelay { get; set; }
}