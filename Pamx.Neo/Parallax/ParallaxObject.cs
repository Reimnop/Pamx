using System.Numerics;
using Pamx.Neo.Objects;

namespace Pamx.Neo.Parallax;

/// <summary>
/// Represents a parallax object in the beatmap.
/// </summary>
public sealed class ParallaxObject : IIdentifiable<string>
{
    /// <summary>
    /// The ID of the object.
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

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
    /// The object's animation
    /// </summary>
    public ParallaxObjectAnimation Animation { get; set; } = new();

    /// <summary>
    /// The shape of the object.
    /// </summary>
    public ObjectShape Shape { get; set; } = ObjectShape.SquareSolid;

    /// <summary>
    /// The custom shape parameters of the object.
    /// </summary>
    public CustomShapeParams? CustomShapeParams { get; set; }

    /// <summary>
    /// The object's text value. Only has an effect when <see cref="Shape"/> is set to <see cref="ObjectShape.Text"/>
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// The beatmap object's gradient params.
    /// </summary>
    public GradientParams Gradient { get; set; } = new();

    /// <summary>
    /// The object's color index.
    /// </summary>
    public int Color { get; set; }
}