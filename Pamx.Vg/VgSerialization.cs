using System.Drawing;
using System.Numerics;
using System.Text.Json.Nodes;
using Pamx.Common;
using Pamx.Common.Data;
using Pamx.Common.Enum;

namespace Pamx.Vg;

public static class VgSerialization
{
    private delegate JsonArray WriteKeyframeValueDelegate<in T>(T value);
    private delegate JsonArray WriteRandomKeyframeValueDelegate<in T>(T value, float interval);

    public static JsonObject SerializeBeatmap(IBeatmap beatmap)
    {
        var json = new JsonObject();
        
        // Write editor settings
        if (beatmap.EditorSettings != default)
            json.Add("editor", SerializeEditorSettings(beatmap.EditorSettings));
        
        // Write triggers
        if (beatmap.Triggers.Count > 0)
            json.Add("triggers", new JsonArray(
                beatmap.Triggers
                    .Select(SerializeTrigger)
                    .Cast<JsonNode>()
                    .ToArray()));
        
        // Write markers
        if (beatmap.Markers.Count > 0)
            json.Add("markers", new JsonArray(
                beatmap.Markers
                    .Select(SerializeMarker)
                    .Cast<JsonNode>()
                    .ToArray()));
        
        // Write checkpoints
        if (beatmap.Checkpoints.Count > 0)
            json.Add("checkpoints", new JsonArray(
                beatmap.Checkpoints
                    .Select(SerializeCheckpoint)
                    .Cast<JsonNode>()
                    .ToArray()));
        
        // Write editor prefab spawns
        if (beatmap.PrefabSpawns.Count > 0)
            json.Add("editor_prefab_spawn", new JsonArray(
                beatmap.PrefabSpawns
                    .Select(SerializeEditorPrefabSpawn)
                    .Cast<JsonNode>()
                    .ToArray()));
        
        // Write parallax
        json.Add("parallax_settings", SerializeParallax(beatmap.Parallax));
        
        // Write themes
        if (beatmap.Themes.Count > 0)
            json.Add("themes", new JsonArray(
                beatmap.Themes
                    .Select(x => SerializeTheme(x, true))
                    .Cast<JsonNode>()
                    .ToArray()));
        
        // Write prefabs
        if (beatmap.Prefabs.Count > 0)
            json.Add("prefabs", new JsonArray(
                beatmap.Prefabs
                    .Select(x => SerializePrefab(x, true))
                    .Cast<JsonNode>()
                    .ToArray()));
        
        // Write prefab objects
        if (beatmap.PrefabObjects.Count > 0)
            json.Add("prefab_objects", new JsonArray(
                beatmap.PrefabObjects
                    .Select(SerializePrefabObject)
                    .Cast<JsonNode>()
                    .ToArray()));
        
        // Write objects
        if (beatmap.Objects.Count > 0)
            json.Add("objects", new JsonArray(
                beatmap.Objects
                    .Select(SerializeObject)
                    .Cast<JsonNode>()
                    .ToArray()));
        
        // Write events
        var events = beatmap.Events;
        json.Add("events", new JsonArray
        {
            // Write movement events
            SerializeArray(
                events.Movement,
                GetFixedKeyframeSerializer<Vector2>(x => [x.X, x.Y])),
            
            // Write zoom events
            SerializeArray(
                events.Zoom,
                GetFixedKeyframeSerializer<float>(x => [x])),
            
            // Write rotation events
            SerializeArray(
                events.Rotation,
                GetFixedKeyframeSerializer<float>(x => [x])),
            
            // Write shake events
            SerializeArray(
                events.Shake,
                GetFixedKeyframeSerializer<float>(x => [x])),
            
            // Write theme events
            SerializeArray(
                events.Theme,
                x =>
                {
                    var json = new JsonObject();
                    if (x.Time != 0.0f)
                        json.Add("t", x.Time);
                    var themeId = x.Value is IIdentifiable<string> identifiable 
                        ? identifiable.Id 
                        : throw new ArgumentException("Can not determine theme id for theme keyframe value");
                    json.Add("evs", new JsonArray
                    {
                        themeId
                    });
                    if (x.Ease != Ease.Linear)
                        json.Add("ct", x.Ease.ToString());
                    return json;
                }),
            
            // Write chroma events
            SerializeArray(
                events.Chroma,
                GetFixedKeyframeSerializer<float>(x => [x])),
            
            // Write bloom events
            SerializeArray(
                events.Bloom,
                GetFixedKeyframeSerializer<BloomData>(x => [x.Intensity, x.Diffusion, (float) x.Color])),
            
            // Write vignette events
            SerializeArray(
                events.Vignette,
                GetFixedKeyframeSerializer<VignetteData>(x => [
                    x.Intensity, 
                    x.Smoothness,
                    x.Rounded ? 1.0f : 0.0f,
                    0.0f, // ???
                    x.Center.X,
                    x.Center.Y,
                    (float?) x.Color ?? 0.0f])),
            
            // Write lens distortion events
            SerializeArray(
                events.LensDistortion,
                GetFixedKeyframeSerializer<LensDistortionData>(x => [
                    x.Intensity,
                    x.Center.X,
                    x.Center.Y])),
            
            // Write grain events
            SerializeArray(
                events.Grain,
                GetFixedKeyframeSerializer<GrainData>(x => [
                    x.Intensity,
                    x.Size,
                    x.Mix])),
            
            // Write gradient events
            SerializeArray(
                events.Gradient,
                GetFixedKeyframeSerializer<GradientData>(x => [
                    x.Intensity,
                    x.Rotation,
                    (float) x.ColorA,
                    (float) x.ColorB,
                    x.Mode switch
                    {
                        GradientOverlayMode.Linear => 0.0f,
                        GradientOverlayMode.Additive => 1.0f,
                        GradientOverlayMode.Multiply => 2.0f,
                        GradientOverlayMode.Screen => 3.0f,
                        _ => throw new ArgumentOutOfRangeException()
                    }])),
            
            // Write glitch events
            SerializeArray(
                events.Glitch,
                GetFixedKeyframeSerializer<GlitchData>(x => [
                    x.Intensity,
                    x.Speed,
                    x.Width])),
            
            // Write hue events
            SerializeArray(
                events.Hue,
                GetFixedKeyframeSerializer<float>(x => [x])),
            
            // Write player events
            SerializeArray(
                events.Player,
                GetFixedKeyframeSerializer<Vector2>(x => [x.X, x.Y])),
        });
        return json;
    }
    
    private static JsonObject SerializeEditorPrefabSpawn(EditorPrefabSpawn editorPrefabSpawn)
    {
        var json = new JsonObject();
        if (editorPrefabSpawn.Expanded)
            json.Add("expanded", true);
        if (editorPrefabSpawn.Active)
            json.Add("active", true);
        json.AddId("prefab", editorPrefabSpawn.Prefab);
        json.Add("keycodes", new JsonArray(
            editorPrefabSpawn.Keycodes
                .Select(x => (JsonNode) x)
                .ToArray()));
        return json;
    }
    
    private static JsonObject SerializeTrigger(Trigger trigger)
    {
        var json = new JsonObject();
        if (trigger.Type != TriggerType.Time)
            json.Add("event_trigger", trigger.Type switch
            {
                TriggerType.Time => 0,
                TriggerType.PlayerHit => 1,
                TriggerType.PlayerDeath => 2,
                TriggerType.PlayerStart => 3,
                _ => throw new ArgumentOutOfRangeException()
            });
        json.AddVector2("event_trigger_time", new Vector2(trigger.From, trigger.To));
        if (trigger.Retrigger != 0)
            json.Add("event_retrigger", trigger.Retrigger);
        if (trigger.EventType != EventType.VnInk)
            json.Add("event_type", trigger.EventType switch
            {
                EventType.VnInk => 0,
                EventType.VnTimeline => 1,
                EventType.PlayerBubble => 2,
                EventType.PlayerLocation => 3,
                EventType.PlayerDash => 4,
                EventType.PlayerXMovement => 5,
                EventType.PlayerYMovement => 6,
                EventType.BgSpin => 7,
                EventType.BgMove => 8,
                EventType.PlayerDashDirection => 9,
                _ => throw new ArgumentOutOfRangeException()
            });
        json.Add("event_data", new JsonArray(
            trigger.Data
                .Select(x => (JsonNode) x)
                .ToArray()));
        return json;
    }
    
    private static JsonObject SerializeMarker(IMarker marker)
    {
        var json = new JsonObject();
        json.AddId("ID", marker);
        if (!string.IsNullOrEmpty(marker.Name))
            json.Add("n", marker.Name);
        if (!string.IsNullOrEmpty(marker.Description))
            json.Add("d", marker.Description);
        if (marker.Color != 0)
            json.Add("c", marker.Color);
        if (marker.Time != 0.0f)
            json.Add("t", marker.Time);
        return json;
    }
    
    private static JsonObject SerializeCheckpoint(ICheckpoint checkpoint)
    {
        var json = new JsonObject();
        json.AddId("ID", checkpoint);
        if (!string.IsNullOrEmpty(checkpoint.Name))
            json.Add("n", checkpoint.Name);
        if (checkpoint.Time != 0.0f)
            json.Add("t", checkpoint.Time);
        json.Add("p", SerializeVector2(checkpoint.Position));
        return json;
    }
    
    private static JsonObject SerializeParallax(IParallax parallax)
    {
        var json = new JsonObject();
        if (parallax.DepthOfField.HasValue)
        {
            json.Add("dof_active", true);
            json.Add("dof_value", parallax.DepthOfField.Value);
        }
        if (parallax.Layers.Count > 0)
            json.Add("l", new JsonArray(
                parallax.Layers
                    .Select(SerializeParallaxLayer)
                    .Cast<JsonNode>()
                    .ToArray()));
        return json;
    }
    
    private static JsonObject SerializeParallaxLayer(IParallaxLayer parallaxLayer)
    {
        var json = new JsonObject();
        if (parallaxLayer.Depth != 0)
            json.Add("d", parallaxLayer.Depth);
        if (parallaxLayer.Color != 0)
            json.Add("c", parallaxLayer.Color);
        if (parallaxLayer.Objects.Count > 0)
            json.Add("o", new JsonArray(
                parallaxLayer.Objects
                    .Select(SerializeParallaxObject)
                    .Cast<JsonNode>()
                    .ToArray()));
        return json;
    }
    
    private static JsonObject SerializeParallaxObject(IParallaxObject parallaxObject)
    {
        var json = new JsonObject();
        json.AddId("id", parallaxObject, true);
        json.Add("t", new JsonObject
        {
            ["p"] = SerializeVector2(parallaxObject.Position),
            ["s"] = SerializeVector2(parallaxObject.Scale),
            ["r"] = parallaxObject.Rotation,
        });
        var anJson = new JsonObject();
        var animation = parallaxObject.Animation;
        if (animation.Position.HasValue)
        {
            anJson.Add("ap", true);
            anJson.Add("p", SerializeVector2(animation.Position.Value));
        }
        if (animation.Scale.HasValue)
        {
            anJson.Add("as", true);
            anJson.Add("s", SerializeVector2(animation.Scale.Value));
        }
        if (animation.Rotation.HasValue)
        {
            anJson.Add("ar", true);
            anJson.Add("r", animation.Rotation.Value);
        }
        anJson.Add("l", animation.LoopLength);
        anJson.Add("ld", animation.LoopDelay);
        json.Add("an", anJson);

        var shape = (int) parallaxObject.Shape & 0xffff;
        var shapeOption = (int) parallaxObject.Shape >> 16;
        
        json.Add("s", new JsonObject
        {
            ["s"] = shape,
            ["so"] = shapeOption,
            ["t"] = parallaxObject.Text,
            ["c"] = parallaxObject.Color,
        });
        return json;
    }
    
    private static JsonObject SerializeEditorSettings(EditorSettings value)
        => new()
        {
            ["bpm"] = new JsonObject
            {
                ["snap"] = new JsonObject
                {
                    ["objects"] = value.Bpm.Snap.HasFlag(BpmSnap.Objects),
                    ["checkpoints"] = value.Bpm.Snap.HasFlag(BpmSnap.Checkpoints),
                },
                ["bpm_value"] = value.Bpm.Value,
                ["bpm_offset"] = value.Bpm.Offset,
                ["BPMValue"] = value.Bpm.Value, // ???
            },
            ["grid"] = new JsonObject
            {
                ["scale"] = SerializeVector2(value.Grid.Scale),
                ["thickness"] = value.Grid.Thickness,
                ["color"] = value.Grid.Color,
            },
            ["general"] = new JsonObject
            {
                ["collapse_length"] = value.General.CollapseLength,
                ["complexity"] = value.General.Complexity,
                ["theme"] = value.General.Theme,
                ["text_select_objects"] = value.General.SelectTextObjects,
                ["text_select_backgrounds"] = value.General.SelectParallaxTextObjects,
            },
            ["preview"] = new JsonObject
            {
                ["cam_zoom_offset"] = value.Preview.CameraZoomOffset,
                ["cam_zoom_offset_color"] = value.Preview.CameraZoomOffsetColor,
            },
            ["autosave"] = new JsonObject
            {
                ["as_max"] = value.AutoSave.Max,
                ["as_interval"] = value.AutoSave.Interval,
            },
        };
    
    private static JsonObject SerializePrefabObject(IPrefabObject prefabObject)
    {
        var json = new JsonObject();
        json.AddId("id", prefabObject, true);
        json.AddId("pid", prefabObject.Prefab, true);
        if (prefabObject.EditorSettings != default)
            json.Add("ed", SerializeObjectEditorSettings(prefabObject.EditorSettings));
        if (prefabObject.Time != 0.0f)
            json.Add("t", prefabObject.Time);
        json.Add("e", new JsonArray
        {
            new JsonObject
            {
                ["ev"] = new JsonArray
                {
                    prefabObject.Position.X,
                    prefabObject.Position.Y,
                },
            },
            new JsonObject
            {
                ["ev"] = new JsonArray
                {
                    prefabObject.Scale.X,
                    prefabObject.Scale.Y,
                },
            },
            new JsonObject
            {
                ["ev"] = new JsonArray
                {
                    prefabObject.Rotation,
                },
            },
        });
        return json;
    }
    
    public static JsonObject SerializeTheme(ITheme theme, bool requiresId = false)
    {
        var json = new JsonObject();
        json.AddId("id", theme, requiresId);
        if (!string.IsNullOrEmpty(theme.Name))
            json.Add("n", theme.Name);
        json.Add("pla", new JsonArray(
            theme.Player
                .Select(x => (JsonNode) x.ToHex())
                .ToArray()));
        json.Add("obj", new JsonArray(
            theme.Object
                .Select(x => (JsonNode) x.ToHex())
                .ToArray()));
        json.Add("fx", new JsonArray(
            theme.Effect
                .Select(x => (JsonNode) x.ToHex())
                .ToArray()));
        json.Add("bg", new JsonArray(
            theme.ParallaxObject
                .Select(x => (JsonNode) x.ToHex())
                .ToArray()));
        json.Add("base_bg", theme.Background.ToHex());
        json.Add("base_gui", theme.Gui.ToHex());
        json.Add("base_gui_accent", theme.GuiAccent.ToHex());
        return json;
    }
    
    public static JsonObject SerializePrefab(IPrefab prefab, bool requiresId = false)
    {
        var json = new JsonObject();
        json.AddId("id", prefab, requiresId);
        
        if (!string.IsNullOrEmpty(prefab.Name))
            json.Add("n", prefab.Name);
        if (!string.IsNullOrEmpty(prefab.Description))
            json.Add("description", prefab.Description);
        if (!string.IsNullOrEmpty(prefab.Preview))
            json.Add("preview", prefab.Preview);
        if (prefab.Offset != 0.0f)
            json.Add("o", prefab.Offset);
        json.Add("type", prefab.Type switch
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
        json.Add("objs", new JsonArray(
            prefab.BeatmapObjects
                .Select(SerializeObject)
                .Cast<JsonNode>()
                .ToArray()));
        return json;
    }
    
    private static JsonObject SerializeObject(IObject @object)
    {
        var json = new JsonObject();
        json.AddId("id", @object, true);
        json.AddId("p_id", @object.Parent);
        if (@object.AutoKillType != AutoKillType.NoAutoKill)
            json.Add("ak_t", @object.AutoKillType switch
            {
                AutoKillType.NoAutoKill => 0,
                AutoKillType.LastKeyframe => 1,
                AutoKillType.LastKeyframeOffset => 2,
                AutoKillType.FixedTime => 3,
                AutoKillType.SongTime => 4,
                _ => throw new ArgumentOutOfRangeException()
            });
        if (@object.AutoKillOffset != 0.0f)
            json.Add("ak_o", @object.AutoKillOffset);
        if (@object.Type != ObjectType.LegacyNormal)
            json.Add("ot", @object.Type switch
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
            json.Add("n", @object.Name);
        if (!string.IsNullOrEmpty(@object.Text))
            json.Add("text", @object.Text);
        if (@object.Origin != default)
            json.AddVector2("o", @object.Origin);
        
        var shape = (int) @object.Shape & 0xffff;
        var shapeOption = (int) @object.Shape >> 16;
        
        if (shape != 0)
            json.Add("s", shape);
        if (shapeOption != 0)
            json.Add("so", shapeOption);
        
        if (@object.RenderType != RenderType.Normal)
            json.Add("gt", @object.RenderType switch
            {
                RenderType.Normal => 0,
                RenderType.RightToLeftGradient => 1,
                RenderType.LeftToRightGradient => 2,
                RenderType.InwardsGradient => 3,
                RenderType.OutwardsGradient => 4,
                _ => throw new ArgumentOutOfRangeException()
            });
        if (@object.ParentType != (ParentType.Position | ParentType.Rotation))
            json.Add("p_t",
                (@object.ParentType.HasFlag(ParentType.Position) ? "1" : "0") +
                (@object.ParentType.HasFlag(ParentType.Scale) ? "1" : "0") +
                (@object.ParentType.HasFlag(ParentType.Rotation) ? "1" : "0"));
        json.Add("p_o", new JsonArray
        {
            @object.ParentOffset.Position,
            @object.ParentOffset.Scale,
            @object.ParentOffset.Rotation,
        });
        if (@object.RenderDepth != 20)
            json.Add("d", @object.RenderDepth);
        if (@object.StartTime != 0.0f)
            json.Add("st", @object.StartTime);
        if (@object.EditorSettings != default)
            json.Add("ed", SerializeObjectEditorSettings(@object.EditorSettings));
        json.Add("e", new JsonArray
        {
            SerializeObjectEventsArray(
                @object.PositionEvents, 
                GetKeyframeSerializer<Vector2>(
                    x => [x.X, x.Y],
                    (x, i) => [x.X, x.Y, i])),
            SerializeObjectEventsArray(
                @object.ScaleEvents, 
                GetKeyframeSerializer<Vector2>(
                    x => [x.X, x.Y],
                    (x, i) => [x.X, x.Y, i])),
            SerializeObjectEventsArray(
                @object.RotationEvents,
                GetKeyframeSerializer<float>(
                    x => [x],
                    (x, i) => [x, 0.0f, i])), // Don't know what the second value is for, but the game blows up if it's not there
            SerializeObjectEventsArray(
                @object.ColorEvents,
                GetFixedKeyframeSerializer<ThemeColor>(x => [x.Index, x.Opacity * 100.0f, x.EndIndex])),
        });
        return json;
    }

    private static JsonObject SerializeObjectEditorSettings(ObjectEditorSettings value)
    {
        var json = new JsonObject();
        if (value.Locked)
            json["lk"] = true;
        if (value.Collapsed)
            json["co"] = true;
        if (value.TextColor != ObjectTimelineColor.None)
            json["tc"] = SerializeObjectTimelineColor(value.TextColor);
        if (value.BackgroundColor != ObjectTimelineColor.None)
            json["bgc"] = SerializeObjectTimelineColor(value.BackgroundColor);
        if (value.Bin != 0)
            json["bin"] = value.Bin;
        if (value.Layer != 0)
            json["layer"] = value.Layer;
        return json;
    }
    
    private static Func<FixedKeyframe<T>, JsonObject> GetFixedKeyframeSerializer<T>(
        WriteKeyframeValueDelegate<T> writeValueCallback)
        => keyframe => SerializeFixedKeyframe(keyframe, writeValueCallback);
    
    private static Func<Keyframe<T>, JsonObject> GetKeyframeSerializer<T>(
        WriteKeyframeValueDelegate<T> writeValueCallback,
        WriteRandomKeyframeValueDelegate<T> writeRandomValueCallback)
        => keyframe => SerializeKeyframe(keyframe, writeValueCallback, writeRandomValueCallback);
    
    private static JsonObject SerializeFixedKeyframe<T>(
        FixedKeyframe<T> keyframe,
        WriteKeyframeValueDelegate<T> writeValueCallback)
    {
        var json = new JsonObject();
        if (keyframe.Time != 0.0f)
            json.Add("t", keyframe.Time);
        json.Add("ev", writeValueCallback(keyframe.Value));
        if (keyframe.Ease != Ease.Linear)
            json.Add("ct", keyframe.Ease.ToString());
        return json;
    }

    private static JsonObject SerializeKeyframe<T>(
        Keyframe<T> keyframe,
        WriteKeyframeValueDelegate<T> writeValueCallback,
        WriteRandomKeyframeValueDelegate<T> writeRandomValueCallback)
    {
        var json = new JsonObject();
        if (keyframe.Time != 0.0f)
            json.Add("t", keyframe.Time);
        json.Add("ev", writeValueCallback(keyframe.Value));
        if (keyframe.Ease != Ease.Linear)
            json.Add("ct", keyframe.Ease.ToString());
        if (keyframe.RandomMode != RandomMode.None)
            json.Add("r", keyframe.RandomMode switch
            {
                RandomMode.None => 0,
                RandomMode.Range => 1,
                RandomMode.Snap => 2,
                RandomMode.Select => 3,
                RandomMode.Scale => 4,
                _ => throw new ArgumentOutOfRangeException(),
            });
        if (keyframe.RandomValue is not null)
            json.Add("er", writeRandomValueCallback(keyframe.RandomValue, keyframe.RandomInterval));
        return json;
    }
    
    private static JsonObject SerializeObjectEventsArray<T>(
        IEnumerable<T> values,
        Func<T, JsonObject> writeValueCallback) => 
        new()
        {
            ["k"] = SerializeArray(values, writeValueCallback)
        };
    
    private static JsonArray SerializeArray<T>(
        IEnumerable<T> values, 
        Func<T, JsonObject> writeValueCallback)
        => new(values
            .Select(writeValueCallback)
            .Cast<JsonNode>()
            .ToArray());
    
    private static JsonObject SerializeObjectTimelineColor(ObjectTimelineColor value)
    {
        var json = new JsonObject();
        if (value.HasFlag(ObjectTimelineColor.Red))
            json.Add("r", true);
        if (value.HasFlag(ObjectTimelineColor.Green))
            json.Add("g", true);
        if (value.HasFlag(ObjectTimelineColor.Blue))
            json.Add("b", true);
        return json;
    }
    
    private static void AddVector2(this JsonObject json, string name, Vector2 value)
        => json.Add(name, SerializeVector2(value));
    
    private static JsonObject SerializeVector2(Vector2 value)
        => new()
        {
            ["x"] = value.X,
            ["y"] = value.Y,
        };
    
    private static void AddId(this JsonObject json, string name, object? value, bool require = false)
    {
        if (value is not IIdentifiable<string> && require)
            throw new ArgumentException($"{value?.GetType()} is not identifiable, but an id is required");

        if (value is IIdentifiable<string> identifiable && !string.IsNullOrEmpty(identifiable.Id))
            json.Add(name, identifiable.Id);
    }
    
    private static string ToHex(this Color color)
        => $"{color.R:X2}{color.G:X2}{color.B:X2}";
}