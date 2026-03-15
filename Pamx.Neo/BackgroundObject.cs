using System.Numerics;

namespace Pamx.Neo;

/// <summary>
/// Represents a 3D background object in the beatmap. (Legacy data, not used in modern versions of PA.)
/// </summary>
public sealed class BackgroundObject
{
    /// <summary>
    /// Whether the object is active or not.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// The object's name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The object's position.
    /// </summary>
    public Vector2 Position { get; set; } = Vector2.Zero;

    /// <summary>
    /// The object's scale.
    /// </summary>
    public Vector2 Scale { get; set; } = Vector2.One;

    /// <summary>
    /// The object's rotation.
    /// </summary>
    public float Rotation { get; set; }

    /// <summary>
    /// The object's theme color index.
    /// </summary>
    public int Color { get; set; }

    /// <summary>
    /// The object's depth layer.
    /// </summary>
    public int Depth { get; set; }

    /// <summary>
    /// Whether the object should fade into the background or not.
    /// </summary>
    public bool IsFading { get; set; }

    /// <summary>
    /// How the object should react to the music.
    /// </summary>
    public BackgroundObjectReactivityType ReactivityType { get; set; } = BackgroundObjectReactivityType.None;

    /// <summary>
    /// How much the object should react to the music.
    /// </summary>
    public float ReactiveScale { get; set; }
}