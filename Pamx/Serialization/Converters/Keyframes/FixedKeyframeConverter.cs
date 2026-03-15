using System.Text.Json;
using Pamx.Keyframes;

namespace Pamx.Serialization.Converters.Keyframes;

internal abstract class FixedKeyframeConverter<T> : KeyframeConverter<FixedKeyframe<T>>
{
    protected override FixedKeyframe<T> GetDefaultValue() => new(GetDefaultKeyframeValue());

    protected override bool TryReadProperties(ref Utf8JsonReader reader, ref FixedKeyframe<T> value,
        JsonSerializerOptions options)
    {
        if (base.TryReadProperties(ref reader, ref value, options))
            return true;

        if (!reader.ValueTextEquals(KeyframeConverterConstants.ValuesKey))
            return false;

        reader.Read();
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException("Expected StartArray token");

        var keyframeValue = GetDefaultKeyframeValue();
        var i = 0;

        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            ReadKeyframeValue(ref reader, ref keyframeValue, i, options);
            i++;
        }

        value.Value = keyframeValue;
        return true;
    }

    protected override void WriteProperties(Utf8JsonWriter writer,
        FixedKeyframe<T> value,
        JsonSerializerOptions options)
    {
        base.WriteProperties(writer, value, options);

        writer.WritePropertyName(KeyframeConverterConstants.ValuesProperty);
        writer.WriteStartArray();
        WriteKeyframeValues(writer, value.Value, options);
        writer.WriteEndArray();
    }

    protected abstract T GetDefaultKeyframeValue();

    protected abstract void ReadKeyframeValue(ref Utf8JsonReader reader, ref T value, int index,
        JsonSerializerOptions options);

    protected abstract void WriteKeyframeValues(Utf8JsonWriter writer, T value, JsonSerializerOptions options);
}