using System.Numerics;
using Pamx.Common.Data;
using Pamx.Common.Enum;

namespace Pamx.Common;

/// <summary>
/// An object within the game
/// </summary>
public interface IObject : IReference<IObject>
{
    /// <inheritdoc />
    IObject IReference<IObject>.Value => this;
    
    /// <summary>
    /// The name of the object
    /// </summary>
    string Name { get; set; }
    
    /// <summary>
    /// The time at which the object is spawned
    /// </summary>
    float StartTime { get; set; }
    
    /// <summary>
    /// The base time at which the object is killed
    /// </summary>
    AutoKillType AutoKillType { get; set; }
    
    /// <summary>
    /// The offset from the base time at which the object is killed
    /// </summary>
    float AutoKillOffset { get; set; }
    
    /// <summary>
    /// The object's transform center
    /// </summary>
    Vector2 Origin { get; set; }
    
    /// <summary>
    /// The object's type
    /// </summary>
    ObjectType Type { get; set; }
    
    /// <summary>
    /// The shape of the object
    /// </summary>
    ObjectShape Shape { get; set; }
    
    /// <summary>
    /// The specific variant of the object's shape. Can be cast from
    /// <see cref="ObjectSquareShape"/>, <see cref="ObjectCircleShape"/>,
    /// <see cref="ObjectTriangleShape"/>, <see cref="ObjectArrowShape"/>
    /// and <see cref="ObjectHexagonShape"/>
    /// </summary>
    int ShapeOption { get; set; }
    
    /// <summary>
    /// The object's text data. Only has an effect when <see cref="Shape"/>
    /// is <see cref="ObjectShape.Text"/>
    /// </summary>
    string Text { get; set; }
    
    /// <summary>
    /// The object's render type
    /// </summary>
    RenderType RenderType { get; set; }
    
    /// <summary>
    /// The specific transform component from which this object inherits
    /// from its parent
    /// </summary>
    ParentType ParentType { get; set; }
    
    /// <summary>
    /// The object's parent animation offset, in time
    /// </summary>
    ParentOffset ParentOffset { get; set; }
    
    /// <summary>
    /// The object's Z depth
    /// </summary>
    int RenderDepth { get; set; }
    
    /// <summary>
    /// The object's editor settings. Only has an effect when in editor
    /// </summary>
    ObjectEditorSettings EditorSettings { get; set; }
    
    /// <summary>
    /// The animated object's position events
    /// </summary>
    IList<Keyframe<Vector2>> PositionEvents { get; }
    
    /// <summary>
    /// The animated object's scale events
    /// </summary>
    IList<Keyframe<Vector2>> ScaleEvents { get; }
    
    /// <summary>
    /// The animated object's rotation events
    /// </summary>
    IList<Keyframe<float>> RotationEvents { get; }
    
    /// <summary>
    /// The animated object's color events
    /// </summary>
    IList<FixedKeyframe<ThemeColor>> ColorEvents { get; }
    
    /// <summary>
    /// The object's parent. If null, the object does not have a parent
    /// </summary>
    IReference<IObject>? Parent { get; set; }
}