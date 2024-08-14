using System.Numerics;
using Pamx.Common.Data;
using Pamx.Common.Enum;

namespace Pamx.Common;

public interface IParallaxObject : IReference<IParallaxObject>
{
    IParallaxObject IReference<IParallaxObject>.Value => this;
    Vector2 Position { get; set; }
    Vector2 Scale { get; set; }
    float Rotation { get; set; }
    ParallaxObjectAnimation Animation { get; set; }
    ObjectShape Shape { get; set; }
    int ShapeOption { get; set; }
    string? Text { get; set; }
    int Color { get; set; }
}