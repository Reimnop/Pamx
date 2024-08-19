using System.Numerics;

namespace Pamx.Common.Data;

/// <summary>
/// The settings for lens distortion keyframes
/// </summary>
public struct LensDistortionData()
{
    /// <summary>
    /// The intensity of the lens distortion effect
    /// </summary>
    public float Intensity { get; set; } = 0.0f;
    
    /// <summary>
    /// The center of the lens distortion effect
    /// </summary>
    public Vector2 Center { get; set; } = Vector2.Zero;
}