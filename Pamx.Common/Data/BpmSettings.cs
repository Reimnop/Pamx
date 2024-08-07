using Pamx.Common.Enum;

namespace Pamx.Common.Data;

public struct BpmSettings()
{
    public BpmSnap Snap { get; set; } = BpmSnap.Objects;
    public float Value { get; set; } = 128.0f;
    public float Offset { get; set; } = 0.0f;
}