using System.Runtime.InteropServices;

namespace Pamx.Common.Data;

/// <summary>
/// The settings for grain keyframes
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct GrainData
{
    /// <summary>
    /// The intensity of the grain effect
    /// </summary>
    public float Intensity;
    
    /// <summary>
    /// The size of each individual grain
    /// </summary>
    public float Size;
    
    /// <summary>
    /// How much of the original image to mix with the grain effect
    /// </summary>
    public float Mix;
    
    /// <summary>
    /// Whether the grain effect should be colored
    /// </summary>
    public bool Colored;
}