namespace Pamx.Common.Enum;

/// <summary>
/// The object's parent animation offset, in time
/// </summary>
public struct ParentOffset()
{
    /// <summary>
    /// A <see cref="ParentOffset"/> with all offsets set to zero
    /// </summary>
    public static ParentOffset Zero => new(0.0f, 0.0f, 0.0f);
    
    /// <summary>
    /// The position offset
    /// </summary>
    public float Position { get; set; } = 0.0f;
    
    /// <summary>
    /// The scale offset
    /// </summary>
    public float Scale { get; set; } = 0.0f;
    
    /// <summary>
    /// The rotation offset
    /// </summary>
    public float Rotation { get; set; } = 0.0f;

    /// <summary>
    /// Creates a new <see cref="ParentOffset"/>
    /// </summary>
    /// <param name="position">The position offset</param>
    /// <param name="scale">The scale offset</param>
    /// <param name="rotation">The rotation offset</param>
    public ParentOffset(float position, float scale, float rotation) : this()
    {
        Position = position;
        Scale = scale;
        Rotation = rotation;
    }
}