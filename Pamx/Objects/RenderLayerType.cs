namespace Pamx.Objects;


/// <summary>
/// The layer which the object is rendered at.
/// </summary>
public enum RenderLayerType
{
    
    /// <summary>
    /// The object is rendered at the normal layer.
    /// </summary>
    Normal,
    
    /// <summary>
    /// The object is rendered above the player.
    /// </summary>
    AbovePlayer,
    
    /// <summary>
    /// The object is rendered in the background layer.
    /// </summary>
    InBackground
}