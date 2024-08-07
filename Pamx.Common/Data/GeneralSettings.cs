namespace Pamx.Common.Data;

public struct GeneralSettings()
{
    public float CollapseLength { get; set; } = 0.4f;
    public int Complexity { get; set; } = 0;
    public int Theme { get; set; } = 0;
    public bool SelectTextObjects { get; set; } = true;
    public bool SelectParallaxTextObjects { get; set; } = false;
}