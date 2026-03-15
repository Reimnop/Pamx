using System.Text.Json;
using Pamx.Neo.Keyframes;
using Pamx.Neo.Objects;
using Pamx.Neo.Serialization.Extensions;
using Pamx.Neo.Serialization.Legacy.Converters.Keyframes;

namespace Pamx.Neo.Serialization.Legacy.Converters.Objects;

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