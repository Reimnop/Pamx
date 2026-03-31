using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pamx.Serialization.Converters.Editor;

internal sealed class KeycodeListConverter : JsonConverter<List<string>>
{
    public override List<string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException("Expected StartArray token");

        var result = new List<string>();
        while (reader.Read())
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.EndArray:
                    return result;
                case JsonTokenType.Number:
                {
                    if (reader.TryGetInt32(out var keycode))
                        result.Add(keycode.ToString());
                    break;
                }
                case JsonTokenType.String:
                    result.Add(reader.GetString() ?? string.Empty);
                    break;
                default:
                    throw new JsonException("Expected Number or String token");
            }
        }

        throw new JsonException("Expected EndArray token");
    }

    public override void Write(Utf8JsonWriter writer, List<string> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        foreach (var keycode in value)
            writer.WriteStringValue(keycode);
        writer.WriteEndArray();
    }
}