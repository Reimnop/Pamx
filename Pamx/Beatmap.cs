using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Pamx.Editor;
using Pamx.Events;
using Pamx.Keyframes;
using Pamx.Legacy;
using Pamx.Objects;
using Pamx.Parallax;
using Pamx.Prefabs;
using Pamx.Serialization;
using Pamx.Themes;

namespace Pamx;

/// <summary>
/// Represents a beatmap with various settings, triggers, objects, and themes.
/// </summary>
public sealed class Beatmap
{
    /// <summary>
    /// The beatmap's editor settings.
    /// </summary>
    [JsonPropertyName("editor")]
    public EditorSettings EditorSettings { get; set; } = new();

    /// <summary>
    /// The beatmap's prefab spawn settings.
    /// </summary>
    [JsonPropertyName("editor_prefab_spawn")]
    public List<EditorPrefabSpawn> PrefabSpawns { get; set; } = [];

    /// <summary>
    /// The beatmap's parallax settings.
    /// </summary>
    [JsonPropertyName("parallax_settings")]
    public ParallaxSettings Parallax { get; set; } = new();

    /// <summary>
    /// The beatmap's background objects. (Legacy data, not used in modern versions of PA.)
    /// </summary>
    [JsonIgnore]
    public List<BackgroundObject> BackgroundObjects { get; set; } = [];

    /// <summary>
    /// The beatmap's checkpoints.
    /// </summary>
    public List<Checkpoint> Checkpoints { get; set; } = [new()];

    /// <summary>
    /// The beatmap's objects.
    /// </summary>
    public List<BeatmapObject> Objects { get; set; } = [];

    /// <summary>
    /// The beatmap's prefab instance objects.
    /// </summary>
    public List<PrefabObject> PrefabObjects { get; set; } = [];

    /// <summary>
    /// The beatmap's prefabs.
    /// </summary>
    public List<Prefab> Prefabs { get; set; } = [];

    /// <summary>
    /// The beatmap's internal themes.
    /// </summary>
    public List<BeatmapTheme> Themes { get; set; } = [];

    /// <summary>
    /// The beatmap's timeline markers.
    /// </summary>
    public List<Marker> Markers { get; set; } = [];

    /// <summary>
    /// The beatmap's event keyframes.
    /// </summary>
    public BeatmapEvents Events { get; set; } = new();

    public static Beatmap CreateDefault() =>
        new()
        {
            PrefabSpawns = Enumerable.Range(0, 5).Select(_ => new EditorPrefabSpawn()).ToList(),
            Objects = [new BeatmapObject { Name = "Default Object" }],
            Events =
            {
                Move = [new FixedKeyframe<Vector2>(new Vector2(0.0f, 0.0f))],
                Zoom = [new FixedKeyframe<float>(30.0f)],
                Rotate = [new FixedKeyframe<float>(0.0f)],
                Shake = [new FixedKeyframe<float>(0.0f)],
                Theme = [new FixedKeyframe<string>("0")],
                Chromatic = [new FixedKeyframe<float>(0.0f)],
                Bloom = [new FixedKeyframe<BloomValue>(BloomValue.Zero)],
                Vignette = [new FixedKeyframe<VignetteValue>(VignetteValue.Zero)],
                LensDistortion = [new FixedKeyframe<LensDistortionValue>(LensDistortionValue.Zero)],
                Grain = [new FixedKeyframe<GrainValue>(GrainValue.Zero)],
                Gradient = [new FixedKeyframe<GradientValue>(GradientValue.Zero)],
                Glitch = [new FixedKeyframe<GlitchValue>(GlitchValue.Zero)],
                Hue = [new FixedKeyframe<float>(0.0f)],
                Player = [new FixedKeyframe<Vector2>(Vector2.Zero)]
            }
        };
}