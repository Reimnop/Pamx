using System.Text.Json;
using Pamx.Objects;
using Pamx.Serialization.Converters.Keyframes;

namespace Pamx.Serialization.Converters.Objects;

internal sealed class FixedObjectColorKeyframeConverter : FixedKeyframeConverter<ObjectColorValue>
{
    protected override ObjectColorValue GetDefaultKeyframeValue() => ObjectColorValue.Default;

    protected override void ReadKeyframeValue(ref Utf8JsonReader reader, ref ObjectColorValue value, int index,
        JsonSerializerOptions options)
    {
        switch (index)
        {
            case 0:
                value.Index = (int)reader.GetSingle();
                break;
            case 1:
                value.Opacity = reader.GetSingle() / 100.0f;
                break;
            case 2:
                value.EndIndex = (int)reader.GetSingle();
                break;
            default:
                reader.Skip();
                break;
        }
    }

    protected override void WriteKeyframeValues(Utf8JsonWriter writer, ObjectColorValue value,
        JsonSerializerOptions options)
    {
        writer.WriteNumberValue((float)value.Index);
        writer.WriteNumberValue(value.Opacity * 100.0f);
        writer.WriteNumberValue((float)value.EndIndex);
    }
}