namespace Pamx.Common.Data;

/// <summary>
/// The settings for grain keyframes
/// </summary>
public struct GrainData()
{
    /// <summary>
    /// The intensity of the grain effect
    /// </summary>
    public float Intensity { get; set; } = 0.0f;
    
    /// <summary>
    /// The size of each individual grain
    /// </summary>
    public float Size { get; set; } = 0.0f;
    
    /// <summary>
    /// How much of the original image to mix with the grain effect
    /// </summary>
    public float Mix { get; set; } = 0.0f;
    
    /// <summary>
    /// Whether the grain effect should be colored
    /// </summary>
    public bool Colored { get; set; } = false;
}