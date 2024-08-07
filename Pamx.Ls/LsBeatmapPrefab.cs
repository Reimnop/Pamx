using Pamx.Common;

namespace Pamx.Ls;

public class LsBeatmapPrefab : LsPrefab, IIdentifiable<string>
{
    public string Id { get; } = LsRandomUtil.GenerateId();
}