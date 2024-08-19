using System.Numerics;

namespace Pamx.Common.Data;

public struct GridSettings()
{
    public Vector2 Scale { get; set; } = new(2.0f, 2.0f);
    public int Thickness { get; set; } = 2;
    public float Opacity { get; set; } = 0.5f;
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