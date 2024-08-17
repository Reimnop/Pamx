using Pamx.Common;
using Pamx.Common.Implementation;

namespace Pamx.Ls;

public class LsBeatmapPrefab() : LsPrefab, IIdentifiable<string>
{
    public string Id { get; } = RandomUtil.GenerateId();
    
    public LsBeatmapPrefab(string id) : this()
    {
        Id = id;
    }
}