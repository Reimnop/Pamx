namespace Pamx.Neo.Objects;

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
    /// Left to right gradient render type.
    /// </summary>
    LeftToRightGradient = 1,
    
    /// <summary>
    /// Right to left gradient render type.
    /// </summary>
    RightToLeftGradient = 2,
    
    /// <summary>
    /// Inwards radial gradient render type.
    /// </summary>
    InwardsGradient = 3,
    
    /// <summary>
    /// Outwards radial gradient render type.
    /// </summary>
    OutwardsGradient = 4
}