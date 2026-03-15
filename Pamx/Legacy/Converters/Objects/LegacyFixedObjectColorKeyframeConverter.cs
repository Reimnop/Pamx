using System.Text.Json;
using Pamx.Keyframes;
using Pamx.Legacy.Converters.Keyframes;
using Pamx.Serialization.Extensions;
using Pamx.Objects;

namespace Pamx.Legacy.Converters.Objects;

internal sealed class LegacyFixedObjectColorKeyframeConverter : LegacyFixedKeyframeConverter<ObjectColorValue>
{
    private static ReadOnlySpan<byte> IndexKey => "x"u8;

    protected override ObjectColorValue GetDefaultValue() => ObjectColorValue.Default;

    protected override FixedKeyframe<ObjectColorValue> CreateKeyframe(ObjectColorValue value, float time, Ease ease) =>
        new(value, time, ease);

    protected override bool TryReadValues(ref Utf8JsonReader reader, ref ObjectColorValue value,
        JsonSerializerOptions options)
    {
        if (reader.ValueTextEquals(IndexKey))
        {
            reader.Read();
            value.Index = reader.GetInt32Like();
            return true;
        }

        return false;
    }
}