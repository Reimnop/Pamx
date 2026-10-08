namespace Pamx.Objects;

/// <summary>
/// The beatmap object's particles parameters.
/// </summary>
public sealed class ParticlesParams
{
    /// <summary>
    /// Amount of particles emitted per second.
    /// </summary>
    public float ParticlesPerSecond { get; set; } 
    
    /// <summary>
    /// Amount of particles emitted per unit the emitter moved.
    /// </summary>
    public float ParticlesPerUnit { get; set; }

    /// <summary>
    /// Whether the particles are in local/world mode.
    /// </summary>
    public bool World { get; set; }
    
    /// <summary>
    /// Whether the particles should despawn at emitter kill or continue their lifetime.
    /// </summary>
    public bool DespawnOnEnd { get; set; }
    
    /// <summary>
    /// Whether it's a radial emitter or not.
    /// </summary>
    public bool Radial { get; set; }
    
    /// <summary>
    /// The Arc of the radial particle emitter.
    /// </summary>
    public float RadialCircleArc { get; set; }

    /// <summary>
    /// Value between 0-1 that defines how much of the radius particles spawn in, counted from the edge inwards.
    /// 1 means the entire emitter, 0 means only at the very edge.
    /// </summary>
    public float RadialCircleRadius { get; set; }

    /// <summary>
    /// The speed at which particles move away from the emitter 
    /// </summary>
    public float RadialStartSpeed { get; set; }
    
    /// <summary>
    /// If the emitter is a Hashi emitter.
    /// </summary>
    public bool Hashi { get; set; }
}