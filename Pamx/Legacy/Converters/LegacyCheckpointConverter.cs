using System.Numerics;
using System.Text.Json;
using Pamx.Serialization.Extensions;

namespace Pamx.Legacy.Converters;

internal sealed class LegacyCheckpointConverter : ReadonlyJsonObjectConverter<Checkpoint>
{
    private static ReadOnlySpan<byte> NameKey => "name"u8;
    private static ReadOnlySpan<byte> TimeKey => "t"u8;
    private static ReadOnlySpan<byte> PositionKey => "pos"u8;
    
    protected override Checkpoint GetDefaultValue() => new();

    protected override bool TryReadProperties(ref Utf8JsonReader reader, ref Checkpoint value, JsonSerializerOptions options)
    {
        if (reader.ValueTextEquals(NameKey))
        {
            reader.Read();
            value.Name = reader.GetString() ?? string.Empty;
            return true;
        }

        if (reader.ValueTextEquals(TimeKey))
        {
            reader.Read();
            value.Time = reader.GetSingleLike();
            return true;
        }

        if (reader.ValueTextEquals(PositionKey))
        {
            reader.Read();
            value.Position = JsonSerializer.Deserialize<Vector2>(ref reader, options);
            return true;
        }

        return false;
    }
}