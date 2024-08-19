namespace Pamx.Common.Enum;

/// <summary>
/// The random mode for keyframes
/// </summary>
public enum RandomMode
{
    /// <summary>
    /// No randomization
    /// </summary>
    None,
    
    /// <summary>
    /// Randomize between a range
    /// </summary>
    Range,
    
    /// <summary>
    /// Randomize between a range, floored to the nearest integer
    /// </summary>
    Snap,
    
    /// <summary>
    /// Select between two random values
    /// </summary>
    Select,
    
    /// <summary>
    /// Scale the original value by a random factor
    /// </summary>
    Scale
}