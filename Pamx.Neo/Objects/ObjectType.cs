namespace Pamx.Neo.Objects;

/// <summary>
/// The type of the beatmap object.
/// </summary>
public enum ObjectType
{
    /// <summary>
    /// Legacy normal object type.
    /// </summary>
    LegacyNormal,
    
    /// <summary>
    /// Legacy helper object type.
    /// </summary>
    LegacyHelper,
    
    /// <summary>
    /// Legacy decoration object type.
    /// </summary>
    LegacyDecoration,
    
    /// <summary>
    /// Legacy empty object type.
    /// </summary>
    LegacyEmpty,
    
    /// <summary>
    /// An object that hits the player.
    /// </summary>
    Hit,
    
    /// <summary>
    /// An object that doesn't hit the player.
    /// </summary>
    NoHit,
    
    /// <summary>
    /// An object that is invisible and doesn't hit the player.
    /// </summary>
    Empty,
}