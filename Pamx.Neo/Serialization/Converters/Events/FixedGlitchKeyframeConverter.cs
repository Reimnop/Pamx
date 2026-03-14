using System.Text.Json;
using Pamx.Neo.Events;
using Pamx.Neo.Serialization.Converters.Keyframes;

namespace Pamx.Neo.Serialization.Converters.Events;

internal sealed class FixedGlitchKeyframeConverter : FixedKeyframeConverter<GlitchValue>
{
    protected override GlitchValue GetDefaultKeyframeValue() => GlitchValue.Zero;

    protected override void ReadKeyframeValue(ref Utf8JsonReader reader, ref GlitchValue value, int index,
        JsonSerializerOptions options)
    {
        switch (index)
        {
            case 0:
                value.Intensity = reader.GetSingle();
                break;
            case 1:
                value.Width = reader.GetSingle();
                break;
            case 2:
                value.Speed = reader.GetSingle();
                break;
            default:
                reader.Skip();
                break;
        }
    }

    protected override void WriteKeyframeValues(Utf8JsonWriter writer, GlitchValue value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.Intensity);
        writer.WriteNumberValue(value.Width);
        writer.WriteNumberValue(value.Speed);
    }
}