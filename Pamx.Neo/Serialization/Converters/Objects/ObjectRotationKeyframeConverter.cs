using System.Text.Json;
using Pamx.Neo.Keyframes;
using Pamx.Neo.Objects;
using Pamx.Neo.Serialization.Converters.Keyframes;

namespace Pamx.Neo.Serialization.Converters.Objects;

internal sealed class ObjectRotationKeyframeConverter : KeyframeConverter<ObjectRotationKeyframe>
{
    protected override ObjectRotationKeyframe GetDefaultValue() => new(0.0f);

    protected override bool TryReadProperties(ref Utf8JsonReader reader, ref ObjectRotationKeyframe value,
        JsonSerializerOptions options)
    {
        if (base.TryReadProperties(ref reader, ref value, options))
            return true;

        if (reader.ValueTextEquals(KeyframeConverterConstants.ValuesKey))
        {
            reader.Read();
            if (reader.TokenType != JsonTokenType.StartArray)
                throw new JsonException("Expected StartArray token");

            var i = 0;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                switch (i)
                {
                    case 0:
                        value.Value = reader.GetSingle();
                        break;
                    case 1:
                        value.IsAbsolute = reader.GetSingle() != 0.0f;
                        break;
                    default:
                        reader.Skip();
                        break;
                }

                i++;
            }

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

            var i = 0;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                switch (i)
                {
                    case 0:
                        value.RandomValue = reader.GetSingle();
                        break;
                    case 2:
                        value.RandomInterval = reader.GetSingle();
                        break;
                    default:
                        reader.Skip();
                        break;
                }

                i++;
            }

            return true;
        }

        return false;
    }

    protected override void WriteProperties(Utf8JsonWriter writer, ObjectRotationKeyframe value,
        JsonSerializerOptions options)
    {
        base.WriteProperties(writer, value, options);

        writer.WritePropertyName(KeyframeConverterConstants.ValuesKey);
        writer.WriteStartArray();
        writer.WriteNumberValue(value.Value);
        writer.WriteNumberValue(value.IsAbsolute ? 1.0f : 0.0f);
        writer.WriteEndArray();

        if (value.RandomMode != RandomMode.None)
            writer.WriteNumber(KeyframeConverterConstants.RandomModeProperty, (int)value.RandomMode);

        writer.WritePropertyName(KeyframeConverterConstants.RandomValuesProperty);
        writer.WriteStartArray();
        writer.WriteNumberValue(value.RandomValue);
        writer.WriteNumberValue(0.0f);
        writer.WriteNumberValue(value.RandomInterval);
        writer.WriteEndArray();
    }
}