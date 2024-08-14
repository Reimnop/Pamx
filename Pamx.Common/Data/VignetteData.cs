using System.Numerics;

namespace Pamx.Common.Data;

public struct VignetteData()
{
    public float Intensity { get; set; } = 0.0f;
    public float Smoothness { get; set; } = 0.0f;
    public int Color { get; set; } = 0;
    public bool Rounded { get; set; } = false;
    public float Roundness { get; set; } = 0.0f;
    public Vector2 Center { get; set; } = Vector2.Zero;
}