using System.Globalization;
using System.Numerics;
using System.Text.Json;
using Pamx.Keyframes;
using Pamx.Objects;
using Pamx.Serialization.Converters.Keyframes;

namespace Pamx.Serialization.Converters.Objects;

internal sealed class RandomVector2KeyframeConverter : KeyframeConverter<RandomKeyframe<Vector2>>
{
    protected override RandomKeyframe<Vector2> GetDefaultValue() => new(Vector2.Zero);

    protected override bool TryReadProperties(ref Utf8JsonReader reader, ref RandomKeyframe<Vector2> value,
        JsonSerializerOptions options)
    {
        if (base.TryReadProperties(ref reader, ref value, options))
            return true;

        if (reader.ValueTextEquals(KeyframeConverterConstants.ValuesKey))
        {
            reader.Read();
            if (reader.TokenType != JsonTokenType.StartArray)
                throw new JsonException("Expected StartArray token");

            float x = 0.0f, y = 0.0f;
            float? px = null, py = null;

            bool hasParticles = false;
            bool world = false, despawn = false, radial = false, hashi = false;
            float seconds = 0f, units = 0f, arc = 0f, radius = 0f, speed = 0f;
            
            var i = 0;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                switch (i)
                {
                    case 0:
                        x = reader.TokenType == JsonTokenType.String ? float.Parse(reader.GetString()!, CultureInfo.InvariantCulture) : reader.GetSingle();
                        break;
                    case 1:
                        y = reader.TokenType == JsonTokenType.String ? float.Parse(reader.GetString()!, CultureInfo.InvariantCulture) : reader.GetSingle();
                        break;
                    case 2:
                        px = reader.TokenType == JsonTokenType.String ? float.Parse(reader.GetString()!, CultureInfo.InvariantCulture) : reader.GetSingle();
                        break;
                    case 3:
                        py = reader.TokenType == JsonTokenType.String ? float.Parse(reader.GetString()!, CultureInfo.InvariantCulture) : reader.GetSingle();
                        break;
                    case 4:
                        hasParticles = true;
                        seconds = reader.GetSingle();
                        break;
                    case 5:
                        units = reader.GetSingle();
                        break;
                    case 6:
                        world = reader.GetInt32() == 1;
                        break;
                    case 7:
                        despawn = reader.GetInt32() == 1;
                        break;
                    case 8:
                        radial = reader.GetInt32() == 1;
                        break;
                    case 9:
                        arc = reader.GetSingle();
                        break;
                    case 10:
                        radius = reader.GetSingle();
                        break;
                    case 11:
                        speed = reader.GetSingle();
                        break;
                    case 12:
                        hashi = reader.GetInt32() == 1;
                        break;
                    default:
                        reader.Skip();
                        break;
                }

                i++;
            }

            value.Value = new Vector2(x, y);
            value.ParticleValue = new Vector2(px ?? 0, py ?? 0);

            if (hasParticles)
            {
                value.ParticlesParams = new ParticlesParams()
                {
                    ParticlesPerSecond = seconds,
                    ParticlesPerUnit = units,
                    World = world,
                    DespawnOnEnd = despawn,
                    Radial = radial,
                    RadialCircleArc = arc,
                    RadialCircleRadius = radius,
                    RadialStartSpeed = speed,
                    Hashi = hashi
                };
            }
            return true;
        }

        if (reader.ValueTextEquals(KeyframeConverterConstants.RandomModeKey))
        {
            reader.Read();
            value.RandomMode = (RandomMode)reader.GetInt32();
            return true;
        }

        if (reader.ValueTextEquals(KeyframeConverterConstants.RandomValuesKey))
        {
            reader.Read();
            if (reader.TokenType != JsonTokenType.StartArray)
                throw new JsonException("Expected StartArray token");

            float x = 0.0f, y = 0.0f, interval = 0.0f;
            var i = 0;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                switch (i)
                {
                    case 0:
                        x = reader.GetSingle();
                        break;
                    case 1:
                        y = reader.GetSingle();
                        break;
                    case 2:
                        interval = reader.GetSingle();
                        break;
                    default:
                        reader.Skip();
                        break;
                }

                i++;
            }

            value.RandomValue = new Vector2(x, y);
            value.RandomInterval = interval;
            return true;
        }

        return false;
    }

    protected override void WriteProperties(Utf8JsonWriter writer, RandomKeyframe<Vector2> value,
        JsonSerializerOptions options)
    {
        base.WriteProperties(writer, value, options);

        writer.WritePropertyName(KeyframeConverterConstants.ValuesKey);
        writer.WriteStartArray();
        writer.WriteNumberValue(value.Value.X);
        writer.WriteNumberValue(value.Value.Y);

        if (value.ParticleValue != Vector2.Zero || value.ParticlesParams != null) 
        {
            writer.WriteNumberValue(value.ParticleValue.X);
            writer.WriteNumberValue(value.ParticleValue.Y);
        }
        
        if (value.ParticlesParams != null)
        {
            writer.WriteNumberValue(value.ParticlesParams.ParticlesPerSecond);
            writer.WriteNumberValue(value.ParticlesParams.ParticlesPerUnit);
            writer.WriteNumberValue(value.ParticlesParams.World ? 1 : 0);
            writer.WriteNumberValue(value.ParticlesParams.DespawnOnEnd ? 1 : 0);
            writer.WriteNumberValue(value.ParticlesParams.Radial ? 1 : 0);
            writer.WriteNumberValue(value.ParticlesParams.RadialCircleArc);
            writer.WriteNumberValue(value.ParticlesParams.RadialCircleRadius);
            writer.WriteNumberValue(value.ParticlesParams.RadialStartSpeed);
            writer.WriteNumberValue(value.ParticlesParams.Hashi ? 1 : 0);
        }
        writer.WriteEndArray();

        if (value.RandomMode != RandomMode.None)
            writer.WriteNumber(KeyframeConverterConstants.RandomModeProperty, (int)value.RandomMode);
        // if (value.RandomValue == Vector2.Zero || value.RandomInterval == 0.0f)
            // return;

        writer.WritePropertyName(KeyframeConverterConstants.RandomValuesProperty);
        writer.WriteStartArray();
        writer.WriteNumberValue(value.RandomValue.X);
        writer.WriteNumberValue(value.RandomValue.Y);
        writer.WriteNumberValue(value.RandomInterval);
        writer.WriteEndArray();
    }
}