using System.Numerics;

namespace Pamx.Common.Data;

public struct Checkpoint()
{
    public string Name { get; set; } = string.Empty;
    public float Time { get; set; }
    public Vector2 Position { get; set; }
}