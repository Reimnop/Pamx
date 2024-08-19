using Pamx.Common;
using Pamx.Common.Implementation;

namespace Pamx.Vg;

public class VgBeatmapTheme() : Theme, IIdentifiable<string>
{
    public string Id { get; set; } = RandomUtil.GenerateId();
    
    public VgBeatmapTheme(string id) : this()
    {
        Id = id;
    }
}