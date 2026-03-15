using System.Text.Json;
using Pamx.Events;
using Pamx.Serialization.Converters.Keyframes;

namespace Pamx.Serialization.Converters.Events;

internal sealed class FixedGrainKeyframeConverter : FixedKeyframeConverter<GrainValue>
{
    protected override GrainValue GetDefaultKeyframeValue() => GrainValue.Zero;

    protected override void ReadKeyframeValue(ref Utf8JsonReader reader, ref GrainValue value, int index,
        JsonSerializerOptions options)
    {
        switch (index)
        {
            case 0:
                value.Intensity = reader.GetSingle();
                break;
            case 1:
                value.Size = reader.GetSingle();
                break;
            case 2:
                value.Mix = reader.GetSingle();
                break;
            default:
                reader.Skip();
                break;
        }
    }

    protected override void WriteKeyframeValues(Utf8JsonWriter writer, GrainValue value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.Intensity);
        writer.WriteNumberValue(value.Size);
        writer.WriteNumberValue(value.Mix);
    }
}