using Pamx.Common;
using Pamx.Common.Implementation;

namespace Pamx.Vg;

public class VgMarker() : IMarker, IIdentifiable<string>
{
    public string Id { get; } = RandomUtil.GenerateId();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Color { get; set; }
    public float Time { get; set; }
    
    public VgMarker(string id) : this()
    {
        Id = id;
    }
}