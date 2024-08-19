namespace Pamx.Common;

/// <summary>
/// Represents a layer in parallax settings
/// </summary>
public interface IParallaxLayer
{
    /// <summary>
    /// The depth of the layer
    /// </summary>
    int Depth { get; set; }
    
    /// <summary>
    /// The color index of the layer
    /// </summary>
    int Color { get; set; }
    
    /// <summary>
    /// The objects in the layer
    /// </summary>
    IList<IParallaxObject> Objects { get; set; }
}