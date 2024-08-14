using Pamx.Common;
using Pamx.Common.Implementation;

namespace Pamx.Vg;

public class VgBeatmapTheme : VgTheme, IIdentifiable<string>
{
    public string Id { get; } = RandomUtil.GenerateId();
}