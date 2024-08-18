using System.Numerics;
using Pamx.Common.Data;
using Pamx.Common.Enum;

namespace Pamx.Common.Implementation;

public class BeatmapObject() : IObject, IIdentifiable<string>
{
    public string Id { get; } = RandomUtil.GenerateId();
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

    public IList<Keyframe<Vector2>> PositionEvents { get; } = [];
    public IList<Keyframe<Vector2>> ScaleEvents { get; } = [];
    public IList<Keyframe<float>> RotationEvents { get; } = [];
    public IList<FixedKeyframe<ThemeColor>> ColorEvents { get; } = [];
    
    public IReference<IObject>? Parent { get; set; }

    public BeatmapObject(string id) : this()
    {
        Id = id;
    }
    
    public IObject Clone()
    {
        var clone = new BeatmapObject(Id)
        {
            Name = Name,
            StartTime = StartTime,
            AutoKillType = AutoKillType,
            AutoKillOffset = AutoKillOffset,
            Origin = Origin,
            Type = Type,
            Shape = Shape,
            ShapeOption = ShapeOption,
            Text = Text,
            RenderType = RenderType,
            ParentType = ParentType,
            ParentOffset = ParentOffset,
            RenderDepth = RenderDepth,
            EditorSettings = EditorSettings,
            Parent = Parent
        };
        
        clone.PositionEvents.AddRange(PositionEvents);
        clone.ScaleEvents.AddRange(ScaleEvents);
        clone.RotationEvents.AddRange(RotationEvents);
        clone.ColorEvents.AddRange(ColorEvents);

        return clone;
    }
}