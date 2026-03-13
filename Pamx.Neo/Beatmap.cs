using Pamx.Neo.Editor;
using Pamx.Neo.Themes;

namespace Pamx.Neo;

/// <summary>
/// Represents a beatmap with various settings, triggers, objects, and themes.
/// </summary>
public sealed class Beatmap
{
    /// <summary>
    /// The beatmap's checkpoints.
    /// </summary>
    public required List<Checkpoint> Checkpoints { get; set; }
    
    /// <summary>
    /// The beatmap's internal themes.
    /// </summary>
    public required List<BeatmapTheme> Themes { get; set; }

    /// <summary>
    /// The beatmap's timeline markers.
    /// </summary>
    public required List<Marker> Markers { get; set; }
}