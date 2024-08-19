using System.Numerics;

namespace Pamx.Common.Data;

/// <summary>
/// The settings for the editor grid overlay
/// </summary>
public struct GridSettings()
{
    /// <summary>
    /// The scale of the grid overlay
    /// </summary>
    public Vector2 Scale { get; set; } = new(2.0f, 2.0f);
    
    /// <summary>
    /// The thickness of each grid line
    /// </summary>
    public int Thickness { get; set; } = 2;
    
    /// <summary>
    /// The opacity of the grid overlay
    /// </summary>
    public float Opacity { get; set; } = 0.5f;
    
    /// <summary>
    /// The color index of the grid overlay
    /// </summary>
    public int Color { get; set; } = 0;
    
    public override bool Equals(object? obj)
    {
        return obj is GridSettings settings &&
               Scale == settings.Scale &&
               Thickness == settings.Thickness &&
               Opacity == settings.Opacity &&
               Color == settings.Color;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(Scale, Thickness, Opacity, Color);
    }
    
    public static bool operator ==(GridSettings left, GridSettings right)
    {
        return left.Equals(right);
    }
    
    public static bool operator !=(GridSettings left, GridSettings right)
    {
        return !(left == right);
    }
}