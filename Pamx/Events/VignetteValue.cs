using System.Numerics;

namespace Pamx.Events;

/// <summary>
/// The value of the vignette event keyframe.
/// </summary>
public record struct VignetteValue()
{
    public static readonly VignetteValue Zero = new();

    /// <summary>
    /// The intensity of the vignette effect.
    /// </summary>
    public float Intensity { get; set; } = 0.0f;

    /// <summary>
    /// The smoothness of the vignette.
    /// </summary>
    public float Smoothness { get; set; } = 0.0f;

    /// <summary>
    /// The color of the vignette.
    /// </summary>
    public int Color { get; set; } = 9;

    /// <summary>
    /// Whether the vignette should be rounded or not.
    /// </summary>
    public bool IsRounded { get; set; } = false;

    /// <summary>
    /// How much the vignette should be rounded. (Legacy data, not used in modern versions of PA.)
    /// </summary>
    public float Roundness { get; set; } = 0.0f;

    /// <summary>
    /// The center of the vignette.
    /// </summary>
    public Vector2 Center { get; set; } = new(0.5f, 0.5f);
}