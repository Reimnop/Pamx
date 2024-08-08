using Pamx.Common.Enum;

namespace Pamx.Common.Data;

public struct FixedKeyframe<T>()
{
    public required float Time { get; set; }
    public required T Value { get; set; }
    public Ease Ease { get; set; } = Ease.Linear;
}