using System.Numerics;

namespace Pamx.Common.Data;

/// <summary>
/// The animation of a parallax object
/// </summary>
public struct ParallaxObjectAnimation()
{
    /// <summary>
    /// The end animation position. Set to null to not animate position
    /// </summary>
    public Vector2? Position { get; set; } = null;
    
    /// <summary>
    /// The end animation scale. Set to null to not animate scale
    /// </summary>
    public Vector2? Scale { get; set; } = null;
    
    /// <summary>
    /// The end animation rotation. Set to null to not animate rotation
    /// </summary>
    public float? Rotation { get; set; } = null;
    
    /// <summary>
    /// The animation duration
    /// </summary>
    public float LoopLength { get; set; } = 0.0f;
    
    /// <summary>
    /// The animation start delay
    /// </summary>
    public float LoopDelay { get; set; } = 0.0f;
}