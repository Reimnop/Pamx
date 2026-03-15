using System.Numerics;
using System.Text.Json;
using Pamx.Neo.Objects;
using Pamx.Neo.Parallax;

namespace Pamx.Neo.Serialization.Converters.Parallax;

public sealed class ParallaxObjectConverter : JsonObjectConverter<ParallaxObject>
{
    private static readonly JsonEncodedText IdProperty = JsonEncodedText.Encode("id");
    private static readonly JsonEncodedText TransformProperty = JsonEncodedText.Encode("t");
    private static readonly JsonEncodedText PositionProperty = JsonEncodedText.Encode("p");
    private static readonly JsonEncodedText ScaleProperty = JsonEncodedText.Encode("s");
    private static readonly JsonEncodedText RotationProperty = JsonEncodedText.Encode("r");
    private static readonly JsonEncodedText AnimationProperty = JsonEncodedText.Encode("an");
    private static readonly JsonEncodedText ShapeProperty = JsonEncodedText.Encode("s");
    private static readonly JsonEncodedText ShapeOptionProperty = JsonEncodedText.Encode("so");
    private static readonly JsonEncodedText TextProperty = JsonEncodedText.Encode("t");
    private static readonly JsonEncodedText GradientTypeProperty = JsonEncodedText.Encode("gt");
    private static readonly JsonEncodedText GradientRotationProperty = JsonEncodedText.Encode("gr");
    private static readonly JsonEncodedText GradientScaleProperty = JsonEncodedText.Encode("gs");
    private static readonly JsonEncodedText ColorProperty = JsonEncodedText.Encode("gs");

    private static ReadOnlySpan<byte> IdKey => "id"u8;
    private static ReadOnlySpan<byte> TransformKey => "t"u8;
    private static ReadOnlySpan<byte> PositionKey => "p"u8;
    private static ReadOnlySpan<byte> ScaleKey => "s"u8;
    private static ReadOnlySpan<byte> RotationKey => "r"u8;
    private static ReadOnlySpan<byte> AnimationKey => "an"u8;
    private static ReadOnlySpan<byte> ShapeKey => "s"u8;
    private static ReadOnlySpan<byte> ShapeOptionKey => "so"u8;
    private static ReadOnlySpan<byte> TextKey => "t"u8;
    private static ReadOnlySpan<byte> GradientTypeKey => "gt"u8;
    private static ReadOnlySpan<byte> GradientRotationKey => "gr"u8;
    private static ReadOnlySpan<byte> GradientScaleKey => "gs"u8;
    private static ReadOnlySpan<byte> ColorKey => "gs"u8;


    protected override ParallaxObject GetDefaultValue() => new();

    protected override bool TryReadProperties(ref Utf8JsonReader reader, ref ParallaxObject value,
        JsonSerializerOptions options)
    {
        if (reader.ValueTextEquals(IdKey))
        {
            reader.Read();
            value.Id = reader.GetString() ?? string.Empty;
            return true;
        }

        if (reader.ValueTextEquals(TransformKey))
        {
            reader.Read();
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException("Expected StartObject token");

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                    continue;

                if (reader.ValueTextEquals(PositionKey))
                {
                    reader.Read();
                    value.Position = JsonSerializer.Deserialize<Vector2>(ref reader, options);
                }
                else if (reader.ValueTextEquals(ScaleKey))
                {
                    reader.Read();
                    value.Scale = JsonSerializer.Deserialize<Vector2>(ref reader, options);
                }
                else if (reader.ValueTextEquals(RotationKey))
                {
                    reader.Read();
                    value.Rotation = reader.GetSingle();
                }
                else
                    reader.TrySkip();
            }

            return true;
        }

        if (reader.ValueTextEquals(AnimationKey))
        {
            reader.Read();
            var result = JsonSerializer.Deserialize<ParallaxObjectAnimation>(ref reader, options);
            if (result is not null)
                value.Animation = result;
            return true;
        }

        if (reader.ValueTextEquals(ShapeKey))
        {
            reader.Read();
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException("Expected StartObject token");

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                    continue;

                if (reader.ValueTextEquals(ShapeKey))
                {
                    reader.Read();
                    value.Shape = ObjectShapeHelper.FromSeparate(reader.GetInt32(), value.Shape.GetShapeOption());
                }
                else if (reader.ValueTextEquals(ShapeOptionKey))
                {
                    reader.Read();
                    value.Shape = ObjectShapeHelper.FromSeparate(value.Shape.GetShape(), reader.GetInt32());
                }
                else if (reader.ValueTextEquals(TextKey))
                {
                    reader.Read();
                    value.Text = reader.GetString() ?? string.Empty;
                }
                else if (reader.ValueTextEquals(GradientTypeKey))
                {
                    reader.Read();
                    value.Gradient.Type = (GradientType)reader.GetInt32();
                }
                else if (reader.ValueTextEquals(GradientRotationKey))
                {
                    reader.Read();
                    value.Gradient.Rotation = reader.GetSingle();
                }
                else if (reader.ValueTextEquals(GradientScaleKey))
                {
                    reader.Read();
                    value.Gradient.Scale = reader.GetSingle();
                }
                else
                    reader.TrySkip();
            }

            return true;
        }

        if (reader.ValueTextEquals(ColorKey))
        {
            reader.Read();
            value.Color = reader.GetInt32();
            return true;
        }

        return false;
    }

    protected override void WriteProperties(Utf8JsonWriter writer, ParallaxObject value, JsonSerializerOptions options)
    {
        writer.WriteString(IdProperty, value.Id);

        writer.WritePropertyName(TransformProperty);
        writer.WriteStartObject();

        if (value.Position != Vector2.Zero)
        {
            writer.WritePropertyName(PositionProperty);
            JsonSerializer.Serialize(writer, value.Position, options);
        }

        if (value.Scale != Vector2.One)
        {
            writer.WritePropertyName(ScaleProperty);
            JsonSerializer.Serialize(writer, value.Scale, options);
        }

        if (value.Rotation != 0.0f)
            writer.WriteNumber(RotationProperty, value.Rotation);

        writer.WriteEndObject();

        writer.WritePropertyName(AnimationProperty);
        JsonSerializer.Serialize(writer, value.Animation, options);

        writer.WritePropertyName(ShapeProperty);
        writer.WriteStartObject();

        value.Shape.ToSeparate(out var shape, out var shapeOption);
        if (shape != 0)
            writer.WriteNumber(ShapeProperty, shape);
        if (shapeOption != 0)
            writer.WriteNumber(ShapeOptionProperty, shapeOption);
        if (!string.IsNullOrEmpty(value.Text))
            writer.WriteString(TextProperty, value.Text);

        if (value.Gradient.Type != GradientType.None)
            writer.WriteNumber(GradientTypeProperty, (int)value.Gradient.Type);
        writer.WriteNumber(GradientRotationProperty, value.Gradient.Rotation);
        writer.WriteNumber(GradientScaleProperty, value.Gradient.Scale);

        writer.WriteEndObject();

        if (value.Color != 0)
            writer.WriteNumber(ColorProperty, value.Color);
    }
}