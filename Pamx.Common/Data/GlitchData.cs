using System.Runtime.InteropServices;

namespace Pamx.Common.Data;

/// <summary>
/// The settings for glitch keyframes
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct GlitchData
{
    /// <summary>
    /// The intensity of the glitch effect
    /// </summary>
    public float Intensity;
    
    /// <summary>
    /// The speed of the glitch effect
    /// </summary>
    public float Speed;
    
    /// <summary>
    /// The width of each individual glitch
    /// </summary>
    public float Width;
}