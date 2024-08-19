namespace Pamx.Common.Data;

public struct AutoSaveSettings()
{
    public int Max { get; set; } = 3;
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