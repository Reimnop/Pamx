using System.Drawing;
using System.Text.Json.Serialization;

namespace Pamx.Themes;

/// <summary>
/// Represents an external color theme.
/// </summary>
public class ExternalTheme
{
    /// <summary>
    /// The theme's name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The theme's colors of the players. There should be 4 colors in this list.
    /// </summary>
    [JsonPropertyName("pla")]
    public List<Color> Players { get; set; } = [];

    /// <summary>
    /// The theme's colors of the beatmap objects. There should be 9 colors in this list.
    /// </summary>
    [JsonPropertyName("obj")]
    public List<Color> Objects { get; set; } = [];

    /// <summary>
    /// The theme's colors of the effects. There should be 9 colors in this list.
    /// </summary>
    [JsonPropertyName("fx")]
    public List<Color> Effects { get; set; } = [];

    /// <summary>
    /// The theme's colors of the parallax objects. There should be 9 colors in this list.
    /// </summary>
    [JsonPropertyName("bg")]
    public List<Color> ParallaxObjects { get; set; } = [];

    /// <summary>
    /// The theme's background color.
    /// </summary>
    [JsonPropertyName("base_bg")]
    public Color Background { get; set; }

    /// <summary>
    /// The theme's GUI color.
    /// </summary>
    [JsonPropertyName("base_gui")]
    public Color Gui { get; set; }

    /// <summary>
    /// The theme's GUI accent color.
    /// </summary>
    [JsonPropertyName("base_gui_accent")]
    public Color GuiAccent { get; set; }

    /// <summary>
    /// The theme's colors of the background objects. (Legacy data, not used in modern versions of PA.)
    /// </summary>
    [JsonIgnore]
    public List<Color> BackgroundObjects { get; set; } = [];
}