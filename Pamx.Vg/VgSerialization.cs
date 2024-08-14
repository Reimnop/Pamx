using System.Numerics;
using System.Text.Json;
using Pamx.Common;
using Pamx.Common.Data;
using Pamx.Common.Enum;

namespace Pamx.Vg;

public static class VgSerialization
{
    public static void SerializePrefab(IPrefab prefab, Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        {
            if (prefab is IIdentifiable<string> identifiable)
                writer.WriteString("id", identifiable.Id);
            
            if (!string.IsNullOrEmpty(prefab.Name))
                writer.WriteString("n", prefab.Name);
            
            if (!string.IsNullOrEmpty(prefab.Description))
                writer.WriteString("description", prefab.Description);
            
            if (!string.IsNullOrEmpty(prefab.Preview))
                writer.WriteString("preview", prefab.Preview);
            
            if (prefab.Offset != 0.0f)
                writer.WriteNumber("o", prefab.Offset);
            
            writer.WriteStartArray("objs");
            {
                foreach (var @object in prefab.BeatmapObjects)
                    SerializeObject(@object, writer);
                
                writer.WriteEndArray();
            }
            
            writer.WriteNumber("type", prefab.Type switch
            {
                PrefabType.Character => 0,
                PrefabType.CharacterParts => 1,
                PrefabType.Props => 2,
                PrefabType.Bullets => 3,
                PrefabType.Pulses => 4,
                PrefabType.Bombs => 5,
                PrefabType.Spinners => 6,
                PrefabType.Beams => 7,
                PrefabType.Static => 8,
                PrefabType.Misc1 => 9,
                PrefabType.Misc2 => 10,
                PrefabType.Misc3 => 11,
                _ => throw new ArgumentOutOfRangeException()
            });
            
            writer.WriteEndObject();
        }
    }
    
    public static void SerializeObject(IObject @object, Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        {
            if (@object is IIdentifiable<string> identifiable)
                writer.WriteString("id", identifiable.Id);
            
            if (@object.Parent is IIdentifiable<string> parentIdentifiable)
                writer.WriteString("p_id", parentIdentifiable.Id);
            
            writer.WriteAutoKillType("ak_t", @object.AutoKillType);
            writer.WriteNumber("ak_o", @object.AutoKillOffset);
            writer.WriteNumber("ot", @object.Type switch
            {
                ObjectType.LegacyNormal => 0,
                ObjectType.LegacyHelper => 1,
                ObjectType.LegacyDecoration => 2,
                ObjectType.LegacyEmpty => 3,
                ObjectType.Hit => 4,
                ObjectType.NoHit => 5,
                ObjectType.Empty => 6,
                _ => throw new ArgumentOutOfRangeException()
            });
            if (!string.IsNullOrEmpty(@object.Name))
                writer.WriteString("n", @object.Name);
            if (!string.IsNullOrEmpty(@object.Text))
                writer.WriteString("text", @object.Text);
            if (@object.Origin != default)
                writer.WriteVector2("o", @object.Origin);
            writer.WriteNumber("s", @object.Shape switch
            {
                ObjectShape.Square => 0,
                ObjectShape.Circle => 1,
                ObjectShape.Triangle => 2,
                ObjectShape.Arrow => 3,
                ObjectShape.Text => 4,
                ObjectShape.Hexagon => 5,
                _ => throw new ArgumentOutOfRangeException()
            });
            writer.WriteNumber("so", @object.ShapeOption);
            writer.WriteNumber("gt", @object.RenderType switch
            {
               RenderType.Normal => 0,
               RenderType.RightToLeftGradient => 1,
               RenderType.LeftToRightGradient => 2,
               RenderType.InwardsGradient => 3,
               RenderType.OutwardsGradient => 4,
               _ => throw new ArgumentOutOfRangeException()
            });
            if (@object.ParentType != (ParentType.Position | ParentType.Rotation))
                writer.WriteString("p_t", 
                    (@object.ParentType.HasFlag(ParentType.Position) ? "1" : "0") + 
                    (@object.ParentType.HasFlag(ParentType.Scale) ? "1" : "0") +
                    (@object.ParentType.HasFlag(ParentType.Rotation) ? "1" : "0"));
            writer.WriteStartArray("p_o");
            {
                writer.WriteNumberValue(@object.ParentOffset.Position);
                writer.WriteNumberValue(@object.ParentOffset.Scale);
                writer.WriteNumberValue(@object.ParentOffset.Rotation);
                writer.WriteEndArray();
            }
            if (@object.RenderDepth != 20)
                writer.WriteNumber("d", @object.RenderDepth);
            writer.WriteNumber("st", @object.StartTime);
            writer.WriteObjectEditorSettings("ed", @object.EditorSettings);
            
            writer.WriteStartArray("e");
            {
                // Write position keyframes
                writer.WriteStartObject();
                {
                    writer.WriteStartArray("k");
                    {
                        foreach (var keyframe in @object.PositionEvents)
                            writer.WriteKeyframe(keyframe, (w, v) =>
                            {
                                w.WriteNumberValue(v.X);
                                w.WriteNumberValue(v.Y);
                            });
                        writer.WriteEndArray();
                    }
                    writer.WriteEndObject();
                }
                
                // Write scale keyframes
                writer.WriteStartObject();
                {
                    writer.WriteStartArray("k");
                    {
                        foreach (var keyframe in @object.ScaleEvents)
                            writer.WriteKeyframe(keyframe, (w, v) =>
                            {
                                w.WriteNumberValue(v.X);
                                w.WriteNumberValue(v.Y);
                            });
                        writer.WriteEndArray();
                    }
                    writer.WriteEndObject();
                }
                
                // Write rotation keyframes
                writer.WriteStartObject();
                {
                    writer.WriteStartArray("k");
                    {
                        foreach (var keyframe in @object.RotationEvents)
                            writer.WriteKeyframe(
                                keyframe, 
                                (w, v) => w.WriteNumberValue(v), 
                                (w, v) =>
                                {
                                    w.WriteNumberValue(v);
                                    w.WriteNumberValue(0.0f); // ?????
                                });
                        writer.WriteEndArray();
                    }
                    writer.WriteEndObject();
                }
                
                // Write color keyframes
                writer.WriteStartObject();
                {
                    writer.WriteStartArray("k");
                    {
                        foreach (var keyframe in @object.ColorEvents)
                            writer.WriteFixedKeyframe(keyframe, (w, v) =>
                            {
                                // It's stored as float in the format, so we have to convert it to float
                                w.WriteNumberValue((float) v.Index);  
                                w.WriteNumberValue(v.Opacity * 100.0f); // It's stored as a percentage
                                w.WriteNumberValue((float) v.EndIndex);
                            });
                        writer.WriteEndArray();
                    }
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
            }
            
            writer.WriteEndObject();
        }
    }
    
    private static void WriteFixedKeyframe<T>(this Utf8JsonWriter writer, FixedKeyframe<T> keyframe, Action<Utf8JsonWriter, T> writeValueCallback)
    {
        writer.WriteStartObject();
        {
            if (keyframe.Time != 0.0f)
                writer.WriteNumber("t", keyframe.Time);
            
            writer.WriteStartArray("ev");
            {
                writeValueCallback(writer, keyframe.Value);
                writer.WriteEndArray();
            }
            writer.WriteEndObject();
        }
    }

    private static void WriteKeyframe<T>(
        this Utf8JsonWriter writer, 
        Keyframe<T> keyframe, 
        Action<Utf8JsonWriter, T> writeValueCallback, 
        Action<Utf8JsonWriter, T>? writeRandomValueCallback = null)
    {
        writer.WriteStartObject();
        {
            if (keyframe.Time != 0.0f)
                writer.WriteNumber("t", keyframe.Time);
            
            writer.WriteStartArray("ev");
            {
                writeValueCallback(writer, keyframe.Value);
                writer.WriteEndArray();
            }

            if (keyframe.RandomMode != RandomMode.None)
            {
                writer.WriteNumber("r", keyframe.RandomMode switch
                {
                    RandomMode.Range => 1,
                    RandomMode.Select => 2,
                    RandomMode.Scale => 3,
                    _ => throw new ArgumentOutOfRangeException()
                });
                writer.WriteStartArray("er");
                {
                    if (keyframe.RandomValue is not null)
                        (writeRandomValueCallback ?? writeValueCallback)(writer, keyframe.RandomValue);
                    if (keyframe.RandomInterval != 0.0f)
                        writer.WriteNumberValue(keyframe.RandomInterval);
                    writer.WriteEndArray();
                }
            }
            writer.WriteEndObject();
        }
    }

    private static void WriteObjectEditorSettings(this Utf8JsonWriter writer, string name, ObjectEditorSettings value)
    {
        writer.WriteStartObject(name);
        {
            if (value.Locked)
                writer.WriteBoolean("lk", value.Locked);
            if (value.Collapsed)
                writer.WriteBoolean("co", value.Collapsed);
            if (value.TextColor != ObjectTimelineColor.None)
                writer.WriteObjectTimelineColor("tc", value.TextColor);
            if (value.BackgroundColor != ObjectTimelineColor.None)
                writer.WriteObjectTimelineColor("bgc", value.BackgroundColor);
            if (value.Bin != 0)
                writer.WriteNumber("b", value.Bin);
            if (value.Layer != 0)
                writer.WriteNumber("l", value.Layer);
            writer.WriteEndObject();
        }
    }

    private static void WriteObjectTimelineColor(this Utf8JsonWriter writer, string name, ObjectTimelineColor value)
    {
        writer.WriteStartObject(name);
        {
            if (value.HasFlag(ObjectTimelineColor.Red))
                writer.WriteBoolean("r", true);
            if (value.HasFlag(ObjectTimelineColor.Green))
                writer.WriteBoolean("g", true);
            if (value.HasFlag(ObjectTimelineColor.Blue))
                writer.WriteBoolean("b", true);
            writer.WriteEndObject();
        }
    }

    private static void WriteVector2(this Utf8JsonWriter writer, string name, Vector2 value)
    {
        writer.WriteStartObject(name);
        {
            writer.WriteNumber("x", value.X);
            writer.WriteNumber("y", value.Y);
            writer.WriteEndObject();
        }
    }
    
    private static void WriteAutoKillType(this Utf8JsonWriter writer, string name, AutoKillType value)
    {
        writer.WriteNumber(name, value switch
        {
            AutoKillType.NoAutoKill => 0,
            AutoKillType.LastKeyframe => 1,
            AutoKillType.LastKeyframeOffset => 2,
            AutoKillType.FixedTime => 3,
            AutoKillType.SongTime => 4,
            _ => throw new ArgumentOutOfRangeException(nameof(value))
        });
    }
}