using Pamx.Ls;

var lsBeatmap = new LsBeatmap();

using var standardOutput = Console.OpenStandardOutput();
LsSerialization.WriteBeatmap(lsBeatmap, standardOutput);