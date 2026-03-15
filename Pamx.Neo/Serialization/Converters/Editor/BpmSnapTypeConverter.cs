using System.Text.Json;
using Pamx.Neo.Editor;

namespace Pamx.Neo.Serialization.Converters.Editor;

public sealed class BpmSnapTypeConverter : JsonObjectConverter<BpmSnapType>
{
    private static readonly JsonEncodedText ObjectsProperty = JsonEncodedText.Encode("objects");
    private static readonly JsonEncodedText ObjectKeyframesProperty = JsonEncodedText.Encode("object_keyframes");
    private static readonly JsonEncodedText CheckpointsProperty = JsonEncodedText.Encode("checkpoints");
    private static readonly JsonEncodedText EventsProperty = JsonEncodedText.Encode("events");

    private static ReadOnlySpan<byte> ObjectsKey => "objects"u8;
    private static ReadOnlySpan<byte> ObjectKeyframesKey => "object_keyframes"u8;
    private static ReadOnlySpan<byte> CheckpointsKey => "checkpoints"u8;
    private static ReadOnlySpan<byte> EventsKey => "events"u8;

    protected override BpmSnapType GetDefaultValue() => BpmSnapType.None;

    protected override bool TryReadProperties(ref Utf8JsonReader reader, ref BpmSnapType value,
        JsonSerializerOptions options)
    {
        if (reader.ValueTextEquals(ObjectsKey))
        {
            reader.Read();
            if (reader.GetBoolean())
                value |= BpmSnapType.Objects;
            return true;
        }

        if (reader.ValueTextEquals(ObjectKeyframesKey))
        {
            reader.Read();
            if (reader.GetBoolean())
                value |= BpmSnapType.ObjectKeyframes;
            return true;
        }

        if (reader.ValueTextEquals(CheckpointsKey))
        {
            reader.Read();
            if (reader.GetBoolean())
                value |= BpmSnapType.Checkpoints;
            return true;
        }

        if (reader.ValueTextEquals(EventsKey))
        {
            reader.Read();
            if (reader.GetBoolean())
                value |= BpmSnapType.Events;
            return true;
        }

        return false;
    }

    protected override void WriteProperties(Utf8JsonWriter writer, BpmSnapType value, JsonSerializerOptions options)
    {
        if ((value & BpmSnapType.Objects) == BpmSnapType.Objects)
            writer.WriteBoolean(ObjectsProperty, true);
        if ((value & BpmSnapType.ObjectKeyframes) == BpmSnapType.ObjectKeyframes)
            writer.WriteBoolean(ObjectKeyframesProperty, true);
        if ((value & BpmSnapType.Checkpoints) == BpmSnapType.Checkpoints)
            writer.WriteBoolean(CheckpointsProperty, true);
        if ((value & BpmSnapType.Events) == BpmSnapType.Events)
            writer.WriteBoolean(EventsProperty, true);
    }
}