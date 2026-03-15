using System.Text.Json;
using Pamx.Keyframes;

namespace Pamx.Serialization.Converters.Keyframes;

internal sealed class FixedStringKeyframeConverter : KeyframeConverter<FixedKeyframe<string>>
{
    protected override FixedKeyframe<string> GetDefaultValue() => new(string.Empty);

    protected override bool TryReadProperties(ref Utf8JsonReader reader,
        ref FixedKeyframe<string> value,
        JsonSerializerOptions options)
    {
        if (base.TryReadProperties(ref reader, ref value, options))
            return true;

        if (!reader.ValueTextEquals(KeyframeConverterConstants.StringValuesKey))
            return false;

        reader.Read();
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException("Expected StartArray token");

        var i = 0;
        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            if (i != 0)
            {
                reader.Skip();
                continue;
            }

            value.Value = reader.GetString() ?? string.Empty;
            i++;
        }

        return true;
    }

    protected override void WriteProperties(Utf8JsonWriter writer,
        FixedKeyframe<string> value,
        JsonSerializerOptions options)
    {
        base.WriteProperties(writer, value, options);

        writer.WritePropertyName(KeyframeConverterConstants.StringValuesProperty);
        writer.WriteStartArray();
        writer.WriteStringValue(value.Value);
        writer.WriteEndArray();
    }
}