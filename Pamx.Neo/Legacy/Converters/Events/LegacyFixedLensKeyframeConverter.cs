using System.Text.Json;
using Pamx.Neo.Events;
using Pamx.Neo.Keyframes;
using Pamx.Neo.Legacy.Converters.Keyframes;
using Pamx.Neo.Serialization.Extensions;

namespace Pamx.Neo.Legacy.Converters.Events;

internal sealed class LegacyFixedLensDistortionKeyframeConverter : LegacyFixedKeyframeConverter<LensDistortionValue>
{
    private static ReadOnlySpan<byte> IntensityKey => "x"u8;

    protected override LensDistortionValue GetDefaultValue() => LensDistortionValue.Zero;

    protected override FixedKeyframe<LensDistortionValue> CreateKeyframe(LensDistortionValue value, float time,
        Ease ease) => new(value, time, ease);

    protected override bool TryReadValues(ref Utf8JsonReader reader, ref LensDistortionValue value,
        JsonSerializerOptions options)
    {
        if (reader.ValueTextEquals(IntensityKey))
        {
            reader.Read();
            value.Intensity = reader.GetSingleLike();
            return true;
        }

        return false;
    }
}