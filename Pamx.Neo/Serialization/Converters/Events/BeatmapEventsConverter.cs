using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Pamx.Neo.Events;
using Pamx.Neo.Keyframes;

namespace Pamx.Neo.Serialization.Converters.Events;

internal sealed class BeatmapEventsConverter : JsonConverter<BeatmapEvents>
{
    public override BeatmapEvents Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException("Expected StartArray token");

        var events = new BeatmapEvents();
        var i = 0;

        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            switch (i)
            {
                case 0:
                    events.Move = JsonSerializer.Deserialize<List<FixedKeyframe<Vector2>>>(ref reader, options) ?? [];
                    break;
                case 1:
                    events.Zoom = JsonSerializer.Deserialize<List<FixedKeyframe<float>>>(ref reader, options) ?? [];
                    break;
                case 2:
                    events.Rotate = JsonSerializer.Deserialize<List<FixedKeyframe<float>>>(ref reader, options) ?? [];
                    break;
                case 3:
                    events.Shake = JsonSerializer.Deserialize<List<FixedKeyframe<float>>>(ref reader, options) ?? [];
                    break;
                case 4:
                    events.Theme = JsonSerializer.Deserialize<List<FixedKeyframe<string>>>(ref reader, options) ?? [];
                    break;
                case 5:
                    events.Chroma = JsonSerializer.Deserialize<List<FixedKeyframe<float>>>(ref reader, options) ?? [];
                    break;
                default:
                    reader.Skip();
                    break;
            }

            i++;
        }

        return events;
    }

    public override void Write(Utf8JsonWriter writer, BeatmapEvents value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        JsonSerializer.Serialize(writer, value.Move, options);
        JsonSerializer.Serialize(writer, value.Zoom, options);
        JsonSerializer.Serialize(writer, value.Rotate, options);
        JsonSerializer.Serialize(writer, value.Shake, options);
        JsonSerializer.Serialize(writer, value.Theme, options);
        JsonSerializer.Serialize(writer, value.Chroma, options);

        writer.WriteEndArray();
    }
}