using Pamx.Common;

namespace Pamx.Ls;

public class LsMarker : IMarker
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Color { get; set; } = 0;
    public float Time { get; set; } = 0.0f;
}