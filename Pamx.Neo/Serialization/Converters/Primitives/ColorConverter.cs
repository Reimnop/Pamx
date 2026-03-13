using System.Buffers;
using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pamx.Neo.Serialization.Converters.Primitives;

internal sealed class ColorConverter : JsonConverter<Color>
{
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException("Expected string for Color conversion.");

        var span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;
        if (span.Length != 6)
            throw new JsonException("Invalid color format.");

        return Color.FromArgb(
            255,
            ParseHexByte(span[0], span[1]),
            ParseHexByte(span[2], span[3]),
            ParseHexByte(span[4], span[5]));
    }

    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
    {
        Span<char> hex = stackalloc char[6];
        WriteHexByte(value.R, hex, 0);
        WriteHexByte(value.G, hex, 2);
        WriteHexByte(value.B, hex, 4);
        writer.WriteStringValue(hex);
    }

    private static byte ParseHexByte(byte h1, byte h2) => (byte)((GetHexVal(h1) << 4) + GetHexVal(h2));

    private static int GetHexVal(byte hex) => hex - (hex < 58 ? 48 : (hex < 91 ? 55 : 87));

    private static void WriteHexByte(byte b, Span<char> destination, int start)
    {
        var hex = b.ToString("X2");
        destination[start] = hex[0];
        destination[start + 1] = hex[1];
    }
}