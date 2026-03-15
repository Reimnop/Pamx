using System.Text.Json;
using Pamx.Events;
using Pamx.Serialization.Converters.Keyframes;

namespace Pamx.Serialization.Converters.Events;

internal sealed class FixedBloomKeyframeConverter : FixedKeyframeConverter<BloomValue>
{
    protected override BloomValue GetDefaultKeyframeValue() => BloomValue.Zero;

    protected override void ReadKeyframeValue(ref Utf8JsonReader reader, ref BloomValue value, int index,
        JsonSerializerOptions options)
    {
        switch (index)
        {
            case 0:
                value.Intensity = reader.GetSingle();
                break;
            case 1:
                value.Diffusion = reader.GetSingle();
                break;
            case 2:
                value.Color = (int)reader.GetSingle();
                break;
            default:
                reader.Skip();
                break;
        }
    }

    protected override void WriteKeyframeValues(Utf8JsonWriter writer, BloomValue value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.Intensity);
        writer.WriteNumberValue(value.Diffusion);
        writer.WriteNumberValue((float)value.Color);
    }
}