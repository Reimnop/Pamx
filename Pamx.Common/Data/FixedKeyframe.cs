using Pamx.Common.Enum;

namespace Pamx.Common.Data;

public struct FixedKeyframe<T>()
{
    public float Time { get; set; }
    public T Value { get; set; }
    public Ease Ease { get; set; } = Ease.Linear;
    
    public FixedKeyframe(float time, T value, Ease ease = Ease.Linear) : this()
    {
        Time = time;
        Value = value;
        Ease = ease;
    }
}