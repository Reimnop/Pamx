using System.Numerics;

namespace Pamx.Common.Data;

public struct ParallaxObjectAnimation()
{
    public Vector2? Position { get; set; } = null;
    public Vector2? Scale { get; set; } = null;
    public float? Rotation { get; set; } = null;
    public float LoopLength { get; set; } = 0.0f;
    public float LoopDelay { get; set; } = 0.0f;
}