using System.Drawing;
using System.Globalization;
using System.Numerics;
using System.Text.Json.Nodes;
using Pamx.Common;
using Pamx.Common.Data;
using Pamx.Common.Enum;
using Pamx.Common.Implementation;

namespace Pamx.Vg;

public static class VgDeserialization
{
    private delegate T ReadKeyframeValueDelegate<out T>(JsonArray eventValues);
    private delegate T ReadRandomKeyframeValueDelegate<out T>(JsonArray eventValues, out float interval);

    public static IBeatmap DeserializeBeatmap(JsonObject json)
    {
        // Read editor settings
        var editorSettings = DeserializeEditorSettings(json["editor"].Get<JsonObject>());
        
        // Read triggers
        var triggers = json["triggers"]
            .GetOrDefault<JsonArray>([])
            .Cast<JsonObject>()
            .Select(DeserializeTrigger);
        
        // Read objects
        var objectLookup = json["objects"]
            .GetOrDefault<JsonArray>([])
            .Cast<JsonObject>()
            .Select(o => (DeserializeBeatmapObject(o, out var parentId), parentId))
            .ToDictionary(
                t => ((IIdentifiable<string>) t.Item1).Id,
                t => t);
        
        // Assign parents to objects
        foreach (var (@object, parentId) in objectLookup.Values)
        {
            if (string.IsNullOrEmpty(parentId))
                continue;
            if (objectLookup.TryGetValue(parentId, out var tuple))
            {
                @object.Parent = tuple.Item1;
                continue;
            }
            @object.Parent = new VgReferenceObject(parentId);
        }
        
        // Read prefabs
        var prefabLookup = json["prefabs"]
            .GetOrDefault<JsonArray>([])
            .Cast<JsonObject>()
            .Select(p =>
            {
                var prefab = DeserializePrefab(
                    p,
                    id => objectLookup.TryGetValue(id, out var tuple) ? tuple.Item1 : null,
                    true);
                return (((IIdentifiable<string>) prefab).Id, prefab);
            })
            .ToDictionary(x => x.Id, x => x);
        
        // Read prefab objects
        var prefabObjects = json["prefab_objects"]
            .GetOrDefault<JsonArray>([])
            .Cast<JsonObject>()
            .Select(o => DeserializePrefabObject(o, id => prefabLookup[id].prefab));
        
        // Read editor prefab spawns
        var editorPrefabSpawns = json["editor_prefab_spawn"]
            .GetOrDefault<JsonArray>([])
            .Cast<JsonObject>()
            .Select(p => DeserializeEditorPrefabSpawn(p, 
                id => prefabLookup.TryGetValue(id, out var tuple) ? tuple.prefab : null));
        
        // Read parallax
        var parallax = DeserializeParallax(json["parallax_settings"].Get<JsonObject>());
        
        // Read checkpoints
        var checkpoints = json["checkpoints"]
            .GetOrDefault<JsonArray>([])
            .Cast<JsonObject>()
            .Select(DeserializeCheckpoint);
        
        // Read markers
        var markers = json["markers"]
            .GetOrDefault<JsonArray>([])
            .Cast<JsonObject>()
            .Select(DeserializeMarker);
        
        // Read themes
        var themeLookup = json["themes"]
            .GetOrDefault<JsonArray>([])
            .Cast<JsonObject>()
            .Select(t => DeserializeTheme(t, true))
            .ToDictionary(t => ((IIdentifiable<string>) t).Id, t => t);
        
        // Read events
        var eventsJson = json["events"].GetOrDefault<JsonArray>([]);
        
        // Read movement events
        var movementEvents = DeserializeArray(
            eventsJson[0].GetOrDefault<JsonArray>([]),
            GetFixedKeyframeDeserializer(x => new Vector2(
                x[0].Get<float>(),
                x[1].Get<float>())));
        
        // Read zoom events
        var zoomEvents = DeserializeArray(
            eventsJson[1].GetOrDefault<JsonArray>([]),
            GetFixedKeyframeDeserializer(x => x[0].Get<float>()));
        
        // Read rotation events
        var rotationEvents = DeserializeArray(
            eventsJson[2].GetOrDefault<JsonArray>([]),
            GetFixedKeyframeDeserializer(x => x[0].Get<float>()));
        
        // Read shake events
        var shakeEvents = DeserializeArray(
            eventsJson[3].GetOrDefault<JsonArray>([]),
            GetFixedKeyframeDeserializer(x => x[0].Get<float>()));
        
        // Read theme events
        var themeEvents = DeserializeArray(
            eventsJson[4].GetOrDefault<JsonArray>([]),
            x =>
            {
                var time = x["t"].GetOrDefault(0.0f);
                var themeId = x["evs"]![0].Get<string>();
                var theme = themeLookup.TryGetValue(themeId, out var themeValue)
                    ? (IReference<ITheme>) themeValue
                    : new VgReferenceTheme(themeId);
                var ease = Ease.Linear;
                if (Enum.TryParse<Ease>(x["ct"].GetOrDefault("Linear"), out var easeValue))
                    ease = easeValue;
                return new FixedKeyframe<IReference<ITheme>>
                {
                    Time = time,
                    Value = theme,
                    Ease = ease,
                };
            });
        
        // Read chroma events
        var chromaEvents = DeserializeArray(
            eventsJson[5].GetOrDefault<JsonArray>([]),
            GetFixedKeyframeDeserializer(x => x[0].Get<float>()));
        
        // Read bloom events
        var bloomEvents = DeserializeArray(
            eventsJson[6].GetOrDefault<JsonArray>([]),
            GetFixedKeyframeDeserializer(x => new BloomData
            {
                Intensity = x[0].Get<float>(),
                Diffusion = x[1].Get<float>(),
                Color = (int) x[2].Get<float>(),
            }));
        
        // Read vignette events
        var vignetteEvents = DeserializeArray(
            eventsJson[7].GetOrDefault<JsonArray>([]),
            GetFixedKeyframeDeserializer(x => new VignetteData
            {
                Intensity = x[0].Get<float>(),
                Smoothness = x[1].Get<float>(),
                Color = (int) x[2].Get<float>(),
                Rounded = x[3].Get<float>() != 0.0f,
                Center = new Vector2(
                    x[4].Get<float>(),
                    x[5].Get<float>()),
            }));
        
        // Read lens distortion events
        var lensDistortionEvents = DeserializeArray(
            eventsJson[8].GetOrDefault<JsonArray>([]),
            GetFixedKeyframeDeserializer(x => new LensDistortionData
            {
                Intensity = x[0].Get<float>(),
                Center = new Vector2(
                    x[1].Get<float>(),
                    x[2].Get<float>()),
            }));
        
        // Read grain events
        var grainEvents = DeserializeArray(
            eventsJson[9].GetOrDefault<JsonArray>([]),
            GetFixedKeyframeDeserializer(x => new GrainData
            {
                Intensity = x[0].Get<float>(),
                Size = x[1].Get<float>(),
                Mix = x[2].Get<float>(),
            }));
        
        // Read gradient events
        var gradientEvents = DeserializeArray(
            eventsJson[10].GetOrDefault<JsonArray>([]),
            GetFixedKeyframeDeserializer(x => new GradientData
            {
                Intensity = x.Count > 0 ? x[0].Get<float>() : 0.0f,
                Rotation = x.Count > 1 ? x[1].Get<float>() : 0.0f,
                ColorA = x.Count > 2 ? (int) x[2].Get<float>() : 0,
                ColorB = x.Count > 3 ? (int) x[3].Get<float>() : 0,
                Mode = (x.Count > 4 ? (int) x[4].Get<float>() : 0) switch
                {
                    0 => GradientOverlayMode.Linear,
                    1 => GradientOverlayMode.Additive,
                    2 => GradientOverlayMode.Multiply,
                    3 => GradientOverlayMode.Screen,
                    _ => throw new ArgumentOutOfRangeException(),
                }
            }));
        
        // Read glitch events
        var glitchEvents = DeserializeArray(
            eventsJson[11].GetOrDefault<JsonArray>([]),
            GetFixedKeyframeDeserializer(x => new GlitchData
            {
                Intensity = x.Count > 0 ? x[0].Get<float>() : 0.0f,
                Speed = x.Count > 1 ? x[1].Get<float>() : 0.0f,
                Width = x.Count > 2 ? x[2].Get<float>() : 0.0f,
            }));
        
        // Read hue events
        var hueEvents = DeserializeArray(
            eventsJson[12].GetOrDefault<JsonArray>([]),
            GetFixedKeyframeDeserializer(x => x[0].Get<float>()));
        
        // Read player events
        var playerEvents = DeserializeArray(
            eventsJson[13].GetOrDefault<JsonArray>([]),
            GetFixedKeyframeDeserializer(x => new Vector2(
                x[0].Get<float>(),
                x[1].Get<float>())));

        var beatmap = new VgBeatmap
        {
            EditorSettings = editorSettings,
            Parallax = parallax,
        };
        
        beatmap.Triggers.AddRange(triggers);
        beatmap.PrefabSpawns.AddRange(editorPrefabSpawns);
        beatmap.Checkpoints.AddRange(checkpoints);
        beatmap.Markers.AddRange(markers);
        beatmap.Prefabs.AddRange(prefabLookup.Values.Select(x => x.prefab));
        beatmap.PrefabObjects.AddRange(prefabObjects);
        beatmap.Themes.AddRange(themeLookup.Values);
        beatmap.Objects.AddRange(objectLookup.Values.Select(x => x.Item1));
        
        var events = beatmap.Events;
        events.Movement.AddRange(movementEvents);
        events.Zoom.AddRange(zoomEvents);
        events.Rotation.AddRange(rotationEvents);
        events.Shake.AddRange(shakeEvents);
        events.Theme.AddRange(themeEvents);
        events.Chroma.AddRange(chromaEvents);
        events.Bloom.AddRange(bloomEvents);
        events.Vignette.AddRange(vignetteEvents);
        events.LensDistortion.AddRange(lensDistortionEvents);
        events.Grain.AddRange(grainEvents);
        events.Gradient.AddRange(gradientEvents);
        events.Glitch.AddRange(glitchEvents);
        events.Hue.AddRange(hueEvents);
        events.Player.AddRange(playerEvents);
        
        return beatmap;
    }
    
    private static EditorPrefabSpawn DeserializeEditorPrefabSpawn(JsonObject json, Func<string, IReference<IPrefab>?> prefabLookup)
    {
        var expanded = json["expanded"].GetOrDefault(false);
        var active = json["active"].GetOrDefault(false);
        var prefabId = json["prefab"].GetOrDefault<string?>(null);
        var prefab = string.IsNullOrEmpty(prefabId) ? null : prefabLookup(prefabId);
        var keycodes = json["keycodes"]
            .GetOrDefault<JsonArray>([])
            .Select(x => x.GetOrDefault(string.Empty));
        var editorPrefabSpawn = new EditorPrefabSpawn
        {
            Expanded = expanded,
            Active = active,
            Prefab = prefab,
        };
        editorPrefabSpawn.Keycodes.AddRange(keycodes);
        return editorPrefabSpawn;
    }

    private static Trigger DeserializeTrigger(JsonObject json)
    {
        var triggerType = json["event_trigger"].GetOrDefault(0) switch
        {
            0 => TriggerType.Time,
            1 => TriggerType.PlayerHit,
            2 => TriggerType.PlayerDeath,
            3 => TriggerType.PlayerStart,
            _ => throw new ArgumentOutOfRangeException()
        };
        var fromTime = (json["event_trigger_time"]?["x"]).GetOrDefault(0.0f);
        var toTime = (json["event_trigger_time"]?["y"]).GetOrDefault(0.0f);
        var retrigger = json["event_retrigger"].GetOrDefault(0);
        var eventType = json["event_type"].GetOrDefault(0) switch
        {
            0 => EventType.VnInk,
            1 => EventType.VnTimeline,
            2 => EventType.PlayerBubble,
            3 => EventType.PlayerLocation,
            4 => EventType.PlayerDash,
            5 => EventType.PlayerXMovement,
            6 => EventType.PlayerYMovement,
            7 => EventType.BgSpin,
            8 => EventType.BgMove,
            9 => EventType.PlayerDashDirection,
            _ => throw new ArgumentOutOfRangeException()
        };
        var eventData = json["event_data"]
            .GetOrDefault<JsonArray>([])
            .Select(x => x.GetOrDefault(string.Empty));
        var trigger = new Trigger
        {
            Type = triggerType,
            From = fromTime,
            To = toTime,
            EventType = eventType,
            Retrigger = retrigger,
        };
        trigger.Data.AddRange(eventData);
        return trigger;
    }
    
    private static IMarker DeserializeMarker(JsonObject json)
    {
        var id = json["ID"].GetOrDefault(RandomUtil.GenerateId());
        var name = json["n"].GetOrDefault(string.Empty);
        var description = json["d"].GetOrDefault(string.Empty);
        var color = json["c"].GetOrDefault(0);
        var time = json["t"].GetOrDefault(0.0f);
        return new VgMarker(id)
        {
            Name = name,
            Description = description,
            Color = color,
            Time = time,
        };
    }
    
    private static ICheckpoint DeserializeCheckpoint(JsonObject json)
    {
        var id = json["ID"].GetOrDefault(RandomUtil.GenerateId());
        var name = json["n"].GetOrDefault(string.Empty);
        var time = json["t"].GetOrDefault(0.0f);
        var position = new Vector2(
            (json["p"]?["x"]).GetOrDefault(0.0f),
            (json["p"]?["y"]).GetOrDefault(0.0f));
        return new VgCheckpoint(id)
        {
            Name = name,
            Time = time,
            Position = position,
        };
    }
    
    private static IParallax DeserializeParallax(JsonObject json)
    {
        var dofActive = json["dof_active"].GetOrDefault(false);
        var dofValue = dofActive ? (int?) json["dof_value"].GetOrDefault(0) : null;
        var layers = json["l"]
            .GetOrDefault<JsonArray>([])
            .Cast<JsonObject>()
            .Select(DeserializeParallaxLayer);
        var parallax = new Parallax
        {
            DepthOfField = dofValue
        };
        parallax.Layers.AddRange(layers);
        return parallax;
    }

    private static IParallaxLayer DeserializeParallaxLayer(JsonObject json)
    {
        var depth = json["d"].GetOrDefault(0);
        var color = json["c"].GetOrDefault(0);
        var objects = json["o"]
            .GetOrDefault<JsonArray>([])
            .Cast<JsonObject>()
            .Select(DeserializeParallaxObject);
        var layer = new ParallaxLayer
        {
            Depth = depth,
            Color = color,
        };
        layer.Objects.AddRange(objects);
        return layer;
    }

    private static IParallaxObject DeserializeParallaxObject(JsonObject json)
    {
        var id = json["id"].Get<string>();
        var position = new Vector2(
            (json["t"]?["p"]?["x"]).GetOrDefault(0.0f),
            (json["t"]?["p"]?["y"]).GetOrDefault(0.0f));
        var scale = new Vector2(
            (json["t"]?["s"]?["x"]).GetOrDefault(0.0f),
            (json["t"]?["s"]?["y"]).GetOrDefault(0.0f));
        var rotation = (json["t"]?["r"]).GetOrDefault(0.0f);
        var positionAnimationEnabled = (json["an"]?["ap"]).GetOrDefault(false);
        var positionAnimation = positionAnimationEnabled
            ? (Vector2?) new Vector2(
                (json["an"]?["p"]?["x"]).GetOrDefault(0.0f),
                (json["an"]?["p"]?["y"]).GetOrDefault(0.0f))
            : null;
        var scaleAnimationEnabled = (json["an"]?["as"]).GetOrDefault(false);
        var scaleAnimation = scaleAnimationEnabled
            ? (Vector2?) new Vector2(
                (json["an"]?["s"]?["x"]).GetOrDefault(0.0f),
                (json["an"]?["s"]?["y"]).GetOrDefault(0.0f))
            : null;
        var rotationAnimationEnabled = (json["an"]?["ar"]).GetOrDefault(false);
        var rotationAnimation = rotationAnimationEnabled
            ? (float?) (json["an"]?["r"]).GetOrDefault(0.0f)
            : null;
        var loopLength = (json["an"]?["l"]).GetOrDefault(1.0f);
        var loopDelay = (json["an"]?["ld"]).GetOrDefault(0.0f);
        var shape = (json["s"]?["s"]).GetOrDefault(0) switch
        {
            0 => ObjectShape.Square,
            1 => ObjectShape.Circle,
            2 => ObjectShape.Triangle,
            3 => ObjectShape.Arrow,
            4 => ObjectShape.Text,
            5 => ObjectShape.Hexagon,
            _ => throw new ArgumentOutOfRangeException()
        };
        var shapeOption = (json["s"]?["so"]).GetOrDefault(0);
        var text = (json["s"]?["t"]).GetOrDefault(string.Empty);
        var color = (json["s"]?["c"]).GetOrDefault(0);
        return new ParallaxObject(id)
        {
            Position = position,
            Scale = scale,
            Rotation = rotation,
            Animation = new ParallaxObjectAnimation
            {
                Position = positionAnimation,
                Scale = scaleAnimation,
                Rotation = rotationAnimation,
                LoopLength = loopLength,
                LoopDelay = loopDelay,
            },
            Shape = shape,
            ShapeOption = shapeOption,
            Text = text,
            Color = color,
        };
    }


    private static EditorSettings DeserializeEditorSettings(JsonObject json)
        => new()
        {
            Bpm = new BpmSettings
            {
                Snap = 
                    ((json["bpm"]?["snap"]?["objects"]).GetOrDefault(false) ? BpmSnap.Objects : BpmSnap.None) |
                    ((json["bpm"]?["snap"]?["checkpoints"]).GetOrDefault(false) ? BpmSnap.Checkpoints : BpmSnap.None),
                Value = (json["bpm"]?["bpm_value"]).GetOrDefault(0.0f),
                Offset = (json["bpm"]?["bpm_offset"]).GetOrDefault(0.0f),
            },
            Grid = new GridSettings
            {
                Scale = new Vector2(
                    (json["grid"]?["scale"]?["x"]).GetOrDefault(0.0f),
                    (json["grid"]?["scale"]?["y"]).GetOrDefault(0.0f)),
                Thickness = (json["grid"]?["thickness"]).GetOrDefault(0),
                Opacity = (json["grid"]?["opacity"]).GetOrDefault(0.0f),
                Color = (json["grid"]?["color"]).GetOrDefault(0),
            },
            General = new GeneralSettings
            {
                CollapseLength = (json["general"]?["collapse_length"]).GetOrDefault(0.0f),
                Complexity = (json["general"]?["complexity"]).GetOrDefault(0),
                Theme = (json["general"]?["theme"]).GetOrDefault(0),
                SelectTextObjects = (json["general"]?["text_select_objects"]).GetOrDefault(false),
                SelectParallaxTextObjects = (json["general"]?["text_select_backgrounds"]).GetOrDefault(false),
            },
            Preview = new PreviewSettings
            {
                CameraZoomOffset = (json["preview"]?["cam_zoom_offset"]).GetOrDefault(0.0f),
                CameraZoomOffsetColor = (json["preview"]?["cam_zoom_offset_color"]).GetOrDefault(0),
            },
            AutoSave = new AutoSaveSettings
            {
                Max = (json["autosave"]?["as_max"]).GetOrDefault(0),
                Interval = (json["autosave"]?["as_interval"]).GetOrDefault(0),
            },
        };
    
    private static IPrefabObject DeserializePrefabObject(JsonObject json, Func<string, IReference<IPrefab>> prefabLookupCallback)
    {
        var id = json["id"].Get<string>();
        var prefabId = json["pid"].Get<string>();
        var editorSettings = DeserializeObjectEditorSettings(json["ed"].Get<JsonObject>());
        var time = json["t"].GetOrDefault(0.0f);
        var position = new Vector2(
            (json["e"]?[0]?["ev"]?[0]).GetOrDefault(0.0f),
            (json["e"]?[0]?["ev"]?[1]).GetOrDefault(0.0f));
        var scale = new Vector2(
            (json["e"]?[1]?["ev"]?[0]).GetOrDefault(0.0f),
            (json["e"]?[1]?["ev"]?[1]).GetOrDefault(0.0f));
        var rotation = (json["e"]?[2]?["ev"]?[0]).GetOrDefault(0.0f);
        return new VgPrefabObject(id, prefabLookupCallback(prefabId))
        {
            EditorSettings = editorSettings,
            Time = time,
            Position = position,
            Scale = scale,
            Rotation = rotation,
        };
    }
    
    public static ITheme DeserializeTheme(JsonObject json, bool requiresId = false)
    {
        var id = requiresId ? json["id"].Get<string>() : json["id"].GetOrDefault<string?>(null);
        var name = json["name"].GetOrDefault(string.Empty);
        var playerColors = json["pla"]
            .GetOrDefault<JsonArray>([])
            .Select(x => ParseColor(x.Get<string>()));
        var objectColors = json["obj"]
            .GetOrDefault<JsonArray>([])
            .Select(x => ParseColor(x.Get<string>()));
        var effectColors = json["fx"]
            .GetOrDefault<JsonArray>([])
            .Select(x => ParseColor(x.Get<string>()));
        var parallaxObjectColors = json["bg"]
            .GetOrDefault<JsonArray>([])
            .Select(x => ParseColor(x.Get<string>()));
        var backgroundColor = ParseColor(json["base_bg"].Get<string>());
        var guiColor = ParseColor(json["base_gui"].Get<string>());
        var guiAccentColor = ParseColor(json["base_gui_accent"].Get<string>());
        
        var theme = string.IsNullOrEmpty(id)
            ? new VgTheme
            {
                Name = name,
                Background = backgroundColor,
                Gui = guiColor,
                GuiAccent = guiAccentColor,
            }
            : new VgBeatmapTheme(id)
            {
                Name = name,
                Background = backgroundColor,
                Gui = guiColor,
                GuiAccent = guiAccentColor,
            };
        
        theme.Player.AddRange(playerColors);
        theme.Object.AddRange(objectColors);
        theme.Effect.AddRange(effectColors);
        theme.ParallaxObject.AddRange(parallaxObjectColors);

        return theme;
    }
    
    public static IPrefab DeserializePrefab(JsonObject json, Func<string, IReference<IObject>?>? objectLookupCallback = null, bool requiresId = false)
    {
        var id = requiresId ? json["id"].Get<string>() : json["id"].GetOrDefault<string?>(null);
        var name = json["n"].GetOrDefault(string.Empty);
        var description = json["description"].GetOrDefault(string.Empty);
        var preview = json["preview"].GetOrDefault(string.Empty);
        var offset = json["o"].GetOrDefault(0.0f);
        var type = json["type"].GetOrDefault(0) switch
        {
            0 => PrefabType.Character,
            1 => PrefabType.CharacterParts,
            2 => PrefabType.Props,
            3 => PrefabType.Bullets,
            4 => PrefabType.Pulses,
            5 => PrefabType.Bombs,
            6 => PrefabType.Spinners,
            7 => PrefabType.Beams,
            8 => PrefabType.Static,
            9 => PrefabType.Misc1,
            10 => PrefabType.Misc2,
            11 => PrefabType.Misc3,
            _ => throw new ArgumentOutOfRangeException()
        };
        
        var objectsJson = json["objs"].GetOrDefault<JsonArray>([]);
        var objectsLookup = objectsJson
            .Cast<JsonObject>()
            .Select(o => (DeserializeBeatmapObject(o, out var parentId), parentId))
            .ToDictionary(t => ((IIdentifiable<string>) t.Item1).Id, t => t);
        
        // Assign parents to objects
        foreach (var (@object, parentId) in objectsLookup.Values)
        {
            if (string.IsNullOrEmpty(parentId))
                continue;
            if (objectsLookup.TryGetValue(parentId, out var tuple))
            {
                @object.Parent = tuple.Item1;
                continue;
            }
            var lookedUpObject = objectLookupCallback?.Invoke(parentId);
            if (lookedUpObject is not null)
            {
                @object.Parent = lookedUpObject;
                continue;
            }
            @object.Parent = new VgReferenceObject(parentId);
        }

        var prefab = string.IsNullOrEmpty(id)
            ? new VgPrefab
            {
                Name = name,
                Description = description,
                Preview = preview,
                Offset = offset,
                Type = type,
            }
            : new VgBeatmapPrefab(id)
            {
                Name = name,
                Description = description,
                Preview = preview,
                Offset = offset,
                Type = type,
            };
        prefab.BeatmapObjects.AddRange(objectsLookup.Values.Select(t => t.Item1));
        return prefab;
    }
    
    public static IObject DeserializeBeatmapObject(JsonObject json, out string? parentId)
    {
        var id = json["id"].Get<string>();
        parentId = json["p_id"].GetOrDefault<string?>(null);
        var autoKillType = json["ak_t"].GetOrDefault(0) switch
        {
            0 => AutoKillType.NoAutoKill,
            1 => AutoKillType.LastKeyframe,
            2 => AutoKillType.LastKeyframeOffset,
            3 => AutoKillType.FixedTime,
            4 => AutoKillType.SongTime,
            _ => throw new ArgumentOutOfRangeException(),
        };
        var autoKillOffset = json["ak_o"].GetOrDefault(0.0f);
        var objectType = json["ot"].GetOrDefault(0) switch
        {
            0 => ObjectType.LegacyNormal,
            1 => ObjectType.LegacyHelper,
            2 => ObjectType.LegacyDecoration,
            3 => ObjectType.LegacyEmpty,
            4 => ObjectType.Hit,
            5 => ObjectType.NoHit,
            6 => ObjectType.Empty,
            _ => throw new ArgumentOutOfRangeException(),
        };
        var name = json["n"].GetOrDefault(string.Empty);
        var text = json["text"].GetOrDefault(string.Empty);
        var origin = new Vector2(
            (json["o"]?["x"]).GetOrDefault(0.0f),
            (json["o"]?["y"]).GetOrDefault(0.0f));
        var shape = json["s"].GetOrDefault(0) switch
        {
            0 => ObjectShape.Square,
            1 => ObjectShape.Circle,
            2 => ObjectShape.Triangle,
            3 => ObjectShape.Arrow,
            4 => ObjectShape.Text,
            5 => ObjectShape.Hexagon,
            _ => throw new ArgumentOutOfRangeException(),
        };
        var shapeOption = json["so"].GetOrDefault(0);
        var renderType = json["gt"].GetOrDefault(0) switch
        {
            0 => RenderType.Normal,
            1 => RenderType.RightToLeftGradient,
            2 => RenderType.LeftToRightGradient,
            3 => RenderType.InwardsGradient,
            4 => RenderType.OutwardsGradient,
            _ => throw new ArgumentOutOfRangeException(),
        };
        var parentTypeString = json["p_t"].GetOrDefault("101");
        if (parentTypeString.Length != 3)
            throw new ArgumentException($"Invalid p_t value length, expected 3, but got {parentTypeString.Length}");
        var parentType = 
            (parentTypeString[0] != '0' ? ParentType.Position : ParentType.None) |
            (parentTypeString[1] != '0' ? ParentType.Scale : ParentType.None) |
            (parentTypeString[2] != '0' ? ParentType.Rotation : ParentType.None);
        var parentOffset = new ParentOffset(
            (json["p_o"]?[0]).GetOrDefault(0.0f),
            (json["p_o"]?[1]).GetOrDefault(0.0f),
            (json["p_o"]?[2]).GetOrDefault(0.0f));
        var renderDepth = json["d"].GetOrDefault(20);
        var startTime = json["st"].GetOrDefault(0.0f);
        var editorSettings = DeserializeObjectEditorSettings(json["ed"].Get<JsonObject>());

        var eventsJson = json["e"].GetOrDefault<JsonArray>([]);
        var positionEvents = DeserializeObjectEventsArray(
            eventsJson[0].Get<JsonObject>(),
            GetKeyframeDeserializer(
                x => new Vector2(
                    x[0].Get<float>(),
                    x[1].Get<float>()),
                (JsonArray x, out float i) =>
                {
                    i = x.Count > 2 ? x[2].Get<float>() : 0.0f;
                    return new Vector2(
                        x.Count > 0 ? x[0].Get<float>() : 0.0f,
                        x.Count > 1 ? x[1].Get<float>() : 0.0f);
                }));
        var scaleEvents = DeserializeObjectEventsArray(
            eventsJson[1].Get<JsonObject>(),
            GetKeyframeDeserializer(
                x => new Vector2(
                    x[0].Get<float>(),
                    x[1].Get<float>()),
                (JsonArray x, out float i) =>
                {
                    i = x.Count > 2 ? x[2].Get<float>() : 0.0f;
                    return new Vector2(
                        x.Count > 0 ? x[0].Get<float>() : 0.0f,
                        x.Count > 1 ? x[1].Get<float>() : 0.0f);
                }));
        var rotationEvents = DeserializeObjectEventsArray(
            eventsJson[2].Get<JsonObject>(),
            GetKeyframeDeserializer(
                x => x[0].Get<float>(),
                (JsonArray x, out float i) =>
                {
                    i = x.Count > 2 ? x[2].Get<float>() : 0.0f;
                    return x.Count > 0 ? x[0].Get<float>() : 0.0f;
                }));
        var colorEvents = DeserializeObjectEventsArray(
            eventsJson[3].Get<JsonObject>(),
            GetFixedKeyframeDeserializer(
                x => new ThemeColor
                {
                    Index = x.Count > 0 ? (int) x[0].Get<float>() : 0,
                    Opacity = x.Count > 1 ? x[1].Get<float>() / 100.0f : 1.0f,
                    EndIndex = x.Count > 2 ? (int) x[2].GetOrDefault(0.0f) : 0,
                }));

        var @object = new VgObject(id)
        {
            AutoKillType = autoKillType,
            AutoKillOffset = autoKillOffset,
            Type = objectType,
            Name = name,
            Text = text,
            Origin = origin,
            Shape = shape,
            ShapeOption = shapeOption,
            RenderType = renderType,
            ParentType = parentType,
            ParentOffset = parentOffset,
            RenderDepth = renderDepth,
            StartTime = startTime,
            EditorSettings = editorSettings,
        };
        @object.PositionEvents.AddRange(positionEvents);
        @object.ScaleEvents.AddRange(scaleEvents);
        @object.RotationEvents.AddRange(rotationEvents);
        @object.ColorEvents.AddRange(colorEvents);
        return @object;
    }
    
    private static Func<JsonObject, FixedKeyframe<T>> GetFixedKeyframeDeserializer<T>(
        ReadKeyframeValueDelegate<T> readValueCallback)
        => json => DeserializeFixedKeyframe(json, readValueCallback);

    private static Func<JsonObject, Keyframe<T>> GetKeyframeDeserializer<T>(
        ReadKeyframeValueDelegate<T> readValueCallback,
        ReadRandomKeyframeValueDelegate<T> readRandomValueCallback)
        => json => DeserializeKeyframe(json, readValueCallback, readRandomValueCallback);

    private static FixedKeyframe<T> DeserializeFixedKeyframe<T>(
        JsonObject json,
        ReadKeyframeValueDelegate<T> readValueCallback)
    {
        var time = json["t"].GetOrDefault(0.0f);
        var value = readValueCallback(json["ev"].GetOrDefault<JsonArray>([]));
        var ease = Ease.Linear;
        if (Enum.TryParse<Ease>(json["ct"].GetOrDefault("Linear"), out var easeValue))
            ease = easeValue;
        return new FixedKeyframe<T>
        {
            Time = time,
            Value = value,
            Ease = ease,
        };
    }
    
    private static Keyframe<T> DeserializeKeyframe<T>(
        JsonObject json, 
        ReadKeyframeValueDelegate<T> readValueCallback,
        ReadRandomKeyframeValueDelegate<T> readRandomValueCallback)
    {
        var time = json["t"].GetOrDefault(0.0f);
        var value = readValueCallback(json["ev"].GetOrDefault<JsonArray>([]));
        var ease = Ease.Linear;
        if (Enum.TryParse<Ease>(json["ct"].GetOrDefault("Linear"), out var easeValue))
            ease = easeValue;
        var randomMode = json["r"].GetOrDefault(0) switch
        {
            0 => RandomMode.None,
            1 => RandomMode.Range,
            2 => RandomMode.Snap,
            3 => RandomMode.Select,
            4 => RandomMode.Scale,
            _ => throw new ArgumentOutOfRangeException(),
        };
        T? randomValue = default;
        var randomInterval = 0.0f;
        if (json["er"] is JsonArray erArray)
            randomValue = readRandomValueCallback(erArray, out randomInterval);
        return new Keyframe<T>
        {
            Time = time,
            Value = value,
            Ease = ease,
            RandomMode = randomMode,
            RandomValue = randomValue,
            RandomInterval = randomInterval,
        };
    }
    
    private static IEnumerable<T> DeserializeObjectEventsArray<T>(
        JsonObject json,
        Func<JsonObject, T> readValueCallback)
    {
        if (json["k"] is not JsonArray array)
            return [];
        return DeserializeArray(array, readValueCallback);
    }

    private static IEnumerable<T> DeserializeArray<T>(JsonArray json, Func<JsonObject, T> readValueCallback)
    {
        foreach (var item in json)
        {
            if (item is not JsonObject obj)
                continue;
            yield return readValueCallback(obj);
        }
    }

    private static ObjectEditorSettings DeserializeObjectEditorSettings(JsonObject json)
        => new()
        {
            Locked = json["lk"].GetOrDefault(false),
            Collapsed = json["co"].GetOrDefault(false),
            TextColor = json["tc"] is JsonObject tcJson 
                ? DeserializeObjectTimelineColor(tcJson)
                : ObjectTimelineColor.None,
            BackgroundColor = json["bgc"] is JsonObject bgcJson 
                ? DeserializeObjectTimelineColor(bgcJson)
                : ObjectTimelineColor.None,
            Bin = json["bin"].GetOrDefault(0),
            Layer = json["l"].GetOrDefault(0),
        };

    private static ObjectTimelineColor DeserializeObjectTimelineColor(JsonObject json)
        => (json["r"].GetOrDefault(false) ? ObjectTimelineColor.Red : ObjectTimelineColor.None) |
           (json["g"].GetOrDefault(false) ? ObjectTimelineColor.Green : ObjectTimelineColor.None) |
           (json["b"].GetOrDefault(false) ? ObjectTimelineColor.Blue : ObjectTimelineColor.None);
    
    private static Color ParseColor(string hex)
    {
        if (string.IsNullOrWhiteSpace(hex))
            throw new ArgumentException("Color hex string is null or empty", nameof(hex));
        hex = hex[0] == '#' ? hex[1..] : hex;
        if (hex.Length != 6)
            throw new ArgumentException("Color hex string must be 6 characters long", nameof(hex));
        var r = int.Parse(hex[..2], NumberStyles.HexNumber);
        var g = int.Parse(hex[2..4], NumberStyles.HexNumber);
        var b = int.Parse(hex[4..6], NumberStyles.HexNumber);
        return Color.FromArgb(r, g, b);
    }
    
    private static T Get<T>(this JsonNode? node)
    {
        if (node is null)
            throw new ArgumentNullException(nameof(node));
        if (node is T node1)
            return node1;
        return node.GetValue<T>();
    }
    
    private static T GetOrDefault<T>(this JsonNode? node, T defaultValue)
    {
        if (node is null)
            return defaultValue;
        if (node is T node1)
            return node1; 
        return node.GetValue<T>();
    }
}