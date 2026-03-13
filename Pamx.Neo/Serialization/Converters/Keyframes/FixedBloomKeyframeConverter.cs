using System.Text.Json;
using Pamx.Neo.Events;
using Pamx.Neo.Keyframes;

namespace Pamx.Neo.Serialization.Converters.Keyframes;

internal sealed class FixedBloomKeyframeConverter : KeyframeConverter<FixedKeyframe<BloomValue>>
{
    protected override FixedKeyframe<BloomValue> GetDefaultValue() => new(BloomValue.Zero);

    protected override bool TryReadProperties(ref Utf8JsonReader reader,
        ref FixedKeyframe<BloomValue> value,
        JsonSerializerOptions options)
    {
        if (base.TryReadProperties(ref reader, ref value, options))
            return true;

        if (!reader.ValueTextEquals(KeyframeConverterConstants.ValuesKey))
            return false;

        reader.Read();
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException("Expected StartArray token");

        float intensity = 0.0f, diffusion = 0.0f;
        var color = 0;
        var i = 0;

        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            switch (i)
            {
                case 0:
                    intensity = reader.GetSingle();
                    break;
                case 1:
                    diffusion = reader.GetSingle();
                    break;
                case 2:
                    color = (int)reader.GetSingle();
                    break;
                default:
                    reader.Skip();
                    break;
            }

            i++;
        }

        value.Value = new BloomValue { Intensity = intensity, Diffusion = diffusion, Color = color };
        return true;
    }

    protected override void WriteProperties(Utf8JsonWriter writer,
        FixedKeyframe<BloomValue> value,
        JsonSerializerOptions options)
    {
        base.WriteProperties(writer, value, options);

        writer.WritePropertyName(KeyframeConverterConstants.ValuesProperty);
        writer.WriteStartArray();
        writer.WriteNumberValue(value.Value.Intensity);
        writer.WriteNumberValue(value.Value.Diffusion);
        writer.WriteNumberValue((float)value.Value.Color);
        writer.WriteEndArray();
    }
}