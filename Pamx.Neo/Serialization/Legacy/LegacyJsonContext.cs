using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Pamx.Neo.Serialization.Converters.Primitives;
using Pamx.Neo.Serialization.Legacy.Converters;
using Pamx.Neo.Serialization.Legacy.Converters.Editor;
using Pamx.Neo.Serialization.Legacy.Converters.Events;
using Pamx.Neo.Serialization.Legacy.Converters.Keyframes;
using Pamx.Neo.Serialization.Legacy.Converters.Primitives;
using Pamx.Neo.Serialization.Legacy.Converters.Themes;

namespace Pamx.Neo.Serialization.Legacy;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
    Converters =
    [
        typeof(LegacyVector2Converter),
        typeof(ColorConverter),
        typeof(EaseConverter),
        
        typeof(LegacyFixedFloatKeyframeConverter),
        typeof(LegacyFixedStringKeyframeConverter),
        typeof(LegacyFixedVector2KeyframeConverter),

        typeof(LegacyBeatmapConverter),

        typeof(LegacyMarkerConverter),
        typeof(LegacyCheckpointConverter),
        typeof(LegacyThemeConverter),

        typeof(LegacyBeatmapEventsConverter),
        typeof(LegacyFixedBloomKeyframeConverter),
        typeof(LegacyFixedBloomKeyframeConverter),
        typeof(LegacyFixedLensDistortionKeyframeConverter),
        typeof(LegacyFixedGrainKeyframeConverter)
    ]
)]
[JsonSerializable(typeof(Beatmap))]
public partial class LegacyJsonContext : JsonSerializerContext
{
    private static JsonSerializerOptions? _customOptions;

    public static JsonSerializerOptions CustomOptions => _customOptions ??= new JsonSerializerOptions(Default.Options)
    {
        // TypeInfoResolver = Default.WithAddedModifier(IgnoreEmptyStrings),
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    };
}