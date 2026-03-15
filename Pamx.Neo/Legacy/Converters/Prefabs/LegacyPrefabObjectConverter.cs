using System.Numerics;
using System.Text.Json;
using Pamx.Neo.Editor;
using Pamx.Neo.Prefabs;
using Pamx.Neo.Serialization.Extensions;

namespace Pamx.Neo.Legacy.Converters.Prefabs;

internal sealed class LegacyPrefabObjectConverter : ReadonlyJsonObjectConverter<PrefabObject>
{
    private static ReadOnlySpan<byte> IdKey => "id"u8;
    private static ReadOnlySpan<byte> PrefabIdKey => "pid"u8;
    private static ReadOnlySpan<byte> StartTimeKey => "st"u8;
    private static ReadOnlySpan<byte> EditorSettingsKey => "ed"u8;
    private static ReadOnlySpan<byte> EventsKey => "e"u8;
    private static ReadOnlySpan<byte> PositionKey => "pos"u8;
    private static ReadOnlySpan<byte> ScaleKey => "sca"u8;
    private static ReadOnlySpan<byte> RotationKey => "rot"u8;
    private static ReadOnlySpan<byte> RotationValueKey => "x"u8;

    protected override PrefabObject GetDefaultValue() => new();

    protected override bool TryReadProperties(ref Utf8JsonReader reader, ref PrefabObject value,
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

        if (reader.ValueTextEquals(StartTimeKey))
        {
            reader.Read();
            value.StartTime = reader.GetSingleLike();
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

                if (reader.ValueTextEquals(PositionKey))
                {
                    reader.Read();
                    value.Position = JsonSerializer.Deserialize<Vector2>(ref reader, options);
                }

                if (reader.ValueTextEquals(ScaleKey))
                {
                    reader.Read();
                    value.Scale = JsonSerializer.Deserialize<Vector2>(ref reader, options);
                }

                if (reader.ValueTextEquals(RotationKey))
                {
                    reader.Read();
                    if (reader.TokenType != JsonTokenType.StartObject)
                        throw new JsonException("Expected StartObject token");

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
                    {
                        if (reader.TokenType != JsonTokenType.PropertyName)
                            continue;

                        if (reader.ValueTextEquals(RotationValueKey))
                        {
                            reader.Read();
                            value.Rotation = reader.GetSingleLike();
                        }
                        else
                            reader.TrySkip();
                    }
                }
                else
                    reader.TrySkip();
            }
        }

        return false;
    }
}