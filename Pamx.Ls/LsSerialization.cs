using System.Drawing;
using System.Globalization;
using System.Numerics;
using System.Text.Json;
using Pamx.Common;
using Pamx.Common.Data;
using Pamx.Common.Enum;

namespace Pamx.Ls;

public static class LsSerialization
{
    public static void SerializeBeatmap(IBeatmap beatmap, Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        {
            // Write the properties of beatmap
            
            // Write editor data
            writer.WriteStartObject("ed");
            {
                writer.WriteString("timeline_pos", "0");
                writer.WriteStartArray("markers");
                {
                    foreach (var marker in beatmap.Markers)
                    {
                        writer.WriteStartObject();
                        {
                            writer.WriteString("active", "True");
                            writer.WriteString("name", marker.Name);
                            writer.WriteString("desc", marker.Description);
                            writer.WriteString("col", marker.Color.ToString());
                            writer.WriteString("t", marker.Time.ToString(CultureInfo.InvariantCulture));
                            writer.WriteEndObject();
                        }
                    }
                    writer.WriteEndArray();
                }
                writer.WriteEndObject();
            }
            
            // Write prefab objects
            writer.WriteStartArray("prefab_objects");
            {
                foreach (var prefabObject in beatmap.PrefabObjects)
                    SerializePrefabObject(prefabObject, writer);
                writer.WriteEndArray();
            }
        
            // Write level metadata
            // Most of the data here isn't used, so we can simply write constant values
            writer.WriteStartObject("level_data");
            {
                writer.WriteString("level_version", "20.4.4");
                writer.WriteString("background_color", "0");
                writer.WriteString("follow_player", "False");
                writer.WriteString("show_intro", "False");
                writer.WriteEndObject();
            }
        
            // Write prefabs
            writer.WriteStartArray("prefabs");
            {
                foreach (var prefab in beatmap.Prefabs)
                    SerializePrefab(prefab, writer, true);
                writer.WriteEndArray();
            }
            
            // Write themes
            writer.WriteStartArray("themes");
            {
                foreach (var theme in beatmap.Themes)
                    SerializeTheme(theme, writer, true);
                writer.WriteEndArray();
            }
            
            // Write checkpoints
            writer.WriteStartArray("checkpoints");
            {
                foreach (var checkpoint in beatmap.Checkpoints)
                {
                    writer.WriteStartObject();
                    {
                        writer.WriteString("active", "False");
                        writer.WriteString("name", checkpoint.Name);
                        writer.WriteString("t", checkpoint.Time.ToString(CultureInfo.InvariantCulture));
                        writer.WriteStartObject("pos");
                        {
                            writer.WriteString("x", checkpoint.Position.X.ToString(CultureInfo.InvariantCulture));
                            writer.WriteString("y", checkpoint.Position.Y.ToString(CultureInfo.InvariantCulture));
                            writer.WriteEndObject();
                        }
                        writer.WriteEndObject();
                    }
                }
                writer.WriteEndArray();
            }
            
            // Write objects
            writer.WriteStartArray("beatmap_objects");
            {
                foreach (var @object in beatmap.Objects)
                    SerializeBeatmapObject(@object, writer);
                writer.WriteEndArray();
            }
            
            // Write background objects
            writer.WriteStartArray("bg_objects");
            {
                foreach (var backgroundObject in beatmap.BackgroundObjects)
                {
                    writer.WriteStartObject();
                    {
                        writer.WriteString("active", backgroundObject.Active ? "True" : "False");
                        writer.WriteString("name", backgroundObject.Name);
                        writer.WriteString("kind", "1");
                        writer.WriteStartObject("pos");
                        {
                            writer.WriteString("x", backgroundObject.Position.X.ToString(CultureInfo.InvariantCulture));
                            writer.WriteString("y", backgroundObject.Position.Y.ToString(CultureInfo.InvariantCulture));
                            writer.WriteEndObject();
                        }
                        writer.WriteStartObject("size");
                        {
                            writer.WriteString("x", backgroundObject.Scale.X.ToString(CultureInfo.InvariantCulture));
                            writer.WriteString("y", backgroundObject.Scale.Y.ToString(CultureInfo.InvariantCulture));
                            writer.WriteEndObject();
                        }
                        writer.WriteString("rot", backgroundObject.Rotation.ToString(CultureInfo.InvariantCulture));
                        writer.WriteString("color", backgroundObject.Color.ToString());
                        writer.WriteString("layer", backgroundObject.Depth.ToString(CultureInfo.InvariantCulture));
                        writer.WriteString("fade", backgroundObject.Fade ? "True" : "False");
                        if (backgroundObject.ReactiveType != BackgroundObjectReactiveType.None)
                        {
                            writer.WriteStartObject("r_set");
                            {
                                writer.WriteString("type", backgroundObject.ReactiveType switch
                                {
                                    BackgroundObjectReactiveType.Bass => "LOW",
                                    BackgroundObjectReactiveType.Mid => "MID",
                                    BackgroundObjectReactiveType.Treble => "HIGH",
                                    _ => "LOW"
                                });
                                writer.WriteString("scale", backgroundObject.ReactiveScale.ToString(CultureInfo.InvariantCulture));
                                writer.WriteEndObject();
                            }
                        }
                        writer.WriteEndObject();
                    }
                }
                writer.WriteEndArray();
            }
            
            // Write events
            writer.WritePropertyName("events");
            SerializeEvents(beatmap.Events, writer);
            
            writer.WriteEndObject();
        }
    }

    private static void SerializeEvents(IBeatmapEvents beatmapEvents, Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        {
            WriteEventKeyframes("pos", beatmapEvents.Movement, writer, (writer, v) =>
            {
                writer.WriteString("x", v.X.ToString(CultureInfo.InvariantCulture));
                writer.WriteString("y", v.Y.ToString(CultureInfo.InvariantCulture));
            });
            WriteEventKeyframes("zoom", beatmapEvents.Zoom, writer, (writer, v) => writer.WriteString("x", v.ToString(CultureInfo.InvariantCulture)));
            WriteEventKeyframes("rot", beatmapEvents.Rotation, writer, (writer, v) => writer.WriteString("x", v.ToString(CultureInfo.InvariantCulture)));
            WriteEventKeyframes("shake", beatmapEvents.Shake, writer, (writer, v) =>
            {
                writer.WriteString("x", v.ToString(CultureInfo.InvariantCulture));
                writer.WriteString("y", "0"); // Don't know why this is here, but it's always 0
            });
            WriteEventKeyframes("theme", beatmapEvents.Theme, writer, (writer, v) =>
            {
                if (v is not IIdentifiable<int> identifiable)
                    throw new ArgumentException($"{v.GetType()} is not identifiable, but an id is required");
                writer.WriteString("x", identifiable.Id.ToString());
            });
            WriteEventKeyframes("chroma", beatmapEvents.Chroma, writer, (writer, v) => writer.WriteString("x", v.ToString(CultureInfo.InvariantCulture)));
            WriteEventKeyframes("bloom", beatmapEvents.Bloom, writer, (writer, v) => writer.WriteString("x", v.Intensity.ToString(CultureInfo.InvariantCulture)));
            WriteEventKeyframes("vignette", beatmapEvents.Vignette, writer, (writer, v) =>
            {
                writer.WriteString("x", v.Intensity.ToString(CultureInfo.InvariantCulture));
                writer.WriteString("y", v.Smoothness.ToString(CultureInfo.InvariantCulture));
                writer.WriteString("z", v.Rounded ? "1" : "0");
                writer.WriteString("x2", v.Roundness.ToString(CultureInfo.InvariantCulture));
                writer.WriteString("y2", v.Center.X.ToString(CultureInfo.InvariantCulture));
                writer.WriteString("z2", v.Center.Y.ToString(CultureInfo.InvariantCulture));
            });
            WriteEventKeyframes("lens", beatmapEvents.LensDistortion, writer, (writer, v) => writer.WriteString("x", v.Intensity.ToString(CultureInfo.InvariantCulture)));
            WriteEventKeyframes("grain", beatmapEvents.Grain, writer, (writer, v) =>
            {
                writer.WriteString("x", v.Intensity.ToString(CultureInfo.InvariantCulture));
                writer.WriteString("y", v.Colored ? "1" : "0");
                writer.WriteString("z", v.Size.ToString(CultureInfo.InvariantCulture));
            });
            writer.WriteEndObject();
        }
    }

    private static void WriteEventKeyframes<T>(string name, IEnumerable<FixedKeyframe<T>> eventKeyframes, Utf8JsonWriter writer, Action<Utf8JsonWriter, T> writeKeyframeDataCallback)
    {
        writer.WriteStartArray(name);
        {
            foreach (var keyframe in eventKeyframes)
            {
                writer.WriteStartObject();
                {
                    writer.WriteString("t", keyframe.Time.ToString(CultureInfo.InvariantCulture));
                    writeKeyframeDataCallback(writer, keyframe.Value);
                    if (keyframe.Ease != Ease.Linear)
                        writer.WriteString("ct", keyframe.Ease.ToString());
                    writer.WriteEndObject();
                }
            }
            writer.WriteEndArray();
        }
    }

    public static void SerializePrefabObject(IPrefabObject prefabObject, Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        {
            writer.WriteId("id", prefabObject, true);
            writer.WriteId("pid", prefabObject.Prefab, true);
            
            // Write the properties of prefab object
            writer.WriteString("st", prefabObject.Time.ToString(CultureInfo.InvariantCulture));
            writer.WritePropertyName("ed");
            SerializeObjectEditorSettings(prefabObject.EditorSettings, writer);
            
            writer.WriteEndObject();
        }
    }

    public static void SerializeTheme(ITheme theme, Utf8JsonWriter writer, bool requiresId = false)
    {
        writer.WriteStartObject();
        {
            writer.WriteThemeId("id", theme, requiresId);
            
            // Write the properties of theme
            writer.WriteString("name", theme.Name);
            writer.WriteString("bg", theme.Background.ToHex());
            writer.WriteString("gui", theme.Gui.ToHex());
            
            writer.WriteStartArray("players");
            {
                foreach (var color in theme.Player)
                    writer.WriteStringValue(color.ToHex());
                writer.WriteEndArray();
            }
            
            writer.WriteStartArray("objs");
            {
                foreach (var color in theme.Object)
                    writer.WriteStringValue(color.ToHex());
                writer.WriteEndArray();
            }
            
            writer.WriteStartArray("bgs");
            {
                foreach (var color in theme.BackgroundObject)
                    writer.WriteStringValue(color.ToHex());
                writer.WriteEndArray();
            }
            
            writer.WriteEndObject();
        }
    }
    
    public static void SerializePrefab(IPrefab prefab, Utf8JsonWriter writer, bool requiresId = false)
    {
        writer.WriteStartObject();
        {
            writer.WriteId("id", prefab, requiresId);
            
            // Write the properties of prefab
            writer.WriteString("name", prefab.Name);
            writer.WriteString("type", prefab.Type switch
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
                _ => "0"
            });
            writer.WriteString("offset", prefab.Offset.ToString(CultureInfo.InvariantCulture));
                
            // Write prefab objects
            writer.WriteStartArray("objects");
            {
                foreach (var beatmapObject in prefab.BeatmapObjects)
                    SerializeBeatmapObject(beatmapObject, writer);
                writer.WriteEndArray();
            }
            writer.WriteEndObject();
        }
    }

    public static void SerializeBeatmapObject(IObject @object, Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        {
            writer.WriteId("id", @object, true);
            writer.WriteId("p", @object.Parent);
            
            // Write the properties of object
            writer.WriteString("name", @object.Name);
            writer.WriteString("pt", 
                (@object.ParentType.HasFlag(ParentType.Position) ? "1" : "0") + 
                (@object.ParentType.HasFlag(ParentType.Scale) ? "1" : "0") +
                (@object.ParentType.HasFlag(ParentType.Rotation) ? "1" : "0"));
            writer.WriteStartArray("po");
            {
                writer.WriteNumberValue(@object.ParentOffset.Position);
                writer.WriteNumberValue(@object.ParentOffset.Scale);
                writer.WriteNumberValue(@object.ParentOffset.Rotation);
                writer.WriteEndArray();
            }
            writer.WriteString("d", @object.RenderDepth.ToString(CultureInfo.InvariantCulture));
            writer.WriteString("ot", @object.Type switch
            {
                ObjectType.LegacyNormal => "0",
                ObjectType.LegacyHelper => "1",
                ObjectType.LegacyDecoration => "2",
                ObjectType.LegacyEmpty => "3",
                _ => "0"
            });
            writer.WriteString("shape", @object.Shape switch
            {
                ObjectShape.Square => "0",
                ObjectShape.Circle => "1",
                ObjectShape.Triangle => "2",
                ObjectShape.Arrow => "3",
                ObjectShape.Text => "4",
                ObjectShape.Hexagon => "5",
                _ => "0"
            });
            writer.WriteString("so", @object.ShapeOption.ToString(CultureInfo.InvariantCulture));
            if (@object.Shape == ObjectShape.Text)
                writer.WriteString("text", @object.Text);
            writer.WriteString("st", @object.StartTime.ToString(CultureInfo.InvariantCulture));
            writer.WriteString("akt", @object.AutoKillType switch
            {
                AutoKillType.NoAutoKill => "0",
                AutoKillType.LastKeyframe => "1",
                AutoKillType.LastKeyframeOffset => "2",
                AutoKillType.FixedTime => "3",
                AutoKillType.SongTime => "4",
                _ => "0"
            });
            writer.WriteString("ako", @object.AutoKillOffset.ToString(CultureInfo.InvariantCulture));
            writer.WriteStartObject("o");
            {
                writer.WriteNumber("x", @object.Origin.X);
                writer.WriteNumber("y", @object.Origin.Y);
                writer.WriteEndObject();
            }
            writer.WritePropertyName("ed");
            SerializeObjectEditorSettings(@object.EditorSettings, writer);
            writer.WriteStartObject("events");
            {
                writer.WriteStartArray("pos");
                {
                    foreach (var keyframe in @object.PositionEvents)
                        SerializeVector2Keyframe(keyframe, writer);
                    writer.WriteEndArray();
                }
                writer.WriteStartArray("sca");
                {
                    foreach (var keyframe in @object.ScaleEvents)
                        SerializeVector2Keyframe(keyframe, writer);
                    writer.WriteEndArray();
                }
                writer.WriteStartArray("rot");
                {
                    foreach (var keyframe in @object.RotationEvents)
                        SerializeFloatKeyframe(keyframe, writer);
                    writer.WriteEndArray();
                }
                writer.WriteStartArray("col");
                {
                    foreach (var keyframe in @object.ColorEvents)
                        SerializeThemeColorKeyframe(keyframe, writer);
                    writer.WriteEndArray();
                }
                writer.WriteEndObject();
            }
            writer.WriteEndObject();
        }
    }

    private static void SerializeObjectEditorSettings(ObjectEditorSettings editorSettings, Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        {
            if (editorSettings.Locked)
                writer.WriteString("locked", "True");
            if (editorSettings.Collapsed)
                writer.WriteString("shrink", "True");
            writer.WriteString("bin", editorSettings.Bin.ToString(CultureInfo.InvariantCulture));
            writer.WriteString("layer", editorSettings.Layer.ToString(CultureInfo.InvariantCulture));
            writer.WriteEndObject();
        }
    }

    private static void SerializeVector2Keyframe(Keyframe<Vector2> keyframe, Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        {
            writer.WriteString("t", keyframe.Time.ToString(CultureInfo.InvariantCulture));
            writer.WriteString("x", keyframe.Value.X.ToString(CultureInfo.InvariantCulture));
            writer.WriteString("y", keyframe.Value.Y.ToString(CultureInfo.InvariantCulture));
            if (keyframe.Ease != Ease.Linear)
                writer.WriteString("ct", keyframe.Ease.ToString());
            if (keyframe.RandomMode != RandomMode.None)
            {
                writer.WriteString("r", keyframe.RandomMode switch
                {
                    RandomMode.Range => "1",
                    RandomMode.Select => "3",
                    RandomMode.Scale => "4", 
                    _ => "0"
                });
                writer.WriteString("rx", keyframe.RandomValue.X.ToString(CultureInfo.InvariantCulture));
                writer.WriteString("ry", keyframe.RandomValue.Y.ToString(CultureInfo.InvariantCulture));
                writer.WriteString("rz", keyframe.RandomInterval.ToString(CultureInfo.InvariantCulture));
            }
            writer.WriteEndObject();
        }
    }
    
    private static void SerializeFloatKeyframe(Keyframe<float> keyframe, Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        {
            writer.WriteString("t", keyframe.Time.ToString(CultureInfo.InvariantCulture));
            writer.WriteString("x", keyframe.Value.ToString(CultureInfo.InvariantCulture));
            if (keyframe.Ease != Ease.Linear)
                writer.WriteString("ct", keyframe.Ease.ToString());
            if (keyframe.RandomMode != RandomMode.None)
            {
                writer.WriteString("r", keyframe.RandomMode switch
                {
                    RandomMode.Range => "1",
                    RandomMode.Select => "3",
                    RandomMode.Scale => "4", 
                    _ => "0"
                });
                writer.WriteString("rx", keyframe.RandomValue.ToString(CultureInfo.InvariantCulture));
                writer.WriteString("rz", keyframe.RandomInterval.ToString(CultureInfo.InvariantCulture));
            }
            writer.WriteEndObject();
        }
    }
    
    private static void SerializeThemeColorKeyframe(FixedKeyframe<ThemeColor> keyframe, Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        {
            writer.WriteString("t", keyframe.Time.ToString(CultureInfo.InvariantCulture));
            writer.WriteString("x", keyframe.Value.Index.ToString());
            if (keyframe.Ease != Ease.Linear)
                writer.WriteString("ct", keyframe.Ease.ToString());
            writer.WriteEndObject();
        }
    }
    
    private static void WriteId(this Utf8JsonWriter writer, string name, object? value, bool require = false)
    {
        if (value is not IIdentifiable<string> && require)
            throw new ArgumentException($"{value?.GetType()} is not identifiable, but an id is required");

        if (value is IIdentifiable<string> identifiable)
            writer.WriteString(name, identifiable.Id);
    }
    
    private static void WriteThemeId(this Utf8JsonWriter writer, string name, object? value, bool require = false)
    {
        if (value is not IIdentifiable<int> && require)
            throw new ArgumentException($"{value?.GetType()} is not identifiable, but an id is required");

        if (value is IIdentifiable<int> identifiable)
            writer.WriteString(name, identifiable.Id.ToString());
    }
    
    private static string ToHex(this Color color)
        => $"{color.R:X2}{color.G:X2}{color.B:X2}";
}