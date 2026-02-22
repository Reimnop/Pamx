using System.Numerics;
using System.Runtime.InteropServices;

namespace Pamx.Common.Data;

/// <summary>
/// The settings for lens distortion keyframes
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct LensDistortionData
{
    /// <summary>
    /// The intensity of the lens distortion effect
    /// </summary>
    public float Intensity;
    
    /// <summary>
    /// The center of the lens distortion effect
    /// </summary>
    public Vector2 Center;
}