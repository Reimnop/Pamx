using System.Numerics;
using System.Text.Json;
using Pamx.Neo.Keyframes;
using Pamx.Neo.Legacy.Converters.Keyframes;
using Pamx.Neo.Serialization.Extensions;

namespace Pamx.Neo.Legacy.Converters.Objects;

internal sealed class LegacyRandomVector2KeyframeConverter : LegacyRandomKeyframeConverter<Vector2>
{
    private static ReadOnlySpan<byte> XKey => "x"u8;
    private static ReadOnlySpan<byte> YKey => "y"u8;
    private static ReadOnlySpan<byte> RandomXKey => "rx"u8;
    private static ReadOnlySpan<byte> RandomYKey => "ry"u8;
    
    protected override Vector2 GetDefaultValue() => Vector2.Zero;

    protected override RandomKeyframe<Vector2> CreateKeyframe(Vector2 value, float time, Ease ease,
        RandomMode randomMode, float randomInterval, Vector2 randomValue) =>
        new(value, time, ease)
        {
            RandomMode = randomMode,
            RandomInterval = randomInterval,
            RandomValue = randomValue
        };

    protected override bool TryReadValues(ref Utf8JsonReader reader, ref Vector2 value, ref Vector2 randomValue,
        JsonSerializerOptions options)
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
        
        if (reader.ValueTextEquals(RandomXKey))
        {
            reader.Read();
            randomValue.X = reader.GetSingleLike();
            return true;
        }

        if (reader.ValueTextEquals(RandomYKey))
        {
            reader.Read();
            randomValue.Y = reader.GetSingleLike();
            return true;
        }

        return false;
    }
}