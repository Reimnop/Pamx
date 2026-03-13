using System.Text.Json;
using Pamx.Neo.Keyframes;

namespace Pamx.Neo.Serialization.Converters.Keyframes;

internal abstract class KeyframeConverter<T> : JsonObjectConverter<T> where T : Keyframe
{
    protected override bool TryReadProperties(ref Utf8JsonReader reader, ref T value, JsonSerializerOptions options)
    {
        if (reader.ValueTextEquals(KeyframeConverterConstants.TimeKey))
        {
            reader.Read();
            value.Time = reader.GetSingle();
            return true;
        }

        if (reader.ValueTextEquals(KeyframeConverterConstants.EaseKey))
        {
            reader.Read();
            value.Ease = JsonSerializer.Deserialize<Ease>(ref reader, options);
            return true;
        }

        return false;
    }

    protected override void WriteProperties(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        if (value.Time != 0.0f)
            writer.WriteNumber(KeyframeConverterConstants.TimeProperty, value.Time);
        if (value.Ease == Ease.Linear)
            return;

        writer.WritePropertyName(KeyframeConverterConstants.EaseProperty);
        JsonSerializer.Serialize(writer, value.Ease, options);
    }
}