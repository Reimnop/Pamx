using System.Text.Json;
using Pamx.Serialization.Extensions;
using Pamx.Objects;
using Pamx.Prefabs;

namespace Pamx.Legacy.Converters.Prefabs;

internal sealed class LegacyPrefabConverter : ReadonlyJsonObjectConverter<Prefab>
{
    private static ReadOnlySpan<byte> IdKey => "id"u8;
    private static ReadOnlySpan<byte> NameKey => "name"u8;
    private static ReadOnlySpan<byte> TypeKey => "type"u8;
    private static ReadOnlySpan<byte> OffsetKey => "offset"u8;
    private static ReadOnlySpan<byte> ObjectsKey => "objects"u8;

    protected override Prefab GetDefaultValue() => new();

    protected override bool TryReadProperties(ref Utf8JsonReader reader, ref Prefab value,
        JsonSerializerOptions options)
    {
        if (reader.ValueTextEquals(IdKey))
        {
            reader.Read();
            value.Id = reader.GetString() ?? string.Empty;
            return true;
        }

        if (reader.ValueTextEquals(NameKey))
        {
            reader.Read();
            value.Name = reader.GetString() ?? string.Empty;
            return true;
        }

        if (reader.ValueTextEquals(TypeKey))
        {
            reader.Read();
            value.Type = (PrefabType)reader.GetInt32Like();
            return true;
        }

        if (reader.ValueTextEquals(OffsetKey))
        {
            reader.Read();
            value.Offset = reader.GetSingleLike();
            return true;
        }

        if (reader.ValueTextEquals(ObjectsKey))
        {
            reader.Read();
            value.Objects = JsonSerializer.Deserialize<List<BeatmapObject>>(ref reader, options) ?? [];
            return true;
        }

        return false;
    }
}