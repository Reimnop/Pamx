using System.Text.Json;
using Pamx.Neo.Events;
using Pamx.Neo.Serialization.Converters.Keyframes;

namespace Pamx.Neo.Serialization.Converters.Events;

internal sealed class FixedGradientKeyframeConverter : FixedKeyframeConverter<GradientValue>
{
    protected override GradientValue GetDefaultKeyframeValue() => GradientValue.Zero;

    protected override void ReadKeyframeValue(ref Utf8JsonReader reader, ref GradientValue value, int index,
        JsonSerializerOptions options)
    {
        switch (index)
        {
            case 0:
                value.Intensity = reader.GetSingle();
                break;
            case 1:
                value.Rotation = reader.GetSingle();
                break;
            case 2:
                value.StartColor = (int)reader.GetSingle();
                break;
            case 3:
                value.EndColor = (int)reader.GetSingle();
                break;
            case 4:
                value.Mode = (GradientOverlayMode)reader.GetSingle();
                break;
            default:
                reader.Skip();
                break;
        }
    }

    protected override void WriteKeyframeValues(Utf8JsonWriter writer, GradientValue value,
        JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.Intensity);
        writer.WriteNumberValue(value.Rotation);
        writer.WriteNumberValue((float)value.StartColor);
        writer.WriteNumberValue((float)value.EndColor);
        writer.WriteNumberValue((float)value.Mode);
    }
}