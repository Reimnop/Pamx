using System.Text.Json;
using Pamx.Neo.Editor;

namespace Pamx.Neo.Serialization.Converters.Editor;

public class ObjectTimelineColorConverter : JsonObjectConverter<ObjectTimelineColor>
{
    private static readonly JsonEncodedText RedProperty = JsonEncodedText.Encode("r");
    private static readonly JsonEncodedText GreenProperty = JsonEncodedText.Encode("g");
    private static readonly JsonEncodedText BlueProperty = JsonEncodedText.Encode("b");

    private static ReadOnlySpan<byte> RedKey => "r"u8;
    private static ReadOnlySpan<byte> GreenKey => "g"u8;
    private static ReadOnlySpan<byte> BlueKey => "b"u8;

    protected override ObjectTimelineColor GetDefaultValue() => ObjectTimelineColor.None;

    protected override bool TryReadProperties(ref Utf8JsonReader reader,
        ref ObjectTimelineColor value,
        JsonSerializerOptions options)
    {
        if (reader.ValueTextEquals(RedKey))
        {
            reader.Read();
            if (reader.GetBoolean())
                value |= ObjectTimelineColor.Red;
            return true;
        }
        
        if (reader.ValueTextEquals(GreenKey))
        {
            reader.Read();
            if (reader.GetBoolean())
                value |= ObjectTimelineColor.Green;
            return true;
        }
        
        if (reader.ValueTextEquals(BlueKey))
        {
            reader.Read();
            if (reader.GetBoolean())
                value |= ObjectTimelineColor.Blue;
            return true;
        }

        return false;
    }

    protected override void WriteProperties(Utf8JsonWriter writer,
        ObjectTimelineColor value,
        JsonSerializerOptions options)
    {
        if ((value & ObjectTimelineColor.Red) == ObjectTimelineColor.Red)
            writer.WriteBoolean(RedProperty, true);
        if ((value & ObjectTimelineColor.Green) == ObjectTimelineColor.Green)
            writer.WriteBoolean(GreenProperty, true);
        if ((value & ObjectTimelineColor.Blue) == ObjectTimelineColor.Blue)
            writer.WriteBoolean(BlueProperty, true);
    }
}