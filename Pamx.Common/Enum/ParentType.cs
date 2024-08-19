namespace Pamx.Common.Enum;

/// <summary>
/// The specific transform component from which this object inherits
/// from its parent
/// </summary>
[Flags]
public enum ParentType
{
    /// <summary>
    /// Inherit the position from the parent
    /// </summary>
    Position = 0b001,
    
    /// <summary>
    /// Inherit the scale from the parent
    /// </summary>
    Scale = 0b010,
    
    /// <summary>
    /// Inherit the rotation from the parent
    /// </summary>
    Rotation = 0b100,
    
    /// <summary>
    /// No inheritance
    /// </summary>
    None = 0,
}