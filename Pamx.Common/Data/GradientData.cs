using System.Runtime.InteropServices;
using Pamx.Common.Enum;

namespace Pamx.Common.Data;

/// <summary>
/// The settings for gradient keyframes
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct GradientData
{
    /// <summary>
    /// The intensity of the gradient overlay
    /// </summary>
    public float Intensity;
    
    /// <summary>
    /// The rotation of the gradient overlay, in degrees
    /// </summary>
    public float Rotation;
    
    /// <summary>
    /// The first color of the gradient overlay
    /// </summary>
    public int ColorA;
    
    /// <summary>
    /// The second color of the gradient overlay
    /// </summary>
    public int ColorB;
    
    /// <summary>
    /// The mode of the gradient overlay
    /// </summary>
    public GradientOverlayMode Mode;
}