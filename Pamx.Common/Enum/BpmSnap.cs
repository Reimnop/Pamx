namespace Pamx.Common.Enum;

/// <summary>
/// The settings for snapping to BPM
/// </summary>
[Flags]
public enum BpmSnap
{
    /// <summary>
    /// Snap objects
    /// </summary>
    Objects = 0b01,
    
    /// <summary>
    /// Snap checkpoints
    /// </summary>
    Checkpoints = 0b10,
    
    /// <summary>
    /// Do not snap anything
    /// </summary>
    None = 0
}