using Pamx.Common;
using Pamx.Common.Implementation;

namespace Pamx.Vg;

public class VgBeatmapPrefab() : VgPrefab, IIdentifiable<string>
{
    public string Id { get; } = RandomUtil.GenerateId();
    
    public VgBeatmapPrefab(string id) : this()
    {
        Id = id;
    }
}