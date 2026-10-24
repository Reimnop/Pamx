using System.Numerics;

namespace Pamx.Events;

/// <summary>
/// The value of the player event keyframe.
/// </summary>
public record struct PlayerValue()
{
    public static readonly PlayerValue Zero = new();

    /// <summary>
    /// The force that is applied to the players.
    /// </summary>
    public Vector2 Force { get; set; } = Vector2.Zero;

    /// <summary>
    /// How the keyframe sets the depth of field.
    /// </summary>
    public PlayerDepthOfFieldMode DepthOfFieldMode { get; set; } = PlayerDepthOfFieldMode.Level;

    /// <summary>
    /// Where the depth of field is focused, if the keyframe turns it on. NaN when the keyframe does not say,
    /// which is the beatmap's parallax settings' value. (Not a nullable, so that the value is a flat struct.)
    /// </summary>
    public float DepthOfFieldFocus { get; set; } = float.NaN;
}
