using System.Buffers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Pamx.Neo.Objects;

namespace Pamx.Neo.Serialization.Converters.Objects;

internal sealed class ParentTypeConverter : JsonConverter<ParentType>
{
    public override ParentType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException("Expected String token");

        var span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;
        if (span.Length != 3)
            throw new JsonException("Invalid string length");

        var result = ParentType.None;
        if (span[0] != (byte)'0')
            result |= ParentType.Position;
        if (span[1] != (byte)'0')
            result |= ParentType.Scale;
        if (span[2] != (byte)'0')
            result |= ParentType.Rotation;

        return result;
    }

    public override void Write(Utf8JsonWriter writer, ParentType value, JsonSerializerOptions options)
    {
        Span<byte> output = stackalloc byte[3];
        output[0] = (value & ParentType.Position) == ParentType.Position ? (byte)'1' : (byte)'0';
        output[1] = (value & ParentType.Scale) == ParentType.Scale ? (byte)'1' : (byte)'0';
        output[2] = (value & ParentType.Rotation) == ParentType.Rotation ? (byte)'1' : (byte)'0';
        
        writer.WriteStringValue(output);
    }
}