using System.Text.Json;
using System.Text.Json.Nodes;
using Pamx.Neo;
using Pamx.Neo.Serialization;
using Pamx.Vg;

namespace Pamx.Tests.Fixtures;

public sealed class BeatmapFixture
{
    public JsonObject Expected { get; }
    public JsonObject Actual { get; }

    public BeatmapFixture()
    {
        var original = File.ReadAllText(@"E:\Project\Programming\pase\static\magnetar.vgd");
        // var original = File.ReadAllText(@"C:\Users\enchart\Downloads\pas\levels\pam4.vgd");

        var expectedBeatmap = VgDeserialization.DeserializeBeatmap((JsonObject)JsonNode.Parse(original)!);
        Expected = VgSerialization.SerializeBeatmap(expectedBeatmap);

        var actualBeatmap = JsonSerializer.Deserialize<Beatmap>(original, JsonContext.CustomOptions)!;
        Actual = (JsonObject)JsonSerializer.SerializeToNode(actualBeatmap, JsonContext.CustomOptions)!;
    }
}