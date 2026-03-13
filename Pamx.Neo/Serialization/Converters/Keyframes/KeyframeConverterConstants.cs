using System.Text.Json;

namespace Pamx.Neo.Serialization.Converters.Keyframes;

internal static class KeyframeConverterConstants
{
    public static readonly JsonEncodedText TimeProperty = JsonEncodedText.Encode("t");
    public static readonly JsonEncodedText EaseProperty = JsonEncodedText.Encode("ct");
    public static readonly JsonEncodedText ValuesProperty = JsonEncodedText.Encode("ev");
    public static readonly JsonEncodedText StringValuesProperty = JsonEncodedText.Encode("evs");

    public static ReadOnlySpan<byte> TimeKey => "t"u8;
    public static ReadOnlySpan<byte> EaseKey => "ct"u8;
    public static ReadOnlySpan<byte> ValuesKey => "ev"u8;
    public static ReadOnlySpan<byte> StringValuesKey => "evs"u8;
}