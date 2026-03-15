using System.Numerics;
using System.Text.Json;
using Pamx.Neo.Serialization.Legacy.Extensions;

namespace Pamx.Neo.Serialization.Legacy.Converters.Primitives;

internal sealed class LegacyVector2Converter : ReadonlyJsonObjectConverter<Vector2>
{
    private static ReadOnlySpan<byte> XKey => "x"u8;
    private static ReadOnlySpan<byte> YKey => "y"u8;

    protected override Vector2 GetDefaultValue() => Vector2.Zero;

    protected override bool TryReadProperties(ref Utf8JsonReader reader, ref Vector2 value, JsonSerializerOptions options)
    {
        if (reader.ValueTextEquals(XKey))
        {
            reader.Read();
            value.X = reader.GetSingleLike();
            return true;
        }

        if (reader.ValueTextEquals(YKey))
        {
            reader.Read();
            value.Y = reader.GetSingleLike();
            return true;
        }

        return false;
    }
}