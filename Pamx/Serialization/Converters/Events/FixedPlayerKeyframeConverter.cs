using System.Text.Json;
using Pamx.Events;
using Pamx.Serialization.Converters.Keyframes;

namespace Pamx.Serialization.Converters.Events;

internal sealed class FixedPlayerKeyframeConverter : FixedKeyframeConverter<PlayerValue>
{
    protected override PlayerValue GetDefaultKeyframeValue() => PlayerValue.Zero;

    protected override void ReadKeyframeValue(ref Utf8JsonReader reader, ref PlayerValue value, int index,
        JsonSerializerOptions options)
    {
        switch (index)
        {
            case 0:
                value.Force = value.Force with { X = reader.GetSingle() };
                break;
            case 1:
                value.Force = value.Force with { Y = reader.GetSingle() };
                break;
            case 2:
                value.DepthOfFieldMode = (PlayerDepthOfFieldMode) (int) MathF.Round(reader.GetSingle());
                break;
            case 3:
                value.DepthOfFieldFocus = reader.GetSingle();
                break;
            default:
                reader.Skip();
                break;
        }
    }

    protected override void WriteKeyframeValues(Utf8JsonWriter writer, PlayerValue value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.Force.X);
        writer.WriteNumberValue(value.Force.Y);
        writer.WriteNumberValue((int) value.DepthOfFieldMode);
        if (!float.IsNaN(value.DepthOfFieldFocus))
            writer.WriteNumberValue(value.DepthOfFieldFocus);
    }
}
