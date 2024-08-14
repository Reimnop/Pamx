using System.Numerics;
using Pamx.Common.Enum;

namespace Pamx.Common.Data;

public struct BackgroundObject()
{
    public bool Active { get; set; } = false;
    public string Name { get; set; } = string.Empty;
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; }
    public float Rotation { get; set; }
    public int Color { get; set; }
    public int Depth { get; set; }
    public bool Fade { get; set; }
    public BackgroundObjectReactiveType ReactiveType { get; set; }
    public float ReactiveScale { get; set; }
}