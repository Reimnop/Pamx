using Pamx.Common.Enum;

namespace Pamx.Common.Data;

/// <summary>
/// The randomizable keyframe for animation
/// </summary>
/// <typeparam name="T">The value type of the keyframe</typeparam>
public struct Keyframe<T>()
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
    /// The keyframe's random mode
    /// </summary>
    public RandomMode RandomMode { get; set; } = RandomMode.None;
    
    /// <summary>
    /// The keyframe's random value
    /// </summary>
    public T? RandomValue { get; set; } = default;
    
    /// <summary>
    /// The keyframe's random interval
    /// </summary>
    public float RandomInterval { get; set; } = 0.0f;
    
    /// <summary>
    /// Creates a new <see cref="Keyframe{T}"/>
    /// </summary>
    /// <param name="time">The keyframe's time</param>
    /// <param name="value">The keyframe's value</param>
    /// <param name="ease">The keyframe's ease</param>
    public Keyframe(float time, T value, Ease ease = Ease.Linear) : this()
    {
        Time = time;
        Value = value;
        Ease = ease;
    }
}