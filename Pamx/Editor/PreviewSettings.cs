using System.Text.Json.Serialization;

namespace Pamx.Editor;

/// <summary>
/// Represents the beatmap's preview settings.
/// </summary>
public sealed class PreviewSettings
{
    /// <summary>
    /// How much should the preview camera be zoomed outside the gameplay bounds.
    /// </summary>
    [JsonPropertyName("cam_zoom_offset")]
    public float CameraZoomOffset { get; set; } = 9.0f; 
    
    /// <summary>
    /// The tint color index of the zoomed out area.
    /// </summary>
    [JsonPropertyName("cam_zoom_offset_color")]
    public int CameraZoomOffsetColor { get; set; } = 0;
}