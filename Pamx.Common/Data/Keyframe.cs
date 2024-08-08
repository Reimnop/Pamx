using Pamx.Common.Enum;

namespace Pamx.Common.Data;

public struct Keyframe<T>() where T : struct
{
    public float Time { get; set; } = 0.0f;
    public T Value { get; set; } = default;
    public Ease Ease { get; set; } = Ease.Linear;
    public RandomMode RandomMode { get; set; } = RandomMode.None;
    public T RandomValue { get; set; } = default;
    public float RandomInterval { get; set; } = 0.0f;
}