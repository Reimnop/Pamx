using Pamx.Common.Enum;

namespace Pamx.Common.Implementation;

/// <inheritdoc />
public class Prefab : IPrefab
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Preview { get; set; }
    public float Offset { get; set; }
    public PrefabType Type { get; set; }
    public IList<IObject> BeatmapObjects { get; set; } = [];
}