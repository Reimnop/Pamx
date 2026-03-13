using System.Diagnostics.CodeAnalysis;

namespace Pamx.Neo.Keyframes;

/// <summary>
/// Represents a fixed value keyframe for animation.
/// </summary>
/// <typeparam name="T">The type of the keyframe's value.</typeparam>
public class FixedKeyframe<T>() : Keyframe
{
    /// <summary>
    /// The keyframe's value.
    /// </summary>
    public required T Value { get; set; }

    [SetsRequiredMembers]
    public FixedKeyframe(T value, float time = 0.0f, Ease ease = Ease.Linear) : this()
    {
        Time = time;
        Value = value;
        Ease = ease;
    }
}