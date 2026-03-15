using System.Text.Json;
using System.Text.Json.Serialization;
using Pamx.Objects;

namespace Pamx.Serialization.Converters.Objects;

internal sealed class CustomShapeParamsConverter : JsonConverter<CustomShapeParams>
{
    public override CustomShapeParams Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException("Expected StartArray token");

        int sides = 0, slices = 0;
        float roundness = 0.0f, thickness = 0.0f;

        var i = 0;
        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            switch (i)
            {
                case 0:
                    sides = (int)reader.GetSingle();
                    break;
                case 1:
                    roundness = reader.GetSingle();
                    break;
                case 2:
                    thickness = reader.GetSingle();
                    break;
                case 3:
                    slices = (int)reader.GetSingle();
                    break;
                default:
                    reader.Skip();
                    break;
            }
            
            i++;
        }

        return new CustomShapeParams
        {
            Sides = sides,
            Roundness = roundness,
            Thickness = thickness,
            Slices = slices
        };
    }

    public override void Write(Utf8JsonWriter writer, CustomShapeParams value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        writer.WriteNumberValue((float)value.Sides);
        writer.WriteNumberValue(value.Roundness);
        writer.WriteNumberValue(value.Thickness);
        writer.WriteNumberValue((float)value.Slices);
        writer.WriteNumberValue(0.0f);
        writer.WriteEndArray();
    }
}