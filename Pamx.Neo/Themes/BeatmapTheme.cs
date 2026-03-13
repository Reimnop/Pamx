namespace Pamx.Neo.Themes;

/// <summary>
/// Represents an internal color theme in the beatmap.
/// </summary>
public sealed class BeatmapTheme : ExternalTheme, IIdentifiable<string>
{
    /// <summary>
    /// The theme's ID
    /// </summary>
    public required string Id { get; set; }
}