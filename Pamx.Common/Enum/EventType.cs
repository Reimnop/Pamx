namespace Pamx.Common.Enum;

/// <summary>
/// The type of event on triggers
/// </summary>
public enum EventType
{
    /// <summary>
    /// Execute a visual novel ink script
    /// </summary>
    VnInk,
    
    /// <summary>
    /// Execute a visual novel timeline script
    /// </summary>
    VnTimeline,
    
    /// <summary>
    /// Show a text bubble above player
    /// </summary>
    PlayerBubble,
    
    /// <summary>
    /// Force player to a location
    /// </summary>
    PlayerLocation,
    
    /// <summary>
    /// Enable or disable player dash ability
    /// </summary>
    PlayerDash,
    
    /// <summary>
    /// Enable or disable player movement in the X axis
    /// </summary>
    PlayerXMovement,
    
    /// <summary>
    /// Enable or disable player movement in the Y axis
    /// </summary>
    PlayerYMovement,
    
    /// <summary>
    /// Rotate player
    /// </summary>
    PlayerDashDirection,
    
    /// <summary>
    /// Rotate the parallax background
    /// </summary>
    BgSpin,
    
    /// <summary>
    /// Move the parallax background
    /// </summary>
    BgMove,
}