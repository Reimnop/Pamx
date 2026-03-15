using System.Text.Json.Serialization;

namespace Pamx.Neo.Parallax;

/// <summary>
/// Represents the parallax objects layer in the beatmap.
/// </summary>
public sealed class ParallaxLayer
{
    /// <summary>
    /// The depth of the layer.
    /// </summary>
    [JsonPropertyName("d")]
    public int Depth { get; set; }
    
    /// <summary>
    /// The color index of the layer.
    /// </summary>
    [JsonPropertyName("c")]
    public int Color { get; set; }

    /// <summary>
    /// The parallax objects in the layer.
    /// </summary>
    [JsonPropertyName("o")]
    public List<ParallaxObject> Objects { get; set; } = [];
}