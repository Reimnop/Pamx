using System.Text.Json;
using Pamx.Events;
using Pamx.Keyframes;
using Pamx.Legacy.Converters.Keyframes;
using Pamx.Serialization.Extensions;

namespace Pamx.Legacy.Converters.Events;

internal sealed class LegacyFixedVignetteKeyframeConverter : LegacyFixedKeyframeConverter<VignetteValue>
{
    private static ReadOnlySpan<byte> IntensityKey => "x"u8;
    private static ReadOnlySpan<byte> SmoothnessKey => "y"u8;
    private static ReadOnlySpan<byte> IsRoundedKey => "z"u8;
    private static ReadOnlySpan<byte> RoundnessKey => "x2"u8;
    private static ReadOnlySpan<byte> CenterXKey => "y2"u8;
    private static ReadOnlySpan<byte> CenterYKey => "z2"u8;

    protected override VignetteValue GetDefaultValue() => VignetteValue.Zero;

    protected override FixedKeyframe<VignetteValue> CreateKeyframe(VignetteValue value, float time, Ease ease) =>
        new(value, time, ease);

    protected override bool TryReadValues(ref Utf8JsonReader reader, ref VignetteValue value,
        JsonSerializerOptions options)
    {
        if (reader.ValueTextEquals(IntensityKey))
        {
            reader.Read();
            value.Intensity = reader.GetSingleLike();
            return true;
        }

        if (reader.ValueTextEquals(SmoothnessKey))
        {
            reader.Read();
            value.Smoothness = reader.GetSingleLike();
            return true;
        }

        if (reader.ValueTextEquals(IsRoundedKey))
        {
            reader.Read();
            value.IsRounded = reader.GetSingleLike() != 0.0f;
            return true;
        }

        if (reader.ValueTextEquals(RoundnessKey))
        {
            reader.Read();
            value.Roundness = reader.GetSingleLike();
            return true;
        }

        if (reader.ValueTextEquals(CenterXKey))
        {
            reader.Read();
            value.Center = value.Center with { X = reader.GetSingleLike() };
            return true;
        }

        if (reader.ValueTextEquals(CenterYKey))
        {
            reader.Read();
            value.Center = value.Center with { Y = reader.GetSingleLike() };
            return true;
        }

        return false;
    }
}