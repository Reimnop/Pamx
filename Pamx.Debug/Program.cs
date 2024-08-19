using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using Pamx.Common;
using Pamx.Common.Data;
using Pamx.Common.Enum;
using Pamx.Vg;

var beatmapText = File.ReadAllText("level.vgd");
var beatmapJson = (JsonObject) JsonNode.Parse(beatmapText)!;
var beatmap = VgDeserialization.DeserializeBeatmap(beatmapJson);

// Compress beatmap by stripping unnecessary data
beatmap.Markers.Clear();
beatmap.EditorSettings = new EditorSettings();

var i = 0;
foreach (var beatmapObject in beatmap.Objects
             .Concat(beatmap.Prefabs.SelectMany(x => x.BeatmapObjects)))
{
    var identifiable = (IIdentifiable<string>)beatmapObject;
    identifiable.Id = $"{i++:X}";
    
    beatmapObject.Name = string.Empty;
    beatmapObject.EditorSettings = new ObjectEditorSettings();
    
    if (beatmapObject.Shape != ObjectShape.Text)
        beatmapObject.Text = string.Empty;
}

foreach (var prefabObject in beatmap.PrefabObjects)
{
    var identifiable = (IIdentifiable<string>)prefabObject;
    identifiable.Id = $"{i++:X}";
    
    prefabObject.EditorSettings = new ObjectEditorSettings();
}

foreach (var prefab in beatmap.Prefabs)
{
    var identifiable = (IIdentifiable<string>)prefab;
    identifiable.Id = $"{i++:X}";
    prefab.Name = string.Empty;
    prefab.Description = string.Empty;
    prefab.Preview = string.Empty;
    prefab.Offset = 0.0f;
}

foreach (var theme in beatmap.Themes)
{
    var identifiable = (IIdentifiable<string>)theme;
    identifiable.Id = $"{i++:X}";
    theme.Name = string.Empty;
}

foreach (var checkpoint in beatmap.Checkpoints)
{
    var identifiable = (IIdentifiable<string>)checkpoint;
    identifiable.Id = string.Empty;
    checkpoint.Name = string.Empty;
}

var json = VgSerialization.SerializeBeatmap(beatmap);
var jsonText = json.ToJsonString(new JsonSerializerOptions
{
    TypeInfoResolver = new DefaultJsonTypeInfoResolver(),
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
});
File.WriteAllText("level_compressed.vgd", jsonText);