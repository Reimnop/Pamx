using System.Text.Json;
using Pamx.Events;
using Pamx.Keyframes;
using Pamx.Legacy.Converters.Keyframes;
using Pamx.Serialization.Extensions;

namespace Pamx.Legacy.Converters.Events;

internal sealed class LegacyFixedBloomKeyframeConverter : LegacyFixedKeyframeConverter<BloomValue>
{
    private static ReadOnlySpan<byte> IntensityKey => "x"u8;

    protected override BloomValue GetDefaultValue() => BloomValue.Zero;

    protected override FixedKeyframe<BloomValue> CreateKeyframe(BloomValue value, float time, Ease ease) =>
        new(value, time, ease);

    protected override bool TryReadValues(ref Utf8JsonReader reader, ref BloomValue value, JsonSerializerOptions options)
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