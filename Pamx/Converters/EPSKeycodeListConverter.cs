using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pamx.Converters;

public class EPSKeycodeListConverter : JsonConverter<List<string>>
{
    public override List<string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException("Expected StartArray token");
        
        var keycodes = new List<string>();

        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Number:
                {
                    if (reader.TryGetInt32(out var keycode))
                        keycodes.Add(keycode.ToString());
                    break;
                }
                case JsonTokenType.String:
                    keycodes.Add(reader.GetString() ?? string.Empty);
                    break;
                default:
                    throw new JsonException($"Unexpected token type: {reader.TokenType}. Expected Number or String.");
            }
        }
        
        return keycodes;
    }

    public override void Write(Utf8JsonWriter writer, List<string> value, JsonSerializerOptions options)
    {
        // Write the list of strings as a JSON array
        writer.WriteStartArray();
        foreach (var item in value)
            writer.WriteStringValue(item);
        writer.WriteEndArray();
    }
}