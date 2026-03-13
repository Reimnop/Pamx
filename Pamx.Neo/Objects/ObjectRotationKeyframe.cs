using System.Diagnostics.CodeAnalysis;
using Pamx.Neo.Keyframes;

namespace Pamx.Neo.Objects;

/// <summary>
/// Repersents a beatmap object rotation keyframe.
/// </summary>
[method: SetsRequiredMembers]
public class ObjectRotationKeyframe(float value, float time = 0.0f, Ease ease = Ease.Linear)
    : RandomKeyframe<float>(value, time, ease)
{
    /// <summary>
    /// Whether the value is relative to the previous keyframe's value or not.
    /// </summary>
    public bool IsRelative { get; set; } = false;
}