using System.Numerics;
using Pamx.Common.Data;

namespace Pamx.Common;

public interface IPrefabObject
{
    float Time { get; set; }
    IReference<IPrefab> Prefab { get; set; }
    ObjectEditorSettings EditorSettings { get; set; }
    
    IList<Keyframe<Vector2>> PositionEvents { get; }
    IList<Keyframe<Vector2>> ScaleEvents { get; }
    IList<Keyframe<float>> RotationEvents { get; }
}