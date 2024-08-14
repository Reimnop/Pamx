using System.Numerics;
using Pamx.Common;
using Pamx.Common.Data;
using Pamx.Common.Enum;
using Pamx.Common.Implementation;

namespace Pamx.Ls;

public class LsObject : IObject, IIdentifiable<string>
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
}