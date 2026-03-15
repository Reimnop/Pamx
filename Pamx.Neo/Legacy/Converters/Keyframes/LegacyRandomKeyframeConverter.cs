using System.Text.Json;
using System.Text.Json.Serialization;
using Pamx.Neo.Keyframes;
using Pamx.Neo.Serialization.Converters.Keyframes;
using Pamx.Neo.Serialization.Extensions;

namespace Pamx.Neo.Legacy.Converters.Keyframes;

internal abstract class LegacyRandomKeyframeConverter<T> : JsonConverter<RandomKeyframe<T>>
{
    private static ReadOnlySpan<byte> RandomIntervalKey => "rz"u8;

    public override RandomKeyframe<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException("Expected StartObject token");

        var value = GetDefaultValue();
        var time = 0.0f;
        var ease = Ease.Linear;

        var randomMode = RandomMode.None;
        var randomInterval = 0.0f;
        var randomValue = GetDefaultValue();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                return CreateKeyframe(value, time, ease, randomMode, randomInterval, randomValue);
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
            else if (!TryReadValues(ref reader, ref value, ref randomValue, options))
                reader.TrySkip();
        }

        throw new JsonException("Expected EndObject token");
    }

    public override void Write(Utf8JsonWriter writer, RandomKeyframe<T> value, JsonSerializerOptions options) =>
        throw new InvalidOperationException("This converter is read-only and cannot write JSON data.");

    protected abstract T GetDefaultValue();

    protected abstract RandomKeyframe<T> CreateKeyframe(T value, float time, Ease ease, RandomMode randomMode,
        float randomInterval, T randomValue);

    protected abstract bool TryReadValues(ref Utf8JsonReader reader, ref T value, ref T randomValue,
        JsonSerializerOptions options);
}