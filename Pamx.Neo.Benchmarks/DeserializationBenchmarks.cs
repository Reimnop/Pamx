using System.Text.Json;
using System.Text.Json.Nodes;
using BenchmarkDotNet.Attributes;
using Pamx.Common;
using Pamx.Neo.Serialization;
using Pamx.Vg;

namespace Pamx.Neo.Benchmark;

[MemoryDiagnoser]
public class DeserializationBenchmarks
{
    // private const string Path = @"E:\Project\Programming\pase\static\magnetar.vgd";
    private const string Path = @"C:\Users\enchart\Downloads\pas\levels\pam4.vgd";

    [Benchmark(Baseline = true)]
    public IBeatmap Old()
    {
        var text = File.ReadAllText(Path);
        var json = (JsonObject)JsonNode.Parse(text)!;
        return VgDeserialization.DeserializeBeatmap(json);
    }

    [Benchmark]
    public Beatmap New()
    {
        using var stream = File.OpenRead(Path);
        return JsonSerializer.Deserialize<Beatmap>(stream, JsonContext.CustomOptions)!;
    }
}