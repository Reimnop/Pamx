using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Pamx.Events;
using Pamx.Keyframes;

namespace Pamx.Serialization.Converters.Events;

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
                    events.Chromatic =
                        JsonSerializer.Deserialize<List<FixedKeyframe<float>>>(ref reader, options) ?? [];
                    break;
                case 6:
                    events.Bloom =
                        JsonSerializer.Deserialize<List<FixedKeyframe<BloomValue>>>(ref reader, options) ?? [];
                    break;
                case 7:
                    events.Vignette =
                        JsonSerializer.Deserialize<List<FixedKeyframe<VignetteValue>>>(ref reader, options) ?? [];
                    break;
                case 8:
                    events.LensDistortion =
                        JsonSerializer.Deserialize<List<FixedKeyframe<LensDistortionValue>>>(ref reader, options) ?? [];
                    break;
                case 9:
                    events.Grain =
                        JsonSerializer.Deserialize<List<FixedKeyframe<GrainValue>>>(ref reader, options) ?? [];
                    break;
                case 10:
                    events.Gradient =
                        JsonSerializer.Deserialize<List<FixedKeyframe<GradientValue>>>(ref reader, options) ?? [];
                    break;
                case 11:
                    events.Glitch =
                        JsonSerializer.Deserialize<List<FixedKeyframe<GlitchValue>>>(ref reader, options) ?? [];
                    break;
                case 12:
                    events.Hue = JsonSerializer.Deserialize<List<FixedKeyframe<float>>>(ref reader, options) ?? [];
                    break;
                case 13:
                    events.Player = JsonSerializer.Deserialize<List<FixedKeyframe<Vector2>>>(ref reader, options) ?? [];
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
        JsonSerializer.Serialize(writer, value.Chromatic, options);
        JsonSerializer.Serialize(writer, value.Bloom, options);
        JsonSerializer.Serialize(writer, value.Vignette, options);
        JsonSerializer.Serialize(writer, value.LensDistortion, options);
        JsonSerializer.Serialize(writer, value.Grain, options);
        JsonSerializer.Serialize(writer, value.Gradient, options);
        JsonSerializer.Serialize(writer, value.Glitch, options);
        JsonSerializer.Serialize(writer, value.Hue, options);
        JsonSerializer.Serialize(writer, value.Player, options);

        writer.WriteEndArray();
    }
}