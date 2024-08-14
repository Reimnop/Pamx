using System.Numerics;
using Pamx.Common;

namespace Pamx.Ls;

public class LsCheckpoint : ICheckpoint
{
    public string Name { get; set; } = string.Empty;
    public float Time { get; set; } = 0.0f;
    public Vector2 Position { get; set; } = Vector2.Zero;
}