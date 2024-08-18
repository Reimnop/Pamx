using System.Numerics;
using Pamx.Common.Data;
using Pamx.Common.Enum;

namespace Pamx.Common.Implementation;

public class ParallaxObject() : IParallaxObject, IIdentifiable<string>
{
    public string Id { get; } = RandomUtil.GenerateId();
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; }
    public float Rotation { get; set; }
    public ParallaxObjectAnimation Animation { get; set; }
    public ObjectShape Shape { get; set; }
    public int ShapeOption { get; set; }
    public string? Text { get; set; }
    public int Color { get; set; }
    
    public ParallaxObject(string id) : this()
    {
        Id = id;
    }
}