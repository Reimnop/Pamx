using System.Numerics;
using System.Text.Json;
using Pamx.Neo.Keyframes;

namespace Pamx.Neo.Serialization.Converters.Keyframes;

internal sealed class FixedVector2KeyframeConverter : KeyframeConverter<FixedKeyframe<Vector2>>
{
    protected override FixedKeyframe<Vector2> GetDefaultValue() => new(Vector2.Zero);

    protected override bool TryReadProperties(ref Utf8JsonReader reader,
        ref FixedKeyframe<Vector2> value,
        JsonSerializerOptions options)
    {
        if (base.TryReadProperties(ref reader, ref value, options))
            return true;

        if (!reader.ValueTextEquals(KeyframeConverterConstants.ValuesKey))
            return false;

        reader.Read();
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException("Expected StartArray token");

        float x = 0.0f, y = 0.0f;
        var i = 0;

        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            switch (i)
            {
                case 0:
                    x = reader.GetSingle();
                    break;
                case 1:
                    y = reader.GetSingle();
                    break;
                default:
                    reader.Skip();
                    break;
            }

            i++;
        }

        value.Value = new Vector2(x, y);
        return true;
    }

    protected override void WriteProperties(Utf8JsonWriter writer,
        FixedKeyframe<Vector2> value,
        JsonSerializerOptions options)
    {
        base.WriteProperties(writer, value, options);

        writer.WritePropertyName(KeyframeConverterConstants.ValuesProperty);
        writer.WriteStartArray();
        writer.WriteNumberValue(value.Value.X);
        writer.WriteNumberValue(value.Value.Y);
        writer.WriteEndArray();
    }
}