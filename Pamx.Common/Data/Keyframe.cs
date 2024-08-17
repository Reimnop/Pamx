using Pamx.Common.Enum;

namespace Pamx.Common.Data;

public struct Keyframe<T>()
{
    public float Time { get; set; }
    public T Value { get; set; }
    public Ease Ease { get; set; } = Ease.Linear;
    public RandomMode RandomMode { get; set; } = RandomMode.None;
    public T? RandomValue { get; set; } = default;
    public float RandomInterval { get; set; } = 0.0f;
    
    public Keyframe(float time, T value, Ease ease = Ease.Linear) : this()
    {
        Time = time;
        Value = value;
        Ease = ease;
    }
}