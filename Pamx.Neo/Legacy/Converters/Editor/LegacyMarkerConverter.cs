using System.Text.Json;
using Pamx.Neo.Editor;
using Pamx.Neo.Serialization.Extensions;

namespace Pamx.Neo.Legacy.Converters.Editor;

internal sealed class LegacyMarkerConverter : ReadonlyJsonObjectConverter<Marker>
{
    private static ReadOnlySpan<byte> NameKey => "name"u8;
    private static ReadOnlySpan<byte> DescriptionKey => "desc"u8;
    private static ReadOnlySpan<byte> ColorKey => "col"u8;
    private static ReadOnlySpan<byte> TimeKey => "t"u8;

    protected override Marker GetDefaultValue() => new();

    protected override bool TryReadProperties(ref Utf8JsonReader reader, ref Marker value,
        JsonSerializerOptions options)
    {
        if (reader.ValueTextEquals(NameKey))
        {
            reader.Read();
            value.Name = reader.GetString() ?? string.Empty;
            return true;
        }
        
        if (reader.ValueTextEquals(DescriptionKey))
        {
            reader.Read();
            value.Description = reader.GetString() ?? string.Empty;
            return true;
        }
        
        if (reader.ValueTextEquals(ColorKey))
        {
            reader.Read();
            value.Color = reader.GetInt32Like();
            return true;
        }

        if (reader.ValueTextEquals(TimeKey))
        {
            reader.Read();
            value.Time = reader.GetSingleLike();
            return true;
        }

        return false;
    }
}