namespace Pamx.Events;

/// <summary>
/// How a player event keyframe sets the depth of field.
/// </summary>
public enum PlayerDepthOfFieldMode
{
    /// <summary>
    /// The depth of field is what the beatmap's parallax settings say.
    /// </summary>
    Level,

    /// <summary>
    /// The depth of field is on, and focused where the keyframe says.
    /// </summary>
    On,

    /// <summary>
    /// The depth of field is off.
    /// </summary>
    Off
}
