using System.Numerics;
using System.Text.Json;
using Pamx.Editor;
using Pamx.Keyframes;
using Pamx.Serialization.Extensions;
using Pamx.Objects;

namespace Pamx.Legacy.Converters.Objects;

internal sealed class LegacyBeatmapObjectConverter : ReadonlyJsonObjectConverter<BeatmapObject>
{
    private static ReadOnlySpan<byte> IdKey => "id"u8;
    private static ReadOnlySpan<byte> PrefabIdKey => "pid"u8;
    private static ReadOnlySpan<byte> PrefabInstanceIdKey => "piid"u8;
    private static ReadOnlySpan<byte> ParentIdKey => "p"u8;
    private static ReadOnlySpan<byte> NameKey => "name"u8;
    private static ReadOnlySpan<byte> ParentTypeKey => "pt"u8;
    private static ReadOnlySpan<byte> ParentOffsetKey => "po"u8;
    private static ReadOnlySpan<byte> RenderDepthKey => "d"u8;
    private static ReadOnlySpan<byte> ObjectTypeKey => "ot"u8;
    private static ReadOnlySpan<byte> IsHelperKey => "h"u8;
    private static ReadOnlySpan<byte> IsEmptyKey => "empty"u8;
    private static ReadOnlySpan<byte> ShapeKey => "shape"u8;
    private static ReadOnlySpan<byte> ShapeOptionKey => "so"u8;
    private static ReadOnlySpan<byte> TextKey => "text"u8;
    private static ReadOnlySpan<byte> StartTimeKey => "st"u8;
    private static ReadOnlySpan<byte> AutoKillTypeKey => "akt"u8;
    private static ReadOnlySpan<byte> IsAutoKillKey => "ak"u8;
    private static ReadOnlySpan<byte> AutoKillOffsetKey => "ako"u8;
    private static ReadOnlySpan<byte> OriginKey => "o"u8;
    private static ReadOnlySpan<byte> EditorSettingsKey => "ed"u8;
    private static ReadOnlySpan<byte> EventsKey => "events"u8;
    private static ReadOnlySpan<byte> PositionEventsKey => "pos"u8;
    private static ReadOnlySpan<byte> ScaleEventsKey => "sca"u8;
    private static ReadOnlySpan<byte> RotationEventsKey => "rot"u8;
    private static ReadOnlySpan<byte> ColorEventsKey => "col"u8;

    protected override BeatmapObject GetDefaultValue() => new() { RenderDepth = 15.0f };

    protected override bool TryReadProperties(ref Utf8JsonReader reader, ref BeatmapObject value,
        JsonSerializerOptions options)
    {
        if (reader.ValueTextEquals(IdKey))
        {
            reader.Read();
            value.Id = reader.GetString() ?? string.Empty;
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

        if (reader.ValueTextEquals(NameKey))
        {
            reader.Read();
            value.Name = reader.GetString() ?? string.Empty;
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
            value.RenderDepth = reader.GetSingleLike();
            return true;
        }

        if (reader.ValueTextEquals(ObjectTypeKey))
        {
            reader.Read();
            value.Type = (ObjectType)reader.GetSingleLike();
            return true;
        }

        if (reader.ValueTextEquals(IsHelperKey))
        {
            reader.Read();
            if (reader.GetBooleanLike())
                value.Type = ObjectType.LegacyHelper;
        }

        if (reader.ValueTextEquals(IsEmptyKey))
        {
            reader.Read();
            if (reader.GetBooleanLike())
                value.Type = ObjectType.LegacyEmpty;
        }

        if (reader.ValueTextEquals(ShapeKey))
        {
            reader.Read();
            value.Shape = ObjectShapeHelper.FromSeparate(reader.GetInt32Like(), value.Shape.GetShapeOption());
            return true;
        }

        if (reader.ValueTextEquals(ShapeOptionKey))
        {
            reader.Read();
            value.Shape = ObjectShapeHelper.FromSeparate(value.Shape.GetShape(), reader.GetInt32Like());
            return true;
        }

        if (reader.ValueTextEquals(TextKey))
        {
            reader.Read();
            value.Text = reader.GetString() ?? string.Empty;
            return true;
        }

        if (reader.ValueTextEquals(StartTimeKey))
        {
            reader.Read();
            value.StartTime = reader.GetSingleLike();
            return true;
        }

        if (reader.ValueTextEquals(AutoKillTypeKey))
        {
            reader.Read();
            value.AutoKillType = (AutoKillType)reader.GetSingleLike();
            return true;
        }

        if (reader.ValueTextEquals(IsAutoKillKey))
        {
            reader.Read();
            if (reader.GetBooleanLike())
                value.AutoKillType = AutoKillType.LastKeyframe;
            return true;
        }

        if (reader.ValueTextEquals(AutoKillOffsetKey))
        {
            reader.Read();
            value.AutoKillOffset = reader.GetSingleLike();
            return true;
        }

        if (reader.ValueTextEquals(OriginKey))
        {
            reader.Read();
            value.Origin = JsonSerializer.Deserialize<Vector2>(ref reader, options);
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

        if (reader.ValueTextEquals(EventsKey))
        {
            reader.Read();
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException("Expected StartObject token");

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                    continue;

                if (reader.ValueTextEquals(PositionEventsKey))
                {
                    reader.Read();
                    value.PositionEvents =
                        JsonSerializer.Deserialize<List<RandomKeyframe<Vector2>>>(ref reader, options) ?? [];
                }
                else if (reader.ValueTextEquals(ScaleEventsKey))
                {
                    reader.Read();
                    value.ScaleEvents =
                        JsonSerializer.Deserialize<List<RandomKeyframe<Vector2>>>(ref reader, options) ?? [];
                }
                else if (reader.ValueTextEquals(RotationEventsKey))
                {
                    reader.Read();
                    value.RotationEvents =
                        JsonSerializer.Deserialize<List<ObjectRotationKeyframe>>(ref reader, options) ?? [];
                }
                else if (reader.ValueTextEquals(ColorEventsKey))
                {
                    reader.Read();
                    value.ColorEvents =
                        JsonSerializer.Deserialize<List<FixedKeyframe<ObjectColorValue>>>(ref reader, options) ?? [];
                }
                else
                    reader.TrySkip();
            }
        }

        return false;
    }
}