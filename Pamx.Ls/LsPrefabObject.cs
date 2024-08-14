using System.Numerics;
using Pamx.Common;
using Pamx.Common.Data;
using Pamx.Common.Implementation;

namespace Pamx.Ls;

public class LsPrefabObject(IReference<IPrefab> prefab) : IPrefabObject, IIdentifiable<string>
{
    public string Id { get; } = RandomUtil.GenerateId();
    public float Time { get; set; }
    public IReference<IPrefab> Prefab { get; set; } = prefab;
    public ObjectEditorSettings EditorSettings { get; set; }
    
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; }
    public float Rotation { get; set; }
}