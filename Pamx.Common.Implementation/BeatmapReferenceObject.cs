using Pamx.Common;

namespace Pamx.Common;

public class BeatmapReferenceObject(string id) : IReference<IObject>, IIdentifiable<string>
{
    public IObject? Value => null;
    public string Id { get; set; } = id;
}