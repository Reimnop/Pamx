using Pamx.Common.Enum;

namespace Pamx.Common.Data;

public struct Keyframe<T>()
{
    public required float Time { get; set; }
    public required T Value { get; set; }
    public Ease Ease { get; set; } = Ease.Linear;
    public RandomMode RandomMode { get; set; } = RandomMode.None;
    public T? RandomValue { get; set; } = default;
    public float RandomInterval { get; set; } = 0.0f;
}