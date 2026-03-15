using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Pamx.Legacy.Converters;
using Pamx.Legacy.Converters.Editor;
using Pamx.Legacy.Converters.Events;
using Pamx.Legacy.Converters.Keyframes;
using Pamx.Legacy.Converters.Objects;
using Pamx.Legacy.Converters.Prefabs;
using Pamx.Legacy.Converters.Themes;
using Pamx.Prefabs;
using Pamx.Serialization.Converters.Objects;
using Pamx.Serialization.Converters.Primitives;
using Pamx.Themes;

namespace Pamx.Legacy;

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
internal partial class LegacyJsonContext : JsonSerializerContext
{
    private static JsonSerializerOptions? _customOptions;

    public static JsonSerializerOptions CustomOptions => _customOptions ??= new JsonSerializerOptions(Default.Options)
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    };
}