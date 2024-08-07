namespace Pamx.Common.Data;

public struct AutoSaveSettings()
{
    public int Max { get; set; } = 3;
    public int Interval { get; set; } = 10;
}