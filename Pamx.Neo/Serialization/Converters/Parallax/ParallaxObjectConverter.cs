using System.Numerics;
using System.Text.Json;
using Pamx.Neo.Objects;
using Pamx.Neo.Parallax;

namespace Pamx.Neo.Serialization.Converters.Parallax;

public sealed class ParallaxObjectConverter : JsonObjectConverter<ParallaxObject>
{
    protected override ParallaxObject GetDefaultValue() => new();

    protected override bool TryReadProperties(ref Utf8JsonReader reader, ref ParallaxObject value,
        JsonSerializerOptions options)
    {
        if (reader.ValueTextEquals("id"u8))
        {
            reader.Read();
            value.Id = reader.GetString() ?? string.Empty;
            return true;
        }

        if (reader.ValueTextEquals("t"u8))
        {
            reader.Read();
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException("Expected StartObject token");

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                    continue;

                if (reader.ValueTextEquals("p"u8))
                {
                    reader.Read();
                    value.Position = JsonSerializer.Deserialize<Vector2>(ref reader, options);
                }
                else if (reader.ValueTextEquals("s"u8))
                {
                    reader.Read();
                    value.Scale = JsonSerializer.Deserialize<Vector2>(ref reader, options);
                }
                else if (reader.ValueTextEquals("r"u8))
                {
                    reader.Read();
                    value.Rotation = reader.GetSingle();
                }
                else
                    reader.TrySkip();
            }

            return true;
        }

        if (reader.ValueTextEquals("an"u8))
        {
            reader.Read();
            var result = JsonSerializer.Deserialize<ParallaxObjectAnimation>(ref reader, options);
            if (result is not null)
                value.Animation = result;
            return true;
        }

        if (reader.ValueTextEquals("s"u8))
        {
            reader.Read();
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException("Expected StartObject token");

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                    continue;

                if (reader.ValueTextEquals("s"u8))
                {
                    reader.Read();
                    value.Shape = ObjectShapeHelper.FromSeparate(reader.GetInt32(), value.Shape.GetShapeOption());
                }
                else if (reader.ValueTextEquals("so"u8))
                {
                    reader.Read();
                    value.Shape = ObjectShapeHelper.FromSeparate(value.Shape.GetShape(), reader.GetInt32());
                }
                else if (reader.ValueTextEquals("t"u8))
                {
                    reader.Read();
                    value.Text = reader.GetString() ?? string.Empty;
                }
                else if (reader.ValueTextEquals("gt"u8))
                {
                    reader.Read();
                    value.Gradient.Type = (GradientType)reader.GetInt32();
                }
                else if (reader.ValueTextEquals("gr"u8))
                {
                    reader.Read();
                    value.Gradient.Rotation = reader.GetSingle();
                }
                else if (reader.ValueTextEquals("gs"u8))
                {
                    reader.Read();
                    value.Gradient.Scale = reader.GetSingle();
                }
                else
                    reader.TrySkip();
            }

            return true;
        }

        if (reader.ValueTextEquals("c"u8))
        {
            reader.Read();
            value.Color = reader.GetInt32();
            return true;
        }

        return false;
    }

    protected override void WriteProperties(Utf8JsonWriter writer, ParallaxObject value, JsonSerializerOptions options)
    {
        writer.WriteString("id"u8, value.Id);

        writer.WritePropertyName("t"u8);
        writer.WriteStartObject();

        if (value.Position != Vector2.Zero)
        {
            writer.WritePropertyName("p"u8);
            JsonSerializer.Serialize(writer, value.Position, options);
        }

        if (value.Scale != Vector2.One)
        {
            writer.WritePropertyName("s"u8);
            JsonSerializer.Serialize(writer, value.Scale, options);
        }

        if (value.Rotation != 0.0f)
            writer.WriteNumber("r"u8, value.Rotation);

        writer.WriteEndObject();

        writer.WritePropertyName("an"u8);
        JsonSerializer.Serialize(writer, value.Animation, options);

        writer.WritePropertyName("s"u8);
        writer.WriteStartObject();

        value.Shape.ToSeparate(out var shape, out var shapeOption);
        if (shape != 0)
            writer.WriteNumber("s"u8, shape);
        if (shapeOption != 0)
            writer.WriteNumber("so"u8, shapeOption);
        if (!string.IsNullOrEmpty(value.Text))
            writer.WriteString("t"u8, value.Text);

        if (value.Gradient.Type != GradientType.None)
            writer.WriteNumber("gt"u8, (int)value.Gradient.Type);
        writer.WriteNumber("gr"u8, value.Gradient.Rotation);
        writer.WriteNumber("gs"u8, value.Gradient.Scale);

        writer.WriteEndObject();

        if (value.Color != 0)
            writer.WriteNumber("c"u8, value.Color);
    }
}