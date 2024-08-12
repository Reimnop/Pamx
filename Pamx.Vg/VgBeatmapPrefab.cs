using Pamx.Common;

namespace Pamx.Vg;

public class VgBeatmapPrefab : VgPrefab, IIdentifiable<string>
{
    public string Id { get; } = VgRandomUtil.GenerateId();
}