using System.Drawing;
using System.Text.Json;
using Pamx.Serialization.Extensions;
using Pamx.Themes;

namespace Pamx.Legacy.Converters.Themes;

internal sealed class LegacyThemeConverter : ReadonlyJsonObjectConverter<BeatmapTheme>
{
    private static ReadOnlySpan<byte> IdKey => "id"u8;
    private static ReadOnlySpan<byte> NameKey => "name"u8;
    private static ReadOnlySpan<byte> BackgroundKey => "bg"u8;
    private static ReadOnlySpan<byte> GuiKey => "gui"u8;
    private static ReadOnlySpan<byte> PlayersKey => "players"u8;
    private static ReadOnlySpan<byte> ObjectsKey => "objs"u8;
    private static ReadOnlySpan<byte> BackgroundObjectsKey => "bgs"u8;

    protected override BeatmapTheme GetDefaultValue() => new();

    protected override bool TryReadProperties(ref Utf8JsonReader reader, ref BeatmapTheme value,
        JsonSerializerOptions options)
    {
        if (reader.ValueTextEquals(IdKey))
        {
            reader.Read();
            value.Id = reader.GetRawString() ?? string.Empty;
            return true;
        }

        if (reader.ValueTextEquals(NameKey))
        {
            reader.Read();
            value.Name = reader.GetString() ?? string.Empty;
            return true;
        }

        if (reader.ValueTextEquals(BackgroundKey))
        {
            reader.Read();
            value.Background = JsonSerializer.Deserialize<Color>(ref reader, options);
            return true;
        }

        if (reader.ValueTextEquals(GuiKey))
        {
            reader.Read();
            value.Gui = JsonSerializer.Deserialize<Color>(ref reader, options);
            return true;
        }

        if (reader.ValueTextEquals(PlayersKey))
        {
            reader.Read();
            var result = JsonSerializer.Deserialize<List<Color>>(ref reader, options);
            if (result is not null)
                value.Players = result;
            return true;
        }

        if (reader.ValueTextEquals(ObjectsKey))
        {
            reader.Read();
            var result = JsonSerializer.Deserialize<List<Color>>(ref reader, options);
            if (result is not null)
                value.Objects = result;
            return true;
        }

        if (reader.ValueTextEquals(BackgroundObjectsKey))
        {
            reader.Read();
            var result = JsonSerializer.Deserialize<List<Color>>(ref reader, options);
            if (result is not null)
                value.BackgroundObjects = result;
            return true;
        }

        return false;
    }
}