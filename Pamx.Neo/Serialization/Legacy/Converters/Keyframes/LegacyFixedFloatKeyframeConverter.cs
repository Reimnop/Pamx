using System.Text.Json;
using Pamx.Neo.Keyframes;
using Pamx.Neo.Serialization.Legacy.Extensions;

namespace Pamx.Neo.Serialization.Legacy.Converters.Keyframes;

internal sealed class LegacyFixedFloatKeyframeConverter : LegacyFixedKeyframeConverter<float>
{
    private static ReadOnlySpan<byte> ValueKey => "x"u8;

    protected override float GetDefaultValue() => 0.0f;

    protected override FixedKeyframe<float> CreateKeyframe(float value, float time, Ease ease) =>
        new(value, time, ease);

    protected override bool TryReadValues(ref Utf8JsonReader reader, ref float value, JsonSerializerOptions options)
    {
        if (reader.ValueTextEquals(ValueKey))
        {
            reader.Read();
            value = reader.GetSingleLike();
            return true;
        }

        return false;
    }
}