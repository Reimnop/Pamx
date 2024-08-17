using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using Pamx.Ls;

var beatmapText = File.ReadAllText("level.lsb");
var beatmapJson = (JsonObject) JsonNode.Parse(beatmapText)!;
var beatmap = LsDeserialization.DeserializeBeatmap(beatmapJson);

// Re-encode the beatmap to see if it's the same
var reEncodedBeatmap = LsSerialization.SerializeBeatmap(beatmap);
File.WriteAllText("level_re_encode.lsb", reEncodedBeatmap.ToJsonString(new JsonSerializerOptions
{
    TypeInfoResolver = new DefaultJsonTypeInfoResolver(),
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
}));