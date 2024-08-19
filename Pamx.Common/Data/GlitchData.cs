namespace Pamx.Common.Data;

/// <summary>
/// The settings for glitch keyframes
/// </summary>
public struct GlitchData()
{
    /// <summary>
    /// The intensity of the glitch effect
    /// </summary>
    public float Intensity { get; set; } = 0.0f;
    
    /// <summary>
    /// The speed of the glitch effect
    /// </summary>
    public float Speed { get; set; } = 0.0f;
    
    /// <summary>
    /// The width of each individual glitch
    /// </summary>
    public float Width { get; set; } = 0.0f;
}