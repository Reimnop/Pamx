using System.Text.Json;
using System.Text.Json.Nodes;
using BenchmarkDotNet.Attributes;
using Pamx.Common;
using Pamx.Ls;
using Pamx.Neo.Legacy;

namespace Pamx.Neo.Benchmark;

[MemoryDiagnoser]
public class LegacyDeserializationBenchmarks
{
    private const string Path = @"E:\Project\Programming\pase\static\feral.lsb";

    [Benchmark(Baseline = true)]
    public IBeatmap LegacyOld()
    {
        var text = File.ReadAllText(Path);
        var json = (JsonObject)JsonNode.Parse(text)!;
        return LsDeserialization.DeserializeBeatmap(json);
    }

    [Benchmark]
    public Beatmap LegacyNew()
    {
        using var stream = File.OpenRead(Path);
        return JsonSerializer.Deserialize<Beatmap>(stream, LegacyJsonContext.CustomOptions)!;
    }
}