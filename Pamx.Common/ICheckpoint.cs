using System.Numerics;

namespace Pamx.Common;

/// <summary>
/// Represents a checkpoint in the beatmap
/// </summary>
public interface ICheckpoint : IReference<ICheckpoint>
{
    /// <inheritdoc />
    ICheckpoint IReference<ICheckpoint>.Value => this;
    
    /// <summary>
    /// The checkpoint's name
    /// </summary>
    string Name { get; set; }
    
    /// <summary>
    /// The checkpoint's time
    /// </summary>
    float Time { get; set; }
    
    /// <summary>
    /// The position where the player should spawn upon respawning at the checkpoint
    /// </summary>
    Vector2 Position { get; set; }
}