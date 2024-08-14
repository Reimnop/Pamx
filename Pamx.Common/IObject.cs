using System.Numerics;
using Pamx.Common.Data;
using Pamx.Common.Enum;

namespace Pamx.Common;

public interface IObject : IReference<IObject>
{
    IObject IReference<IObject>.Value => this;
    string Name { get; set; }
    float StartTime { get; set; }
    AutoKillType AutoKillType { get; set; }
    float AutoKillOffset { get; set; }
    Vector2 Origin { get; set; }
    ObjectType Type { get; set; }
    ObjectShape Shape { get; set; }
    int ShapeOption { get; set; }
    string Text { get; set; }
    RenderType RenderType { get; set; }
    ParentType ParentType { get; set; }
    ParentOffset ParentOffset { get; set; }
    int RenderDepth { get; set; }
    ObjectEditorSettings EditorSettings { get; set; }
    
    IList<Keyframe<Vector2>> PositionEvents { get; }
    IList<Keyframe<Vector2>> ScaleEvents { get; }
    IList<Keyframe<float>> RotationEvents { get; }
    IList<FixedKeyframe<ThemeColor>> ColorEvents { get; }
    
    IReference<IObject>? Parent { get; set; }
}