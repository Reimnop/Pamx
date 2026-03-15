using System.Numerics;
using System.Text.Json;
using Pamx.Neo.Keyframes;
using Pamx.Neo.Serialization.Extensions;

namespace Pamx.Neo.Serialization.Legacy.Converters.Keyframes;

internal sealed class LegacyFixedVector2KeyframeConverter : LegacyFixedKeyframeConverter<Vector2>
{
    private static ReadOnlySpan<byte> XKey => "x"u8;
    private static ReadOnlySpan<byte> YKey => "y"u8;

    protected override Vector2 GetDefaultValue() => Vector2.Zero;

    protected override FixedKeyframe<Vector2> CreateKeyframe(Vector2 value, float time, Ease ease) =>
        new(value, time, ease);

    protected override bool TryReadValues(ref Utf8JsonReader reader, ref Vector2 value, JsonSerializerOptions options)
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