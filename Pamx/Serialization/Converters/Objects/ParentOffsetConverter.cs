using System.Text.Json;
using System.Text.Json.Serialization;
using Pamx.Serialization.Extensions;
using Pamx.Objects;

namespace Pamx.Serialization.Converters.Objects;

internal sealed class ParentOffsetConverter : JsonConverter<ParentOffset>
{
    public override ParentOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException("Expected StartArray token");

        float position = 0.0f, scale = 0.0f, rotation = 0.0f;
        var i = 0;

        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            switch (i)
            {
                case 0:
                    position = reader.GetSingleLike();
                    break;
                case 1:
                    scale = reader.GetSingleLike();
                    break;
                case 2:
                    rotation = reader.GetSingleLike();
                    break;
                default:
                    reader.Skip();
                    break;
            }

            i++;
        }

        return new ParentOffset { Position = position, Scale = scale, Rotation = rotation };
    }

    public override void Write(Utf8JsonWriter writer, ParentOffset value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        writer.WriteNumberValue(value.Position);
        writer.WriteNumberValue(value.Scale);
        writer.WriteNumberValue(value.Rotation);
        writer.WriteEndArray();
    }
}