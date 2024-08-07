using System.Numerics;

namespace Pamx.Common.Data;

public struct GridSettings()
{
    public Vector2 Scale { get; set; } = new(2.0f, 2.0f);
    public int Thickness { get; set; } = 2;
    public float Opacity { get; set; } = 0.5f;
    public int Color { get; set; } = 0;
}