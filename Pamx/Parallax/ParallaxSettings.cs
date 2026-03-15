using System.Text.Json.Serialization;

namespace Pamx.Parallax;

/// <summary>
/// Represents the parallax objects settings of the beatmap.
/// </summary>
public sealed class ParallaxSettings
{
    /// <summary>
    /// The parallax object layers.
    /// </summary>
    [JsonPropertyName("l")]
    public List<ParallaxLayer> Layers { get; set; } =
    [
        new() { Depth = 100, Color = 1 },
        new() { Depth = 200, Color = 2 },
        new() { Depth = 300, Color = 3 },
        new() { Depth = 400, Color = 4 },
        new() { Depth = 500, Color = 5 }
    ];

    /// <summary>
    /// The value of depth of field effect.
    /// </summary>
    [JsonPropertyName("dof_value")]
    public int DepthOfField { get; set; } = 40;

    /// <summary>
    /// Whether the depth of field effect is enabled or not.
    /// </summary>
    [JsonPropertyName("dof_active")]
    public bool IsDepthOfFieldEnabled { get; set; } = false;
}