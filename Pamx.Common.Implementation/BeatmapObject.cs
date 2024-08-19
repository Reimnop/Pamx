using System.Numerics;
using Pamx.Common.Data;
using Pamx.Common.Enum;

namespace Pamx.Common.Implementation;

/// <inheritdoc cref="Pamx.Common.IObject"/>
public class BeatmapObject() : IObject, IIdentifiable<string>
{
    /// <inheritdoc />
    public string Id { get; set; } = RandomUtil.GenerateId();
    
    /// <inheritdoc />
    public string Name { get; set; } = string.Empty;
    
    /// <inheritdoc />
    public float StartTime { get; set; }
    
    /// <inheritdoc />
    public AutoKillType AutoKillType { get; set; }
    
    /// <inheritdoc />
    public float AutoKillOffset { get; set; }
    
    /// <inheritdoc />
    public Vector2 Origin { get; set; }
    
    /// <inheritdoc />
    public ObjectType Type { get; set; }
    
    /// <inheritdoc />
    public ObjectShape Shape { get; set; }
    
    /// <inheritdoc />
    public int ShapeOption { get; set; }
    
    /// <inheritdoc />
    public string Text { get; set; } = string.Empty;
    
    /// <inheritdoc />
    public RenderType RenderType { get; set; }
    
    /// <inheritdoc />
    public ParentType ParentType { get; set; }
    
    /// <inheritdoc />
    public ParentOffset ParentOffset { get; set; }
    
    /// <inheritdoc />
    public int RenderDepth { get; set; }
    
    /// <inheritdoc />
    public ObjectEditorSettings EditorSettings { get; set; }

    /// <inheritdoc />
    public IList<Keyframe<Vector2>> PositionEvents { get; set; } = [];
    
    /// <inheritdoc />
    public IList<Keyframe<Vector2>> ScaleEvents { get; set; } = [];
    
    /// <inheritdoc />
    public IList<Keyframe<float>> RotationEvents { get; set; } = [];
    
    /// <inheritdoc />
    public IList<FixedKeyframe<ThemeColor>> ColorEvents { get; set; } = [];
    
    /// <inheritdoc />
    public IReference<IObject>? Parent { get; set; }

    /// <summary>
    /// Creates a new <see cref="BeatmapObject"/>
    /// </summary>
    /// <param name="id">The object's id</param>
    public BeatmapObject(string id) : this()
    {
        Id = id;
    }
}