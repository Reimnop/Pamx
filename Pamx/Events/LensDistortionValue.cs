using System.Numerics;

namespace Pamx.Events;

/// <summary>
/// The value of the lens distortion event keyframe.
/// </summary>
public record struct LensDistortionValue()
{
    public static readonly LensDistortionValue Zero = new();

    /// <summary>
    /// The intensity of the lens distortion effect.
    /// </summary>
    public float Intensity { get; set; } = 0.0f;
    
    /// <summary>
    /// The center of the lens distortion effect.
    /// </summary>
    public Vector2 Center { get; set; } = new(0.5f, 0.5f);
}