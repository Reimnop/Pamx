using System.Numerics;
using System.Text.Json;

namespace Pamx.Serialization.Converters.Keyframes;

internal sealed class FixedVector2KeyframeConverter : FixedKeyframeConverter<Vector2>
{
    protected override Vector2 GetDefaultKeyframeValue() => Vector2.Zero;

    protected override void ReadKeyframeValue(ref Utf8JsonReader reader, ref Vector2 value, int index,
        JsonSerializerOptions options)
    {
        switch (index)
        {
            case 0:
                value.X = reader.GetSingle();
                break;
            case 1:
                value.Y = reader.GetSingle();
                break;
            default:
                reader.Skip();
                break;
        }
    }

    protected override void WriteKeyframeValues(Utf8JsonWriter writer, Vector2 value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.X);
        writer.WriteNumberValue(value.Y);
    }
}