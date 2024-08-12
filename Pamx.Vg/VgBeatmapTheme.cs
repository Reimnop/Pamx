using Pamx.Common;

namespace Pamx.Vg;

public class VgBeatmapTheme : VgTheme, IIdentifiable<string>
{
    public string Id { get; } = VgRandomUtil.GenerateId();
}