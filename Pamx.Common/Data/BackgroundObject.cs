using System.Numerics;
using Pamx.Common.Enum;

namespace Pamx.Common.Data;

/// <summary>
/// The background object
/// </summary>
public struct BackgroundObject()
{
    /// <summary>
    /// Whether the object is active
    /// </summary>
    public bool Active { get; set; } = false;
    
    /// <summary>
    /// The object's name
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// The object's position
    /// </summary>
    public Vector2 Position { get; set; }
    
    /// <summary>
    /// The object's scale
    /// </summary>
    public Vector2 Scale { get; set; }
    
    /// <summary>
    /// The object's rotation
    /// </summary>
    public float Rotation { get; set; }
    
    /// <summary>
    /// The object's color
    /// </summary>
    public int Color { get; set; }
    
    /// <summary>
    /// The object's depth
    /// </summary>
    public int Depth { get; set; }
    
    /// <summary>
    /// Whether the object should fade into the background
    /// </summary>
    public bool Fade { get; set; }
    
    /// <summary>
    /// How the object should react to the music
    /// </summary>
    public BackgroundObjectReactiveType ReactiveType { get; set; }
    
    /// <summary>
    /// How much the object should react to the music
    /// </summary>
    public float ReactiveScale { get; set; }
}