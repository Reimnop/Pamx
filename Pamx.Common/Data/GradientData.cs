using Pamx.Common.Enum;

namespace Pamx.Common.Data;

/// <summary>
/// The settings for gradient keyframes
/// </summary>
public struct GradientData()
{
    /// <summary>
    /// The intensity of the gradient overlay
    /// </summary>
    public float Intensity { get; set; } = 0.0f;
    
    /// <summary>
    /// The rotation of the gradient overlay, in degrees
    /// </summary>
    public float Rotation { get; set; } = 0.0f;
    
    /// <summary>
    /// The first color of the gradient overlay
    /// </summary>
    public int ColorA { get; set; } = 0;
    
    /// <summary>
    /// The second color of the gradient overlay
    /// </summary>
    public int ColorB { get; set; } = 0;
    
    /// <summary>
    /// The mode of the gradient overlay
    /// </summary>
    public GradientOverlayMode Mode { get; set; } = GradientOverlayMode.Linear;
}