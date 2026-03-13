namespace Pamx.Neo.Keyframes;

/// <summary>
/// Represents a keyframe for animation.
/// </summary>
public abstract class Keyframe
{
    /// <summary>
    /// The keyframe's time.
    /// </summary>
    public float Time { get; set; }

    /// <summary>
    /// The keyframe's easing function type.
    /// </summary>
    public Ease Ease { get; set; } = Ease.Linear;
}