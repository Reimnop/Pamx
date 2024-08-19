using Pamx.Common.Enum;

namespace Pamx.Common.Data;

/// <summary>
/// The object's editor settings
/// </summary>
public struct ObjectEditorSettings()
{
    /// <summary>
    /// Whether the object is locked in editor
    /// </summary>
    public bool Locked { get; set; } = false;
    
    /// <summary>
    /// Whether the object is collapsed
    /// </summary>
    public bool Collapsed { get; set; } = false;
    
    /// <summary>
    /// The object's bin
    /// </summary>
    public int Bin { get; set; } = 0;
    
    /// <summary>
    /// The object's layer
    /// </summary>
    public int Layer { get; set; } = 0;
    
    /// <summary>
    /// The object strip's text color
    /// </summary>
    public ObjectTimelineColor TextColor { get; set; }
    
    /// <summary>
    /// The object strip's background color
    /// </summary>
    public ObjectTimelineColor BackgroundColor { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not ObjectEditorSettings settings)
            return false;

        return Locked == settings.Locked &&
               Collapsed == settings.Collapsed &&
               Bin == settings.Bin &&
               Layer == settings.Layer &&
               TextColor == settings.TextColor &&
               BackgroundColor == settings.BackgroundColor;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(Locked, Collapsed, Bin, Layer, TextColor, BackgroundColor);
    }
    
    public static bool operator ==(ObjectEditorSettings left, ObjectEditorSettings right)
    {
        return left.Equals(right);
    }
    
    public static bool operator !=(ObjectEditorSettings left, ObjectEditorSettings right)
    {
        return !left.Equals(right);
    }
}