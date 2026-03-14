using System.Numerics;
using System.Text.Json;
using Pamx.Neo.Editor;
using Pamx.Neo.Prefabs;

namespace Pamx.Neo.Serialization.Converters.Prefabs;

public sealed class PrefabObjectConverter : JsonObjectConverter<PrefabObject>
{
    private static readonly JsonEncodedText IdProperty = JsonEncodedText.Encode("id");
    private static readonly JsonEncodedText PrefabIdProperty = JsonEncodedText.Encode("pid");
    private static readonly JsonEncodedText EditorSettingsProperty = JsonEncodedText.Encode("ed");
    private static readonly JsonEncodedText StartTimeProperty = JsonEncodedText.Encode("t");
    private static readonly JsonEncodedText EventsProperty = JsonEncodedText.Encode("e");
    private static readonly JsonEncodedText EventValuesProperty = JsonEncodedText.Encode("ev");

    private static ReadOnlySpan<byte> IdKey => "id"u8;
    private static ReadOnlySpan<byte> PrefabIdKey => "pid"u8;
    private static ReadOnlySpan<byte> EditorSettingsKey => "ed"u8;
    private static ReadOnlySpan<byte> StartTimeKey => "t"u8;
    private static ReadOnlySpan<byte> EventsKey => "e"u8;
    private static ReadOnlySpan<byte> EventValuesKey => "ev"u8;

    protected override PrefabObject GetDefaultValue()
    {
        return new PrefabObject();
    }

    protected override bool TryReadProperties(ref Utf8JsonReader reader, ref PrefabObject value,
        JsonSerializerOptions options)
    {
        if (reader.ValueTextEquals(IdKey))
        {
            reader.Read();
            value.Id = reader.GetString() ?? string.Empty;
            return true;
        }

        if (reader.ValueTextEquals(PrefabIdKey))
        {
            reader.Read();
            value.PrefabId = reader.GetString() ?? string.Empty;
            return true;
        }

        if (reader.ValueTextEquals(EditorSettingsKey))
        {
            reader.Read();
            var result = JsonSerializer.Deserialize<ObjectEditorSettings>(ref reader, options);
            if (result is not null)
                value.EditorSettings = result;
            return true;
        }

        if (reader.ValueTextEquals(StartTimeKey))
        {
            reader.Read();
            value.StartTime = reader.GetSingle();
            return true;
        }

        if (reader.ValueTextEquals(EventsKey))
        {
            reader.Read();

            if (reader.TokenType != JsonTokenType.StartArray)
                throw new JsonException("Expected StartArray token");

            var i = 0;
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                switch (i)
                {
                    case 0:
                        value.Position = ReadVector2Event(ref reader);
                        break;
                    case 1:
                        value.Scale = ReadVector2Event(ref reader);
                        break;
                    case 2:
                        value.Rotation = ReadFloatEvent(ref reader);
                        break;
                    default:
                        reader.Skip();
                        break;
                }

                i++;
            }
        }

        return false;
    }

    protected override void WriteProperties(Utf8JsonWriter writer, PrefabObject value, JsonSerializerOptions options)
    {
        writer.WriteString(IdProperty, value.Id);
        writer.WriteString(PrefabIdProperty, value.PrefabId);

        if (!value.EditorSettings.IsDefault())
        {
            writer.WritePropertyName(EditorSettingsProperty);
            JsonSerializer.Serialize(writer, value.EditorSettings, options);
        }

        if (value.StartTime != 0.0f)
            writer.WriteNumber(StartTimeProperty, value.StartTime);

        writer.WritePropertyName(EventsProperty);
        writer.WriteStartArray();
        WriteEvent(writer, value.Position);
        WriteEvent(writer, value.Scale);
        WriteEvent(writer, value.Rotation);
        writer.WriteEndArray();
    }

    private static Vector2 ReadVector2Event(ref Utf8JsonReader reader)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException("Expected StartObject token");

        float x = 0.0f, y = 0.0f;
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                return new Vector2(x, y);
            if (reader.TokenType != JsonTokenType.PropertyName)
                continue;

            if (reader.ValueTextEquals(EventValuesKey))
            {
                reader.Read();
                if (reader.TokenType != JsonTokenType.StartArray)
                    throw new JsonException("Expected StartArray token");

                var i = 0;
                while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                {
                    switch (i)
                    {
                        case 0:
                            x = reader.GetSingle();
                            break;
                        case 1:
                            y = reader.GetSingle();
                            break;
                        default:
                            reader.Skip();
                            break;
                    }

                    i++;
                }
            }
            else
                reader.TrySkip();
        }

        throw new JsonException("Expected EndObject token");
    }

    private static float ReadFloatEvent(ref Utf8JsonReader reader)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException("Expected StartObject token");

        var value = 0.0f;
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                return value;
            if (reader.TokenType != JsonTokenType.PropertyName)
                continue;

            if (reader.ValueTextEquals(EventValuesKey))
            {
                reader.Read();
                if (reader.TokenType != JsonTokenType.StartArray)
                    throw new JsonException("Expected StartArray token");

                var i = 0;
                while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                {
                    switch (i)
                    {
                        case 0:
                            value = reader.GetSingle();
                            break;
                        default:
                            reader.Skip();
                            break;
                    }

                    i++;
                }
            }
            else
                reader.TrySkip();
        }

        throw new JsonException("Expected EndObject token");
    }

    private static void WriteEvent<T>(Utf8JsonWriter writer, T value)
    {
        // TODO: optimize

        writer.WriteStartObject();
        writer.WritePropertyName(EventValuesKey);
        writer.WriteStartArray();

        switch (value)
        {
            case Vector2 val:
                writer.WriteNumberValue(val.X);
                writer.WriteNumberValue(val.Y);
                break;
            case float val:
                writer.WriteNumberValue(val);
                break;
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}