using System.Numerics;
using Pamx.Common.Data;

namespace Pamx.Common;

public interface IPrefabObject
{
    float Time { get; set; }
    IReference<IPrefab> Prefab { get; set; }
    ObjectEditorSettings EditorSettings { get; set; }
    
    Vector2 Position { get; set; }
    Vector2 Scale { get; set; }
    float Rotation { get; set; }
}