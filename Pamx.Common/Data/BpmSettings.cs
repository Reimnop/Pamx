using Pamx.Common.Enum;

namespace Pamx.Common.Data;

/// <summary>
/// The editor settings for beatmap bpm
/// </summary>
public struct BpmSettings()
{
    /// <summary>
    /// The bpm snap settings
    /// </summary>
    public BpmSnap Snap { get; set; } = BpmSnap.Objects;
    
    /// <summary>
    /// The bpm value
    /// </summary>
    public float Value { get; set; } = 128.0f;
    
    /// <summary>
    /// The time offset
    /// </summary>
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