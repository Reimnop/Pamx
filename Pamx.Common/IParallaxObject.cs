using System.Numerics;
using Pamx.Common.Data;
using Pamx.Common.Enum;

namespace Pamx.Common;

/// <summary>
/// Represents a parallax object in a layer
/// </summary>
public interface IParallaxObject : IReference<IParallaxObject>
{
    /// <inheritdoc />
    IParallaxObject IReference<IParallaxObject>.Value => this;
    
    /// <summary>
    /// The object's position
    /// </summary>
    Vector2 Position { get; set; }
    
    /// <summary>
    /// The object's scale
    /// </summary>
    Vector2 Scale { get; set; }
    
    /// <summary>
    /// The object's rotation
    /// </summary>
    float Rotation { get; set; }
    
    /// <summary>
    /// The object's animation
    /// </summary>
    ParallaxObjectAnimation Animation { get; set; }
    
    /// <summary>
    /// The object's shape
    /// </summary>
    ObjectShape Shape { get; set; }
    
    /// <summary>
    /// The object's text data. Only has an effect when <see cref="Shape"/>
    /// is <see cref="ObjectShape.Text"/>
    /// </summary>
    string? Text { get; set; }
    
    /// <summary>
    /// The object's color index
    /// </summary>
    int Color { get; set; }
}