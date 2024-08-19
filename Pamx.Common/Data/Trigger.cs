using Pamx.Common.Enum;

namespace Pamx.Common.Data;

/// <summary>
/// The trigger for an event
/// </summary>
public struct Trigger()
{
    /// <summary>
    /// What the trigger should trigger on
    /// </summary>
    public TriggerType Type { get; set; } = TriggerType.Time;
    
    /// <summary>
    /// When to start triggering
    /// </summary>
    public float From { get; set; } = 0.0f;
    
    /// <summary>
    /// When to stop triggering
    /// </summary>
    public float To { get; set; } = 100.0f;
    
    /// <summary>
    /// How many times to trigger. -1 for infinite
    /// </summary>
    public int Retrigger { get; set; } = -1;
    
    /// <summary>
    /// The event type to trigger
    /// </summary>
    public EventType EventType { get; set; } = EventType.VnInk;
    
    /// <summary>
    /// The event data of the event
    /// </summary>
    public IList<string> Data { get; set; } = [];
}