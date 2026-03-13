using System.Numerics;
using System.Text.Json;

namespace Pamx.Neo.Serialization.Converters.Primitives;

internal sealed class Vector2Converter : JsonObjectConverter<Vector2>
{
    private static readonly JsonEncodedText XProperty = JsonEncodedText.Encode("x");
    private static readonly JsonEncodedText YProperty = JsonEncodedText.Encode("y");
    
    private static ReadOnlySpan<byte> XKey => "x"u8;
    private static ReadOnlySpan<byte> YKey => "y"u8;

    protected override Vector2 GetDefaultValue() => Vector2.Zero;

    protected override bool TryReadProperties(ref Utf8JsonReader reader, ref Vector2 value, JsonSerializerOptions options)
    {
        if (reader.ValueTextEquals(XKey))
        {
            reader.Read();
            value.X = reader.GetSingle();
            return true;
        }

        if (reader.ValueTextEquals(YKey))
        {
            reader.Read();
            value.Y = reader.GetSingle();
            return true;
        }

        return false;
    }

    protected override void WriteProperties(Utf8JsonWriter writer, Vector2 value, JsonSerializerOptions options)
    {
        writer.WriteNumber(XProperty, value.X);
        writer.WriteNumber(YProperty, value.Y);
    }
}