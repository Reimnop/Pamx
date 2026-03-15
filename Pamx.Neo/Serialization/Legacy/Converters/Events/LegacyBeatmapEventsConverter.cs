using System.Numerics;
using System.Text.Json;
using Pamx.Neo.Events;
using Pamx.Neo.Keyframes;

namespace Pamx.Neo.Serialization.Legacy.Converters.Events;

internal sealed class LegacyBeatmapEventsConverter : ReadonlyJsonObjectConverter<BeatmapEvents>
{
    private static ReadOnlySpan<byte> MoveKey => "pos"u8;
    private static ReadOnlySpan<byte> ZoomKey => "zoom"u8;
    private static ReadOnlySpan<byte> RotateKey => "rot"u8;
    private static ReadOnlySpan<byte> ShakeKey => "shake"u8;
    private static ReadOnlySpan<byte> ThemeKey => "theme"u8;
    private static ReadOnlySpan<byte> ChromaticKey => "chroma"u8;
    private static ReadOnlySpan<byte> BloomKey => "bloom"u8;
    private static ReadOnlySpan<byte> VignetteKey => "vignette"u8;
    private static ReadOnlySpan<byte> LensDistortionKey => "lens"u8;
    private static ReadOnlySpan<byte> GrainKey => "grain"u8;

    protected override BeatmapEvents GetDefaultValue() => new();

    protected override bool TryReadProperties(ref Utf8JsonReader reader, ref BeatmapEvents value,
        JsonSerializerOptions options)
    {
        if (reader.ValueTextEquals(MoveKey))
        {
            reader.Read();
            value.Move = JsonSerializer.Deserialize<List<FixedKeyframe<Vector2>>>(ref reader, options) ?? [];
            return true;
        }

        if (reader.ValueTextEquals(ZoomKey))
        {
            reader.Read();
            value.Zoom = JsonSerializer.Deserialize<List<FixedKeyframe<float>>>(ref reader, options) ?? [];
            return true;
        }

        if (reader.ValueTextEquals(RotateKey))
        {
            reader.Read();
            value.Rotate = JsonSerializer.Deserialize<List<FixedKeyframe<float>>>(ref reader, options) ?? [];
            return true;
        }

        if (reader.ValueTextEquals(ShakeKey))
        {
            reader.Read();
            value.Shake = JsonSerializer.Deserialize<List<FixedKeyframe<float>>>(ref reader, options) ?? [];
            return true;
        }

        if (reader.ValueTextEquals(ThemeKey))
        {
            reader.Read();
            value.Theme = JsonSerializer.Deserialize<List<FixedKeyframe<string>>>(ref reader, options) ?? [];
            return true;
        }

        if (reader.ValueTextEquals(ChromaticKey))
        {
            reader.Read();
            value.Chromatic = JsonSerializer.Deserialize<List<FixedKeyframe<float>>>(ref reader, options) ?? [];
            return true;
        }

        if (reader.ValueTextEquals(BloomKey))
        {
            reader.Read();
            value.Bloom = JsonSerializer.Deserialize<List<FixedKeyframe<BloomValue>>>(ref reader, options) ?? [];
            return true;
        }

        if (reader.ValueTextEquals(VignetteKey))
        {
            reader.Read();
            value.Vignette = JsonSerializer.Deserialize<List<FixedKeyframe<VignetteValue>>>(ref reader, options) ?? [];
            return true;
        }

        if (reader.ValueTextEquals(LensDistortionKey))
        {
            reader.Read();
            value.LensDistortion =
                JsonSerializer.Deserialize<List<FixedKeyframe<LensDistortionValue>>>(ref reader, options) ?? [];
            return true;
        }

        if (reader.ValueTextEquals(GrainKey))
        {
            reader.Read();
            value.Grain = JsonSerializer.Deserialize<List<FixedKeyframe<GrainValue>>>(ref reader, options) ?? [];
            return true;
        }

        return false;
    }
}