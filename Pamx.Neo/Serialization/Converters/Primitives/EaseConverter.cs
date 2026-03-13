using System.Text.Json;
using System.Text.Json.Serialization;
using Pamx.Neo.Keyframes;

namespace Pamx.Neo.Serialization.Converters.Primitives;

internal sealed class EaseConverter : JsonConverter<Ease>
{
    private static readonly JsonEncodedText LinearProperty = JsonEncodedText.Encode("Linear");
    private static readonly JsonEncodedText InstantProperty = JsonEncodedText.Encode("Instant");
    private static readonly JsonEncodedText InSineProperty = JsonEncodedText.Encode("InSine");
    private static readonly JsonEncodedText OutSineProperty = JsonEncodedText.Encode("OutSine");
    private static readonly JsonEncodedText InOutSineProperty = JsonEncodedText.Encode("InOutSine");
    private static readonly JsonEncodedText InElasticProperty = JsonEncodedText.Encode("InElastic");
    private static readonly JsonEncodedText OutElasticProperty = JsonEncodedText.Encode("OutElastic");
    private static readonly JsonEncodedText InOutElasticProperty = JsonEncodedText.Encode("InOutElastic");
    private static readonly JsonEncodedText InBackProperty = JsonEncodedText.Encode("InBack");
    private static readonly JsonEncodedText OutBackProperty = JsonEncodedText.Encode("OutBack");
    private static readonly JsonEncodedText InOutBackProperty = JsonEncodedText.Encode("InOutBack");
    private static readonly JsonEncodedText InBounceProperty = JsonEncodedText.Encode("InBounce");
    private static readonly JsonEncodedText OutBounceProperty = JsonEncodedText.Encode("OutBounce");
    private static readonly JsonEncodedText InOutBounceProperty = JsonEncodedText.Encode("InOutBounce");
    private static readonly JsonEncodedText InQuadProperty = JsonEncodedText.Encode("InQuad");
    private static readonly JsonEncodedText OutQuadProperty = JsonEncodedText.Encode("OutQuad");
    private static readonly JsonEncodedText InOutQuadProperty = JsonEncodedText.Encode("InOutQuad");
    private static readonly JsonEncodedText InCircProperty = JsonEncodedText.Encode("InCirc");
    private static readonly JsonEncodedText OutCircProperty = JsonEncodedText.Encode("OutCirc");
    private static readonly JsonEncodedText InOutCircProperty = JsonEncodedText.Encode("InOutCirc");
    private static readonly JsonEncodedText InExpoProperty = JsonEncodedText.Encode("InExpo");
    private static readonly JsonEncodedText OutExpoProperty = JsonEncodedText.Encode("OutExpo");
    private static readonly JsonEncodedText InOutExpoProperty = JsonEncodedText.Encode("InOutExpo");

    private static ReadOnlySpan<byte> LinearKey => "Linear"u8;
    private static ReadOnlySpan<byte> InstantKey => "Instant"u8;
    private static ReadOnlySpan<byte> InSineKey => "InSine"u8;
    private static ReadOnlySpan<byte> OutSineKey => "OutSine"u8;
    private static ReadOnlySpan<byte> InOutSineKey => "InOutSine"u8;
    private static ReadOnlySpan<byte> InElasticKey => "InElastic"u8;
    private static ReadOnlySpan<byte> OutElasticKey => "OutElastic"u8;
    private static ReadOnlySpan<byte> InOutElasticKey => "InOutElastic"u8;
    private static ReadOnlySpan<byte> InBackKey => "InBack"u8;
    private static ReadOnlySpan<byte> OutBackKey => "OutBack"u8;
    private static ReadOnlySpan<byte> InOutBackKey => "InOutBack"u8;
    private static ReadOnlySpan<byte> InBounceKey => "InBounce"u8;
    private static ReadOnlySpan<byte> OutBounceKey => "OutBounce"u8;
    private static ReadOnlySpan<byte> InOutBounceKey => "InOutBounce"u8;
    private static ReadOnlySpan<byte> InQuadKey => "InQuad"u8;
    private static ReadOnlySpan<byte> OutQuadKey => "OutQuad"u8;
    private static ReadOnlySpan<byte> InOutQuadKey => "InOutQuad"u8;
    private static ReadOnlySpan<byte> InCircKey => "InCirc"u8;
    private static ReadOnlySpan<byte> OutCircKey => "OutCirc"u8;
    private static ReadOnlySpan<byte> InOutCircKey => "InOutCirc"u8;
    private static ReadOnlySpan<byte> InExpoKey => "InExpo"u8;
    private static ReadOnlySpan<byte> OutExpoKey => "OutExpo"u8;
    private static ReadOnlySpan<byte> InOutExpoKey => "InOutExpo"u8;

    public override Ease Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.ValueTextEquals(LinearKey)) return Ease.Linear;
        if (reader.ValueTextEquals(InstantKey)) return Ease.Instant;
        if (reader.ValueTextEquals(InSineKey)) return Ease.InSine;
        if (reader.ValueTextEquals(OutSineKey)) return Ease.OutSine;
        if (reader.ValueTextEquals(InOutSineKey)) return Ease.InOutSine;
        if (reader.ValueTextEquals(InElasticKey)) return Ease.InElastic;
        if (reader.ValueTextEquals(OutElasticKey)) return Ease.OutElastic;
        if (reader.ValueTextEquals(InOutElasticKey)) return Ease.InOutElastic;
        if (reader.ValueTextEquals(InBackKey)) return Ease.InBack;
        if (reader.ValueTextEquals(OutBackKey)) return Ease.OutBack;
        if (reader.ValueTextEquals(InOutBackKey)) return Ease.InOutBack;
        if (reader.ValueTextEquals(InBounceKey)) return Ease.InBounce;
        if (reader.ValueTextEquals(OutBounceKey)) return Ease.OutBounce;
        if (reader.ValueTextEquals(InOutBounceKey)) return Ease.InOutBounce;
        if (reader.ValueTextEquals(InQuadKey)) return Ease.InQuad;
        if (reader.ValueTextEquals(OutQuadKey)) return Ease.OutQuad;
        if (reader.ValueTextEquals(InOutQuadKey)) return Ease.InOutQuad;
        if (reader.ValueTextEquals(InCircKey)) return Ease.InCirc;
        if (reader.ValueTextEquals(OutCircKey)) return Ease.OutCirc;
        if (reader.ValueTextEquals(InOutCircKey)) return Ease.InOutCirc;
        if (reader.ValueTextEquals(InExpoKey)) return Ease.InExpo;
        if (reader.ValueTextEquals(OutExpoKey)) return Ease.OutExpo;
        if (reader.ValueTextEquals(InOutExpoKey)) return Ease.InOutExpo;

        throw new JsonException("Invalid easing value");
    }

    public override void Write(Utf8JsonWriter writer, Ease value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value switch
        {
            Ease.Linear => LinearProperty,
            Ease.Instant => InstantProperty,
            Ease.InSine => InSineProperty,
            Ease.OutSine => OutSineProperty,
            Ease.InOutSine => InOutSineProperty,
            Ease.InElastic => InElasticProperty,
            Ease.OutElastic => OutElasticProperty,
            Ease.InOutElastic => InOutElasticProperty,
            Ease.InBack => InBackProperty,
            Ease.OutBack => OutBackProperty,
            Ease.InOutBack => InOutBackProperty,
            Ease.InBounce => InBounceProperty,
            Ease.OutBounce => OutBounceProperty,
            Ease.InOutBounce => InOutBounceProperty,
            Ease.InQuad => InQuadProperty,
            Ease.OutQuad => OutQuadProperty,
            Ease.InOutQuad => InOutQuadProperty,
            Ease.InCirc => InCircProperty,
            Ease.OutCirc => OutCircProperty,
            Ease.InOutCirc => InOutCircProperty,
            Ease.InExpo => InExpoProperty,
            Ease.OutExpo => OutExpoProperty,
            Ease.InOutExpo => InOutExpoProperty,
            _ => throw new ArgumentOutOfRangeException(nameof(value))
        });
}