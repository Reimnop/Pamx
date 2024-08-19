using System.Numerics;
using Pamx.Common.Data;
using Pamx.Common.Enum;

namespace Pamx.Common.Implementation;

/// <inheritdoc cref="Pamx.Common.IObject"/>
public class BeatmapObject() : IObject, IIdentifiable<string>
{
    public string Id { get; set; } = RandomUtil.GenerateId();
    public string Name { get; set; } = string.Empty;
    public float StartTime { get; set; }
    public AutoKillType AutoKillType { get; set; }
    public float AutoKillOffset { get; set; }
    public Vector2 Origin { get; set; }
    public ObjectType Type { get; set; }
    public ObjectShape Shape { get; set; }
    public int ShapeOption { get; set; }
    public string Text { get; set; } = string.Empty;
    public RenderType RenderType { get; set; }
    public ParentType ParentType { get; set; }
    public ParentOffset ParentOffset { get; set; }
    public int RenderDepth { get; set; }
    public ObjectEditorSettings EditorSettings { get; set; }
    public IList<Keyframe<Vector2>> PositionEvents { get; set; } = [];
    public IList<Keyframe<Vector2>> ScaleEvents { get; set; } = [];
    public IList<Keyframe<float>> RotationEvents { get; set; } = [];
    public IList<FixedKeyframe<ThemeColor>> ColorEvents { get; set; } = [];
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