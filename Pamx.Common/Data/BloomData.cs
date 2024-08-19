namespace Pamx.Common.Data;

/// <summary>
/// The settings for bloom keyframes
/// </summary>
public struct BloomData()
{
    /// <summary>
    /// The intensity of the bloom
    /// </summary>
    public float Intensity { get; set; } = 0.0f;
    
    /// <summary>
    /// How much the bloom should diffuse into surrounding pixels
    /// </summary>
    public float Diffusion { get; set; } = 0.0f;
    
    /// <summary>
    /// The color index of the tint of the bloom
    /// </summary>
    public int Color { get; set; } = 0;
}