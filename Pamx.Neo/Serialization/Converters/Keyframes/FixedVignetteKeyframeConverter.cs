using System.Numerics;
using System.Text.Json;
using Pamx.Neo.Events;
using Pamx.Neo.Keyframes;

namespace Pamx.Neo.Serialization.Converters.Keyframes;

internal sealed class FixedVignetteKeyframeConverter : KeyframeConverter<FixedKeyframe<VignetteValue>>
{
    protected override FixedKeyframe<VignetteValue> GetDefaultValue() => new(VignetteValue.Zero);

    protected override bool TryReadProperties(ref Utf8JsonReader reader,
        ref FixedKeyframe<VignetteValue> value,
        JsonSerializerOptions options)
    {
        if (base.TryReadProperties(ref reader, ref value, options))
            return true;

        if (!reader.ValueTextEquals(KeyframeConverterConstants.ValuesKey))
            return false;

        reader.Read();
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException("Expected StartArray token");

        float intensity = 0.0f, smoothness = 0.0f, centerX = 0.0f, centerY = 0.0f;
        var color = 0;
        var isRounded = false;
        var i = 0;

        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            switch (i)
            {
                case 0:
                    intensity = reader.GetSingle();
                    break;
                case 1:
                    smoothness = reader.GetSingle();
                    break;
                case 2:
                    isRounded = reader.GetSingle() != 0.0f;
                    break;
                case 4:
                    centerX = reader.GetSingle();
                    break;
                case 5:
                    centerY = reader.GetSingle();
                    break;
                case 6:
                    color = (int)reader.GetSingle();
                    break;
                default:
                    reader.Skip();
                    break;
            }

            i++;
        }

        value.Value = new VignetteValue
        {
            Intensity = intensity,
            Smoothness = smoothness,
            Color = color,
            IsRounded = isRounded,
            Center = new Vector2(centerX, centerY)
        };
        return true;
    }

    protected override void WriteProperties(Utf8JsonWriter writer,
        FixedKeyframe<VignetteValue> value,
        JsonSerializerOptions options)
    {
        base.WriteProperties(writer, value, options);

        writer.WritePropertyName(KeyframeConverterConstants.ValuesProperty);
        writer.WriteStartArray();
        writer.WriteNumberValue(value.Value.Intensity);
        writer.WriteNumberValue(value.Value.Smoothness);
        writer.WriteNumberValue(value.Value.IsRounded ? 1.0f : 0.0f);
        writer.WriteNumberValue(0.0f);
        writer.WriteNumberValue(value.Value.Center.X);
        writer.WriteNumberValue(value.Value.Center.Y);
        writer.WriteNumberValue((float)value.Value.Color);
        writer.WriteEndArray();
    }
}