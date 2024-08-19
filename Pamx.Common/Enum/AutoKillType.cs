namespace Pamx.Common.Enum;

/// <summary>
/// The base time at which the object is killed
/// </summary>
public enum AutoKillType
{
    /// <summary>
    /// Do not kill the object
    /// </summary>
    NoAutoKill,
    
    /// <summary>
    /// Kill the object at the last keyframe
    /// </summary>
    LastKeyframe,
    
    /// <summary>
    /// Kill the object at the last keyframe with a time offset
    /// </summary>
    LastKeyframeOffset,
    
    /// <summary>
    /// Kill the object at a fixed time since the object's start time
    /// </summary>
    FixedTime,
    
    /// <summary>
    /// Kill the object at a fixed time since the start of the beatmap
    /// </summary>
    SongTime,
}