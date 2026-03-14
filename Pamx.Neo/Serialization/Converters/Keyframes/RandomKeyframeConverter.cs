using System.Text.Json;
using Pamx.Neo.Keyframes;

namespace Pamx.Neo.Serialization.Converters.Keyframes;

internal abstract class RandomKeyframeConverter<T> : KeyframeConverter<RandomKeyframe<T>>
{
    protected override RandomKeyframe<T> GetDefaultValue() => new(GetDefaultKeyframeValue());

    protected override bool TryReadProperties(ref Utf8JsonReader reader, ref RandomKeyframe<T> value,
        JsonSerializerOptions options)
    {
        if (base.TryReadProperties(ref reader, ref value, options))
            return true;

        if (reader.ValueTextEquals(KeyframeConverterConstants.ValuesKey))
        {
            reader.Read();
            if (reader.TokenType != JsonTokenType.StartArray)
                throw new JsonException("Expected StartArray token");

            var keyframeValue = GetDefaultKeyframeValue();
            var i = 0;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                ReadKeyframeValue(ref reader, ref keyframeValue, i, options);
                i++;
            }

            value.Value = keyframeValue;
            return true;
        }

        if (reader.ValueTextEquals(KeyframeConverterConstants.RandomValuesKey))
        {
            reader.Read();
            if (reader.TokenType != JsonTokenType.StartArray)
                throw new JsonException("Expected StartArray token");

            var keyframeValue = GetDefaultKeyframeValue();
            var randomInterval = 0.0f;
            var i = 0;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                ReadKeyframeRandomValue(ref reader, ref keyframeValue, ref randomInterval, i, options);
                i++;
            }

            value.RandomValue = keyframeValue;
            value.RandomInterval = randomInterval;
            return true;
        }

        return false;
    }

    protected override void WriteProperties(Utf8JsonWriter writer,
        RandomKeyframe<T> value,
        JsonSerializerOptions options)
    {
        base.WriteProperties(writer, value, options);

        writer.WritePropertyName(KeyframeConverterConstants.ValuesProperty);
        writer.WriteStartArray();
        WriteKeyframeValues(writer, value.Value, options);
        writer.WriteEndArray();

        if (value.RandomMode != RandomMode.None)
            writer.WriteNumber(KeyframeConverterConstants.RandomModeProperty, (int)value.RandomMode);

        if (value.RandomValue is not null)
        {
            writer.WritePropertyName(KeyframeConverterConstants.RandomValuesProperty);
            writer.WriteStartArray();
            WriteKeyframeRandomValues(writer, value.Value, value.RandomInterval, options);
            writer.WriteEndArray();
        }
    }

    protected abstract T GetDefaultKeyframeValue();

    protected abstract void ReadKeyframeValue(ref Utf8JsonReader reader, ref T value, int index,
        JsonSerializerOptions options);

    protected abstract void ReadKeyframeRandomValue(ref Utf8JsonReader reader, ref T value, ref float interval,
        int index, JsonSerializerOptions options);

    protected abstract void WriteKeyframeValues(Utf8JsonWriter writer, T value, JsonSerializerOptions options);
    protected abstract void WriteKeyframeRandomValues(Utf8JsonWriter writer, T value, float interval, JsonSerializerOptions options);
}