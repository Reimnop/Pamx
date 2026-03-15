using System.Buffers;
using System.Buffers.Text;
using System.Text.Json;

namespace Pamx.Neo.Serialization.Legacy.Extensions;

public static class Utf8JsonReaderExtensions
{
    extension(ref Utf8JsonReader reader)
    {
        public int GetInt32Like()
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Number when reader.TryGetInt32(out var result):
                    return result;
                case JsonTokenType.Number:
                    return (int)reader.GetDouble();
                case JsonTokenType.String when reader.HasValueSequence || reader.ValueIsEscaped:
                {
                    var text = reader.GetString();
                    if (int.TryParse(text, out var result))
                        return result;
                    if (double.TryParse(text, out var fallback))
                        return (int)fallback;
                    return 0;
                }
                case JsonTokenType.String when Utf8Parser.TryParse(reader.ValueSpan, out int result, out _):
                    return result;
                case JsonTokenType.String when Utf8Parser.TryParse(reader.ValueSpan, out double fallback, out _):
                    return (int)fallback;
                default:
                    return 0;
            }
        }

        public float GetSingleLike() =>
            reader.TokenType switch
            {
                JsonTokenType.Number => reader.GetSingle(),
                JsonTokenType.String when reader.HasValueSequence || reader.ValueIsEscaped => float.TryParse(
                    reader.GetString(), out var f)
                    ? f
                    : 0.0f,
                JsonTokenType.String when Utf8Parser.TryParse(reader.ValueSpan, out float f, out _) => f,
                JsonTokenType.String when Utf8Parser.TryParse(reader.ValueSpan, out double d, out _) => (float)d,
                _ => 0.0f
            };

        public string? GetRawString() =>
            reader.TokenType switch
            {
                JsonTokenType.String => reader.GetString(),
                JsonTokenType.Number => System.Text.Encoding.UTF8.GetString(reader.HasValueSequence 
                    ? reader.ValueSequence.ToArray() 
                    : reader.ValueSpan),
                JsonTokenType.True => "true",
                JsonTokenType.False => "false",
                JsonTokenType.Null => null,
                _ => throw new JsonException($"Cannot read token type {reader.TokenType} as string.")
            };
    }
}