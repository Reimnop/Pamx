using System.Numerics;

namespace Pamx.Common;

public interface ICheckpoint : IReference<ICheckpoint>
{
    ICheckpoint IReference<ICheckpoint>.Value => this;
    string Name { get; set; }
    float Time { get; set; }
    Vector2 Position { get; set; }
}