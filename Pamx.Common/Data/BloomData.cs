using System.Runtime.InteropServices;

namespace Pamx.Common.Data;

/// <summary>
/// The settings for bloom keyframes
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct BloomData
{
    /// <summary>
    /// The intensity of the bloom
    /// </summary>
    public float Intensity;

    /// <summary>
    /// How much the bloom should diffuse into surrounding pixels
    /// </summary>
    public float Diffusion;

    /// <summary>
    /// The color index of the tint of the bloom
    /// </summary>
    public int Color;
}