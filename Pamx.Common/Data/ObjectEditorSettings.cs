using Pamx.Common.Enum;

namespace Pamx.Common.Data;

/// <summary>
/// The object's editor settings
/// </summary>
public struct ObjectEditorSettings()
{
    /// <summary>
    /// Whether the object is locked in editor
    /// </summary>
    public bool Locked { get; set; } = false;
    
    /// <summary>
    /// Whether the object is collapsed
    /// </summary>
    public bool Collapsed { get; set; } = false;
    
    /// <summary>
    /// The object's bin
    /// </summary>
    public int Bin { get; set; } = 0;
    
    /// <summary>
    /// The object's layer
    /// </summary>
    public int Layer { get; set; } = 0;
    
    /// <summary>
    /// The object strip's text color
    /// </summary>
    public ObjectTimelineColor TextColor { get; set; }
    
    /// <summary>
    /// The object strip's background color
    /// </summary>
    public ObjectTimelineColor BackgroundColor { get; set; }
}