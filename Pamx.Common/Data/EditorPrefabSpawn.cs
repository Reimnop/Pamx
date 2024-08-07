namespace Pamx.Common.Data;

public struct EditorPrefabSpawn(IReference<IPrefab> prefab)
{
    public bool Expanded { get; set; } = false;
    public bool Active { get; set; } = false;
    public IReference<IPrefab> Prefab { get; set; } = prefab;
    public IList<string> Keycodes { get; } = [];
}