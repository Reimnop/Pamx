using System.Numerics;
using System.Text.Json;
using Pamx.Neo.Events;
using Pamx.Neo.Keyframes;

namespace Pamx.Neo.Serialization.Converters.Keyframes;

internal sealed class FixedLensDistortionKeyframeConverter : KeyframeConverter<FixedKeyframe<LensDistortionValue>>
{
    protected override FixedKeyframe<LensDistortionValue> GetDefaultValue() => new(LensDistortionValue.Zero);

    protected override bool TryReadProperties(ref Utf8JsonReader reader,
        ref FixedKeyframe<LensDistortionValue> value,
        JsonSerializerOptions options)
    {
        if (base.TryReadProperties(ref reader, ref value, options))
            return true;

        if (!reader.ValueTextEquals(KeyframeConverterConstants.ValuesKey))
            return false;

        reader.Read();
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException("Expected StartArray token");

        float intensity = 0.0f, centerX = 0.0f, centerY = 0.0f;
        var i = 0;

        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            switch (i)
            {
                case 0:
                    intensity = reader.GetSingle();
                    break;
                case 1:
                    centerX = reader.GetSingle();
                    break;
                case 2:
                    centerY = reader.GetSingle();
                    break;
                default:
                    reader.Skip();
                    break;
            }

            i++;
        }

        value.Value = new LensDistortionValue { Intensity = intensity, Center = new Vector2(centerX, centerY) };
        return true;
    }

    protected override void WriteProperties(Utf8JsonWriter writer,
        FixedKeyframe<LensDistortionValue> value,
        JsonSerializerOptions options)
    {
        base.WriteProperties(writer, value, options);

        writer.WritePropertyName(KeyframeConverterConstants.ValuesProperty);
        writer.WriteStartArray();
        writer.WriteNumberValue(value.Value.Intensity);
        writer.WriteNumberValue(value.Value.Center.X);
        writer.WriteNumberValue(value.Value.Center.Y);
        writer.WriteEndArray();
    }
}