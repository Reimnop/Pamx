namespace Pamx.Objects;

/// <summary>
/// The type of the beatmap object's gradient.
/// </summary>
public enum GradientType
{
    /// <summary>
    /// No gradient.
    /// </summary>
    None = 0,
    
    /// <summary>
    /// Right to left gradient render type.
    /// </summary>
    RightToLeftGradient = 1,
    
    /// <summary>
    /// Left to right gradient render type.
    /// </summary>
    LeftToRightGradient = 2,
    
    /// <summary>
    /// Outwards radial gradient render type.
    /// </summary>
    OutwardsGradient = 3,
    
    /// <summary>
    /// Inwards radial gradient render type.
    /// </summary>
    InwardsGradient = 4
}