using Pamx.Common.Enum;

namespace Pamx.Common.Data;

/// <summary>
/// The fixed keyframe for animation
/// </summary>
/// <typeparam name="T">The value type of the keyframe</typeparam>
public struct FixedKeyframe<T>()
{
    /// <summary>
    /// The keyframe's time
    /// </summary>
    public float Time { get; set; }
    
    /// <summary>
    /// The keyframe's value
    /// </summary>
    public T Value { get; set; }
    
    /// <summary>
    /// The keyframe's ease
    /// </summary>
    public Ease Ease { get; set; } = Ease.Linear;
    
    /// <summary>
    /// Creates a new <see cref="FixedKeyframe{T}"/>
    /// </summary>
    /// <param name="time">The keyframe's time</param>
    /// <param name="value">The keyframe's value</param>
    /// <param name="ease">The keyframe's ease</param>
    public FixedKeyframe(float time, T value, Ease ease = Ease.Linear) : this()
    {
        Time = time;
        Value = value;
        Ease = ease;
    }
}