using Pamx.Common;
using Pamx.Common.Implementation;

namespace Pamx.Ls;

public class LsBeatmapTheme(int id) : Theme, IIdentifiable<int>
{
    public int Id { get; } = id;
}