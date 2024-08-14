using System.Drawing;
using System.Numerics;
using System.Text.Json;
using Pamx.Common;
using Pamx.Common.Data;
using Pamx.Common.Enum;

namespace Pamx.Vg;

public static class VgSerialization
{
    public static void SerializeBeatmap(IBeatmap beatmap, Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        {
            writer.WriteEditorSettings("editor", beatmap.EditorSettings);
            writer.WriteStartArray("triggers");
            {
                foreach (var trigger in beatmap.Triggers)
                {
                    writer.WriteStartObject();
                    {
                        writer.WriteNumber("event_trigger", trigger.Type switch
                        {
                            TriggerType.Time => 0,
                            TriggerType.PlayerHit => 1,
                            TriggerType.PlayerDeath => 2,
                            TriggerType.PlayerStart => 3,
                            _ => throw new ArgumentOutOfRangeException()
                        });

                        var eventTriggerTime = new Vector2(trigger.From, trigger.To); // It's saved as a vector2 internally
                        writer.WriteVector2("event_trigger_time", eventTriggerTime);
                        
                        writer.WriteNumber("event_retrigger", trigger.Retrigger);
                        writer.WriteNumber("event_type", trigger.EventType switch
                        {
                            EventType.VnInk => 0,
                            EventType.VnTimeline => 1,
                            EventType.PlayerBubble => 2,
                            EventType.PlayerLocation => 3,
                            EventType.PlayerDash => 4,
                            EventType.PlayerXMovement => 5,
                            EventType.PlayerYMovement => 6,
                            EventType.PlayerDashDirection => 9,
                            EventType.BgSpin => 7,
                            EventType.BgMove => 8,
                            _ => throw new ArgumentOutOfRangeException()
                        });
                        
                        writer.WriteStartArray("event_data");
                        {
                            foreach (var data in trigger.Data)
                                writer.WriteStringValue(data);
                            writer.WriteEndArray();
                        }
                        
                        writer.WriteEndObject();
                    }
                }
                writer.WriteEndArray();
            }
            writer.WriteStartArray("editor_prefab_spawn");
            {
                foreach (var prefabSpawn in beatmap.PrefabSpawns)
                {
                    writer.WriteStartObject();
                    {
                        writer.WriteBoolean("expanded", prefabSpawn.Expanded);
                        writer.WriteBoolean("active", prefabSpawn.Active);
                        writer.WriteId("prefab", prefabSpawn.Prefab);
                        writer.WriteStartArray("keycodes");
                        {
                            foreach (var keycode in prefabSpawn.Keycodes)
                                writer.WriteStringValue(keycode);
                            writer.WriteEndArray();
                        }
                        writer.WriteEndObject();
                    }
                }
                writer.WriteEndArray();
            }
            writer.WriteParallax("parallax_settings", beatmap.Parallax);
            writer.WriteStartArray("checkpoints");
            {
                foreach (var checkpoint in beatmap.Checkpoints)
                {
                    writer.WriteStartObject();
                    {
                        writer.WriteId("ID", checkpoint, true);
                        if (!string.IsNullOrEmpty(checkpoint.Name))
                            writer.WriteString("n", checkpoint.Name);
                        if (checkpoint.Time != 0.0f)
                            writer.WriteNumber("t", checkpoint.Time);
                        if (checkpoint.Position != default)
                            writer.WriteVector2("p", checkpoint.Position);
                        writer.WriteEndObject();
                    }
                }
                writer.WriteEndArray();
            }
            writer.WriteStartArray("markers");
            {
                foreach (var marker in beatmap.Markers)
                {
                    writer.WriteStartObject();
                    {
                        writer.WriteId("ID", marker, true);
                        if (!string.IsNullOrEmpty(marker.Name))
                            writer.WriteString("n", marker.Name);
                        if (!string.IsNullOrEmpty(marker.Description))
                            writer.WriteString("d", marker.Description);
                        if (marker.Color != 0)
                            writer.WriteNumber("c", marker.Color);
                        if (marker.Time != 0.0f)
                            writer.WriteNumber("t", marker.Time);
                        writer.WriteEndObject();
                    }
                }
                writer.WriteEndArray();
            }
            writer.WriteStartArray("objects");
            {
                foreach (var beatmapObject in beatmap.Objects)
                    SerializeObject(beatmapObject, writer);
                writer.WriteEndArray();
            }
            writer.WriteStartArray("prefab_objects");
            {
                foreach (var prefabObject in beatmap.PrefabObjects)
                    SerializePrefabObject(prefabObject, writer);
                writer.WriteEndArray();
            }
            writer.WriteStartArray("prefabs");
            {
                foreach (var prefab in beatmap.Prefabs)
                    SerializePrefab(prefab, writer, true);
                writer.WriteEndArray();
            }
            writer.WriteStartArray("themes");
            {
                foreach (var theme in beatmap.Themes)
                    SerializeTheme(theme, writer, true);
                writer.WriteEndArray();
            }
            writer.WriteStartArray("events");
            {
                // Write movement events
                writer.WriteArray(beatmap.Events.Movement, (w, keyframe) =>
                {
                    w.WriteFixedKeyframe(keyframe, (w2, v) =>
                    {
                        w2.WriteNumberValue(v.X);
                        w2.WriteNumberValue(v.Y);
                    });
                });
                
                // Write zoom events
                writer.WriteArray(beatmap.Events.Zoom, (w, keyframe) =>
                {
                    w.WriteFixedKeyframe(keyframe, (w2, v) => w2.WriteNumberValue(v));
                });
                
                // Write rotation events
                writer.WriteArray(beatmap.Events.Rotation, (w, keyframe) =>
                {
                    w.WriteFixedKeyframe(keyframe, (w2, v) => w2.WriteNumberValue(v));
                });
                
                // Write shake events
                writer.WriteArray(beatmap.Events.Shake, (w, keyframe) =>
                {
                    w.WriteFixedKeyframe(keyframe, (w2, v) => w2.WriteNumberValue(v));
                });
                
                // Write theme events
                writer.WriteArray(beatmap.Events.Theme, (w, keyframe) =>
                {
                    w.WriteStartObject();
                    {
                        if (keyframe.Time != 0.0f)
                            w.WriteNumber("t", keyframe.Time);
                        if (keyframe.Ease != Ease.Linear)
                            w.WriteString("ct", keyframe.Ease.ToString());
                        if (keyframe.Value is not IIdentifiable<string> identifiable)
                            throw new ArgumentException("Can not determine theme id for theme keyframe value");
                        w.WriteStartArray("evs");
                        {
                            w.WriteStringValue(identifiable.Id);
                            w.WriteEndArray();
                        }
                        w.WriteEndObject();
                    }
                });
                
                // Write chroma events
                writer.WriteArray(beatmap.Events.Chroma, (w, keyframe) =>
                {
                    w.WriteFixedKeyframe(keyframe, (w2, v) => w2.WriteNumberValue(v));
                });
                
                // Write bloom events
                writer.WriteArray(beatmap.Events.Bloom, (w, keyframe) =>
                {
                    w.WriteFixedKeyframe(keyframe, (w2, v) =>
                    {
                        w2.WriteNumberValue(v.Intensity);
                        w2.WriteNumberValue(v.Diffusion);
                        w2.WriteNumberValue(v.Color);
                    });
                });
                
                // Write vignette events
                writer.WriteArray(beatmap.Events.Vignette, (w, keyframe) =>
                {
                    w.WriteFixedKeyframe(keyframe, (w2, v) =>
                    {
                        w2.WriteNumberValue(v.Intensity);
                        w2.WriteNumberValue(v.Smoothness);
                        w2.WriteNumberValue(v.Color);
                        w2.WriteNumberValue(v.Rounded ? 1.0f : 0.0f);
                        w2.WriteNumberValue(v.Center.X);
                        w2.WriteNumberValue(v.Center.Y);
                    });
                });
                
                // Write lens distortion events
                writer.WriteArray(beatmap.Events.LensDistortion, (w, keyframe) =>
                {
                    w.WriteFixedKeyframe(keyframe, (w2, v) =>
                    {
                        w2.WriteNumberValue(v.Intensity);
                        w2.WriteNumberValue(v.Center.X);
                        w2.WriteNumberValue(v.Center.Y);
                    });
                });
                
                // Write grain events
                writer.WriteArray(beatmap.Events.Grain, (w, keyframe) =>
                {
                    w.WriteFixedKeyframe(keyframe, (w2, v) =>
                    {
                        w2.WriteNumberValue(v.Intensity);
                        w2.WriteNumberValue(v.Size);
                        w2.WriteNumberValue(v.Mix);
                    });
                });
                
                // Write gradient events
                writer.WriteArray(beatmap.Events.Gradient, (w, keyframe) =>
                {
                    w.WriteFixedKeyframe(keyframe, (w2, v) =>
                    {
                        w2.WriteNumberValue(v.Intensity);
                        w2.WriteNumberValue(v.Rotation);
                        w2.WriteNumberValue(v.ColorA);
                        w2.WriteNumberValue(v.ColorB);
                        w2.WriteNumberValue(v.Mode switch
                        {
                            GradientOverlayMode.Linear => 0.0f,
                            GradientOverlayMode.Additive => 1.0f,
                            GradientOverlayMode.Multiply => 2.0f,
                            GradientOverlayMode.Screen => 3.0f,
                            _ => throw new ArgumentOutOfRangeException()
                        });
                    });
                });
                
                // Write glitch events
                writer.WriteArray(beatmap.Events.Glitch, (w, keyframe) =>
                {
                    w.WriteFixedKeyframe(keyframe, (w2, v) =>
                    {
                        w2.WriteNumberValue(v.Intensity);
                        w2.WriteNumberValue(v.Speed);
                        w2.WriteNumberValue(v.Width);
                    });
                });
                
                // Write hue events
                writer.WriteArray(beatmap.Events.Hue, (w, keyframe) =>
                {
                    w.WriteFixedKeyframe(keyframe, (w2, v) => w2.WriteNumberValue(v));
                });
                
                // Write player events
                writer.WriteArray(beatmap.Events.Player, (w, keyframe) =>
                {
                    w.WriteFixedKeyframe(keyframe, (w2, v) =>
                    {
                        w2.WriteNumberValue(v.X);
                        w2.WriteNumberValue(v.Y);
                    });
                });
                writer.WriteEndArray();
            }
            writer.WriteEndObject();
        }
    }
    
    public static void SerializePrefabObject(IPrefabObject prefabObject, Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        {
            writer.WriteId("id", prefabObject, true);
            writer.WriteId("pid", prefabObject.Prefab, true);
            writer.WriteObjectEditorSettings("ed", prefabObject.EditorSettings);
            writer.WriteNumber("t", prefabObject.Time);
            
            writer.WriteStartArray("e");
            {
                // Write position
                writer.WriteStartObject();
                {
                    writer.WriteStartArray("ev");
                    {
                        writer.WriteNumberValue(prefabObject.Position.X);
                        writer.WriteNumberValue(prefabObject.Position.Y);
                        writer.WriteEndArray();
                    }
                    writer.WriteEndObject();
                }
                
                // Write scale
                writer.WriteStartObject();
                {
                    writer.WriteStartArray("ev");
                    {
                        writer.WriteNumberValue(prefabObject.Scale.X);
                        writer.WriteNumberValue(prefabObject.Scale.Y);
                        writer.WriteEndArray();
                    }
                    writer.WriteEndObject();
                }
                
                // Write rotation
                writer.WriteStartObject();
                {
                    writer.WriteStartArray("ev");
                    {
                        writer.WriteNumberValue(prefabObject.Rotation);
                        writer.WriteEndArray();
                    }
                    writer.WriteEndObject();
                }
                
                writer.WriteEndArray();
            }
            
            writer.WriteEndObject();
        }
    }
    
    public static void SerializeTheme(ITheme theme, Utf8JsonWriter writer, bool requiresId = false)
    {
        writer.WriteStartObject();
        {
            writer.WriteId("id", theme, requiresId);
            writer.WriteString("name", theme.Name);
            
            writer.WriteStartArray("pla");
            {
                foreach (var color in theme.Player)
                    writer.WriteStringValue(color.ToHex());
                writer.WriteEndArray();
            }
            
            writer.WriteStartArray("obj");
            {
                foreach (var color in theme.Object)
                    writer.WriteStringValue(color.ToHex());
                writer.WriteEndArray();
            }
            
            writer.WriteStartArray("fx");
            {
                foreach (var color in theme.Effect)
                    writer.WriteStringValue(color.ToHex());
                writer.WriteEndArray();
            }
            
            writer.WriteStartArray("bg");
            {
                foreach (var color in theme.ParallaxObject)
                    writer.WriteStringValue(color.ToHex());
                writer.WriteEndArray();
            }
            
            writer.WriteString("base_bg", theme.Background.ToHex());
            writer.WriteString("gui", theme.Gui.ToHex());
            writer.WriteString("gui_accent", theme.GuiAccent.ToHex());
            
            writer.WriteEndObject();
        }
    }

    public static void SerializePrefab(IPrefab prefab, Utf8JsonWriter writer, bool requiresId = false)
    {
        writer.WriteStartObject();
        {
            writer.WriteId("id", prefab, requiresId);

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
            writer.WriteId("id", @object, true);
            writer.WriteId("p_id", @object.Parent);

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
                writer.WriteKeyframeArray(@object.PositionEvents, (w, v) =>
                {
                    w.WriteNumberValue(v.X);
                    w.WriteNumberValue(v.Y);
                });

                // Write scale keyframes
                writer.WriteKeyframeArray(@object.ScaleEvents, (w, v) =>
                {
                    w.WriteNumberValue(v.X);
                    w.WriteNumberValue(v.Y);
                });

                // Write rotation keyframes
                writer.WriteKeyframeArray(
                    @object.RotationEvents, 
                    (w, v) => w.WriteNumberValue(v),
                    (w, v) =>
                    {
                        w.WriteNumberValue(v);
                        w.WriteNumberValue(0.0f); // ?????
                    });

                // Write color keyframes
                writer.WriteFixedKeyframeArray(@object.ColorEvents, (w, v) =>
                {
                    w.WriteNumberValue(v.Index);
                    w.WriteNumberValue(v.Opacity * 100.0f); // It's stored as a percentage
                    w.WriteNumberValue(v.EndIndex);
                });
                writer.WriteEndArray();
            }

            writer.WriteEndObject();
        }
    }

    private static void WriteParallax(this Utf8JsonWriter writer, string name, IParallax parallax)
    {
        writer.WriteStartObject(name);
        {
            if (parallax.DepthOfField.HasValue)
            {
                writer.WriteBoolean("dof_active", true);
                writer.WriteNumber("dof_value", parallax.DepthOfField.Value);
            }
            
            writer.WriteStartArray("l");
            {
                foreach (var layer in parallax.Layers)
                {
                    writer.WriteStartObject();
                    {
                        writer.WriteNumber("d", layer.Depth);
                        writer.WriteNumber("c", layer.Color);
                        writer.WriteStartArray("o");
                        {
                            foreach (var parallaxObject in layer.Objects)
                            {
                                writer.WriteStartObject();
                                {
                                    writer.WriteId("id", parallaxObject, true);
                                    writer.WriteStartObject("t");
                                    {
                                        writer.WriteVector2("p", parallaxObject.Position);
                                        writer.WriteVector2("s", parallaxObject.Scale);
                                        writer.WriteNumber("r", parallaxObject.Rotation);
                                        writer.WriteEndObject();
                                    }
                                    writer.WriteStartObject("an");
                                    {
                                        var animation = parallaxObject.Animation;
                                        if (animation.Position.HasValue)
                                        {
                                            writer.WriteBoolean("ap", true);
                                            writer.WriteVector2("p", animation.Position.Value);
                                        }
                                        if (animation.Scale.HasValue)
                                        {
                                            writer.WriteBoolean("as", true);
                                            writer.WriteVector2("s", animation.Scale.Value);
                                        }
                                        if (animation.Rotation.HasValue)
                                        {
                                            writer.WriteBoolean("ar", true);
                                            writer.WriteNumber("r", animation.Rotation.Value);
                                        }
                                        if (animation.LoopLength != 1.0f)
                                            writer.WriteNumber("l", animation.LoopLength);
                                        if (animation.LoopDelay != 0.0f)
                                            writer.WriteNumber("ld", animation.LoopDelay);
                                        writer.WriteEndObject();
                                    }
                                    writer.WriteStartObject("s");
                                    {
                                        writer.WriteNumber("s", parallaxObject.Shape switch
                                        {
                                            ObjectShape.Square => 0,
                                            ObjectShape.Circle => 1,
                                            ObjectShape.Triangle => 2,
                                            ObjectShape.Arrow => 3,
                                            ObjectShape.Text => 4,
                                            ObjectShape.Hexagon => 5,
                                            _ => throw new ArgumentOutOfRangeException()
                                        });
                                        writer.WriteNumber("so", parallaxObject.ShapeOption);
                                        if (!string.IsNullOrEmpty(parallaxObject.Text))
                                            writer.WriteString("t", parallaxObject.Text);
                                        writer.WriteEndObject();
                                    }
                                    writer.WriteNumber("c", parallaxObject.Color);
                                    writer.WriteEndObject();
                                }
                            }
                            writer.WriteEndArray();
                        }
                        writer.WriteEndObject();
                    }
                }
                writer.WriteEndArray();
            }
            
            writer.WriteEndObject();
        }
    }

    private static void WriteEditorSettings(this Utf8JsonWriter writer, string name, EditorSettings value)
    {
        writer.WriteStartObject(name);
        {
            // Write bpm settings
            writer.WriteStartObject("bpm");
            {
                var bpm = value.Bpm;
                writer.WriteStartObject("snap");
                {
                    var snap = bpm.Snap;
                    writer.WriteBoolean("objects", snap.HasFlag(BpmSnap.Objects));
                    writer.WriteBoolean("checkpoints", snap.HasFlag(BpmSnap.Checkpoints));
                    writer.WriteEndObject();
                }
                writer.WriteNumber("bpm_value", bpm.Value);
                writer.WriteNumber("bpm_offset", bpm.Offset);
                writer.WriteNumber("BPMValue", bpm.Value); // TODO: WTF is this?????
                writer.WriteEndObject();
            }
            
            // Write grid settings
            writer.WriteStartObject("grid");
            {
                var grid = value.Grid;
                writer.WriteVector2("scale", grid.Scale);
                writer.WriteNumber("thickness", grid.Thickness);
                writer.WriteNumber("opacity", grid.Opacity);
                writer.WriteNumber("color", grid.Color);
                writer.WriteEndObject();
            }
            
            // Write general settings
            writer.WriteStartObject("general");
            {
                var general = value.General;
                writer.WriteNumber("collapse_length", general.CollapseLength);
                writer.WriteNumber("complexity", general.Complexity);
                writer.WriteNumber("theme", general.Theme);
                writer.WriteBoolean("text_select_objects", general.SelectTextObjects);
                writer.WriteBoolean("text_select_backgrounds", general.SelectParallaxTextObjects);
                writer.WriteEndObject();
            }
            
            // Write preview settings
            writer.WriteStartObject("preview");
            {
                var preview = value.Preview;
                writer.WriteNumber("cam_zoom_offset", preview.CameraZoomOffset);
                writer.WriteNumber("cam_zoom_offset_color", preview.CameraZoomOffsetColor);
                writer.WriteEndObject();
            }
            
            // Write auto save settings
            writer.WriteStartObject("autosave");
            {
                var autoSave = value.AutoSave;
                writer.WriteNumber("as_max", autoSave.Max);
                writer.WriteNumber("as_interval", autoSave.Interval);
                writer.WriteEndObject();
            }
            writer.WriteEndObject();
        }
    }
    
    private static void WriteFixedKeyframeArray<T>(
        this Utf8JsonWriter writer,
        IEnumerable<FixedKeyframe<T>> keyframes,
        Action<Utf8JsonWriter, T> writeValueCallback)
    {
        writer.WriteStartObject();
        {
            writer.WriteStartArray("k");
            {
                foreach (var keyframe in keyframes)
                    writer.WriteFixedKeyframe(keyframe, writeValueCallback);
                writer.WriteEndArray();
            }
            writer.WriteEndObject();
        }
    }

    private static void WriteKeyframeArray<T>(
        this Utf8JsonWriter writer,
        IEnumerable<Keyframe<T>> keyframes,
        Action<Utf8JsonWriter, T> writeValueCallback,
        Action<Utf8JsonWriter, T>? writeRandomValueCallback = null)
    {
        writer.WriteStartObject();
        {
            writer.WriteStartArray("k");
            {
                foreach (var keyframe in keyframes)
                    writer.WriteKeyframe(keyframe, writeValueCallback, writeRandomValueCallback);
                writer.WriteEndArray();
            }
            writer.WriteEndObject();
        }
    }

    private static void WriteFixedKeyframe<T>(
        this Utf8JsonWriter writer, 
        FixedKeyframe<T> keyframe,
        Action<Utf8JsonWriter, T> writeValueCallback)
    {
        writer.WriteStartObject();
        {
            if (keyframe.Time != 0.0f)
                writer.WriteNumber("t", keyframe.Time);
            
            if (keyframe.Ease != Ease.Linear)
                writer.WriteString("ct", keyframe.Ease.ToString());

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
            
            if (keyframe.Ease != Ease.Linear)
                writer.WriteString("ct", keyframe.Ease.ToString());

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

    private static void WriteId(this Utf8JsonWriter writer, string name, object? value, bool require = false)
    {
        if (value is not IIdentifiable<string> && require)
            throw new ArgumentException($"{value?.GetType()} is not identifiable, but an id is required");

        if (value is IIdentifiable<string> identifiable)
            writer.WriteString(name, identifiable.Id);
    }
    
    private static void WriteArray<T>(this Utf8JsonWriter writer, IEnumerable<T> items, Action<Utf8JsonWriter, T> writeCallback)
    {
        writer.WriteStartArray();
        {
            foreach (var item in items)
                writeCallback(writer, item);
            writer.WriteEndArray();
        }
    }
    
    private static string ToHex(this Color color)
        => $"{color.R:X2}{color.G:X2}{color.B:X2}";
}