using System.Text.Json;
using Pamx.Neo.Editor;
using Pamx.Neo.Serialization.Extensions;

namespace Pamx.Neo.Serialization.Legacy.Converters.Editor;

internal sealed class LegacyObjectEditorSettingsConverter : ReadonlyJsonObjectConverter<ObjectEditorSettings>
{
    private static ReadOnlySpan<byte> IsLockedKey => "locked"u8;
    private static ReadOnlySpan<byte> IsCollapsedKey => "shrink"u8;
    private static ReadOnlySpan<byte> BinKey => "bin"u8;
    private static ReadOnlySpan<byte> LayerKey => "layer"u8;

    protected override ObjectEditorSettings GetDefaultValue() => new();

    protected override bool TryReadProperties(ref Utf8JsonReader reader, ref ObjectEditorSettings value,
        JsonSerializerOptions options)
    {
        if (reader.ValueTextEquals(IsLockedKey))
        {
            reader.Read();
            value.IsLocked = reader.GetBooleanLike();
            return true;
        }

        if (reader.ValueTextEquals(IsCollapsedKey))
        {
            reader.Read();
            value.IsCollapsed = reader.GetBooleanLike();
            return true;
        }

        if (reader.ValueTextEquals(BinKey))
        {
            reader.Read();
            value.Bin = reader.GetInt32Like();
            return true;
        }

        if (reader.ValueTextEquals(LayerKey))
        {
            reader.Read();
            value.Layer = reader.GetInt32Like();
            return true;
        }

        return false;
    }
}