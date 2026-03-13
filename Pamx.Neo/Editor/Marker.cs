using System.Text.Json.Serialization;

namespace Pamx.Neo.Editor;

/// <summary>
/// Represents a marker on the beatmap's timeline.
/// </summary>
public sealed class Marker : IIdentifiable<string>
{
    /// <summary>
    /// The marker's ID.
    /// </summary>
    [JsonPropertyName("ID")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// The marker's name.
    /// </summary>
    [JsonPropertyName("n")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The marker's description.
    /// </summary>
    [JsonPropertyName("d")]
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// The marker's color index.
    /// </summary>
    [JsonPropertyName("c")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Color { get; set; }
    
    /// <summary>
    /// The marker's time.
    /// </summary>
    [JsonPropertyName("t")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public float Time { get; set; }
}