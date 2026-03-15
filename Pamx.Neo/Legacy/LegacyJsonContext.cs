using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Pamx.Neo.Legacy.Converters;
using Pamx.Neo.Legacy.Converters.Editor;
using Pamx.Neo.Legacy.Converters.Events;
using Pamx.Neo.Legacy.Converters.Keyframes;
using Pamx.Neo.Legacy.Converters.Objects;
using Pamx.Neo.Legacy.Converters.Prefabs;
using Pamx.Neo.Legacy.Converters.Themes;
using Pamx.Neo.Prefabs;
using Pamx.Neo.Serialization.Converters.Objects;
using Pamx.Neo.Serialization.Converters.Primitives;
using Pamx.Neo.Themes;

namespace Pamx.Neo.Legacy;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
    Converters =
    [
        typeof(Vector2Converter),
        typeof(ColorConverter),
        typeof(EaseConverter),

        typeof(LegacyFixedFloatKeyframeConverter),
        typeof(LegacyFixedStringKeyframeConverter),
        typeof(LegacyFixedVector2KeyframeConverter),

        typeof(LegacyBeatmapConverter),

        typeof(LegacyMarkerConverter),
        typeof(LegacyPrefabObjectConverter),
        typeof(LegacyPrefabConverter),
        typeof(LegacyThemeConverter),
        typeof(LegacyCheckpointConverter),

        typeof(LegacyBeatmapObjectConverter),
        typeof(ParentTypeConverter),
        typeof(ParentOffsetConverter),
        typeof(LegacyObjectEditorSettingsConverter),
        typeof(LegacyRandomVector2KeyframeConverter),
        typeof(LegacyObjectRotationKeyframeConverter),
        typeof(LegacyFixedObjectColorKeyframeConverter),

        typeof(LegacyBackgroundObjectConverter),

        typeof(LegacyBeatmapEventsConverter),
        typeof(LegacyFixedBloomKeyframeConverter),
        typeof(LegacyFixedVignetteKeyframeConverter),
        typeof(LegacyFixedLensDistortionKeyframeConverter),
        typeof(LegacyFixedGrainKeyframeConverter)
    ]
)]
[JsonSerializable(typeof(Beatmap))]
[JsonSerializable(typeof(BeatmapTheme))]
[JsonSerializable(typeof(Prefab))]
[JsonSerializable(typeof(List<BackgroundObject>))]
public partial class LegacyJsonContext : JsonSerializerContext
{
    private static JsonSerializerOptions? _customOptions;

    public static JsonSerializerOptions CustomOptions => _customOptions ??= new JsonSerializerOptions(Default.Options)
    {
        TypeInfoResolver = Default,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    };
}