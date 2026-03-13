using System.Text.Json;
using Pamx.Neo.Events;
using Pamx.Neo.Keyframes;

namespace Pamx.Neo.Serialization.Converters.Keyframes;

internal sealed class FixedGrainKeyframeConverter : KeyframeConverter<FixedKeyframe<GrainValue>>
{
    protected override FixedKeyframe<GrainValue> GetDefaultValue() => new(GrainValue.Zero);

    protected override bool TryReadProperties(ref Utf8JsonReader reader,
        ref FixedKeyframe<GrainValue> value,
        JsonSerializerOptions options)
    {
        if (base.TryReadProperties(ref reader, ref value, options))
            return true;

        if (!reader.ValueTextEquals(KeyframeConverterConstants.ValuesKey))
            return false;

        reader.Read();
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException("Expected StartArray token");

        float intensity = 0.0f, size = 0.0f, mix = 0.0f;
        var i = 0;

        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            switch (i)
            {
                case 0:
                    intensity = reader.GetSingle();
                    break;
                case 1:
                    size = reader.GetSingle();
                    break;
                case 2:
                    mix = reader.GetSingle();
                    break;
                default:
                    reader.Skip();
                    break;
            }

            i++;
        }

        value.Value = new GrainValue { Intensity = intensity, Size = size, Mix = mix };
        return true;
    }

    protected override void WriteProperties(Utf8JsonWriter writer,
        FixedKeyframe<GrainValue> value,
        JsonSerializerOptions options)
    {
        base.WriteProperties(writer, value, options);

        writer.WritePropertyName(KeyframeConverterConstants.ValuesProperty);
        writer.WriteStartArray();
        writer.WriteNumberValue(value.Value.Intensity);
        writer.WriteNumberValue(value.Value.Size);
        writer.WriteNumberValue(value.Value.Mix);
        writer.WriteEndArray();
    }
}