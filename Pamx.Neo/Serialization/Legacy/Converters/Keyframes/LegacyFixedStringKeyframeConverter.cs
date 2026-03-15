using System.Text.Json;
using Pamx.Neo.Keyframes;
using Pamx.Neo.Serialization.Legacy.Extensions;

namespace Pamx.Neo.Serialization.Legacy.Converters.Keyframes;

internal sealed class LegacyFixedStringKeyframeConverter : LegacyFixedKeyframeConverter<string>
{
    private static ReadOnlySpan<byte> XKey => "x"u8;

    protected override string GetDefaultValue() => "0";

    protected override FixedKeyframe<string> CreateKeyframe(string value, float time, Ease ease) =>
        new(value, time, ease);

    protected override bool TryReadValues(ref Utf8JsonReader reader, ref string value, JsonSerializerOptions options)
    {
        if (reader.ValueTextEquals(XKey))
        {
            reader.Read();
            value = reader.GetString() ?? string.Empty;
            return true;
        }

        return false;
    }
}