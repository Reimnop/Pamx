namespace Pamx.Objects;

/// <summary>
/// The type of the time at which the beatmap object is killed.
/// </summary>
public enum AutoKillType
{
    /// <summary>
    /// Do not kill the object at all.
    /// </summary>
    NoAutoKill,
    
    /// <summary>
    /// Kill the object at its last keyframe.
    /// </summary>
    LastKeyframe,
    
    /// <summary>
    /// Kill the object at its last keyframe with a time offset.
    /// </summary>
    LastKeyframeOffset,
    
    /// <summary>
    /// Kill the object at a fixed time since its start time.
    /// </summary>
    FixedTime,
    
    /// <summary>
    /// Kill the object at a fixed time since the start of the beatmap.
    /// </summary>
    SongTime,
}