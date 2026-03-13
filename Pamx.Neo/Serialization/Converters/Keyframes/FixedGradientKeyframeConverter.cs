using System.Text.Json;
using Pamx.Neo.Events;
using Pamx.Neo.Keyframes;

namespace Pamx.Neo.Serialization.Converters.Keyframes;

internal sealed class FixedGradientKeyframeConverter : KeyframeConverter<FixedKeyframe<GradientValue>>
{
    protected override FixedKeyframe<GradientValue> GetDefaultValue() => new(GradientValue.Zero);

    protected override bool TryReadProperties(ref Utf8JsonReader reader,
        ref FixedKeyframe<GradientValue> value,
        JsonSerializerOptions options)
    {
        if (base.TryReadProperties(ref reader, ref value, options))
            return true;

        if (!reader.ValueTextEquals(KeyframeConverterConstants.ValuesKey))
            return false;

        reader.Read();
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException("Expected StartArray token");

        float intensity = 0.0f, rotation = 0.0f;
        int startColor = 9, endColor = 9;
        var mode = GradientOverlayMode.Multiply;
        var i = 0;

        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            switch (i)
            {
                case 0:
                    intensity = reader.GetSingle();
                    break;
                case 1:
                    rotation = reader.GetSingle();
                    break;
                case 2:
                    startColor = (int)reader.GetSingle();
                    break;
                case 3:
                    endColor = (int)reader.GetSingle();
                    break;
                case 4:
                    mode = (GradientOverlayMode)reader.GetSingle();
                    break;
                default:
                    reader.Skip();
                    break;
            }

            i++;
        }

        value.Value = new GradientValue
        {
            Intensity = intensity,
            Rotation = rotation,
            StartColor = startColor,
            EndColor = endColor,
            Mode = mode
        };
        return true;
    }

    protected override void WriteProperties(Utf8JsonWriter writer,
        FixedKeyframe<GradientValue> value,
        JsonSerializerOptions options)
    {
        base.WriteProperties(writer, value, options);

        writer.WritePropertyName(KeyframeConverterConstants.ValuesProperty);
        writer.WriteStartArray();
        writer.WriteNumberValue(value.Value.Intensity);
        writer.WriteNumberValue(value.Value.Rotation);
        writer.WriteNumberValue((float)value.Value.StartColor);
        writer.WriteNumberValue((float)value.Value.EndColor);
        writer.WriteNumberValue((float)value.Value.Mode);
        writer.WriteEndArray();
    }
}