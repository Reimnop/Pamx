using System.Numerics;
using System.Text.Json;
using Pamx.Serialization.Extensions;

namespace Pamx.Legacy.Converters;

internal sealed class LegacyBackgroundObjectConverter : ReadonlyJsonObjectConverter<BackgroundObject>
{
    private static ReadOnlySpan<byte> IsActiveKey => "active"u8;
    private static ReadOnlySpan<byte> NameKey => "name"u8;
    private static ReadOnlySpan<byte> PositionKey => "pos"u8;
    private static ReadOnlySpan<byte> ScaleKey => "size"u8;
    private static ReadOnlySpan<byte> RotationKey => "rot"u8;
    private static ReadOnlySpan<byte> ColorKey => "color"u8;
    private static ReadOnlySpan<byte> DepthKey => "layer"u8;
    private static ReadOnlySpan<byte> IsFadingKey => "fade"u8;
    private static ReadOnlySpan<byte> ReactivityKey => "r_set"u8;
    private static ReadOnlySpan<byte> ReactivityTypeKey => "type"u8;
    private static ReadOnlySpan<byte> ReactivityBassKey => "LOW"u8;
    private static ReadOnlySpan<byte> ReactivityMidKey => "MID"u8;
    private static ReadOnlySpan<byte> ReactivityTrebleKey => "HIGH"u8;
    private static ReadOnlySpan<byte> ReactivityScaleKey => "scale"u8;

    protected override BackgroundObject GetDefaultValue() => new();

    protected override bool TryReadProperties(ref Utf8JsonReader reader, ref BackgroundObject value,
        JsonSerializerOptions options)
    {
        if (reader.ValueTextEquals(IsActiveKey))
        {
            reader.Read();
            value.IsActive = reader.GetBooleanLike();
            return true;
        }

        if (reader.ValueTextEquals(NameKey))
        {
            reader.Read();
            value.Name = reader.GetString() ?? string.Empty;
            return true;
        }

        if (reader.ValueTextEquals(PositionKey))
        {
            reader.Read();
            value.Position = JsonSerializer.Deserialize<Vector2>(ref reader, options);
            return true;
        }

        if (reader.ValueTextEquals(ScaleKey))
        {
            reader.Read();
            value.Scale = JsonSerializer.Deserialize<Vector2>(ref reader, options);
            return true;
        }

        if (reader.ValueTextEquals(RotationKey))
        {
            reader.Read();
            value.Rotation = reader.GetSingleLike();
            return true;
        }

        if (reader.ValueTextEquals(ColorKey))
        {
            reader.Read();
            value.Color = reader.GetInt32Like();
            return true;
        }

        if (reader.ValueTextEquals(DepthKey))
        {
            reader.Read();
            value.Depth = reader.GetInt32Like();
            return true;
        }

        if (reader.ValueTextEquals(IsFadingKey))
        {
            reader.Read();
            value.IsFading = reader.GetBooleanLike();
            return true;
        }

        if (reader.ValueTextEquals(ReactivityKey))
        {
            reader.Read();
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException("Expected StartObject token");

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                    continue;

                if (reader.ValueTextEquals(ReactivityTypeKey))
                {
                    reader.Read();
                    if (reader.TokenType != JsonTokenType.String)
                        throw new JsonException("Expected String token");

                    if (reader.ValueTextEquals(ReactivityBassKey))
                        value.ReactivityType = BackgroundObjectReactivityType.Bass;
                    else if (reader.ValueTextEquals(ReactivityMidKey))
                        value.ReactivityType = BackgroundObjectReactivityType.Mid;
                    else if (reader.ValueTextEquals(ReactivityTrebleKey))
                        value.ReactivityType = BackgroundObjectReactivityType.Treble;
                    else
                        reader.Skip();
                }
                else if (reader.ValueTextEquals(ReactivityScaleKey))
                {
                    reader.Read();
                    value.ReactiveScale = reader.GetSingleLike();
                }
                else
                    reader.TrySkip();
            }

            return true;
        }

        return false;
    }
}