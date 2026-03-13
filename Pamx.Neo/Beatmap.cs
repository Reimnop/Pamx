using Pamx.Neo.Themes;

namespace Pamx.Neo;

/// <summary>
/// Represents a beatmap with various settings, triggers, objects, and themes.
/// </summary>
public sealed class Beatmap
{
    /// <summary>
    /// The beatmap's internal themes.
    /// </summary>
    public required List<BeatmapTheme> Themes { get; set; }
}