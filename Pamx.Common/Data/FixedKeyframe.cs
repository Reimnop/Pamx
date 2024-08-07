using Pamx.Common.Enum;

namespace Pamx.Common.Data;

public struct FixedKeyframe<T>() where T : struct
{
    public float Time { get; set; } = 0.0f;
    public T Value { get; set; } = default;
    public Easing Easing { get; set; } = Easing.Linear;
}