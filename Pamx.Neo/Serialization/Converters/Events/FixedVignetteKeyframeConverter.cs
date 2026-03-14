using System.Text.Json;
using Pamx.Neo.Events;
using Pamx.Neo.Serialization.Converters.Keyframes;

namespace Pamx.Neo.Serialization.Converters.Events;

internal sealed class FixedVignetteKeyframeConverter : FixedKeyframeConverter<VignetteValue>
{
    protected override VignetteValue GetDefaultKeyframeValue() => VignetteValue.Zero;

    protected override void ReadKeyframeValue(ref Utf8JsonReader reader, ref VignetteValue value, int index,
        JsonSerializerOptions options)
    {
        switch (index)
        {
            case 0:
                value.Intensity = reader.GetSingle();
                break;
            case 1:
                value.Smoothness = reader.GetSingle();
                break;
            case 2:
                value.IsRounded = reader.GetSingle() != 0.0f;
                break;
            case 4:
                value.Center = value.Center with { X = reader.GetSingle() };
                break;
            case 5:
                value.Center = value.Center with { Y = reader.GetSingle() };
                break;
            case 6:
                value.Color = (int)reader.GetSingle();
                break;
            default:
                reader.Skip();
                break;
        }
    }

    protected override void WriteKeyframeValues(Utf8JsonWriter writer, VignetteValue value,
        JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.Intensity);
        writer.WriteNumberValue(value.Smoothness);
        writer.WriteNumberValue(value.IsRounded ? 1.0f : 0.0f);
        writer.WriteNumberValue(0.0f);
        writer.WriteNumberValue(value.Center.X);
        writer.WriteNumberValue(value.Center.Y);
        writer.WriteNumberValue((float)value.Color);
    }
}