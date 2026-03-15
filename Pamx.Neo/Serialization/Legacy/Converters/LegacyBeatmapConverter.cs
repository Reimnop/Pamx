using System.Text.Json;
using Pamx.Neo.Editor;
using Pamx.Neo.Events;
using Pamx.Neo.Objects;
using Pamx.Neo.Prefabs;
using Pamx.Neo.Themes;

namespace Pamx.Neo.Serialization.Legacy.Converters;

internal sealed class LegacyBeatmapConverter : ReadonlyJsonObjectConverter<Beatmap>
{
    private static ReadOnlySpan<byte> EditorDataKey => "ed"u8;
    private static ReadOnlySpan<byte> MarkersKey => "markers"u8;
    private static ReadOnlySpan<byte> PrefabObjectsKey => "prefab_objects"u8;
    private static ReadOnlySpan<byte> PrefabsKey => "prefabs"u8;
    private static ReadOnlySpan<byte> ThemesKey => "themes"u8;
    private static ReadOnlySpan<byte> CheckpointsKey => "checkpoints"u8;
    private static ReadOnlySpan<byte> ObjectsKey => "beatmap_objects"u8;
    private static ReadOnlySpan<byte> BackgroundObjectsKey => "bg_objects"u8;
    private static ReadOnlySpan<byte> EventsKey => "events"u8;

    protected override Beatmap GetDefaultValue() => new();

    protected override bool TryReadProperties(ref Utf8JsonReader reader, ref Beatmap value,
        JsonSerializerOptions options)
    {
        if (reader.ValueTextEquals(EditorDataKey))
        {
            reader.Read();
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException("Expected StartObject token");

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                    continue;

                if (reader.ValueTextEquals(MarkersKey))
                {
                    reader.Read();
                    var result = JsonSerializer.Deserialize<List<Marker>>(ref reader, options);
                    if (result is not null)
                        value.Markers = result;
                }
                else
                    reader.TrySkip();
            }

            return true;
        }

        if (reader.ValueTextEquals(PrefabObjectsKey))
        {
            reader.Read();
            var result = JsonSerializer.Deserialize<List<PrefabObject>>(ref reader, options);
            if (result is not null)
                value.PrefabObjects = result;
            return true;
        }

        if (reader.ValueTextEquals(PrefabsKey))
        {
            reader.Read();
            var result = JsonSerializer.Deserialize<List<Prefab>>(ref reader, options);
            if (result is not null)
                value.Prefabs = result;
            return true;
        }

        if (reader.ValueTextEquals(ThemesKey))
        {
            reader.Read();
            var result = JsonSerializer.Deserialize<List<BeatmapTheme>>(ref reader, options);
            if (result is not null)
                value.Themes = result;
            return true;
        }

        if (reader.ValueTextEquals(CheckpointsKey))
        {
            reader.Read();
            var result = JsonSerializer.Deserialize<List<Checkpoint>>(ref reader, options);
            if (result is not null)
                value.Checkpoints = result;
            return true;
        }

        if (reader.ValueTextEquals(ObjectsKey))
        {
            reader.Read();
            var result = JsonSerializer.Deserialize<List<BeatmapObject>>(ref reader, options);
            if (result is not null)
                value.Objects = result;
            return true;
        }

        if (reader.ValueTextEquals(BackgroundObjectsKey))
        {
            reader.Read();
            var result = JsonSerializer.Deserialize<List<BackgroundObject>>(ref reader, options);
            if (result is not null)
                value.BackgroundObjects = result;
            return true;
        }

        if (reader.ValueTextEquals(EventsKey))
        {
            reader.Read();
            var result = JsonSerializer.Deserialize<BeatmapEvents>(ref reader, options);
            if (result is not null)
                value.Events = result;
            return true;
        }

        return false;
    }
}