using System.Text.Json;

namespace Pamx.Serialization.Converters.Keyframes;

internal sealed class FixedFloatKeyframeConverter : FixedKeyframeConverter<float>
{
    protected override float GetDefaultKeyframeValue() => 0.0f;

    protected override void ReadKeyframeValue(ref Utf8JsonReader reader, ref float value, int index,
        JsonSerializerOptions options)
    {
        if (index == 0)
            value = reader.GetSingle();
        else
            reader.Skip();
    }

    protected override void WriteKeyframeValues(Utf8JsonWriter writer, float value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}