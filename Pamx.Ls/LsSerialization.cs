using System.Drawing;
using System.Globalization;
using System.Numerics;
using System.Text.Json.Nodes;
using Pamx.Common;
using Pamx.Common.Data;
using Pamx.Common.Enum;

namespace Pamx.Ls;

public static class LsSerialization
{
    public static JsonObject SerializeBeatmap(IBeatmap beatmap)
    {
        var json = new JsonObject();
        
        // Write level metadata (unused, but still read by the game)
        json.Add("level_data", new JsonObject
        {
            ["level_version"] = "20.4.4",
            ["background_color"] = "0",
            ["follow_player"] = "False",
            ["show_intro"] = "False",
        });
        
        // Write editor data
        json.Add("ed", new JsonObject
        {
            ["timeline_pos"] = "0",
            ["markers"] = new JsonArray(
                    beatmap.Markers
                        .Select(x => new JsonObject
                        {
                            ["active"] = "True",
                            ["name"] = x.Name,
                            ["desc"] = x.Description,
                            ["col"] = x.Color.ToString(),
                            ["t"] = x.Time.ToString(CultureInfo.InvariantCulture),
                        })
                        .Cast<JsonNode>()
                        .ToArray()),
        });
        
        // Write checkpoints
        json.Add("checkpoints", new JsonArray(
            beatmap.Checkpoints
                .Select(x => new JsonObject
                {
                    ["active"] = "False",
                    ["name"] = x.Name,
                    ["t"] = x.Time.ToString(CultureInfo.InvariantCulture),
                    ["pos"] = new JsonObject
                    {
                        ["x"] = x.Position.X.ToString(CultureInfo.InvariantCulture),
                        ["y"] = x.Position.Y.ToString(CultureInfo.InvariantCulture),
                    },
                })
                .Cast<JsonNode>()
                .ToArray()));
        
        // Write prefabs
        json.Add("prefabs", new JsonArray(
            beatmap.Prefabs
                .Select(x => SerializePrefab(x, true))
                .Cast<JsonNode>()
                .ToArray()));
        
        // Write prefab objects
        json.Add("prefab_objects", new JsonArray(
            beatmap.PrefabObjects
                .Select(SerializePrefabObject)
                .Cast<JsonNode>()
                .ToArray()));
        
        // Write themes
        json.Add("themes", new JsonArray(
            beatmap.Themes
                .Select(SerializeTheme)
                .Cast<JsonNode>()
                .ToArray()));
        
        // Write background objects
        json.Add("bg_objects", new JsonArray(
            beatmap.BackgroundObjects
                .Select(SerializeBackgroundObject)
                .Cast<JsonNode>()
                .ToArray()));
        
        // Write objects
        json.Add("beatmap_objects", new JsonArray(
            beatmap.Objects
                .Select(SerializeBeatmapObject)
                .Cast<JsonNode>()
                .ToArray()));
        
        // Write events
        var events = beatmap.Events;
        json.Add("events", new JsonObject
        {
            ["pos"] = SerializeEventsArray(events.Movement, (j, kf) =>
            {
                j.Add("x", kf.Value.X.ToString(CultureInfo.InvariantCulture));
                j.Add("y", kf.Value.Y.ToString(CultureInfo.InvariantCulture));
            }),
            ["zoom"] = SerializeEventsArray(events.Zoom, (j, kf) =>
                j.Add("x", kf.Value.ToString(CultureInfo.InvariantCulture))),
            ["rot"] = SerializeEventsArray(events.Rotation, (j, kf) =>
                j.Add("x", kf.Value.ToString(CultureInfo.InvariantCulture))),
            ["shake"] = SerializeEventsArray(events.Shake, (j, kf) =>
            {
                j.Add("x", kf.Value.ToString(CultureInfo.InvariantCulture));
                j.Add("y", "0");
            }),
            ["theme"] = SerializeEventsArray(events.Theme, (j, kf) =>
            {
                if (kf.Value is not IIdentifiable<int> identifiable)
                    throw new ArgumentException($"{kf.Value.GetType()} is not identifiable, but an id is required");
                j.Add("x", identifiable.Id.ToString());
            }),
            ["chroma"] = SerializeEventsArray(events.Chroma, (j, kf) =>
                j.Add("x", kf.Value.ToString(CultureInfo.InvariantCulture))),
            ["bloom"] = SerializeEventsArray(events.Bloom, (j, kf) =>
                j.Add("x", kf.Value.Intensity.ToString(CultureInfo.InvariantCulture))),
            ["vignette"] = SerializeEventsArray(events.Vignette, (j, kf) =>
            {
                j.Add("x", kf.Value.Intensity.ToString(CultureInfo.InvariantCulture));
                j.Add("y", kf.Value.Smoothness.ToString(CultureInfo.InvariantCulture));
                j.Add("z", kf.Value.Rounded ? "1" : "0");
                j.Add("x2", kf.Value.Roundness?.ToString(CultureInfo.InvariantCulture) ?? "0");
                j.Add("y2", kf.Value.Center.X.ToString(CultureInfo.InvariantCulture));
                j.Add("z2", kf.Value.Center.Y.ToString(CultureInfo.InvariantCulture));
            }),
            ["lens"] = SerializeEventsArray(events.LensDistortion, (j, kf) =>
                j.Add("x", kf.Value.Intensity.ToString(CultureInfo.InvariantCulture))),
            ["grain"] = SerializeEventsArray(events.Grain, (j, kf) =>
            {
                j.Add("x", kf.Value.Intensity.ToString(CultureInfo.InvariantCulture));
                j.Add("y", kf.Value.Colored ? "1" : "0");
                j.Add("z", kf.Value.Size.ToString(CultureInfo.InvariantCulture));
            }),
        });
        
        return json;
    }

    private static JsonArray SerializeEventsArray<T>(
        IEnumerable<FixedKeyframe<T>> events,
        Action<JsonObject, FixedKeyframe<T>> valueSerializer)
    {
        var json = new JsonArray();
        foreach (var keyframe in events)
        {
            var keyframeJson = new JsonObject
            {
                ["t"] = keyframe.Time.ToString(CultureInfo.InvariantCulture),
            };
            valueSerializer(keyframeJson, keyframe);
            if (keyframe.Ease != Ease.Linear)
                keyframeJson.Add("ct", keyframe.Ease.ToString());
            json.Add(keyframeJson);
        }
        return json;
    }

    private static JsonObject SerializeBackgroundObject(BackgroundObject @object)
    {
        var json = new JsonObject
        {
            ["active"] = @object.Active ? "True" : "False",
            ["name"] = @object.Name,
            ["kind"] = "1",
            ["pos"] = new JsonObject
            {
                ["x"] = @object.Position.X.ToString(CultureInfo.InvariantCulture),
                ["y"] = @object.Position.Y.ToString(CultureInfo.InvariantCulture),
            },
            ["size"] = new JsonObject
            {
                ["x"] = @object.Scale.X.ToString(CultureInfo.InvariantCulture),
                ["y"] = @object.Scale.Y.ToString(CultureInfo.InvariantCulture),
            },
            ["rot"] = @object.Rotation.ToString(CultureInfo.InvariantCulture),
            ["color"] = @object.Color.ToString(CultureInfo.InvariantCulture),
            ["layer"] = @object.Depth.ToString(CultureInfo.InvariantCulture),
            ["fade"] = @object.Fade ? "True" : "False",
        };

        if (@object.ReactiveType != BackgroundObjectReactiveType.None)
        {
            json.Add("r_set", new JsonObject
            {
                ["type"] = @object.ReactiveType switch
                {
                    BackgroundObjectReactiveType.Bass => "LOW",
                    BackgroundObjectReactiveType.Mid => "MID",
                    BackgroundObjectReactiveType.Treble => "HIGH",
                    _ => throw new ArgumentOutOfRangeException()
                },
                ["scale"] = @object.ReactiveScale.ToString(CultureInfo.InvariantCulture),
            });
        }
        
        return json;
    }

    private static JsonObject SerializePrefabObject(IPrefabObject prefabObject)
    {
        var json = new JsonObject();
        json.AddId("id", prefabObject, true);
        json.AddId("pid", prefabObject.Prefab, true);
        json.Add("st", prefabObject.Time.ToString(CultureInfo.InvariantCulture));
        json.Add("ed", SerializeObjectEditorSettings(prefabObject.EditorSettings));
        json.Add("e", new JsonObject
        {
            ["pos"] = new JsonObject
            {
                ["x"] = prefabObject.Position.X.ToString(CultureInfo.InvariantCulture),
                ["y"] = prefabObject.Position.Y.ToString(CultureInfo.InvariantCulture),
            },
            ["sca"] = new JsonObject
            {
                ["x"] = prefabObject.Scale.X.ToString(CultureInfo.InvariantCulture),
                ["y"] = prefabObject.Scale.Y.ToString(CultureInfo.InvariantCulture),
            },
            ["rot"] = new JsonObject
            {
                ["x"] = prefabObject.Rotation.ToString(CultureInfo.InvariantCulture),  
            },
        });
        return json;
    }
    
    public static JsonObject SerializeTheme(ITheme theme)
    {
        var json = new JsonObject();
        json.AddThemeId("id", theme, true);
        json.Add("name", theme.Name);
        json.Add("bg", theme.Background.ToHex());
        json.Add("gui", theme.Gui.ToHex());
        
        json.Add("players", new JsonArray(
            theme.Player
                .Select(c => c.ToHex())
                .Select(x => JsonValue.Create(x))
                .Cast<JsonNode>()
                .ToArray()));
        json.Add("objs", new JsonArray(
            theme.Object
                .Select(c => c.ToHex())
                .Select(x => JsonValue.Create(x))
                .Cast<JsonNode>()
                .ToArray()));
        json.Add("bgs", new JsonArray(
            theme.BackgroundObject
                .Select(c => c.ToHex())
                .Select(x => JsonValue.Create(x))
                .Cast<JsonNode>()
                .ToArray()));
        
        return json;
    }
    
    public static JsonObject SerializePrefab(IPrefab prefab, bool requiresId = false)
    {
        var json = new JsonObject();
        json.AddId("id", prefab, requiresId);
        json.Add("name", prefab.Name);
        json.Add("type", prefab.Type switch
        {
            PrefabType.Bombs => "0",
            PrefabType.Bullets => "1",
            PrefabType.Beams => "2",
            PrefabType.Spinners => "3",
            PrefabType.Pulses => "4",
            PrefabType.Character => "5",
            PrefabType.Misc1 => "6",
            PrefabType.Misc2 => "7",
            PrefabType.Misc3 => "8",
            PrefabType.Misc4 => "9",
            _ => throw new ArgumentOutOfRangeException()
        });
        json.Add("objects", new JsonArray(
            prefab.BeatmapObjects
                .Select(SerializeBeatmapObject)
                .Cast<JsonNode>()
                .ToArray()));
        return json;
    }

    public static JsonObject SerializeBeatmapObject(IObject @object)
    {
        var json = new JsonObject();
        json.AddId("id", @object, true);
        if (@object.Parent is not null)
            json.AddId("p", @object.Parent);
        json.Add("name", @object.Name);
        json.Add("pt", 
            $"{(@object.ParentType.HasFlag(ParentType.Position) ? '1' : '0')}" + 
            $"{(@object.ParentType.HasFlag(ParentType.Scale) ? '1' : '0')}" + 
            $"{(@object.ParentType.HasFlag(ParentType.Rotation) ? '1' : '0')}");
        var parentOffset = new JsonArray
        {
            @object.ParentOffset.Position,
            @object.ParentOffset.Scale,
            @object.ParentOffset.Rotation
        };
        json.Add("po", parentOffset);
        json.Add("d", @object.RenderDepth.ToString());
        json.Add("ot", @object.Type switch
        {
            ObjectType.LegacyNormal => "0",
            ObjectType.LegacyHelper => "1",
            ObjectType.LegacyDecoration => "2",
            ObjectType.LegacyEmpty => "3",
            _ => throw new ArgumentOutOfRangeException()
        });
        
        var shape = (int) @object.Shape & 0xffff;
        var shapeOption = (int) @object.Shape >> 16;
        json.Add("shape", shape.ToString());
        json.Add("so", shapeOption.ToString());
        
        json.Add("text", @object.Text);
        json.Add("st", @object.StartTime.ToString(CultureInfo.InvariantCulture));
        json.Add("akt", @object.AutoKillType switch
        {
            AutoKillType.NoAutoKill => "0",
            AutoKillType.LastKeyframe => "1",
            AutoKillType.LastKeyframeOffset => "2",
            AutoKillType.FixedTime => "3",
            AutoKillType.SongTime => "4",
            _ => throw new ArgumentOutOfRangeException()
        });
        json.Add("ako", @object.AutoKillOffset.ToString(CultureInfo.InvariantCulture));
        json.Add("o", new JsonObject
        {
            ["x"] = @object.Origin.X,
            ["y"] = @object.Origin.Y
        });
        json.Add("ed", SerializeObjectEditorSettings(@object.EditorSettings));
        json.Add("events", new JsonObject
        {
            ["pos"] = new JsonArray(
                @object.PositionEvents
                    .Select(SerializeVector2Keyframe)
                    .Cast<JsonNode>()
                    .ToArray()),
            ["sca"] = new JsonArray(
                @object.ScaleEvents
                    .Select(SerializeVector2Keyframe)
                    .Cast<JsonNode>()
                    .ToArray()),
            ["rot"] = new JsonArray(
                @object.RotationEvents
                    .Select(SerializeFloatKeyframe)
                    .Cast<JsonNode>()
                    .ToArray()),
            ["col"] = new JsonArray(
                @object.ColorEvents
                    .Select(SerializeThemeColorKeyframe)
                    .Cast<JsonNode>()
                    .ToArray()),
        });
        return json;
    }

    private static JsonObject SerializeVector2Keyframe(Keyframe<Vector2> keyframe)
    {
        var json = new JsonObject();
        json.Add("t", keyframe.Time.ToString(CultureInfo.InvariantCulture));
        json.Add("x", keyframe.Value.X.ToString(CultureInfo.InvariantCulture));
        json.Add("y", keyframe.Value.Y.ToString(CultureInfo.InvariantCulture));
        if (keyframe.Ease != Ease.Linear)
            json.Add("ct", keyframe.Ease.ToString());
        if (keyframe.RandomMode != RandomMode.None)
        {
            json.Add("r", keyframe.RandomMode switch
            {
                RandomMode.None => "0",
                RandomMode.Range => "1",
                RandomMode.Snap => "2",
                RandomMode.Select => "3",
                RandomMode.Scale => "4",
                _ => throw new ArgumentOutOfRangeException()
            });
            json.Add("rx", keyframe.RandomValue.X.ToString(CultureInfo.InvariantCulture));
            json.Add("ry", keyframe.RandomValue.Y.ToString(CultureInfo.InvariantCulture));
            json.Add("rz", keyframe.RandomInterval.ToString(CultureInfo.InvariantCulture));
        }
        return json;
    }
    
    private static JsonObject SerializeFloatKeyframe(Keyframe<float> keyframe)
    {
        var json = new JsonObject();
        json.Add("t", keyframe.Time.ToString(CultureInfo.InvariantCulture));
        json.Add("x", keyframe.Value.ToString(CultureInfo.InvariantCulture));
        if (keyframe.Ease != Ease.Linear)
            json.Add("ct", keyframe.Ease.ToString());
        if (keyframe.RandomMode != RandomMode.None)
        {
            json.Add("r", keyframe.RandomMode switch
            {
                RandomMode.None => "0",
                RandomMode.Range => "1",
                RandomMode.Select => "3",
                RandomMode.Scale => "4",
                _ => throw new ArgumentOutOfRangeException()
            });
            json.Add("rx", keyframe.RandomValue.ToString(CultureInfo.InvariantCulture));
            json.Add("rz", keyframe.RandomInterval.ToString(CultureInfo.InvariantCulture));
        }
        return json;
    }
    
    private static JsonObject SerializeThemeColorKeyframe(FixedKeyframe<ThemeColor> keyframe)
        => new()
        {
            ["t"] = keyframe.Time.ToString(CultureInfo.InvariantCulture),
            ["x"] = keyframe.Value.Index.ToString(),
        };

    private static JsonObject SerializeObjectEditorSettings(ObjectEditorSettings editorSettings)
    {
        var json = new JsonObject();
        if (editorSettings.Locked)
            json.Add("locked", "True");
        if (editorSettings.Collapsed)
            json.Add("shrink", "True");
        json.Add("bin", editorSettings.Bin.ToString());
        json.Add("layer", editorSettings.Layer.ToString());
        return json;
    }
    
    private static void AddThemeId(this JsonObject json, string key, ITheme theme, bool require = false)
    {
        if (theme is not IIdentifiable<int> && require)
            throw new ArgumentException($"{theme.GetType()} is not identifiable, but an id is required");

        if (theme is IIdentifiable<int> identifiable)
            json.Add(key, identifiable.Id.ToString());
    }

    private static void AddId(this JsonObject json, string key, object? value, bool require = false)
    {
        if (value is not IIdentifiable<string> && require)
            throw new ArgumentException($"{value?.GetType()} is not identifiable, but an id is required");

        if (value is IIdentifiable<string> identifiable)
            json.Add(key, identifiable.Id);
    }
    
    private static string ToHex(this Color color)
        => $"{color.R:X2}{color.G:X2}{color.B:X2}";
}