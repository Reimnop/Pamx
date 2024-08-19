namespace Pamx.Common.Data;

/// <summary>
/// The general settings for the editor
/// </summary>
public struct GeneralSettings()
{
    /// <summary>
    /// How long the collapsed objects should be in the timeline
    /// </summary>
    public float CollapseLength { get; set; } = 0.4f;
    
    /// <summary>
    /// Editor complexity, currently unused
    /// </summary>
    public int Complexity { get; set; } = 0;
    
    /// <summary>
    /// Editor theme, currently unused
    /// </summary>
    public int Theme { get; set; } = 0;
    
    /// <summary>
    /// Whether text objects can be selected by clicking on them
    /// </summary>
    public bool SelectTextObjects { get; set; } = true;
    
    /// <summary>
    /// Whether parallax text objects can be selected by clicking on them
    /// </summary>
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