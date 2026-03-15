using System.Text.Json.Serialization;

namespace Pamx.Editor;

/// <summary>
/// The beatmap object's editor settings.
/// </summary>
public class ObjectEditorSettings
{
    /// <summary>
    /// Whether the object's start time is locked in editor or not.
    /// </summary>
    [JsonPropertyName("lk")]
    public bool IsLocked { get; set; }

    /// <summary>
    /// Whether the object is collapsed or not.
    /// </summary>
    [JsonPropertyName("co")]
    public bool IsCollapsed { get; set; }

    /// <summary>
    /// The object's bin.
    /// </summary>
    [JsonPropertyName("bin")]
    public int Bin { get; set; }

    /// <summary>
    /// The object's layer.
    /// </summary>
    [JsonPropertyName("l")]
    public int Layer { get; set; }

    /// <summary>
    /// The object strip's text color.
    /// </summary>
    [JsonPropertyName("tc")]
    public ObjectTimelineColor TextColor { get; set; } = ObjectTimelineColor.None;

    /// <summary>
    /// The object strip's background color.
    /// </summary>
    [JsonPropertyName("bgc")]
    public ObjectTimelineColor BackgroundColor { get; set; } = ObjectTimelineColor.None;

    internal bool IsDefault() =>
        !IsLocked &&
        !IsCollapsed &&
        Bin == 0 &&
        Layer == 0 &&
        TextColor == ObjectTimelineColor.None &&
        BackgroundColor == ObjectTimelineColor.None;
}