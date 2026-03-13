using System.Text.Json;
using Pamx.Neo.Keyframes;

namespace Pamx.Neo.Serialization.Converters.Keyframes;

internal sealed class FixedFloatKeyframeConverter : KeyframeConverter<FixedKeyframe<float>>
{
    protected override FixedKeyframe<float> GetDefaultValue() => new(0.0f);

    protected override bool TryReadProperties(ref Utf8JsonReader reader,
        ref FixedKeyframe<float> value,
        JsonSerializerOptions options)
    {
        if (base.TryReadProperties(ref reader, ref value, options))
            return true;

        if (!reader.ValueTextEquals(KeyframeConverterConstants.ValuesKey))
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

            value.Value = reader.GetSingle();
            i++;
        }

        return true;
    }

    protected override void WriteProperties(Utf8JsonWriter writer,
        FixedKeyframe<float> value,
        JsonSerializerOptions options)
    {
        base.WriteProperties(writer, value, options);

        writer.WritePropertyName(KeyframeConverterConstants.ValuesProperty);
        writer.WriteStartArray();
        writer.WriteNumberValue(value.Value);
        writer.WriteEndArray();
    }
}