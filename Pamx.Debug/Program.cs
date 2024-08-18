using System.Numerics;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using Pamx.Common.Data;
using Pamx.Common.Enum;
using Pamx.Vg;

var beatmapText = File.ReadAllText("level.vgd");
var beatmapJson = (JsonObject) JsonNode.Parse(beatmapText)!;
var beatmap = VgDeserialization.DeserializeBeatmap(beatmapJson);

// Add a text object to know if it's working
var textObject = new VgObject
{
    Name = "Pamx",
    AutoKillType = AutoKillType.NoAutoKill,
    Shape = ObjectShape.Text,
    Text = "<b>Pamx</b> works!!!!!",
    Parent = VgReferenceObject.Camera,
    PositionEvents = { new Keyframe<Vector2>() },
    ScaleEvents = { new Keyframe<Vector2>(0.0f, Vector2.One * 2.0f) },
    RotationEvents = { new Keyframe<float>() },
    ColorEvents = { new FixedKeyframe<ThemeColor>() },
};

beatmap.Objects.Add(textObject);

// Re-encode the beatmap to see if it's the same
var json = VgSerialization.SerializeBeatmap(beatmap);
var jsonText = json.ToJsonString(new JsonSerializerOptions
{
    TypeInfoResolver = new DefaultJsonTypeInfoResolver(),
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
});
File.WriteAllText("level_re_encode.vgd", jsonText);