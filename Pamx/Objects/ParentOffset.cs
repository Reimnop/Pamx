namespace Pamx.Objects;

/// <summary>
/// The beatmap object's parent animation time offset.
/// </summary>
public struct ParentOffset()
{
    public static ParentOffset Zero => new();
    
    /// <summary>
    /// The position offset.
    /// </summary>
    public float Position { get; set; } = 0.0f;
    
    /// <summary>
    /// The scale offset.
    /// </summary>
    public float Scale { get; set; } = 0.0f;
    
    /// <summary>
    /// The rotation offset
    /// </summary>
    public float Rotation { get; set; } = 0.0f;
}