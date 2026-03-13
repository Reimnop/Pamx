using Pamx.Neo.Editor;
using Pamx.Neo.Events;
using Pamx.Neo.Objects;
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
    public List<Checkpoint> Checkpoints { get; set; } = [new()];

    /// <summary>
    /// The beatmap's internal themes.
    /// </summary>
    public List<BeatmapTheme> Themes { get; set; } = [];

    /// <summary>
    /// The beatmap's timeline markers.
    /// </summary>
    public List<Marker> Markers { get; set; } = [];

    /// <summary>
    /// The beatmap's objects.
    /// </summary>
    public List<BeatmapObject> Objects { get; set; } = []; // TODO

    /// <summary>
    /// The beatmap's event keyframes.
    /// </summary>
    public BeatmapEvents Events { get; set; } = new();
}