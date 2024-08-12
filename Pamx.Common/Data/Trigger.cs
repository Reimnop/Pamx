using Pamx.Common.Enum;

namespace Pamx.Common.Data;

public struct Trigger()
{
    public TriggerType Type { get; set; } = TriggerType.Time;
    public float From { get; set; } = 0.0f;
    public float To { get; set; } = 100.0f;
    public int Retrigger { get; set; } = -1;
    public EventType EventType { get; set; } = EventType.VnInk;
    public IList<string> Data { get; } = [];
}