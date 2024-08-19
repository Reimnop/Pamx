namespace Pamx.Common.Enum;

/// <summary>
/// The color of the object in the timeline
/// </summary>
[Flags]
public enum ObjectTimelineColor
{
    /// <summary>
    /// Red
    /// </summary>
    Red = 0b001,
    
    /// <summary>
    /// Green
    /// </summary>
    Green = 0b010,
    
    /// <summary>
    /// Blue
    /// </summary>
    Blue = 0b100,
    
    /// <summary>
    /// Yellow
    /// </summary>
    Yellow = Red | Green,
    
    /// <summary>
    /// Cyan
    /// </summary>
    Cyan = Green | Blue,
    
    /// <summary>
    /// Magenta
    /// </summary>
    Magenta = Red | Blue,
    
    /// <summary>
    /// White
    /// </summary>
    White = Red | Green | Blue,
    
    /// <summary>
    /// No color
    /// </summary>
    None = 0,
}