using Pamx.Common.Enum;

namespace Pamx.Common.Data;

public struct ObjectEditorSettings()
{
    public bool Locked { get; set; } = false;
    public bool Collapsed { get; set; } = false;
    public int Bin { get; set; } = 0;
    public int Layer { get; set; } = 0;
    public ObjectTimelineColor TimelineColor { get; set; }
}