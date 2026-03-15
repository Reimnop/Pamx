using System.Text.Json;
using System.Text.Json.Serialization;
using Pamx.Neo.Keyframes;
using Pamx.Neo.Serialization.Converters.Keyframes;
using Pamx.Neo.Serialization.Extensions;

namespace Pamx.Neo.Serialization.Legacy.Converters.Keyframes;

internal abstract class LegacyFixedKeyframeConverter<T> : JsonConverter<FixedKeyframe<T>>
{
    public override FixedKeyframe<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException("Expected StartObject token");

        var value = GetDefaultValue();
        var time = 0.0f;
        var ease = Ease.Linear;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                return CreateKeyframe(value, time, ease);
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
            else if (!TryReadValues(ref reader, ref value, options))
                reader.TrySkip();
        }

        throw new JsonException("Expected EndObject token");
    }

    public override void Write(Utf8JsonWriter writer, FixedKeyframe<T> value, JsonSerializerOptions options) =>
        throw new InvalidOperationException("This converter is read-only and cannot write JSON data.");

    protected abstract T GetDefaultValue();
    protected abstract FixedKeyframe<T> CreateKeyframe(T value, float time, Ease ease);
    protected abstract bool TryReadValues(ref Utf8JsonReader reader, ref T value, JsonSerializerOptions options);
}