using System.Numerics;

namespace Pamx.Common.Data;

public struct LensDistortionData()
{
    public float Intensity { get; set; } = 0.0f;
    public Vector2 Center { get; set; } = Vector2.Zero;
}