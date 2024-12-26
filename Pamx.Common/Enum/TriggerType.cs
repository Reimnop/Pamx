namespace Pamx.Common.Enum;

/// <summary>
/// The type of trigger
/// </summary>
public enum TriggerType
{
    /// <summary>
    /// Triggers at a time
    /// </summary>
    Time,
    
    /// <summary>
    /// Triggers when player is hit
    /// </summary>
    PlayerHit,
    
    /// <summary>
    /// Triggers when player dies
    /// </summary>
    PlayerDeath,
    
    /// <summary>
    /// Triggers when player starts
    /// </summary>
    PlayerStart,
    
    /// <summary>
    /// Unknown trigger type
    /// </summary>
    Unknown,
}