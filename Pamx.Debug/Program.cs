using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using Pamx;
using Pamx.Common;
using Pamx.Common.Enum;
using Pamx.Common.Implementation;
using Pamx.Ls;
using Pamx.Vg;

var beatmapText = File.ReadAllText("level.vgd");
var beatmapJson = (JsonObject) JsonNode.Parse(beatmapText)!;
var beatmap = VgDeserialization.DeserializeBeatmap(beatmapJson);

// Conversion
// Convert objects
foreach (var beatmapObject in beatmap.Objects.Concat(beatmap.Prefabs.SelectMany(x => x.BeatmapObjects)))
{
    beatmapObject.Type = beatmapObject.Type switch
    {
        ObjectType.Empty => ObjectType.LegacyEmpty,
        ObjectType.NoHit => ObjectType.LegacyHelper,
        ObjectType.Hit => ObjectType.LegacyNormal,
        _ => beatmapObject.Type,
    };
}

foreach (var prefab in beatmap.Prefabs)
{
    prefab.Type = PrefabType.Misc4;
}

// Convert themes
var themeIdToIntId = beatmap.Themes
    .ToDictionary(x => ((IIdentifiable<string>) x).Id, _ => RandomUtil.GenerateLsThemeId());
var vgThemes = new List<ITheme>(beatmap.Themes);
beatmap.Themes.Clear();
beatmap.Themes.AddRange(vgThemes.Select(x => x.CloneWithLsId(themeIdToIntId[((IIdentifiable<string>) x).Id])));

// We have to convert the theme keyframes as well
var vgThemeKeyframes = beatmap.Events.Theme.Select(x =>
{
    x.Value = new LsReferenceTheme(themeIdToIntId[((IIdentifiable<string>)x.Value).Id]);
    return x;
}).ToList();
beatmap.Events.Theme.Clear();
beatmap.Events.Theme.AddRange(vgThemeKeyframes);

var json = LsSerialization.SerializeBeatmap(beatmap);
var jsonText = json.ToJsonString(new JsonSerializerOptions
{
    TypeInfoResolver = new DefaultJsonTypeInfoResolver(),
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
});
File.WriteAllText("level_re_encode.lsb", jsonText);