using Pamx.Ls;

var lsTheme = new LsTheme();

using var standardOutput = Console.OpenStandardOutput();
LsSerialization.WriteTheme(lsTheme, standardOutput);