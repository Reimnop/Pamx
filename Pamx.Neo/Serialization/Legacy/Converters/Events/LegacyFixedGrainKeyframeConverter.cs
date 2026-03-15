using System.Text.Json;
using Pamx.Neo.Events;
using Pamx.Neo.Keyframes;
using Pamx.Neo.Serialization.Extensions;
using Pamx.Neo.Serialization.Legacy.Converters.Keyframes;

namespace Pamx.Neo.Serialization.Legacy.Converters.Events;

internal sealed class LegacyFixedGrainKeyframeConverter : LegacyFixedKeyframeConverter<GrainValue>
{
    private static ReadOnlySpan<byte> IntensityKey => "x"u8;
    private static ReadOnlySpan<byte> IsColoredKey => "y"u8;
    private static ReadOnlySpan<byte> SizeKey => "z"u8;

    protected override GrainValue GetDefaultValue() => GrainValue.Zero;

    protected override FixedKeyframe<GrainValue> CreateKeyframe(GrainValue value, float time, Ease ease) =>
        new(value, time, ease);

    protected override bool TryReadValues(ref Utf8JsonReader reader, ref GrainValue value,
        JsonSerializerOptions options)
    {
        if (reader.ValueTextEquals(IntensityKey))
        {
            reader.Read();
            value.Intensity = reader.GetSingleLike();
            return true;
        }

        if (reader.ValueTextEquals(IsColoredKey))
        {
            reader.Read();
            value.IsColored = reader.GetSingleLike() != 0.0f;
            return true;
        }

        if (reader.ValueTextEquals(SizeKey))
        {
            reader.Read();
            value.Size = reader.GetSingleLike();
            return true;
        }

        return false;
    }
}