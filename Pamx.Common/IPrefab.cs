using Pamx.Common.Enum;

namespace Pamx.Common;

public interface IPrefab : IReference<IPrefab>
{
    IPrefab IReference<IPrefab>.Value => this;
    string Name { get; set; }
    string? Description { get; set; }
    string? Preview { get; set; } // TODO: base64 png
    float Offset { get; set; }
    PrefabType Type { get; set; }
    IList<IObject> BeatmapObjects { get; set; }
}