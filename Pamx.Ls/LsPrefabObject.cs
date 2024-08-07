using System.Numerics;
using Pamx.Common;
using Pamx.Common.Data;

namespace Pamx.Ls;

public class LsPrefabObject(IReference<IPrefab> prefab) : IPrefabObject, IIdentifiable<string>
{
    public string Id { get; } = LsRandomUtil.GenerateId();
    public float Time { get; set; }
    public IReference<IPrefab> Prefab { get; set; } = prefab;
    public ObjectEditorSettings EditorSettings { get; set; }
    
    public IList<Keyframe<Vector2>> PositionEvents { get; } = [];
    public IList<Keyframe<Vector2>> ScaleEvents { get; } = [];
    public IList<Keyframe<float>> RotationEvents { get; } = [];
}