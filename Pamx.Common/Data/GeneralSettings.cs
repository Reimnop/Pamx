namespace Pamx.Common.Data;

public struct GeneralSettings()
{
    public float CollapseLength { get; set; } = 0.4f;
    public int Complexity { get; set; } = 0;
    public int Theme { get; set; } = 0;
    public bool SelectTextObjects { get; set; } = true;
    public bool SelectParallaxTextObjects { get; set; } = false;
    
    public override bool Equals(object? obj)
    {
        return obj is GeneralSettings settings &&
               CollapseLength == settings.CollapseLength &&
               Complexity == settings.Complexity &&
               Theme == settings.Theme &&
               SelectTextObjects == settings.SelectTextObjects &&
               SelectParallaxTextObjects == settings.SelectParallaxTextObjects;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(CollapseLength, Complexity, Theme, SelectTextObjects, SelectParallaxTextObjects);
    }
    
    public static bool operator ==(GeneralSettings left, GeneralSettings right)
    {
        return left.Equals(right);
    }
    
    public static bool operator !=(GeneralSettings left, GeneralSettings right)
    {
        return !(left == right);
    }
}