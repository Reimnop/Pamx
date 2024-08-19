namespace Pamx.Common.Data;

public struct EditorPrefabSpawn()
{
    public bool Expanded { get; set; } = false;
    public bool Active { get; set; } = false;
    public IReference<IPrefab>? Prefab { get; set; } = null;
    public IList<string> Keycodes { get; set; } = [];
}