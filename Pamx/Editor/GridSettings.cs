using System.Numerics;

namespace Pamx.Editor;

/// <summary>
/// Represents the beatmap's grid overlay settings.
/// </summary>
public sealed class GridSettings
{
    /// <summary>
    /// The grid overlay's scale.
    /// </summary>
    public Vector2 Scale { get; set; } = new(2.0f, 2.0f);
    
    /// <summary>
    /// The thickness of each grid line.
    /// </summary>
    public int Thickness { get; set; } = 2;
    
    /// <summary>
    /// The opacity of the grid overlay
    /// </summary>
    public float Opacity { get; set; } = 0.5f;
    
    /// <summary>
    /// The color index of the grid overlay
    /// </summary>
    public int Color { get; set; }
}