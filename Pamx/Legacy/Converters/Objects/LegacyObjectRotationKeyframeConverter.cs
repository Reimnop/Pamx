using System.Text.Json;
using System.Text.Json.Serialization;
using Pamx.Keyframes;
using Pamx.Serialization.Extensions;
using Pamx.Objects;
using Pamx.Serialization.Converters.Keyframes;

namespace Pamx.Legacy.Converters.Objects;

internal sealed class LegacyObjectRotationKeyframeConverter : JsonConverter<ObjectRotationKeyframe>
{
    private static ReadOnlySpan<byte> ValueKey => "x"u8;
    private static ReadOnlySpan<byte> RandomIntervalKey => "rz"u8;
    private static ReadOnlySpan<byte> RandomValueKey => "rx"u8;

    public override ObjectRotationKeyframe Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException("Expected StartObject token");

        var value = 0.0f;
        var time = 0.0f;
        var ease = Ease.Linear;

        var randomMode = RandomMode.None;
        var randomInterval = 0.0f;
        var randomValue = 0.0f;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                return new ObjectRotationKeyframe(value, time, ease)
                {
                    RandomMode = randomMode,
                    RandomInterval = randomInterval,
                    RandomValue = randomValue
                };
            if (reader.TokenType != JsonTokenType.PropertyName)
                continue;

            if (reader.ValueTextEquals(KeyframeConverterConstants.TimeKey))
            {
                reader.Read();
                time = reader.GetSingleLike();
            }
            else if (reader.ValueTextEquals(KeyframeConverterConstants.EaseKey))
            {
                reader.Read();
                ease = JsonSerializer.Deserialize<Ease>(ref reader, options);
            }
            else if (reader.ValueTextEquals(ValueKey))
            {
                reader.Read();
                value = reader.GetSingleLike();
            }
            else if (reader.ValueTextEquals(KeyframeConverterConstants.RandomModeKey))
            {
                reader.Read();
                randomMode = (RandomMode)reader.GetInt32Like();
            }
            else if (reader.ValueTextEquals(RandomIntervalKey))
            {
                reader.Read();
                randomInterval = reader.GetSingleLike();
            }
            else if (reader.ValueTextEquals(RandomValueKey))
            {
                reader.Read();
                randomValue = reader.GetSingleLike();
            }
            else
                reader.TrySkip();
        }

        throw new JsonException("Expected EndObject token");
    }

    public override void Write(Utf8JsonWriter writer, ObjectRotationKeyframe value, JsonSerializerOptions options) =>
        throw new InvalidOperationException("This converter is read-only and cannot write JSON data.");
}