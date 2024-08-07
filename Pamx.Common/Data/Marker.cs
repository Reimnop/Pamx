namespace Pamx.Common.Data;

public struct Marker()
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ColorIndex { get; set; } = 0;
    public float Time { get; set; } = 0.0f;
}