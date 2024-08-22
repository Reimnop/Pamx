using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Nodes;
using Pamx.Common;
using Pamx.Common.Data;
using Pamx.Common.Enum;
using Pamx.Common.Implementation;

namespace Pamx.Ls;

public static class LsDeserialization
{
    public static IBeatmap DeserializeBeatmap(JsonObject json)
    {
        // Read editor data
        var editorData = json["ed"].Get<JsonObject>();
        var markers = editorData["markers"]
            .GetOrDefault<JsonArray>([])
            .Select(x => x.Get<JsonObject>())
            .Select(x => new LsMarker
            {
                Name = x["name"].Get<string>(),
                Description = x["desc"].Get<string>(),
                Color = x["col"].Get<int>(),
                Time = x["t"].Get<float>(),
            });
        
        // Read objects and store as dictionary
        var objectLookup = json["beatmap_objects"]
            .GetOrDefault<JsonArray>([])
            .Cast<JsonObject>()
            .Select(x => (DeserializeBeatmapObject(x, out var parentId), parentId))
            .ToDictionary(x => ((IIdentifiable<string>) x.Item1).Id, x => x);
        
        // Assign parents
        foreach (var (_, (@object, parentId)) in objectLookup)
        {
            if (string.IsNullOrEmpty(parentId))
                continue;
            if (objectLookup.TryGetValue(parentId, out var tuple))
            {
                @object.Parent = tuple.Item1;
                continue;
            }
            @object.Parent = new BeatmapReferenceObject(parentId);
        }

        // Read prefabs and store it as a dictionary
        var prefabLookup = json["prefabs"]
            .GetOrDefault<JsonArray>([])
            .Cast<JsonObject>()
            .Select(x => DeserializePrefab(
                x, 
                id => objectLookup.TryGetValue(id, out var tuple) ? tuple.Item1 : null, 
                true))
            .GroupBy(x => ((IIdentifiable<string>) x).Id)
            .Select(x => x.First())
            .ToDictionary(x => ((IIdentifiable<string>) x).Id, x => x);
        
        // Read prefab objects
        var prefabObjects = json["prefab_objects"]
            .GetOrDefault<JsonArray>([])
            .Cast<JsonObject>()
            .Select(x => DeserializePrefabObject(x, id => prefabLookup[id]));
        
        // level_data is not used, so we don't need to consume those
        
        // Read checkpoints
        var checkpoints = json["checkpoints"]
            .GetOrDefault<JsonArray>([])
            .Cast<JsonObject>()
            .Select(x => new LsCheckpoint
            {
                Name = x["name"].Get<string>(),
                Time = x["t"].Get<float>(),
                Position = new Vector2(
                    (x["pos"]?["x"]).Get<float>(),
                    (x["pos"]?["y"]).Get<float>()),
            });
        
        // Read background objects
        var backgroundObjects = json["background_objects"]
            .GetOrDefault<JsonArray>([])
            .Cast<JsonObject>()
            .Select(DeserializeBackgroundObject);
        
        // Read themes and store it as a dictionary
        var themeLookup = json["themes"]
            .GetOrDefault<JsonArray>([])
            .Cast<JsonObject>()
            .Select(DeserializeTheme)
            .GroupBy(x => ((IIdentifiable<int>) x).Id)
            .Select(x => x.First())
            .ToDictionary(x => ((IIdentifiable<int>) x).Id, x => x);
        
        // Read events
        var eventsJson = json["events"].Get<JsonObject>();
        var movementEvents = DeserializeEventKeyframes(eventsJson, "pos", json =>
        {
            var x = json["x"].Get<float>();
            var y = json["y"].Get<float>();
            return new Vector2(x, y);
        });
        var zoomEvents = DeserializeEventKeyframes(eventsJson, "zoom", json => 
            json["x"].Get<float>());
        var rotationEvents = DeserializeEventKeyframes(eventsJson, "rot", json => 
            json["x"].Get<float>());
        var shakeEvents = DeserializeEventKeyframes(eventsJson, "shake", json => 
            json["x"].Get<float>());
        var themeEvents = DeserializeEventKeyframes<IReference<ITheme>>(eventsJson, "theme", json =>
        {
            var id = json["x"].Get<int>();
            if (themeLookup.TryGetValue(id, out var theme))
                return theme;
            return new LsReferenceTheme(id);
        });
        var chromaEvents = DeserializeEventKeyframes(eventsJson, "chroma", json => 
            json["x"].Get<float>());
        var bloomEvents = DeserializeEventKeyframes(eventsJson, "bloom", json => 
            new BloomData
            {
                Intensity = json["x"].Get<float>()
            });
        var vignetteEvents = DeserializeEventKeyframes(eventsJson, "vignette", json => 
            new VignetteData
            {
                Intensity = json["x"].Get<float>(),
                Smoothness = json["y"].Get<float>(),
                Rounded = json["z"].Get<float>() != 0.0f,
                Roundness = json["x2"].Get<float>(),
                Center = new Vector2(
                    json["y2"].Get<float>(),
                    json["z2"].Get<float>()),
            });
        var lensDistortionEvents = DeserializeEventKeyframes(eventsJson, "lens", json => 
            new LensDistortionData
            {
                Intensity = json["x"].Get<float>(),
            });
        var grainEvents = DeserializeEventKeyframes(eventsJson, "grain", json => 
            new GrainData
            {
                Intensity = json["x"].Get<float>(),
                Colored = json["y"].Get<float>() != 0.0f,
                Size = json["z"].Get<float>(),
            });

        var beatmap = new Beatmap();
        beatmap.Markers.AddRange(markers);
        beatmap.Prefabs.AddRange(prefabLookup.Values);
        beatmap.PrefabObjects.AddRange(prefabObjects);
        beatmap.Checkpoints.AddRange(checkpoints);
        beatmap.BackgroundObjects.AddRange(backgroundObjects);
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
        
        return beatmap;
    }

    private static IEnumerable<FixedKeyframe<T>> DeserializeEventKeyframes<T>(
        JsonObject eventsJson, 
        string name,
        Func<JsonObject, T> readCallback)
    {
        var json = eventsJson[name].Get<JsonArray>();
        foreach (var keyframeJson in json.Cast<JsonObject>())
        {
            var time = keyframeJson["t"].Get<float>();
            var ease = Ease.Linear;
            if (Enum.TryParse<Ease>(keyframeJson["ct"].GetOrDefault("Linear"), out var easeValue))
                ease = easeValue;
            yield return new FixedKeyframe<T>
            {
                Time = time,
                Value = readCallback(keyframeJson),
                Ease = ease
            };
        }
    }

    private static BackgroundObject DeserializeBackgroundObject(JsonObject json)
    {
        var active = bool.Parse(json["active"].Get<string>());
        var name = json["name"].Get<string>();
        var position = new Vector2(
            (json["pos"]?["x"]).Get<float>(),
            (json["pos"]?["y"]).Get<float>());
        var size = new Vector2(
            (json["size"]?["x"]).Get<float>(),
            (json["size"]?["y"]).Get<float>());
        var rotation = json["rot"].Get<float>();
        var color = json["color"].Get<int>();
        var depth = json["layer"].Get<int>();
        var fade = bool.Parse(json["fade"].Get<string>());
        var reactiveType = (json["r_set"]?["type"]).GetOrDefault<string?>(null) switch
        {
            "LOW" => BackgroundObjectReactiveType.Bass,
            "MID" => BackgroundObjectReactiveType.Mid,
            "HIGH" => BackgroundObjectReactiveType.Treble,
            _ => BackgroundObjectReactiveType.None
        };
        var reactiveScale = float.Parse((json["r_set"]?["scale"]).GetOrDefault("0"), CultureInfo.InvariantCulture);
        return new BackgroundObject
        {
            Active = active,
            Name = name,
            Position = position,
            Scale = size,
            Rotation = rotation,
            Color = color,
            Depth = depth,
            Fade = fade,
            ReactiveType = reactiveType,
            ReactiveScale = reactiveScale
        };
    }

    private static IPrefabObject DeserializePrefabObject(JsonObject json, Func<string, IReference<IPrefab>> prefabLookupCallback)
    {
        var id = json["id"].Get<string>();
        var prefabId = json["pid"].Get<string>();
        var prefab = prefabLookupCallback(prefabId);
        var time = json["st"].Get<float>();
        var editorSettings = GetObjectEditorSettings(json["ed"].Get<JsonObject>());
        return new LsPrefabObject(id, prefab)
        {
            Time = time,
            EditorSettings = editorSettings,
        };
    }
    
    public static ITheme DeserializeTheme(JsonObject json)
    {
        var id = json["id"].Get<int>();
        var name = json["name"].Get<string>();
        var background = ParseColor(json["bg"].Get<string>());
        var gui = ParseColor(json["gui"].Get<string>());
        var players = json["players"]
            .Get<JsonArray>()
            .Select(x => x.Get<string>())
            .Select(ParseColor);
        var objects = json["objs"]
            .Get<JsonArray>()
            .Select(x => x.Get<string>())
            .Select(ParseColor);
        var backgroundObjects = json["bgs"]
            .Get<JsonArray>()
            .Select(x => x.Get<string>())
            .Select(ParseColor);
        
        var theme = new LsBeatmapTheme(id)
        {
            Name = name,
            Background = background,
            Gui = gui,
        };
        theme.Player.AddRange(players);
        theme.Object.AddRange(objects);
        theme.BackgroundObject.AddRange(backgroundObjects);
        
        return theme;
    }

    public static IPrefab DeserializePrefab(JsonObject json, Func<string, IReference<IObject>?>? objectLookupCallback = null, bool requiresId = false)
    {
        var id = requiresId ? json["id"].Get<string>() : json["id"].GetOrDefault<string?>(null);
        var name = json["name"].Get<string>();
        var type = json["type"].Get<string>() switch
        {
            "0" => PrefabType.Bombs,
            "1" => PrefabType.Bullets,
            "2" => PrefabType.Beams,
            "3" => PrefabType.Spinners,
            "4" => PrefabType.Pulses,
            "5" => PrefabType.Character,
            "6" => PrefabType.Misc1,
            "7" => PrefabType.Misc2,
            "8" => PrefabType.Misc3,
            "9" => PrefabType.Misc4,
            _ => throw new ArgumentOutOfRangeException(),
        };
        var offset = json["offset"].Get<float>();
        
        var objectsJson = json["objects"].GetOrDefault<JsonArray>([]);
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
            @object.Parent = new BeatmapReferenceObject(parentId);
        }
        
        var prefab = string.IsNullOrEmpty(id)
            ? new Prefab
            {
                Name = name,
                Type = type,
                Offset = offset,
            }
            : new BeatmapPrefab(id)
            {
                Name = name,
                Type = type,
                Offset = offset,
            };

        foreach (var (@object, _) in objectsLookup.Values)
            prefab.BeatmapObjects.Add(@object);
        
        return prefab;
    }
    
    public static IObject DeserializeBeatmapObject(JsonObject json, out string? parentId)
    {
        var id = json["id"].Get<string>();
        parentId = json["p"].GetOrDefault<string?>(null);
        var name = json["name"].GetOrDefault(string.Empty);
        var ptStr = json["pt"].GetOrDefault<string>("101");
        if (ptStr.Length != 3)
            throw new ArgumentException($"Invalid pt value length, expected 3, but got {ptStr.Length}");
        var parentType = 
            (ptStr[0] != '0' ? ParentType.Position : ParentType.None) |
            (ptStr[1] != '0' ? ParentType.Scale : ParentType.None) |
            (ptStr[2] != '0' ? ParentType.Rotation : ParentType.None);
        var parentOffsetJson = json["po"].GetOrDefault<JsonArray>([0.0f, 0.0f, 0.0f]);
        var parentOffset = new ParentOffset
        {
            Position = parentOffsetJson[0].Get<float>(),
            Scale = parentOffsetJson[1].Get<float>(),
            Rotation = parentOffsetJson[2].Get<float>(),
        };
        var renderDepth = json["d"].GetOrDefault(15);
        var objectType = json["ot"].GetOrDefault(0) switch
        {
            0 => ObjectType.LegacyNormal,
            1 => ObjectType.LegacyHelper,
            2 => ObjectType.LegacyDecoration,
            3 => ObjectType.LegacyEmpty,
            _ => throw new ArgumentOutOfRangeException(),
        };
        
        if (json.ContainsKey("h") && json["h"].GetOrDefault("False") != "False")
            objectType = ObjectType.LegacyHelper;
        
        if (json.ContainsKey("empty") && json["empty"].GetOrDefault("False") != "False")
            objectType = ObjectType.LegacyEmpty;
        
        var shape = json["shape"].GetOrDefault<string>("0") switch
        {
            "0" => ObjectShape.Square,
            "1" => ObjectShape.Circle,
            "2" => ObjectShape.Triangle,
            "3" => ObjectShape.Arrow,
            "4" => ObjectShape.Text,
            "5" => ObjectShape.Hexagon,
            _ => throw new ArgumentOutOfRangeException(),
        };
        var shapeOption = int.Parse(json["so"].GetOrDefault<string>("0"));
        var text = json["text"].GetOrDefault(string.Empty);
        var startTime = float.Parse(json["st"].GetOrDefault<string>("0"), CultureInfo.InvariantCulture);
        var autoKillType = json["akt"].GetOrDefault(0) switch
        {
            0 => AutoKillType.NoAutoKill,
            1 => AutoKillType.LastKeyframe,
            2 => AutoKillType.LastKeyframeOffset,
            3 => AutoKillType.FixedTime,
            4 => AutoKillType.SongTime,
            _ => throw new ArgumentOutOfRangeException(),
        };
        if (json.ContainsKey("ak") && json["ak"].GetOrDefault("False") != "False")
            autoKillType = AutoKillType.LastKeyframe;
        
        var autoKillOffset = json["ako"].GetOrDefault(0.0f);
        var originJson = json["o"].Get<JsonObject>();
        var origin = new Vector2(
            originJson["x"].Get<float>(), 
            originJson["y"].Get<float>());
        var editorSettings = GetObjectEditorSettings(json["ed"].Get<JsonObject>());
        var posJson = (json["events"]?["pos"]).Get<JsonArray>();
        var scaJson = (json["events"]?["sca"]).Get<JsonArray>();
        var rotJson = (json["events"]?["rot"]).Get<JsonArray>();
        var colJson = (json["events"]?["col"]).Get<JsonArray>();
        var positionEvents = posJson
            .Cast<JsonObject>()
            .Select(GetVector2Keyframe);
        var scaleEvents = scaJson
            .Cast<JsonObject>()
            .Select(GetVector2Keyframe);
        var rotationEvents = rotJson
            .Cast<JsonObject>()
            .Select(GetFloatKeyframe);
        var colorEvents = colJson
            .Cast<JsonObject>()
            .Select(GetThemeColorKeyframe);
        var @object = new BeatmapObject(id)
        {
            Name = name,
            ParentType = parentType,
            ParentOffset = parentOffset,
            RenderDepth = renderDepth,
            Type = objectType,
            Shape = shape,
            ShapeOption = shapeOption,
            Text = text,
            StartTime = startTime,
            AutoKillType = autoKillType,
            AutoKillOffset = autoKillOffset,
            Origin = origin,
            EditorSettings = editorSettings,
        };
        @object.PositionEvents.AddRange(positionEvents);
        @object.ScaleEvents.AddRange(scaleEvents);
        @object.RotationEvents.AddRange(rotationEvents);
        @object.ColorEvents.AddRange(colorEvents);
        return @object;
    }
    
    private static FixedKeyframe<ThemeColor> GetThemeColorKeyframe(JsonObject json)
    {
        var time = float.Parse(json["t"].GetOrDefault<string>("0"), CultureInfo.InvariantCulture);
        var value = new ThemeColor
        {
            Index = int.Parse(json["x"].GetOrDefault<string>("0")),
        };
        return new FixedKeyframe<ThemeColor>
        {
            Time = time,
            Value = value,
        };
    }
    
    private static Keyframe<float> GetFloatKeyframe(JsonObject json)
    {
        var time = float.Parse(json["t"].GetOrDefault("0"), CultureInfo.InvariantCulture);
        var value = float.Parse(json["x"].GetOrDefault("0"), CultureInfo.InvariantCulture);
        var ease = Ease.Linear;
        if (Enum.TryParse<Ease>(json["ct"].GetOrDefault("Linear"), out var easeValue))
            ease = easeValue;
        var randomMode = json["r"].GetOrDefault("0") switch
        {
            "0" => RandomMode.None,
            "1" => RandomMode.Range,
            "2" => RandomMode.Snap,
            "3" => RandomMode.Select,
            "4" => RandomMode.Scale,
            _ => throw new ArgumentOutOfRangeException(),
        };
        var randomValue = float.Parse(json["rx"].GetOrDefault("0"), CultureInfo.InvariantCulture);
        var randomInterval = float.Parse(json["rz"].GetOrDefault("0"), CultureInfo.InvariantCulture);
        return new Keyframe<float>
        {
            Time = time,
            Value = value,
            Ease = ease,
            RandomMode = randomMode,
            RandomValue = randomValue,
            RandomInterval = randomInterval,
        };
    }

    private static Keyframe<Vector2> GetVector2Keyframe(JsonObject json)
    {
        var time = float.Parse(json["t"].GetOrDefault("0"), CultureInfo.InvariantCulture);
        var x = float.Parse(json["x"].GetOrDefault("0"), CultureInfo.InvariantCulture);
        var y = float.Parse(json["y"].GetOrDefault("0"), CultureInfo.InvariantCulture);
        var ease = Ease.Linear;
        if (Enum.TryParse<Ease>(json["ct"].GetOrDefault("Linear"), out var easeValue))
            ease = easeValue;
        var randomMode = json["r"].GetOrDefault("0") switch
        {
            "0" => RandomMode.None,
            "1" => RandomMode.Range,
            "2" => RandomMode.Snap,
            "3" => RandomMode.Select,
            "4" => RandomMode.Scale,
            _ => throw new ArgumentOutOfRangeException(),
        };
        var randomX = float.Parse(json["rx"].GetOrDefault("0"), CultureInfo.InvariantCulture);
        var randomY = float.Parse(json["ry"].GetOrDefault("0"), CultureInfo.InvariantCulture);
        var randomInterval = float.Parse(json["rz"].GetOrDefault("0"), CultureInfo.InvariantCulture);
        return new Keyframe<Vector2>
        {
            Time = time,
            Value = new Vector2(x, y),
            Ease = ease,
            RandomMode = randomMode,
            RandomValue = new Vector2(randomX, randomY),
            RandomInterval = randomInterval,
        };
    }

    private static ObjectEditorSettings GetObjectEditorSettings(JsonObject json)
    {
        var locked = json["locked"].GetOrDefault("False") != "False";
        var collapsed = json["shrink"].GetOrDefault("False") != "False";
        var bin = int.Parse(json["bin"].GetOrDefault("0"));
        var layer = int.Parse(json["layer"].GetOrDefault("0"));
        return new ObjectEditorSettings
        {
            Locked = locked,
            Collapsed = collapsed,
            Bin = bin,
            Layer = layer,
        };
    }

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
        
        // Dumb workaround for numeric types
        if (typeof(T) == typeof(int) && node.GetValueKind() == JsonValueKind.String)
            return (T)(object) int.Parse(node.GetValue<string>());
        if (typeof(T) == typeof(float) && node.GetValueKind() == JsonValueKind.String)
            return (T)(object) float.Parse(node.GetValue<string>(), CultureInfo.InvariantCulture);
        
        return node.GetValue<T>();
    }
    
    private static T GetOrDefault<T>(this JsonNode? node, T defaultValue)
    {
        if (node is null)
            return defaultValue;
        if (node is T node1)
            return node1;
        
        // Dumb workaround for numeric types
        if (typeof(T) == typeof(int) && node.GetValueKind() == JsonValueKind.String)
            return (T)(object) int.Parse(node.GetValue<string>());
        if (typeof(T) == typeof(float) && node.GetValueKind() == JsonValueKind.String)
            return (T)(object) float.Parse(node.GetValue<string>(), CultureInfo.InvariantCulture);
        
        return node.GetValue<T>();
    }
}