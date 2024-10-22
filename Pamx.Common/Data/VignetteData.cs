using System.Numerics;

namespace Pamx.Common.Data;

/// <summary>
/// The settings for vignette keyframes
/// </summary>
public struct VignetteData()
{
    /// <summary>
    /// The intensity of the vignette effect
    /// </summary>
    public float Intensity { get; set; } = 0.0f;
    
    /// <summary>
    /// The smoothness of the vignette effect
    /// </summary>
    public float Smoothness { get; set; } = 0.0f;
    
    /// <summary>
    /// The color of the vignette effect
    /// </summary>
    public int? Color { get; set; } = null;
    
    /// <summary>
    /// Whether the vignette effect should be rounded
    /// </summary>
    public bool Rounded { get; set; } = false;
    
    /// <summary>
    /// How much the vignette effect should be rounded
    /// </summary>
    public float? Roundness { get; set; } = null;
    
    /// <summary>
    /// The center of the vignette effect
    /// </summary>
    public Vector2 Center { get; set; } = Vector2.Zero;
}