using System.Numerics;
using System.Text.Json;
using Pamx.Neo.Editor;
using Pamx.Neo.Objects;

namespace Pamx.Neo.Serialization.Converters.Objects;

internal sealed class BeatmapObjectConverter : JsonObjectConverter<BeatmapObject>
{
    private static readonly JsonEncodedText IdProperty = JsonEncodedText.Encode("id");
    private static readonly JsonEncodedText PrefabIdProperty = JsonEncodedText.Encode("pre_id");
    private static readonly JsonEncodedText PrefabInstanceIdProperty = JsonEncodedText.Encode("pre_iid");
    private static readonly JsonEncodedText ParentIdProperty = JsonEncodedText.Encode("p_id");
    private static readonly JsonEncodedText AutoKillTypeProperty = JsonEncodedText.Encode("ak_t");
    private static readonly JsonEncodedText AutoKillOffsetProperty = JsonEncodedText.Encode("ak_o");
    private static readonly JsonEncodedText ObjectTypeProperty = JsonEncodedText.Encode("ot");
    private static readonly JsonEncodedText NameProperty = JsonEncodedText.Encode("n");
    private static readonly JsonEncodedText TextProperty = JsonEncodedText.Encode("text");
    private static readonly JsonEncodedText OriginProperty = JsonEncodedText.Encode("o");
    private static readonly JsonEncodedText ShapeProperty = JsonEncodedText.Encode("s");
    private static readonly JsonEncodedText ShapeOptionProperty = JsonEncodedText.Encode("so");
    private static readonly JsonEncodedText CustomShapeParamsProperty = JsonEncodedText.Encode("csp");
    private static readonly JsonEncodedText GradientTypeProperty = JsonEncodedText.Encode("gt");
    private static readonly JsonEncodedText GradientRotationProperty = JsonEncodedText.Encode("gr");
    private static readonly JsonEncodedText GradientScaleProperty = JsonEncodedText.Encode("gs");
    private static readonly JsonEncodedText ParentTypeProperty = JsonEncodedText.Encode("p_t");
    private static readonly JsonEncodedText ParentOffsetProperty = JsonEncodedText.Encode("p_o");
    private static readonly JsonEncodedText RenderDepthProperty = JsonEncodedText.Encode("d");
    private static readonly JsonEncodedText StartTimeProperty = JsonEncodedText.Encode("st");
    private static readonly JsonEncodedText EditorSettingsProperty = JsonEncodedText.Encode("ed");
    private static readonly JsonEncodedText EventsProperty = JsonEncodedText.Encode("e");

    private static ReadOnlySpan<byte> IdKey => "id"u8;
    private static ReadOnlySpan<byte> PrefabIdKey => "pre_id"u8;
    private static ReadOnlySpan<byte> PrefabInstanceIdKey => "pre_iid"u8;
    private static ReadOnlySpan<byte> ParentIdKey => "p_id"u8;
    private static ReadOnlySpan<byte> AutoKillTypeKey => "ak_t"u8;
    private static ReadOnlySpan<byte> AutoKillOffsetKey => "ak_o"u8;
    private static ReadOnlySpan<byte> ObjectTypeKey => "ot"u8;
    private static ReadOnlySpan<byte> NameKey => "n"u8;
    private static ReadOnlySpan<byte> TextKey => "text"u8;
    private static ReadOnlySpan<byte> OriginKey => "o"u8;
    private static ReadOnlySpan<byte> ShapeKey => "s"u8;
    private static ReadOnlySpan<byte> ShapeOptionKey => "so"u8;
    private static ReadOnlySpan<byte> CustomShapeParamsKey => "csp"u8;
    private static ReadOnlySpan<byte> GradientTypeKey => "gt"u8;
    private static ReadOnlySpan<byte> GradientRotationKey => "gr"u8;
    private static ReadOnlySpan<byte> GradientScaleKey => "gs"u8;
    private static ReadOnlySpan<byte> ParentTypeKey => "p_t"u8;
    private static ReadOnlySpan<byte> ParentOffsetKey => "p_o"u8;
    private static ReadOnlySpan<byte> RenderDepthKey => "d"u8;
    private static ReadOnlySpan<byte> StartTimeKey => "st"u8;
    private static ReadOnlySpan<byte> EditorSettingsKey => "ed"u8;
    private static ReadOnlySpan<byte> EventsKey => "e"u8;

    protected override BeatmapObject GetDefaultValue() => new();

    protected override bool TryReadProperties(ref Utf8JsonReader reader,
        ref BeatmapObject value,
        JsonSerializerOptions options)
    {
        if (reader.ValueTextEquals(IdKey))
        {
            reader.Read();
            value.Id = reader.GetString() ?? throw new JsonException("Couldn't parse the object's ID");
            return true;
        }

        if (reader.ValueTextEquals(PrefabIdKey))
        {
            reader.Read();
            value.PrefabId = reader.GetString() ?? string.Empty;
            return true;
        }

        if (reader.ValueTextEquals(PrefabInstanceIdKey))
        {
            reader.Read();
            value.PrefabInstanceId = reader.GetString() ?? string.Empty;
            return true;
        }

        if (reader.ValueTextEquals(ParentIdKey))
        {
            reader.Read();
            value.ParentId = reader.GetString() ?? string.Empty;
            return true;
        }

        if (reader.ValueTextEquals(AutoKillTypeKey))
        {
            reader.Read();
            value.AutoKillType = (AutoKillType)reader.GetInt32();
            return true;
        }

        if (reader.ValueTextEquals(AutoKillOffsetKey))
        {
            reader.Read();
            value.AutoKillOffset = reader.GetSingle();
            return true;
        }

        if (reader.ValueTextEquals(ObjectTypeKey))
        {
            reader.Read();
            value.Type = (ObjectType)reader.GetInt32();
            return true;
        }

        if (reader.ValueTextEquals(NameKey))
        {
            reader.Read();
            value.Name = reader.GetString() ?? string.Empty;
            return true;
        }

        if (reader.ValueTextEquals(TextKey))
        {
            reader.Read();
            value.Text = reader.GetString() ?? string.Empty;
            return true;
        }

        if (reader.ValueTextEquals(OriginKey))
        {
            reader.Read();
            value.Origin = JsonSerializer.Deserialize<Vector2>(ref reader, options);
            return true;
        }

        if (reader.ValueTextEquals(ShapeKey))
        {
            reader.Read();
            value.Shape = ObjectShapeHelper.FromSeparate(reader.GetInt32(), value.Shape.GetShapeOption());
            return true;
        }

        if (reader.ValueTextEquals(ShapeOptionKey))
        {
            reader.Read();
            value.Shape = ObjectShapeHelper.FromSeparate(value.Shape.GetShape(), reader.GetInt32());
            return true;
        }

        if (reader.ValueTextEquals(CustomShapeParamsKey))
        {
            reader.Read();
            value.CustomShapeParams = JsonSerializer.Deserialize<CustomShapeParams>(ref reader, options);
            return true;
        }

        if (reader.ValueTextEquals(GradientTypeKey))
        {
            reader.Read();
            value.Gradient.Type = (GradientType)reader.GetInt32();
            return true;
        }

        if (reader.ValueTextEquals(GradientRotationKey))
        {
            reader.Read();
            value.Gradient.Rotation = reader.GetSingle();
            return true;
        }

        if (reader.ValueTextEquals(GradientScaleKey))
        {
            reader.Read();
            value.Gradient.Scale = reader.GetSingle();
            return true;
        }

        if (reader.ValueTextEquals(ParentTypeKey))
        {
            reader.Read();
            value.ParentType = JsonSerializer.Deserialize<ParentType>(ref reader, options);
            return true;
        }

        if (reader.ValueTextEquals(ParentOffsetKey))
        {
            reader.Read();
            value.ParentOffset = JsonSerializer.Deserialize<ParentOffset>(ref reader, options);
            return true;
        }

        if (reader.ValueTextEquals(RenderDepthKey))
        {
            reader.Read();
            value.RenderDepth = reader.GetSingle();
            return true;
        }

        if (reader.ValueTextEquals(StartTimeKey))
        {
            reader.Read();
            value.StartTime = reader.GetSingle();
            return true;
        }

        if (reader.ValueTextEquals(EditorSettingsKey))
        {
            reader.Read();
            var result = JsonSerializer.Deserialize<ObjectEditorSettings>(ref reader, options);
            if (result is not null)
                value.EditorSettings = result;
            return true;
        }

        // TODO: events

        return false;
    }

    protected override void WriteProperties(Utf8JsonWriter writer, BeatmapObject value, JsonSerializerOptions options)
    {
        writer.WriteString(IdProperty, value.Id);
        if (!string.IsNullOrEmpty(value.PrefabId))
            writer.WriteString(PrefabIdProperty, value.PrefabId);
        if (!string.IsNullOrEmpty(value.PrefabInstanceId))
            writer.WriteString(PrefabInstanceIdProperty, value.PrefabInstanceId);
        if (!string.IsNullOrEmpty(value.ParentId))
            writer.WriteString(ParentIdProperty, value.ParentId);

        if (value.AutoKillType != AutoKillType.NoAutoKill)
            writer.WriteNumber(AutoKillTypeProperty, (int)value.AutoKillType);
        if (value.AutoKillOffset != 0.0f)
            writer.WriteNumber(AutoKillOffsetProperty, value.AutoKillOffset);
        if (value.Type != ObjectType.LegacyNormal)
            writer.WriteNumber(ObjectTypeProperty, (int)value.Type);

        if (!string.IsNullOrEmpty(value.Name))
            writer.WriteString(NameProperty, value.Name);
        if (!string.IsNullOrEmpty(value.Text))
            writer.WriteString(TextProperty, value.Text);
        if (value.Origin != Vector2.Zero)
        {
            writer.WritePropertyName(OriginProperty);
            JsonSerializer.Serialize(writer, value.Origin, options);
        }

        value.Shape.ToSeparate(out var shape, out var shapeOption);
        if (shape != 0)
            writer.WriteNumber(ShapeProperty, shape);
        if (shapeOption != 0)
            writer.WriteNumber(ShapeOptionProperty, shapeOption);
        if (value.CustomShapeParams is not null)
        {
            writer.WritePropertyName(CustomShapeParamsProperty);
            JsonSerializer.Serialize(writer, value.CustomShapeParams, options);
        }

        if (value.Gradient.Type != GradientType.None)
            writer.WriteNumber(GradientTypeProperty, (int)value.Gradient.Type);
        writer.WriteNumber(GradientRotationProperty, value.Gradient.Rotation);
        writer.WriteNumber(GradientScaleProperty, value.Gradient.Scale);

        if (value.ParentType is not (ParentType.Position | ParentType.Rotation))
        {
            writer.WritePropertyName(ParentTypeProperty);
            JsonSerializer.Serialize(writer, value.ParentType, options);
        }

        writer.WritePropertyName(ParentOffsetProperty);
        JsonSerializer.Serialize(writer, value.ParentOffset, options);

        if (Math.Abs(value.RenderDepth - 20.0f) > float.Epsilon)
            writer.WriteNumber(RenderDepthProperty, value.RenderDepth);
        if (value.StartTime != 0.0f)
            writer.WriteNumber(StartTimeProperty, value.StartTime);

        if (!value.EditorSettings.IsDefault())
        {
            writer.WritePropertyName(EditorSettingsProperty);
            JsonSerializer.Serialize(writer, value.EditorSettings, options);
        }

        // TODO: events
    }
}