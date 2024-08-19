using Pamx.Common.Enum;

namespace Pamx.Common.Data;

public struct BpmSettings()
{
    public BpmSnap Snap { get; set; } = BpmSnap.Objects;
    public float Value { get; set; } = 128.0f;
    public float Offset { get; set; } = 0.0f;
    
    public override bool Equals(object? obj)
    {
        return obj is BpmSettings settings &&
               Snap == settings.Snap &&
               Value == settings.Value &&
               Offset == settings.Offset;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(Snap, Value, Offset);
    }
    
    public static bool operator ==(BpmSettings left, BpmSettings right)
    {
        return left.Equals(right);
    }
    
    public static bool operator !=(BpmSettings left, BpmSettings right)
    {
        return !(left == right);
    }
}