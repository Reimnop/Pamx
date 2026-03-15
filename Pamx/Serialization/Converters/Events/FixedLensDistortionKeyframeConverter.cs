using System.Text.Json;
using Pamx.Events;
using Pamx.Serialization.Converters.Keyframes;

namespace Pamx.Serialization.Converters.Events;

internal sealed class FixedLensDistortionKeyframeConverter : FixedKeyframeConverter<LensDistortionValue>
{
    protected override LensDistortionValue GetDefaultKeyframeValue() => LensDistortionValue.Zero;

    protected override void ReadKeyframeValue(ref Utf8JsonReader reader, ref LensDistortionValue value, int index,
        JsonSerializerOptions options)
    {
        switch (index)
        {
            case 0:
                value.Intensity = reader.GetSingle();
                break;
            case 1:
                value.Center = value.Center with { X = reader.GetSingle() };
                break;
            case 2:
                value.Center = value.Center with { Y = reader.GetSingle() };
                break;
            default:
                reader.Skip();
                break;
        }
    }

    protected override void WriteKeyframeValues(Utf8JsonWriter writer, LensDistortionValue value,
        JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.Intensity);
        writer.WriteNumberValue(value.Center.X);
        writer.WriteNumberValue(value.Center.Y);
    }
}