using System.Text.Json;
using Pamx.Neo.Objects;
using Pamx.Neo.Serialization.Converters.Keyframes;

namespace Pamx.Neo.Serialization.Converters.Objects;

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