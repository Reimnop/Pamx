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
    /// Whether the value is absolute or relative to the previous keyframe's value.
    /// </summary>
    public bool IsAbsolute { get; set; } = false;
}