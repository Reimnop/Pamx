using System.Numerics;
using Pamx.Common;
using Pamx.Common.Implementation;

namespace Pamx.Vg;

public class VgCheckpoint() : ICheckpoint, IIdentifiable<string>
{
    public string Id { get; } = RandomUtil.GenerateId();
    public string Name { get; set; } = string.Empty;
    public float Time { get; set; } = 0.0f;
    public Vector2 Position { get; set; } = Vector2.Zero;
    
    public VgCheckpoint(string id) : this()
    {
        Id = id;
    }
}