using System.Text.Json;
using Pamx.Neo.Events;
using Pamx.Neo.Keyframes;

namespace Pamx.Neo.Serialization.Converters.Keyframes;

internal sealed class FixedGlitchKeyframeConverter : KeyframeConverter<FixedKeyframe<GlitchValue>>
{
    protected override FixedKeyframe<GlitchValue> GetDefaultValue() => new(GlitchValue.Zero);

    protected override bool TryReadProperties(ref Utf8JsonReader reader,
        ref FixedKeyframe<GlitchValue> value,
        JsonSerializerOptions options)
    {
        if (base.TryReadProperties(ref reader, ref value, options))
            return true;

        if (!reader.ValueTextEquals(KeyframeConverterConstants.ValuesKey))
            return false;

        reader.Read();
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException("Expected StartArray token");

        float intensity = 0.0f, width = 1.0f, speed = 0.88f;
        var i = 0;

        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            switch (i)
            {
                case 0:
                    intensity = reader.GetSingle();
                    break;
                case 1:
                    width = reader.GetSingle();
                    break;
                case 2:
                    speed = reader.GetSingle();
                    break;
                default:
                    reader.Skip();
                    break;
            }

            i++;
        }

        value.Value = new GlitchValue { Intensity = intensity, Speed = speed, Width = width };
        return true;
    }

    protected override void WriteProperties(Utf8JsonWriter writer,
        FixedKeyframe<GlitchValue> value,
        JsonSerializerOptions options)
    {
        base.WriteProperties(writer, value, options);

        writer.WritePropertyName(KeyframeConverterConstants.ValuesProperty);
        writer.WriteStartArray();
        writer.WriteNumberValue(value.Value.Intensity);
        writer.WriteNumberValue(value.Value.Width);
        writer.WriteNumberValue(value.Value.Speed);
        writer.WriteEndArray();
    }
}