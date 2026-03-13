using System.Diagnostics.CodeAnalysis;

namespace Pamx.Neo.Keyframes;

/// <summary>
/// Represents a randomizable value keyframe for animation.
/// </summary>
[method: SetsRequiredMembers]
public class RandomKeyframe<T>(T value, float time = 0.0f, Ease ease = Ease.Linear)
    : FixedKeyframe<T>(value, time, ease)
{
    /// <summary>
    /// The keyframe's random mode
    /// </summary>
    public RandomMode RandomMode { get; set; } = RandomMode.None;

    /// <summary>
    /// The keyframe's random value.
    /// </summary>
    public T? RandomValue { get; set; }

    /// <summary>
    /// The keyframe's random interval.
    /// </summary>
    public float RandomInterval { get; set; } = 0.0f;
}