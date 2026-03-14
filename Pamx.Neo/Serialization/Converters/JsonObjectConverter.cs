using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pamx.Neo.Serialization.Converters;

public abstract class JsonObjectConverter<T> : JsonConverter<T>
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException("Expected StartObject token");

        var value = GetDefaultValue();
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                return value;
            if (reader.TokenType != JsonTokenType.PropertyName)
                continue;
            if (!TryReadProperties(ref reader, ref value, options))
                reader.TrySkip();
        }
        
        throw new JsonException("Expected EndObject token");
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        WriteProperties(writer, value, options);
        writer.WriteEndObject();
    }

    protected abstract T GetDefaultValue();
    protected abstract bool TryReadProperties(ref Utf8JsonReader reader, ref T value, JsonSerializerOptions options);
    protected abstract void WriteProperties(Utf8JsonWriter writer, T value, JsonSerializerOptions options);
}