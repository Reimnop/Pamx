using System.Numerics;
using System.Text.Json;
using Pamx.Keyframes;
using Pamx.Serialization.Converters.Keyframes;

namespace Pamx.Serialization.Converters.Objects;

internal sealed class RandomVector2KeyframeConverter : KeyframeConverter<RandomKeyframe<Vector2>>
{
    protected override RandomKeyframe<Vector2> GetDefaultValue() => new(Vector2.Zero);

    protected override bool TryReadProperties(ref Utf8JsonReader reader, ref RandomKeyframe<Vector2> value,
        JsonSerializerOptions options)
    {
        if (base.TryReadProperties(ref reader, ref value, options))
            return true;

        if (reader.ValueTextEquals(KeyframeConverterConstants.ValuesKey))
        {
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

        if (reader.ValueTextEquals(KeyframeConverterConstants.RandomModeKey))
        {
            reader.Read();
            value.RandomMode = (RandomMode)reader.GetInt32();
            return true;
        }

        if (reader.ValueTextEquals(KeyframeConverterConstants.RandomValuesKey))
        {
            reader.Read();
            if (reader.TokenType != JsonTokenType.StartArray)
                throw new JsonException("Expected StartArray token");

            float x = 0.0f, y = 0.0f, interval = 0.0f;
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
                    case 2:
                        interval = reader.GetSingle();
                        break;
                    default:
                        reader.Skip();
                        break;
                }

                i++;
            }

            value.RandomValue = new Vector2(x, y);
            value.RandomInterval = interval;
            return true;
        }

        return false;
    }

    protected override void WriteProperties(Utf8JsonWriter writer, RandomKeyframe<Vector2> value,
        JsonSerializerOptions options)
    {
        base.WriteProperties(writer, value, options);

        writer.WritePropertyName(KeyframeConverterConstants.ValuesKey);
        writer.WriteStartArray();
        writer.WriteNumberValue(value.Value.X);
        writer.WriteNumberValue(value.Value.Y);
        writer.WriteEndArray();

        if (value.RandomMode != RandomMode.None)
            writer.WriteNumber(KeyframeConverterConstants.RandomModeProperty, (int)value.RandomMode);
        // if (value.RandomValue == Vector2.Zero || value.RandomInterval == 0.0f)
            // return;

        writer.WritePropertyName(KeyframeConverterConstants.RandomValuesProperty);
        writer.WriteStartArray();
        writer.WriteNumberValue(value.RandomValue.X);
        writer.WriteNumberValue(value.RandomValue.Y);
        writer.WriteNumberValue(value.RandomInterval);
        writer.WriteEndArray();
    }
}