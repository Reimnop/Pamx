using System.Text.Json.Serialization;

namespace Pamx.Neo.Editor;

/// <summary>
/// Represents the beatmap's general editor settings.
/// </summary>
public sealed class GeneralSettings
{
    /// <summary>
    /// How long the collapsed objects should be in the timeline.
    /// </summary>
    public float CollapseLength { get; set; } = 0.4f;
    
    /// <summary>
    /// Editor complexity, currently unused.
    /// </summary>
    public int Complexity { get; set; } = 0;
    
    /// <summary>
    /// Editor theme, currently unused.
    /// </summary>
    public int Theme { get; set; } = 0;
    
    /// <summary>
    /// Whether text objects can be selected by clicking on them.
    /// </summary>
    [JsonPropertyName("text_select_objects")]
    public bool SelectTextObjects { get; set; } = true;
    
    /// <summary>
    /// Whether parallax text objects can be selected by clicking on them.
    /// </summary>
    [JsonPropertyName("text_select_backgrounds")]
    public bool SelectParallaxTextObjects { get; set; } = false;
}