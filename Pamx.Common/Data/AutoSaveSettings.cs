namespace Pamx.Common.Data;

/// <summary>
/// The editor settings for auto-saving
/// </summary>
public struct AutoSaveSettings()
{
    /// <summary>
    /// The maximum number of auto-saves to keep
    /// </summary>
    public int Max { get; set; } = 3;
    
    /// <summary>
    /// The interval in minutes between auto-saves
    /// </summary>
    public int Interval { get; set; } = 10;
    
    public override bool Equals(object? obj)
    {
        return obj is AutoSaveSettings settings &&
               Max == settings.Max &&
               Interval == settings.Interval;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(Max, Interval);
    }
    
    public static bool operator ==(AutoSaveSettings left, AutoSaveSettings right)
    {
        return left.Equals(right);
    }
    
    public static bool operator !=(AutoSaveSettings left, AutoSaveSettings right)
    {
        return !(left == right);
    }
}