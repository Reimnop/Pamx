using System.Drawing;
using System.Text.Json.Serialization;

namespace Pamx.Neo.Themes;

/// <summary>
/// Represents an external color theme.
/// </summary>
public class ExternalTheme
{
    /// <summary>
    /// The theme's name
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The theme's colors of the players. There should be 4 colors in this list.
    /// </summary>
    [JsonPropertyName("pla")]
    public required List<Color> Players { get; set; }

    /// <summary>
    /// The theme's colors of the beatmap objects. There should be 4 colors in this list.
    /// </summary>
    [JsonPropertyName("obj")]
    public required List<Color> Objects { get; set; }

    /// <summary>
    /// The theme's colors of the effects. There should be 4 colors in this list.
    /// </summary>
    [JsonPropertyName("fx")]
    public required List<Color> Effects { get; set; }

    /// <summary>
    /// The theme's colors of the parallax objects. There should be 4 colors in this list.
    /// </summary>
    [JsonPropertyName("bg")]
    public required List<Color> ParallaxObjects { get; set; }

    /// <summary>
    /// The theme's background color.
    /// </summary>
    [JsonPropertyName("base_bg")]
    public required Color Background { get; set; }

    /// <summary>
    /// The theme's GUI color.
    /// </summary>
    [JsonPropertyName("base_gui")]
    public required Color Gui { get; set; }

    /// <summary>
    /// The theme's GUI accent color.
    /// </summary>
    [JsonPropertyName("base_gui_accent")]
    public required Color GuiAccent { get; set; }
}