using System.Numerics;
using System.Runtime.InteropServices;

namespace Pamx.Common.Data;

/// <summary>
/// The settings for vignette keyframes
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct VignetteData
{
    /// <summary>
    /// The intensity of the vignette effect
    /// </summary>
    public float Intensity;

    /// <summary>
    /// The smoothness of the vignette effect
    /// </summary>
    public float Smoothness;
    
    /// <summary>
    /// The color of the vignette effect
    /// </summary>
    public int? Color;
    
    /// <summary>
    /// Whether the vignette effect should be rounded
    /// </summary>
    public bool Rounded;
    
    /// <summary>
    /// How much the vignette effect should be rounded
    /// </summary>
    public float? Roundness;
    
    /// <summary>
    /// The center of the vignette effect
    /// </summary>
    public Vector2 Center;
}